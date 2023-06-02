using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Modules
{
    public class BigMapModule : Module
    {
        internal override int MajorVersion => 1;
        internal override int MinorVersion => 0;
        internal override int PatchVersion => 0;
        public override string ModuleName { get; } = "Big Map SP";
        public override string ModuleDescription { get; } = "Enables use of larger radar from MP";
        public override bool DefaultActivationState { get { return Global.Config.BigMapModuleEnabled; } }
        private bool _active = false;
        public BigMapModule(): base() { }
        public override void OnControlDoublePressed(GTA.Control control)
        {
            if (control == GTA.Control.MultiplayerInfo)
            {
                Function.Call(Hash.SET_BIGMAP_ACTIVE, !_active);
                _active = !_active;
            }
        }
    }
}
