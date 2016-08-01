using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using POD.Droid.Providers;
using POD.Forms.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastProvider))]
namespace POD.Droid.Providers
{
    public class ToastProvider : IToastProvider
    {
        public void Notify(string message, bool centered = false)
        {
            AndHUD.Shared.ShowToast(Xamarin.Forms.Forms.Context, message, MaskType.Clear, TimeSpan.FromSeconds(2), centered);
        }
    }
}