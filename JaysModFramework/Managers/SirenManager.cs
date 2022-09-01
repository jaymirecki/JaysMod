using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JaysModFramework
{
    public class SirenManager : Script
    {
        private static bool Enabled = false;
        public SirenManager()
        {
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J && Enabled && Game.Player.Character.IsInVehicle())
            {
                new Vehicle(Game.Player.Character.CurrentVehicle).ToggleSirenNoise();
            }
        }
        public static void Activate()
        {
            Enabled = true;
        }
        public static void Deactivate()
        {
            Enabled = false;
        }
    }
}
