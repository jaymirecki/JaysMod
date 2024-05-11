using JMF.Native;

namespace JMF.Modules
{
    public class BigMap : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Big Map SP";
        public override string ModuleDescription { get; } = "Enables use of larger radar from MP";
        public override ModuleSettings Settings { get { return Global.Config.BigMapSettings; } }
        private bool _active = false;
        public BigMap() : base() { }
        public override void OnControlDoublePressed(Control control)
        {
            if (control == Control.MultiplayerInfo)
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
    public class BigMapSettings: ModuleSettings
    {

    }
}
