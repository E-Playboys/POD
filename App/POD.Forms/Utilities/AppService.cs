using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POD.Forms.Providers;
using Prism.Navigation;
using Prism.Services;

namespace POD.Forms.Utilities
{
    public interface IAppService
    {
        INavigationService Navigation { get; }
        IPageDialogService Dialog { get; }
        IHudProvider Hud { get; }
        IToastProvider Toast { get; }
    }

    public class AppService : IAppService
    {
        public INavigationService Navigation { get; }
        public IPageDialogService Dialog { get; }
        public IHudProvider Hud { get; }
        public IToastProvider Toast { get; }

        public AppService(INavigationService navigationService, IPageDialogService dialogService, IHudProvider hudProvider, IToastProvider toastProvider)
        {
            Navigation = navigationService;
            Dialog = dialogService;
            Hud = hudProvider;
            Toast = toastProvider;
        }
    }
}
