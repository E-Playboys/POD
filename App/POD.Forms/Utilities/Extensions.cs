using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POD.Forms.Providers;
using Xamarin.Forms;

namespace POD.Forms.Utilities
{
    public static class StringExtensions
    {
        public static void ToToast(this string message, bool centered = false)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var toastProvider = DependencyService.Get<IToastProvider>();
                toastProvider.Notify(message, centered);
            });
        }
    }
}
