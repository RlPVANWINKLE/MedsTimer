using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using MedsTimer.Data;
using System.Collections.ObjectModel;


namespace MedsTimer;

public partial class App : Application
{
    public static Repository Repository { get; private set; }

    public App(Repository repository)
	{
		InitializeComponent();
        MainPage = new AppShell();

        Repository = repository;

        LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        var CCP = App.Repository.GetAllCough_Cold_Pain();
        var Presc = App.Repository.GetAllPrescriptions();
        if (Presc.Count > 0 && Presc != null)
        {
            foreach (var p in Presc)
            {
                p.Notification();
            }
        }
        if (CCP.Count > 0 && CCP != null)
        {
            foreach (var c in CCP)
            {
                c.Notification();
            }
        }

    }

    private void Current_NotificationActionTapped(NotificationActionEventArgs e)
    {
        //throw new NotImplementedException();
        if (e.IsTapped)
        {
            
        }

    }

}
