using System;
using System.Collections.Generic;
using System.Text;
using BigTed;
using POD.Forms.Providers;
using POD.iOS.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(HudProvider))]
namespace POD.iOS.Providers
{
    public class HudProvider : IHudProvider
    {
        public void DisplayProgress(string message, int progress = -1)
        {
            BTProgressHUD.Show(message, progress, ProgressHUD.MaskType.Black);
        }

        public void DisplaySuccess(string message)
        {
            BTProgressHUD.ShowSuccessWithStatus(message);
        }

        public void DisplayError(string message)
        {
            BTProgressHUD.ShowErrorWithStatus(message);
        }

        public void Dismiss()
        {
            BTProgressHUD.Dismiss();
        }
    }
}
