using MedsTimer.Views.People;

namespace MedsTimer.Views.Reporting;

public partial class Reports : ContentPage
{
	public Reports()
	{
		InitializeComponent();
	}

    private void Home_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PeopleList(), false);
    }

    private void HouseHold_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Report(1),false);
    }

    private void TotalPresc_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Report(2), false);
    }

    private void MemberPresc_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Report(3), false);
    }
}