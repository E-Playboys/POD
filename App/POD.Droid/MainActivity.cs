using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using POD.Forms;

namespace POD.Droid
{
    [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, WindowSoftInputMode = SoftInput.AdjustPan)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            RegisterHockeyAppServices();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            base.OnPause();

            UnregisterHockeyAppServices();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnregisterHockeyAppServices();
        }

        private void RegisterHockeyAppServices()
        {
            CrashManager.Register(this);
            MetricsManager.Register(this, Application);

#if DEBUG
            UpdateManager.Register(this); // this is only used in development
#endif
        }

        private void UnregisterHockeyAppServices()
        {
#if DEBUG
            UpdateManager.Unregister();
#endif
        }
    }
}

