using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace TryBlazorMobileBinding.Droid
{
    [Activity(Label = "TryBlazorMobileBinding", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private App _app;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            _app = new App();
            LoadApplication(_app);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
        }

#pragma warning disable IDE1006 // Naming Styles
        private async void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            await _app.UnhandledExceptionAsync("AndroidEnvironment.UnhandledExceptionRaiser", e.Exception);
            e.Handled = true;
        }

        private async void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            await _app.UnhandledExceptionAsync("TaskScheduler.UnobservedTaskException", e.Exception);
            e.SetObserved();
        }

        private async void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                await _app.UnhandledExceptionAsync("CurrentDomain.UnhandledException", ex);

            }
            else
            {
                throw new Exception("A CurrentDomain.UnhandledException occured and was not handleable by the application.");
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}