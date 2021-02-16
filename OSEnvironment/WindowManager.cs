using System;
using System.Collections.Generic;
using System.Diagnostics;

using Utils;

namespace OSEnvironment
{
    public class WindowManager
    {
        public WindowManager()
        {

        }

        public string[] ListWindows()
        {
            return Procedural.Map((Process p) => {
                return p.MainWindowTitle;
            }, 
            Procedural.Filter(() Process.GetProcesses());
        }
    }
}
