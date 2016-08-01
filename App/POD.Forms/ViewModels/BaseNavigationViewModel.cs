using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MvvmHelpers;
using POD.Forms.Utilities;
using Xamarin.Forms;

namespace POD.Forms.ViewModels
{
    /// <summary>
    /// Base class for all view models, providing: 
    /// - Navigation ability within the view model
    /// - Run task safely with all exceptions be reported to Hockey App and notified to users
    /// - Cancel running tasks when a user navigates away from a page
    /// </summary>
    public abstract class BaseNavigationViewModel : BaseViewModel, INavigation
    {
        #region Navigation

        private INavigation _navigation => Application.Current?.MainPage?.Navigation;

        public void RemovePage(Page page)
        {
            _navigation?.RemovePage(page);
        }

        public void InsertPageBefore(Page page, Page before)
        {
            _navigation?.InsertPageBefore(page, before);
        }

        public async Task PushAsync(Page page)
        {
            var task = _navigation?.PushAsync(page);
            if (task != null)
                await task;
        }

        public async Task<Page> PopAsync()
        {
            var task = _navigation?.PopAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PopToRootAsync()
        {
            var task = _navigation?.PopToRootAsync();
            if (task != null)
                await task;
        }

        public async Task PushModalAsync(Page page)
        {
            var task = _navigation?.PushModalAsync(page);
            if (task != null)
                await task;
        }

        public async Task<Page> PopModalAsync()
        {
            var task = _navigation?.PopModalAsync();
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PushAsync(Page page, bool animated)
        {
            var task = _navigation?.PushAsync(page, animated);
            if (task != null)
                await task;
        }

        public async Task<Page> PopAsync(bool animated)
        {
            var task = _navigation?.PopAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public async Task PopToRootAsync(bool animated)
        {
            var task = _navigation?.PopToRootAsync(animated);
            if (task != null)
                await task;
        }

        public async Task PushModalAsync(Page page, bool animated)
        {
            var task = _navigation?.PushModalAsync(page, animated);
            if (task != null)
                await task;
        }

        public async Task<Page> PopModalAsync(bool animated)
        {
            var task = _navigation?.PopModalAsync(animated);
            return task != null ? await task : await Task.FromResult(null as Page);
        }

        public IReadOnlyList<Page> NavigationStack => _navigation?.NavigationStack;

        public IReadOnlyList<Page> ModalStack => _navigation?.ModalStack;

        #endregion

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

        public virtual void CancelTasks()
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
