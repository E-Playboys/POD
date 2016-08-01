using System.Collections.Generic;
using POD.Forms.ViewModels;
using Xamarin.Forms;

namespace POD.Forms.Pages
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseNavigationViewModel, new()
    {
        private bool _isIntialized;

        public Color BarTextColor { get; set; }

        public Color BarBackgroundColor { get; set; }

        private T _viewModel;
        public T ViewModel => _viewModel ?? (_viewModel = new T());

        protected BaseContentPage()
        {
            BarBackgroundColor = Color.Green;
            BarTextColor = Color.White;

            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var nav = Parent as NavigationPage;
            if (nav != null)
            {
                //nav.BarBackgroundColor = BarBackgroundColor;
                //nav.BarTextColor = BarTextColor;
            }

            if (!_isIntialized)
            {
                _isIntialized = true;
                OnLoaded();
            }
        }

        protected virtual void OnLoaded()
        {
            TrackPage(new Dictionary<string, string>());
        }

        protected virtual void TrackPage(Dictionary<string, string> metadata)
        {
            // TODO: Hockey App to track this page
        }
    }
}
