using System.Collections.Generic;
using Xamarin.Forms;

namespace POD.Forms.Views
{
    public abstract class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TrackPage(new Dictionary<string, string>());
        }

        /// <summary>
        /// Tracks this page on Hockey App
        /// </summary>
        /// <param name="metadata"></param>
        protected virtual void TrackPage(Dictionary<string, string> metadata)
        {
            // TODO: Hockey App to track this page
        }
    }
}
