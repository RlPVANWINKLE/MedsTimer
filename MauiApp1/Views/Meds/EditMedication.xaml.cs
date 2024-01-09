using MedsTimer.Views.People;
using Plugin.LocalNotification;
using System.Collections.ObjectModel;

namespace MedsTimer.Views.Meds;

public partial class EditMedication : ContentPage
{
	int MedId;
    string MedType;
    int MemberId;
    ObservableCollection<MedicationType> type = new ObservableCollection<MedicationType>();
    public ObservableCollection<MedicationType> _type { get { return type; } }
    ObservableCollection<TimesaDay> timesaday = new ObservableCollection<TimesaDay>();
    public ObservableCollection<TimesaDay> _timesaday { get { return timesaday; } }
    public EditMedication(int id, string type, int mbrId)
	{
		InitializeComponent();
        MedId = id;
        MedType = type;
        MemberId = mbrId;
        if (MemberId == -1)
        {
            Home.IsVisible = false;
            _AllMeds.IsVisible = true;
        }
        else
        {
            Home.IsVisible = true;
            _AllMeds.IsVisible = false;
        }
        enableFalse();
	}
    private void enableFalse()
    {
        MedicationName.IsEnabled = false;
        TypePicker.IsEnabled = false;
        TimesADay.IsEnabled = false;
        Dose1.IsEnabled = false;
        Dose2.IsEnabled = false;
        Dose3.IsEnabled = false;
        Dose4.IsEnabled = false;
        HowOften.IsEnabled = false;
        LastTaken.IsEnabled = false;
        BtnAddMedication.IsVisible = false;
    }
    public class MedicationType
    {
        public string TypeText { get; set; }
    }
    public class TimesaDay
    {
        public string Times { get; set; }
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
        if (MedType == "Prescription")
        {
            var med = App.Repository.getPrescriptions(MedId);
            MedicationName.Text = med.MedicationName;
            TypePicker.SelectedIndex = 0;
            TypePicker.IsEnabled = false;
            TimesADay.SelectedIndex = med.NumberofTimesaDay - 1;
            Dose1.Time = DateTime.Parse(med.Dose1).TimeOfDay;
            if (med.Dose2 != null)
            {
                Dose2.Time = DateTime.Parse(med.Dose2).TimeOfDay;
            }
            if (med.Dose3 != null)
            {
                Dose3.Time = DateTime.Parse(med.Dose3).TimeOfDay;
            }
            if (med.Dose4 != null)
            {
                Dose4.Time = DateTime.Parse(med.Dose4).TimeOfDay;
            }
            refresh();
        }
        else
        {
            var med = App.Repository.getCough_Cold_Pain(MedId);
            MedicationName.Text = med.MedicationName;
            TypePicker.SelectedIndex = 1;
            TypePicker.IsEnabled = false;
            HowOften.Text = med.HowOften.ToString();
            LastTaken.Time = DateTime.Parse(med.LastTaken).TimeOfDay;
            refresh();
        }
    }

    private void BtnAddMedication_Clicked(object sender, EventArgs e)
    {
        if (TypePicker.SelectedIndex == -1)
        {
            DisplayAlert("Invalid Field", "Please select a Medication Type", "Okay");
            return;
        }
        if (TypePicker.SelectedIndex == 0)
        {
            var prescription = new Prescription();
            prescription.MedicationId = MedId;
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
            prescription.MemberId = MemberId;
            prescription.Type = "Prescription";
            if (TimesADay.SelectedIndex == 0)
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                if(prescription.Notification_Id2 > 0)
                {
                    LocalNotificationCenter.Current.Cancel(prescription.Notification_Id2);
                }
                if (prescription.Notification_Id3 > 0)
                {
                    LocalNotificationCenter.Current.Cancel(prescription.Notification_Id3);
                }
                if (prescription.Notification_Id4 > 0)
                {
                    LocalNotificationCenter.Current.Cancel(prescription.Notification_Id4);
                }
            }
            else if (TimesADay.SelectedIndex == 1)
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                prescription.Dose2 = DateTime.Parse(Dose2.Time.ToString()).ToShortTimeString();
                if (prescription.Notification_Id2 < 1)
                {
                    prescription.Notification_Id2 = App.Repository.AddNotification();
                }
                if (prescription.Notification_Id3 > 0)
                {
                    LocalNotificationCenter.Current.Cancel(prescription.Notification_Id3);
                }
                if (prescription.Notification_Id4 > 0)
                {
                    LocalNotificationCenter.Current.Cancel(prescription.Notification_Id4);
                }
            }
            else if (TimesADay.SelectedIndex == 2)
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                prescription.Dose2 = DateTime.Parse(Dose2.Time.ToString()).ToShortTimeString();
                prescription.Dose3 = DateTime.Parse(Dose3.Time.ToString()).ToShortTimeString();
                if (prescription.Notification_Id2 < 1)
                {
                    prescription.Notification_Id2 = App.Repository.AddNotification();
                }
                if (prescription.Notification_Id3 < 1)
                {
                    prescription.Notification_Id3 = App.Repository.AddNotification();
                }
                if (prescription.Notification_Id4 > 0)
                {
                    LocalNotificationCenter.Current.Cancel(prescription.Notification_Id4);
                }
            }
            else
            {
                prescription.Dose1 = DateTime.Parse(Dose1.Time.ToString()).ToShortTimeString();
                prescription.Dose2 = DateTime.Parse(Dose2.Time.ToString()).ToShortTimeString();
                prescription.Dose3 = DateTime.Parse(Dose3.Time.ToString()).ToShortTimeString();
                prescription.Dose4 = DateTime.Parse(Dose4.Time.ToString()).ToShortTimeString();
                if (prescription.Notification_Id2 < 1)
                {
                    prescription.Notification_Id2 = App.Repository.AddNotification();
                }
                if (prescription.Notification_Id3 < 1)
                {
                    prescription.Notification_Id3 = App.Repository.AddNotification();
                }
                if (prescription.Notification_Id4 < 1)
                {
                    prescription.Notification_Id4 = App.Repository.AddNotification();
                }
            }
            var success = App.Repository.UpdatePrescription(prescription);
            if (success)
            {
                prescription.Success("Edit");
                prescription.Notification();
                if (MemberId == -1)
                {
                    Navigation.PushAsync(new AllMeds(), false);
                }
                else
                {
                    Navigation.PushAsync(new MainPage(prescription.MemberId), false);
                }
            }
            else
            {
                DisplayAlert("Error", "An Error Occured, Please confirm your entries and try again.", "Okay");
            }
        }
        else
        {
            var cough_Cold_Pain = new Cough_Cold_Pain();
            cough_Cold_Pain.MedicationId = MedId;
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
            cough_Cold_Pain.MemberId = MemberId;
            cough_Cold_Pain.LastTaken = DateTime.Parse(LastTaken.Time.ToString()).ToShortTimeString();
            cough_Cold_Pain.Type = "Cough_Cold_Pain";
            var Notification_Id = new[] { cough_Cold_Pain.Notification_Id1, cough_Cold_Pain.Notification_Id2, cough_Cold_Pain.Notification_Id3, cough_Cold_Pain.Notification_Id4, cough_Cold_Pain.Notification_Id5, cough_Cold_Pain.Notification_Id6 };
            for (int i = 0; i < 24 / cough_Cold_Pain.HowOften; i++)
            {
                if (Notification_Id[i] < 1)
                {
                    Notification_Id[i] = App.Repository.AddNotification();
                }
            }
            for(int i = 0; i < Notification_Id.Length; i++)
            {
               if(i+1 > 24/ cough_Cold_Pain.HowOften)
                {
                    LocalNotificationCenter.Current.Cancel(Notification_Id[i]);
                } 
            }
            var success = App.Repository.UpdateCough_Cold_Pain(cough_Cold_Pain);
            if (success)
            {
                cough_Cold_Pain.Success("Edit");
                cough_Cold_Pain.Notification();
                if (MemberId == -1)
                {
                    
                    Navigation.PushAsync(new AllMeds(), false);
                }
                else
                {
                    Navigation.PushAsync(new MainPage(cough_Cold_Pain.MemberId), false);
                }
            }
            else
            {
                DisplayAlert("Error", "An Error Occured, Please confirm your entries and try again.", "Okay");
            }
        }
    }

    private void TimesADay_SelectedIndexChanged(object sender, EventArgs e)
    {
        refresh();
    }
    private void refresh()
    {
        if (TypePicker.SelectedIndex == 0)
        {
            lblTAD.IsVisible = true;
            TimesADay.IsVisible = true;
            lblHO.IsVisible = false;
            lblTLT.IsVisible = false;
            HowOften.IsVisible = false;
            LastTaken.IsVisible = false;
        }
        else if (TypePicker.SelectedIndex == 1 || TypePicker.SelectedIndex == 2)
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
        }
        else if (TimesADay.SelectedIndex == 1)
        {
            lblDose1.IsVisible = true;
            Dose1.IsVisible = true;
            lblDose2.IsVisible = true;
            lblDose3.IsVisible = false;
            lblDose4.IsVisible = false;
            Dose2.IsVisible = true;
            Dose3.IsVisible = false;
            Dose4.IsVisible = false;
        }
        else if (TimesADay.SelectedIndex == 2)
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
    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        refresh();
    }

    private void BtnDeleteMedication_Clicked(object sender, EventArgs e)
    {
        if(MedType == "Prescription")
        {
            var deleted = App.Repository.getPrescriptions(MedId);
            App.Repository.DeletePrescription(MedId);
            LocalNotificationCenter.Current.Cancel(deleted.Notification_Id);
            LocalNotificationCenter.Current.Cancel(deleted.Notification_Id2);
            LocalNotificationCenter.Current.Cancel(deleted.Notification_Id3);
            LocalNotificationCenter.Current.Cancel(deleted.Notification_Id4);
            if(MemberId == -1)
            {
                Navigation.PushAsync(new AllMeds(), false);
            }
            else
            {
                Navigation.PushAsync(new MainPage(MemberId), false);
            }

        }
        else
        {
            App.Repository.DeleteCold_Cough_Pain(MedId);
            LocalNotificationCenter.Current.Cancel(MedId);
            if (MemberId == -1)
            {
                Navigation.PushAsync(new AllMeds(), false);
            }
            else
            {
                Navigation.PushAsync(new MainPage(MemberId), false);
            }
        }
    }

    private void BtnEditMedication_Clicked_2(object sender, EventArgs e)
    {
            MedicationName.IsEnabled = true;
            TypePicker.IsEnabled = true;
            TimesADay.IsEnabled = true;
            Dose1.IsEnabled = true;
            Dose2.IsEnabled = true;
            Dose3.IsEnabled = true;
            Dose4.IsEnabled = true;
            HowOften.IsEnabled = true;
            LastTaken.IsEnabled = true;
            BtnAddMedication.IsVisible = true;
            BtnEditMedication.IsVisible = false;
    }

    private void Home_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage(MemberId), false);

    }

    private void _AllMeds_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AllMeds(), false);
    }
}