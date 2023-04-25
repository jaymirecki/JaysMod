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
        private static void Notify(string value, bool overrideDebugFlag = false)
        {
            if (DEBUG || overrideDebugFlag)
            {
                GTA.UI.Notification.Show(value);
            }
        }
        public static void Notify<T>(T value, bool overrideDebugFlag = false)
        {
            if (value == null)
            {
                Notify("NULL", overrideDebugFlag);
            }
            else
            {
                Notify(value.ToString(), overrideDebugFlag);
            }
        }
        public static void Notify<T>(string label, T value, bool overrideDebugFlag = false)
        {
            Notify(label + ": " + value.ToString(), overrideDebugFlag);
        }
    }
}
