
namespace JMF.Modules
{
    public class SilentSirens : InternalModule
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName => "Silent Sirens";
        public override string ModuleDescription => "Allows turning off a siren but leaving emergency lights on";
        public override bool DefaultActivationState => Global.Config.SirenModuleEnabled;
        public override void OnControlReleased(Control control)
        {
            if (control == Control.VehicleRadioWheel)
            {
                Game.Player.Character.CurrentVehicle.ToggleSirenNoise();
            }
            else if (control == Control.VehicleHorn)
            {
                if (!Game.Player.Character.CurrentVehicle.SirenOn)
                {
                    Debug.Notify("Siren turned on", true);
                }
            }
        }
        public override void OnTick()
        {

        }
    }
}
