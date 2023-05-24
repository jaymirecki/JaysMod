using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public static class Framework
    {
        public static bool Initialized { get; private set; }
        public static void Initialize()
        {
            if (!Initialized)
            {
                ForceInitialize();
            }
        }
        public static void ForceInitialize()
        {
            Global.Database.InitializeIfNot();
            Initialized = true;
        }
    }
}
