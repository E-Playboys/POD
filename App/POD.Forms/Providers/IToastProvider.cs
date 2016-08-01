using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POD.Forms.Providers
{
    public interface IToastProvider
    {
        void Notify(string message, bool centered = false);
    }
}
