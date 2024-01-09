using System.Collections.ObjectModel;

namespace MedsTimer.Views.Meds;

public partial class AddMedication : ContentPage
{
    ObservableCollection<MedicationType> type = new ObservableCollection<MedicationType>();
    public ObservableCollection<MedicationType> _type { get { return type; } }
    ObservableCollection<TimesaDay> timesaday = new ObservableCollection<TimesaDay>();
    public ObservableCollection<TimesaDay> _timesaday { get { return timesaday; } }
    int MemberId;
    public AddMedication(int Id)
	{
		InitializeComponent();
        MemberId = Id;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        type.Add(new MedicationType() { TypeText = "Prescription" });
        type.Add(new MedicationType() { TypeText = "Cough and Cold/Pain Med" });
        TypePicker.ItemsSource = type;
        timesaday.Add(new TimesaDay() { Times = "1" });
        timesaday.Add(new TimesaDay() { Times = "2" });
        timesaday.Add(new TimesaDay() { Times = "3" });
        timesaday.Add(new TimesaDay() { Times = "4" });
        TimesADay.ItemsSource = timesaday;
    }
    public class MedicationType
    {
        public string TypeText { get; set; }
    }
    public class TimesaDay
    {
        public string Times { get; set; }
    }

    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(TypePicker.SelectedIndex == 0)
        {
            lblTAD.IsVisible = true;
            TimesADay.IsVisible = true;
            lblHO.IsVisible = false;
            lblTLT.IsVisible = false;
            HowOften.IsVisible = false;
            LastTaken.IsVisible = false;
        }
        else if(TypePicker.SelectedIndex == 1 || TypePicker.SelectedIndex == 2)
        {
            lblHO.IsVisible = true;
            lblTLT.IsVisible = true;
            HowOften.IsVisible = true;
            LastTaken.IsVisible = true;
            lblTAD.IsVisible = false;
            TimesADay.IsVisible = false;
            lblDose1.IsVisible = false;
            Dose1.IsVisible = false;
            lblDose2.IsVisible = false;
            lblDose3.IsVisible = false;
            lblDose4.IsVisible = false;
            Dose2.IsVisible = false;
            Dose3.IsVisible = false;
            Dose4.IsVisible = false;
        }
    }

    private void TimesADay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TimesADay.SelectedIndex == 0)
        {
            lblDose1.IsVisible = true;
            Dose1.IsVisible = true;
            lblDose2.IsVisible = false;
            lblDose3.IsVisible = false;
            lblDose4.IsVisible = false;
            Dose2.IsVisible = false;
            Dose3.IsVisible = false;
            Dose4.IsVisible = false;
        }else if(TimesADay.SelectedIndex == 1)
        {
            lblDose1.IsVisible = true;
            Dose1.IsVisible = true;
            lblDose2.IsVisible = true;
            lblDose3.IsVisible = false;
            lblDose4.IsVisible = false;
            Dose2.IsVisible = true;
            Dose3.IsVisible = false;
            Dose4.IsVisible = false;
        }else if(TimesADay.SelectedIndex == 2)
        {
            lblDose1.IsVisible = true;
            Dose1.IsVisible = true;
            lblDose2.IsVisible = true;
            lblDose3.IsVisible = true;
            lblDose4.IsVisible = false;
            Dose2.IsVisible = true;
            Dose3.IsVisible = true;
            Dose4.IsVisible = false;
        }
        else if (TimesADay.SelectedIndex == 3)
        {
            lblDose1.IsVisible = true;
            Dose1.IsVisible = true;
            lblDose2.IsVisible = true;
            lblDose3.IsVisible = true;
            lblDose4.IsVisible = true;
            Dose2.IsVisible = true;
            Dose3.IsVisible = true;
            Dose4.IsVisible = true;
        }
    }
    private void AddMedication_Clicked(object sender, EventArgs e)
    {
        if (TypePicker.SelectedIndex == -1)
        {
            DisplayAlert("Invalid Field", "Please select a Medication Type", "Okay");
            return;
        }
        if (TypePicker.SelectedIndex == 0)
        {
            var prescription = new Prescription();
            if (prescription._NotNull(MedicationName.Text))
            {
                prescription.MedicationName = MedicationName.Text;
            }
            else
            {
                DisplayAlert("Invalid Field", "Please confirm all entries are valid", "Okay");
                return;
            }
            if (TimesADay.SelectedIndex == -1)
            {
                DisplayAlert("Invalid Field", "Please confirm all entries are valid", "Okay");
                return;
            }
            prescription.NumberofTimesaDay = TimesADay.SelectedIndex + 1;
            prescription.Type = "Prescription";
            prescription.MemberId = MemberId;
            prescription.Notification_Id = App.Repository.AddNotification();
            if (TimesADay.SelectedIndex == 0)
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
            }
            else if (TimesADay.SelectedIndex == 1)
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                prescription.Dose2 = DateTime.Parse(Dose2.Time.ToString()).ToShortTimeString();
                prescription.Notification_Id2 = App.Repository.AddNotification();
            }
            else if (TimesADay.SelectedIndex == 2)
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                prescription.Dose2 = DateTime.Parse(Dose2.Time.ToString()).ToShortTimeString();
                prescription.Dose3 = DateTime.Parse(Dose3.Time.ToString()).ToShortTimeString();
                prescription.Notification_Id3 = App.Repository.AddNotification();

            }
            else
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                prescription.Dose2 = DateTime.Parse(Dose2.Time.ToString()).ToShortTimeString();
                prescription.Dose3 = DateTime.Parse(Dose3.Time.ToString()).ToShortTimeString();
                prescription.Dose4 = DateTime.Parse(Dose4.Time.ToString()).ToShortTimeString();
                prescription.Notification_Id4 = App.Repository.AddNotification();

            }
            var success = App.Repository.AddMedication(prescription);
            if (success)
            {
                prescription.Success("Add");
                prescription.Notification();
                Navigation.PushAsync(new MainPage(MemberId), false);
            }
            else
            {
                DisplayAlert("Error", "An Error Occured, Please confirm your entries and try again.", "Okay");
            }
        }
        else
        {
            var cough_Cold_Pain = new Cough_Cold_Pain();
            if (cough_Cold_Pain._NotNull(MedicationName.Text) || cough_Cold_Pain._NotNull(HowOften.Text))
            {
                cough_Cold_Pain.MedicationName = MedicationName.Text;
                cough_Cold_Pain.HowOften = Int32.Parse(HowOften.Text);
            }
            else
            {
                DisplayAlert("Invalid Field", "Please confirm all entries are valid", "Okay");
                return;
            }
            cough_Cold_Pain.LastTaken = DateTime.Parse(LastTaken.Time.ToString()).ToShortTimeString();
            cough_Cold_Pain.Type = "Cough_Cold_Pain";
            cough_Cold_Pain.MemberId = MemberId;
            var num = 24/Int32.Parse(HowOften.Text);
            if (num == 1)
            {
                cough_Cold_Pain.Notification_Id1 = App.Repository.AddNotification();
            }else if (num == 2)
            {
                cough_Cold_Pain.Notification_Id1 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id2 = App.Repository.AddNotification();
            }else if(num == 3)
            {
                cough_Cold_Pain.Notification_Id1 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id2 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id3 = App.Repository.AddNotification();
            }else if(num == 4){
                cough_Cold_Pain.Notification_Id1 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id2 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id3 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id4 = App.Repository.AddNotification();
            }else if(num == 6)
            {
                cough_Cold_Pain.Notification_Id1 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id2 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id3 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id4 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id5 = App.Repository.AddNotification();
                cough_Cold_Pain.Notification_Id6 = App.Repository.AddNotification();
            }
            var success = App.Repository.AddMedication(cough_Cold_Pain);
            if (success)
            {
                cough_Cold_Pain.Success("Add");
                cough_Cold_Pain.Notification();
                Navigation.PushAsync(new MainPage(MemberId), false);
            }
            else
            {
                DisplayAlert("Error", "An Error Occured, Please confirm your entries and try again.", "Okay");
            }
        }
    
    }

    private void Home_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage(MemberId), false);
    }
}