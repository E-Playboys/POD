using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POD.Forms.Utilities
{
    public class Hud : IDisposable
    {
        bool _cancel;

        public Hud(string message)
        {
            StartHud(message);
        }

        private async void StartHud(string message)
        {
            await Task.Delay(100);

            if (_cancel)
            {
                _cancel = false;
                return;
            }

            _cancel = false;
            App.Current.HudProvider.DisplayProgress(message);
        }

        public void Dispose()
        {
            _cancel = true;
            App.Current.HudProvider.Dismiss();
        }
    }
}
