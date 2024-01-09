using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using MedsTimer.Views.Meds;
using MedsTimer.Views.People;
using CommunityToolkit.Mvvm.Messaging;
//using static MedsTimer.IService;

namespace MedsTimer;

public partial class Login : ContentPage
{
    public static int medicationId = 0;
    public static string medicationtype;
    //private readonly IService _service;
    List<Members> members;
	public Login()
	{
		InitializeComponent();
        members = App.Repository.getMembers();
        //_service = service;
    }



    private async void Loginbtn_Clicked(object sender, EventArgs e)
    {
        var isAvailable = await CrossFingerprint.Current.IsAvailableAsync();
        if (isAvailable)
        {
            var request = new AuthenticationRequestConfiguration
                ("Login using biometrics", "Confirm login with your biometrics")
            {
                FallbackTitle = "Use PIN/Pattern/Password",
                AllowAlternativeAuthentication = true,
            };
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

            if (result.Authenticated && medicationId == 0)
            { 
                //_service.Send(new MessageData("Hello", true));
                if(members.Count == 0 || members == null)
                {
                    await Navigation.PushAsync(new newPerson());
                }
                else if(members.Count == 1)
                {
                    await Navigation.PushAsync(new MainPage(members[0].MemberId));
                }
                else
                {
                    await Navigation.PushAsync(new PeopleList());
                }                
            }
            else if (result.Authenticated && medicationId != 0)
            {
                int MemberId;
                if(medicationtype == "Prescription")
                {
                    MemberId = App.Repository.getPrescriptions(medicationId).MemberId;
                }
                else
                {
                    MemberId = App.Repository.getCough_Cold_Pain(medicationId).MemberId;
                }
                await Navigation.PushAsync(new EditMedication(medicationId, medicationtype, MemberId), false);
            }

        }
    }
}