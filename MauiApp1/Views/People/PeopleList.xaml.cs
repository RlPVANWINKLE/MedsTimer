using System.Collections.ObjectModel;
using MedsTimer.Views.Meds;
using MedsTimer.Views.Reporting;
using MauiIcons.Material;
using Plugin.LocalNotification;

namespace MedsTimer.Views.People;

public partial class PeopleList : ContentPage
{
    List<Members> members = new List<Members>();
	public PeopleList()
	{
		InitializeComponent();

	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        members = App.Repository.getMembers();
        Household.ItemsSource = members;
    }

    private void Household_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Members;
        Navigation.PushAsync(new MainPage(selected.MemberId),false);
    }

    private void AddMember_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new newPerson(), false);
    }

    private void Logout_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login(), false);
    }

    private void ViewAllMeds_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AllMeds(), false);
    }
    private void Reporting_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Reports(), false);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (int)button.BindingContext;
        var pres = App.Repository.GetPrescriptions(item);
        var ccp = App.Repository.GetCough_Cold_pain(item);
        foreach (var item1 in ccp)
        {
            LocalNotificationCenter.Current.Cancel(item1.MedicationId);
            LocalNotificationCenter.Current.Cancel(item1.MedicationId + item1.MedicationId);
            LocalNotificationCenter.Current.Cancel(item1.MedicationId + item1.MedicationId + item1.MedicationId);
            LocalNotificationCenter.Current.Cancel(item1.MedicationId + item1.MedicationId + item1.MedicationId + item1.MedicationId);
            App.Repository.DeleteCold_Cough_Pain(item1.MedicationId);
        }
        foreach (var item1 in pres)
        {
            LocalNotificationCenter.Current.Cancel(item1.MedicationId);
            App.Repository.DeletePrescription(item1.MedicationId);
        }
        App.Repository.DeleteMember(item);
        OnAppearing();
    }
}