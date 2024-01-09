using MedsTimer.Views.People;
using System.Collections.ObjectModel;

namespace MedsTimer.Views.Meds;


public partial class AllMeds : ContentPage
{
	public AllMeds()
	{
		InitializeComponent();
        var prescription = App.Repository.GetAllPrescriptions();
        foreach (var item in prescription)
        {
            if (DateTime.Parse(item.Dose1).TimeOfDay <= DateTime.Now.AddMinutes(10).TimeOfDay)
            {
                item.StatusColor = "Yellow";
            }
            else { item.StatusColor = "blue"; }
        }
        _Medications.ItemsSource = prescription;

        var cough_and_cold = App.Repository.GetAllCough_Cold_Pain();
        _CoughAndColdMedications.ItemsSource = cough_and_cold;
    }

    private void Back_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PeopleList());
    }

    private void _Medications_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Medication;
        Navigation.PushAsync(new EditMedication(selected.MedicationId, "Prescription", -1), false);
    }

    private void _CoughAndColdMedications_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Medication;
        Navigation.PushAsync(new EditMedication(selected.MedicationId, "Cough_Cold_Pain", -1), false);
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var search = SearchBar.Text;
        var prescription = App.Repository.GetAllPrescriptions();
        var newpresc = new ObservableCollection<Prescription>();
        var cough_and_cold = App.Repository.GetAllCough_Cold_Pain();
        var newccp = new ObservableCollection<Cough_Cold_Pain>();
        foreach (var item in prescription)
        {
            if (item.MedicationName.ToUpper().Contains(search.ToUpper()) || item.NumberofTimesaDay.ToString().Contains(search) || item.Type.ToUpper().Contains(search.ToUpper()))
            {
                newpresc.Add(item);
                _Medications.ItemsSource = newpresc;
            }
        }
        foreach (var item in cough_and_cold)
        {
            if (item.MedicationName.ToUpper().Contains(search.ToUpper()) || item.HowOften.ToString().Contains(search) || item.Type.ToUpper().Contains(search.ToUpper()))
            {
                newccp.Add(item);
                _CoughAndColdMedications.ItemsSource = newccp;
            }
        }
        if(newccp.Count <= 0) {
            _CoughAndColdMedications.ItemsSource=null;
        }
        if(newpresc.Count <= 0)
        {
            _Medications.ItemsSource = null;
        }
    }
}