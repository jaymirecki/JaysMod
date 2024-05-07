
using JMF.UI;

namespace JMF.Modules
{
    public class SilentSirens : InternalModule
    {
        private const Control CONTROL = Control.VehicleRadioWheel;
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName => "Silent Sirens";
        public override string ModuleDescription => "Allows turning off a siren but leaving emergency lights on";
        public override bool DefaultActivationState => Global.Config.SirenModuleEnabled;
        private bool sirenHelpMessageSent = false;
        public override void OnControlReleased(Control control)
        {
            if (control == CONTROL && Game.Player.Character.IsInAnyVehicle)
            {
                Game.Player.Character.CurrentVehicle.ToggleSirenNoise();
            }
        }
        public override void OnTick()
        {
            Ped player = Game.Player.Character;
            if (player.IsInAnyVehicle)
            {
                if (player.CurrentVehicle.SirenOn)
                {
                    if (!sirenHelpMessageSent)
                    {
                        Help.Show(new TextComponent("Press ") + new TextComponent(CONTROL) + new TextComponent(" to toggle siren noise"));
                        sirenHelpMessageSent = true;
                    }
                }
                else
                {
                    sirenHelpMessageSent = false;
                }
            }
            else
            {
                sirenHelpMessageSent = false;
            }
        }
    }
}
