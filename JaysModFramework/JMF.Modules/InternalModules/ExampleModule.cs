using JMF.Native;

namespace JMF.Modules
{
    public class ExampleModule : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = 
            new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Example Module";
        public override string ModuleDescription { get; } = 
            "An example module that implements the core functionality " +
            "available to every module";
        public override ModuleSettings Settings { 
            get { return Framework.Config.ExampleModuleSettings; } 
        }
        public ExampleModule() : base() { }
        public override void OnControlReleased(Control control)
        {
            if (control == Control.Aim)
            {
                DebugInfo("OnControlRelease");
                throw new System.Exception("control released exception");
            }
        }
        public override void OnControlDoublePressed(Control control)
        {
            if (control == Control.MultiplayerInfo)
            {
                DebugInfo("OnControlDoublePressed");
                throw new System.Exception("control released exception");
            }
        }
        public override void OnControlHeld(Control control)
        {
            DebugInfo("OnControlHeld");
            throw new System.Exception("control released exception");
        }
        public override void OnActivate()
        {
            DebugInfo("OnActivate");
            throw new System.Exception("control released exception");
        }
        public override void OnDeactivate()
        {
            DebugInfo("OnDeactivate");
            throw new System.Exception("control released exception");
        }
        public override void OnTick()
        {
            DebugInfo("OnTick");
        }
        private void DebugInfo<T>(T message)
        {
            Debug.Info($"Example Module: {message}");
        }
    }
}
