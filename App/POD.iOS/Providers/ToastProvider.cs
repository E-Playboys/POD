using System;
using System.Collections.Generic;
using System.Text;
using BigTed;
using POD.Forms.Providers;
using POD.iOS.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastProvider))]
namespace POD.iOS.Providers
{
    public class ToastProvider : IToastProvider
    {
        public void Notify(string message, bool centered = false)
        {
            BTProgressHUD.ShowToast(message, ProgressHUD.MaskType.Clear, centered, 2000);
        }
    }
}
