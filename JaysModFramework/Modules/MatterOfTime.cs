﻿using GTA;
using System;

namespace JaysModFramework
{
    public class MatterOfTime: Module
    {
        private  int _minutes = 0;

        public override string ModuleName => "A Matter of Time";

        public override string ModuleDescription => "Functionality for modifying the flow of time. The Time Stone!";

        public override bool DefaultActivationState => false;
        public override void OnActivate()
        {
            World.IsClockPaused = true;
            _minutes = DateTime.Now.Minute;
        }
        public override void OnDeactivate()
        {
            World.IsClockPaused = false;
        }
        public override void OnTick()
        {
            if (DateTime.Now.Minute != _minutes)
            {
                Debug.Log(DebugSeverity.Info, _minutes.ToString());
                World.CurrentDate = World.CurrentDate.AddMinutes(1);
                _minutes = DateTime.Now.Minute;
            }
        }
    }
}
