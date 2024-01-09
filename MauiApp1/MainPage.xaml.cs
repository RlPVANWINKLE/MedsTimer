using MedsTimer.Views.Meds;
using MedsTimer.Views.People;
using MedsTimer.Views.Reporting;

namespace MedsTimer;
public partial class MainPage : ContentPage
{
    int MemberId;
    public MainPage(int id)
	{
        InitializeComponent();
        MemberId = id;
        //LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var prescription = App.Repository.GetPrescriptions(MemberId);
        foreach (var item in prescription)
        {
            if(DateTime.Parse(item.Dose1).TimeOfDay <= DateTime.Now.AddMinutes(10).TimeOfDay)
            {
                item.StatusColor = "Yellow";
            }
        }
        _Medications.ItemsSource = prescription;

        var cough_and_cold = App.Repository.GetCough_Cold_pain(MemberId);
        _CoughAndColdMedications.ItemsSource = cough_and_cold;
        if (prescription.Count <= 0 && cough_and_cold.Count <= 0)
        {
            lblCough_Cold.IsVisible = false;
            lblPrescription.IsVisible = false;
            lblnone.IsVisible = true;
        }else if(prescription.Count <= 0)
        {
            lblPrescription.IsVisible = false;
            lblnone.IsVisible = false;
        }else if(cough_and_cold.Count <= 0)
        {
            lblCough_Cold.IsVisible = false;
            lblnone.IsVisible = false;
        }
        else
        {
            lblCough_Cold.IsVisible = true;
            lblPrescription.IsVisible = true;
            lblnone.IsVisible = false;
        }
    }

    private async void AddMedication_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new AddMedication());
        await Navigation.PushAsync(new AddMedication(MemberId), false);
    }

    private void _CoughAndColdMedications_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Medication;
        Navigation.PushAsync(new EditMedication(selected.MedicationId, "Cough_And_Cold", selected.MemberId), false);
    }

    private void _Medications_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Medication;
        Navigation.PushAsync(new EditMedication(selected.MedicationId, "Prescription",selected.MemberId), false);
    }
    private void Logout_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login(), false);
    }
    private void OtherPeople_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PeopleList(), false);
    }
}

