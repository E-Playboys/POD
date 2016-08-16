using System;
using System.Collections.Generic;
using System.Diagnostics;
using MvvmHelpers;
using Newtonsoft.Json;
using Plugin.Connectivity;
using POD.Forms.Pages;
using POD.Forms.Providers;
using POD.Forms.Utilities;
using POD.Forms.ViewModels;
using Prism.Autofac;
using Prism.Autofac.Forms;
using Xamarin.Forms;

namespace POD.Forms
{
    public partial class App : PrismApplication
    {
        public new static App Current => (App)Application.Current;

        private IHudProvider _hudProvider;
        public IHudProvider HudProvider => _hudProvider ?? (_hudProvider = DependencyService.Get<IHudProvider>());

        private IToastProvider _toastProvider;
        public IToastProvider ToastProvider => _toastProvider ?? (_toastProvider = DependencyService.Get<IToastProvider>());

        public static bool IsNetworkReachable { get; set; }

        protected override void OnInitialized()
        {
            InitializeComponent();

            // Global exception handler for view models
            MessagingCenter.Subscribe<BaseNavigationViewModel, Exception>(this, Messages.ExceptionOccurred, OnAppExceptionOccurred);

            // Network status
            IsNetworkReachable = CrossConnectivity.Current.IsConnected;
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                IsNetworkReachable = args.IsConnected;
            };

            MainPage = new NavigationPage(new DebtListPage());
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<DebtListPage>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

		private void OnAppExceptionOccurred(BaseViewModel viewModel, Exception exception)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    _hudProvider?.Dismiss();

                    var msg = exception.Message;
                    if (msg.Length > 300)
                        msg = msg.Substring(0, 300);

                    msg.ToToast(true);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
        }
    }
}
