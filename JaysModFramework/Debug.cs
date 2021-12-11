using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public static class Debug
    {
        public static void Log(string value)
        {
            GTA.UI.Notification.Show(value);
        }
        public static void Log<T>(T value)
        {
            Log(value.ToString());
        }
    }
}
