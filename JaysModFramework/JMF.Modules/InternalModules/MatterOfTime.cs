using System;

namespace JMF.Modules
{
    public class MatterOfTime : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        private int _minutes = 0;

        public override string ModuleName => "A Matter of Time";

        public override string ModuleDescription => "Functionality for modifying the flow of time. The Time Stone!";
        public override ModuleSettings Settings { get { return Global.Config.MatterOfTimeSettings; } }

        public override void OnActivate()
        {
            Game.Clock.Pause(true);
            _minutes = DateTime.Now.Minute;
        }
        public override void OnDeactivate()
        {
            Game.Clock.Pause(false);
        }
        public override void OnTick()
        {
            if (DateTime.Now.Minute != _minutes)
            {
                Game.Clock.Date = Game.Clock.Date.AddMinutes(1);
                _minutes = DateTime.Now.Minute;
            }
        }
    }
}
