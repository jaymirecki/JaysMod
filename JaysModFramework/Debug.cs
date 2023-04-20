using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public static class Debug
    {
        public static bool DEBUG = false;
        private static void Log(string value, bool overrideDebugFlag = false)
        {
            if (DEBUG || overrideDebugFlag)
            {
                GTA.UI.Notification.Show(value);
            }
        }
        public static void Log<T>(T value, bool overrideDebugFlag = false)
        {
            Log(value.ToString(), overrideDebugFlag);
        }
        public static void Log<T>(string label, T value, bool overrideDebugFlag = false)
        {
            Log(label + ": " + value.ToString(), overrideDebugFlag);
        }
    }
}
