using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POD.Forms.Providers
{
    public interface IHudProvider
    {
        void DisplayProgress(string message, int progress = -1);

        void DisplaySuccess(string message);

        void DisplayError(string message);

        void Dismiss();
    }
}
