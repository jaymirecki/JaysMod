using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Managers
{
    public class BigMapModule : Module
    {
        public override string ModuleName { get; } = "BigMapModule";
        public override string ModuleDescription { get; } = "Enables use of larger radar from MP";
        public override bool DefaultActivationState { get { return Global.Config.BigMapModuleEnabled; } }
        private bool _active = false;
        private DateTime _controlJustPressed = DateTime.MinValue;
        public BigMapModule(): base() { }
        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public override void OnTick(object sender, EventArgs e) 
        {
            if (Game.IsEnabledControlJustReleased(GTA.Control.MultiplayerInfo))
            {
                TimeSpan duration = DateTime.UtcNow - _controlJustPressed;
                if (duration.TotalSeconds < 1)
                {
                    Function.Call(Hash.SET_BIGMAP_ACTIVE, !_active);
                    _active = !_active;
                    _controlJustPressed = DateTime.MinValue;
                }
                else
                {
                    _controlJustPressed = DateTime.UtcNow;
                }
            }
        }
    }
}
