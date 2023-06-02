using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public class MatterOfTime: Script
    {
        private static int Minutes = 0;
        private static bool Activated = false;

        public MatterOfTime()
        {
            Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (Activated)
            {
                if (DateTime.Now.Minute != Minutes)
                {
                    World.CurrentDate = World.CurrentDate.AddMinutes(1);
                    Minutes = DateTime.Now.Minute;
                }
            }
        }
        public static void Activate()
        {
            Activated = true;
            Minutes = DateTime.Now.Minute;
        }
        public static void Deactivate()
        {
            Activated = false;
        }
        public static void Toggle()
        {
            Activated = !Activated;
            if (Activated)
            {
                Minutes = DateTime.Now.Minute;
            }
        }
    }
}
