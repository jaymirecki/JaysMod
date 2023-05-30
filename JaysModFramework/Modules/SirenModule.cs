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
        public override void OnControlHeld(GTA.Control control)
        {
            if (control == GTA.Control.VehicleRadioWheel)
            {
                new Vehicle(Game.Player.Character.CurrentVehicle).ToggleSirenNoise();
            }
        }
    }
}
