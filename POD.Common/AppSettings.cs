using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POD.Common
{
    public class AppSettings
    {
#if DEBUG
        // Settings for DEBUG build mode
#else
        // Settings for RELEASE build mode
#endif
    }
}
