using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using CommunityToolkit.Maui;
using MedsTimer.Data;
using MauiIcons.Core;
using MauiIcons.Fluent;
using MauiIcons.Material;
using MauiIcons.Cupertino;
using CommunityToolkit.Mvvm.Messaging;

namespace MedsTimer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>().UseFluentMauiIcons()
            .UseMauiApp<App>().UseMaterialMauiIcons()
            .UseMauiApp<App>().UseCupertinoMauiIcons()
            .UseLocalNotification(config =>
            {
                config
                .AddCategory(new NotificationCategory(NotificationCategoryType.Alarm)
                {
                    ActionList = new HashSet<NotificationAction>(new List<NotificationAction>()
                    {
                        new NotificationAction(1337)
                        {
                            Title = "Hello",
                            Android = { LaunchAppWhenTapped = false }
                        }
                    })
                })
                .AddAndroid(android =>
                {
                    android.AddChannel(new NotificationChannelRequest { Id = $"loudalarm", Name = "loudalarm", Description = "loudalarm", Sound = "loudalarm"});
                });
            })
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Medication.db");
        builder.Services.AddSingleton(s =>
        ActivatorUtilities.CreateInstance<Repository>(s, dbPath));
        builder.Services.AddSingleton<Login>();
        builder.Services.AddTransient<IMessenger, WeakReferenceMessenger>();

//#if DEBUG
//        builder.Logging.AddDebug();
//#endif

		return builder.Build();
	}
}
