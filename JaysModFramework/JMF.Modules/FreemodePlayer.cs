using JMF.Native;

namespace JMF.Modules
{
    public class FreemodePlayer : Module
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Freemode Player";
        public override string ModuleDescription { get; } = "Converts the player to male freemode ped";
        public override bool DefaultActivationState { get { return false; } }
        public FreemodePlayer() : base() { }
        private Model originalModel;
        public override void OnActivate()
        {
            originalModel = Game.Player.Character.Model;
            Game.Player.SetModel(PedHash.FreemodeMale01);
        }
        public override void OnDeactivate()
        {
            Game.Player.SetModel(originalModel);
        }
    }
}
