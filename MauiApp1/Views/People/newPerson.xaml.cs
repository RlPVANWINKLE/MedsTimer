namespace MedsTimer.Views.People;

public partial class newPerson : ContentPage
{
	public newPerson()
	{
		InitializeComponent();
	}

    private void Home_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PeopleList(), false);
    }

    public void BtnAddMember_Clicked(object sender, EventArgs e)
    {
        Members memmber = new Members();
        if (memmber._NotNull(MemberFName.Text) && memmber._NotNull(MemberLName.Text) && memmber._NotNull(MemberAge.Text))
        {
            memmber.MemberFName = MemberFName.Text;
            memmber.MemberLName = MemberLName.Text;
            memmber.MemberAge = MemberAge.Text;
            var memberId = App.Repository.addMember(memmber);
            Navigation.PushAsync(new MainPage(memberId), false);
        }
        else
        {
            DisplayAlert("Invalid Field", "Please confirm all fields have a value", "Okay");
            return;
        }
    }
}