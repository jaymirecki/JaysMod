using JaysModFramework.Native;

namespace JaysModFramework.Modules
{
    public class BigMap : Module
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Big Map SP";
        public override string ModuleDescription { get; } = "Enables use of larger radar from MP";
        public override bool DefaultActivationState { get { return Global.Config.BigMapModuleEnabled; } }
        private bool _active = false;
        public BigMap() : base() { }
        public override void OnControlDoublePressed(Utilities.Control control)
        {
            if (control == Utilities.Control.MultiplayerInfo)
            {
                SetMapActive(!_active);
            }
        }
        private void SetMapActive(bool active)
        {
            Function.Call(Hash.SetBigmapActive, active);
            _active = active;
        }
        public override void OnDeactivate()
        {
            SetMapActive(false);
        }
    }
}
