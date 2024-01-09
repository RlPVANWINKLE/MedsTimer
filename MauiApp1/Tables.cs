using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using SQLite;


namespace MedsTimer
{
    public class NotNull
    {
        public bool _NotNull(string entry) {
            if (string.IsNullOrEmpty(entry)) return false;
            else return true;
        }
    }
    [Table("Notifications")]
    public class Notifications
    {
        [PrimaryKey, AutoIncrement, Column("Notification_Id")]
        public int Notification_Id { get; set; }
        public string Notification_Title { get; set; }
    }

    [Table("Members")]
    public class Members : NotNull
    {
        [PrimaryKey, AutoIncrement, Column("MemberId")]
        public int MemberId { get; set; }
        public string MemberFName { get; set; }
        public string MemberLName { get; set; }
        public string MemberAge { get; set; }
    }
    public class Medication : NotNull
    {
        [PrimaryKey, AutoIncrement, Column("MedicationId")]
        public int MedicationId { get; set; }
        public int MemberId { get; set; }
        public string MedicationName { get; set; }
        public string Type { get; set; }
        public string StatusColor { get; set; }
        public async void Success(string type)
        {
            if(await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
            var Notification = new NotificationRequest();
            Notification.NotificationId = 10000;
            Notification.Title = MedicationName;
            if(type == "Add")
            {
                Notification.Description = $"{MedicationName} has been added";
            }
            else
            {
                Notification.Description = $"{MedicationName} has been updated";
            }

            await LocalNotificationCenter.Current.Show(Notification);
        }
        public virtual async void Notification()
        {
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
            var Notification = new NotificationRequest();
            Notification.NotificationId = MedicationId;
            Notification.Title = MedicationName;
            await LocalNotificationCenter.Current.Show(Notification);
        }
    }
    [Table("prescription")]
    public class Prescription:Medication
    {
        public int NumberofTimesaDay { get; set; }
        public int Notification_Id { get; set; }
        public int Notification_Id2 { get; set; }
        public int Notification_Id3 { get; set; }
        public int Notification_Id4 { get; set; }
        public string Dose1 { get; set; }
        public string Dose2 { get; set; }
        public string Dose3 { get; set; }
        public string Dose4 { get; set; }
        public override async void Notification()
        {
            
            var notification = new NotificationRequest
            {
                NotificationId = Notification_Id,
                Title = MedicationName,
                Description = $"Its time for {App.Repository.getMember(MemberId).MemberFName} to take {MedicationName}",
                Sound = DeviceInfo.Platform == DevicePlatform.Android ? "loudalarm" : "loudalarm.mp3",
                Subtitle = Type,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Parse(Dose1),
                    RepeatType = NotificationRepeat.Daily,
                    Android = new AndroidScheduleOptions
                    {
                        AlarmType = AndroidAlarmType.RtcWakeup
                    }
                },
                Android =
                {
                    ChannelId = "loudalarm"
                }
            };
            await LocalNotificationCenter.Current.Show(notification);
            if(Dose2 != null)
            {
                notification = new NotificationRequest
                {
                    NotificationId = Notification_Id2,
                    Title = MedicationName,
                    Description = $"Its time for {App.Repository.getMember(MemberId).MemberFName} to take {MedicationName}",
                    Subtitle = Type,
                    Sound = DeviceInfo.Platform == DevicePlatform.Android ? "loudalarm" : "loudalarm.mp3",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Parse(Dose2),
                        RepeatType = NotificationRepeat.Daily,
                        Android = new AndroidScheduleOptions
                        {
                            AlarmType = AndroidAlarmType.RtcWakeup
                        }
                    },
                    Android =
                {
                    ChannelId = "loudalarm"
                }
                };
                await LocalNotificationCenter.Current.Show(notification);
            }
            if (Dose3 != null)
            {
                notification = new NotificationRequest
                {
                    NotificationId = Notification_Id3,
                    Title = MedicationName,
                    Description = $"Its time for {App.Repository.getMember(MemberId).MemberFName} to take {MedicationName}",
                    Sound = DeviceInfo.Platform == DevicePlatform.Android ? "loudalarm" : "loudalarm.mp3",
                    Subtitle = Type,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Parse(Dose3),
                        RepeatType = NotificationRepeat.Daily,
                        Android = new AndroidScheduleOptions
                        {
                            AlarmType = AndroidAlarmType.RtcWakeup
                        }
                    },
                    Android =
                    {
                        ChannelId = "loudalarm"
                    }
                };
                await LocalNotificationCenter.Current.Show(notification);
            }
            if (Dose4 != null)
            {
                notification = new NotificationRequest
                {
                    NotificationId = Notification_Id4,
                    Title = MedicationName,
                    Description = $"Its time for {App.Repository.getMember(MemberId).MemberFName} to take {MedicationName}",
                    Sound = DeviceInfo.Platform == DevicePlatform.Android ? "loudalarm" : "loudalarm.mp3",
                    Subtitle = Type,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Parse(Dose4),
                        RepeatType = NotificationRepeat.Daily,
                        Android = new AndroidScheduleOptions
                        {
                            AlarmType = AndroidAlarmType.RtcWakeup
                        }
                    },
                    Android =
                {
                    ChannelId = "loudalarm"
                }
                };
                await LocalNotificationCenter.Current.Show(notification);
            }

        }
    }
    [Table("cough_cold_pain")]
    public class Cough_Cold_Pain:Medication
    {
        public int Notification_Id1 { get; set; }
        public int Notification_Id2 { get; set; }
        public int Notification_Id3 { get; set; }
        public int Notification_Id4 { get; set; }
        public int Notification_Id5 { get; set; }
        public int Notification_Id6 { get; set; }
        public int HowOften { get; set; }
        public string LastTaken { get; set; }
        public override async void Notification()
        {
            var Notification_Id = new[] { Notification_Id1, Notification_Id2, Notification_Id3, Notification_Id4, Notification_Id5, Notification_Id6 };
            NotificationRequest notification = new NotificationRequest
            {
                NotificationId = Notification_Id1,
                Title = MedicationName,
                Description = $"Its time for {App.Repository.getMember(MemberId).MemberFName} to take {MedicationName}",
                Sound = DeviceInfo.Platform == DevicePlatform.Android ? "loudalarm" : "loudalarm.mp3",
                Subtitle = Type,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Parse(LastTaken),
                    RepeatType = NotificationRepeat.Daily,
                    Android = new AndroidScheduleOptions
                    {
                        AlarmType = AndroidAlarmType.RtcWakeup
                    }
                },
                Android =
                {
                    ChannelId = "loudalarm"
                }
            };
            await LocalNotificationCenter.Current.Show(notification);

            for (int i = 1; i < 24/HowOften; i++)
            {
                notification = new NotificationRequest
                {
                    NotificationId = Notification_Id[i],
                    Title = MedicationName,
                    Description = $"Its time for {App.Repository.getMember(MemberId).MemberFName} to take {MedicationName}",
                    Sound = DeviceInfo.Platform == DevicePlatform.Android ? "loudalarm" : "loudalarm.mp3",
                    Subtitle = Type,
                    CategoryType = NotificationCategoryType.Alarm,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Parse(LastTaken).AddHours(i * HowOften),
                        RepeatType = NotificationRepeat.Daily,
                        Android = new AndroidScheduleOptions
                        {
                            AlarmType = AndroidAlarmType.RtcWakeup,
                        }
                    },
                    Android =
                    {
                        ChannelId = "loudalarm",
                        VisibilityType = AndroidVisibilityType.Public
                    }
                };
                await LocalNotificationCenter.Current.Show(notification);

            }

        }
    }
}
