using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using POD.Forms.Utilities;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace POD.Forms.ViewModels
{
    /// <summary>
    /// Base class for all view models, providing: 
    /// - Navigation ability within the view model
    /// - Run task safely with all exceptions be reported to Hockey App and notified to users
    /// - Cancel running tasks when a user navigates away from a page
    /// </summary>
    public abstract class BaseViewModel : BindableBase, INavigationAware
    {
        protected readonly IAppService AppService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        protected BaseViewModel(IAppService appService)
        {
            AppService = appService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            CancelTasks();
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        #region Task Safe

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        /// <summary>
		/// All tasks are created as unstarted tasks and are processe via a proxy method that will run the task safely
		/// Instead of wrapping every task body in a try/catch, we'll process tasks in RunSafe
		/// RunSafe will start the task within the scope of a try/catch block and notify the app of any exceptions
		/// This can also be used to cancel running tasks when a user navigates away from a page - each VM has a cancellation token
		/// </summary>
		public async Task RunSafe(Task task, bool checkNetworkReachable = false, bool notifyOnError = true)
        {
            if (checkNetworkReachable && !App.IsNetworkReachable)
            {
                MessagingCenter.Send<BaseViewModel, Exception>(this, Messages.ExceptionOccurred, new WebException("Please connect to the network!"));
                return;
            }

            Exception exception = null;

            try
            {
                await Task.Run(() =>
                {
                    if (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        task.Start();
                        task.Wait(_cancellationTokenSource.Token);
                    }
                });
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Task Cancelled");
            }
            catch (AggregateException e)
            {
                var ex = e.InnerException;
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                exception = ex;
            }
            catch (Exception e)
            {
                exception = e;
            }

            if (exception != null)
            {
                // TODO: Hockey App to report exception
                Debug.WriteLine(exception);

                if (notifyOnError)
                {
                    NotifyException(exception);
                }
            }
        }

        private void NotifyException(Exception exception)
        {
            MessagingCenter.Send<BaseViewModel, Exception>(this, Messages.ExceptionOccurred, exception);
        }

        public void CancelTasks()
        {
            if (!_cancellationTokenSource.IsCancellationRequested && _cancellationTokenSource.Token.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }

        #endregion
    }
}
