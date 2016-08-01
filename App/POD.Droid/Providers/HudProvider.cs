using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using POD.Droid.Providers;
using POD.Forms.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(HudProvider))]
namespace POD.Droid.Providers
{
    public class HudProvider : IHudProvider
    {
        public void DisplayProgress(string message, int progress = -1)
        {
            AndroidHUD.AndHUD.Shared.Show(Xamarin.Forms.Forms.Context, message, progress);
        }

        public void DisplaySuccess(string message)
        {
            AndroidHUD.AndHUD.Shared.ShowSuccess(Xamarin.Forms.Forms.Context, message, AndroidHUD.MaskType.Black, TimeSpan.FromSeconds(1));
        }

        public void DisplayError(string message)
        {
            AndroidHUD.AndHUD.Shared.ShowError(Xamarin.Forms.Forms.Context, message, AndroidHUD.MaskType.Black, TimeSpan.FromSeconds(1));
        }

        public void Dismiss()
        {
            AndroidHUD.AndHUD.Shared.Dismiss(Xamarin.Forms.Forms.Context);
        }
    }
}