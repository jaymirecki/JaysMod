using GTA;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Modules
{
    public class SilentSirens : Module
    {
        internal override int MajorVersion => 1;
        internal override int MinorVersion => 0;
        internal override int PatchVersion => 0;
        public override string ModuleName => "Silent Sirens";
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
