using GTA;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Modules
{
    public class SirenModule : Module
    {

        public override string ModuleName => "SirenModule";

        public override string ModuleDescription => "Allows turning off a siren but leaving emergency lights on";

        public override bool DefaultActivationState => Global.Config.SirenModuleEnabled;
        public SirenModule() : base()
        {
            KeyDown += OnKeyDown;
        }

        public override void OnTick()
        {
            
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.J && IsActive && Game.Player.Character.IsInVehicle())
            {
                new Vehicle(Game.Player.Character.CurrentVehicle).ToggleSirenNoise();
            }
        }
    }
}
