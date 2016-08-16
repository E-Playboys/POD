using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Plugin.Connectivity;
using POD.Forms.Providers;
using POD.Forms.Utilities;
using POD.Forms.ViewModels;
using POD.Forms.Views;
using Prism.Unity;
using Xamarin.Forms;
using Microsoft.Practices.Unity;

namespace POD.Forms
{
    public partial class App : PrismApplication
    {
        public new static App Current => (App)Application.Current;

        public static bool IsNetworkReachable { get; set; }

        protected override void OnInitialized()
        {
            InitializeComponent();

            // Global exception handler for view models
            MessagingCenter.Subscribe<BaseViewModel, Exception>(this, Messages.ExceptionOccurred, OnAppExceptionOccurred);

            // Network status
            IsNetworkReachable = CrossConnectivity.Current.IsConnected;
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                IsNetworkReachable = args.IsConnected;
            };

            NavigationService.NavigateAsync("DebtListPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<DebtListPage>();

            Container.RegisterType<IAppService, AppService>();
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
                    var appService = Container.Resolve<IAppService>();

                    appService.Hud.Dismiss();

                    var msg = exception.Message;
                    if (msg.Length > 300)
                        msg = msg.Substring(0, 300);

                    appService.Toast.Notify(msg, true);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });
        }
    }
}
