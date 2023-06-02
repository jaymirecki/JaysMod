using GTA;
using System;

namespace JaysModFramework
{
    public class MatterOfTime: Module
    {
        private static int Minutes = 0;
        private static bool Activated = false;

        public override string ModuleName => "A Matter of Time";

        public override string ModuleDescription => "Functionality for modifying the flow of time. The Time Stone!";

        public override bool DefaultActivationState => false;
    }
}
