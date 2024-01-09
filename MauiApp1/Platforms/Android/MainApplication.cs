using Android.App;
using Android.OS;
using Android.Runtime;

namespace MedsTimer;

[Application]
public class MainApplication : MauiApplication
{
	public static readonly string ChannelId = "backgroundServiceChannel";
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{

    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

}
