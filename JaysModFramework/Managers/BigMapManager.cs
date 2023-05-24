using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Managers
{
    public class BigMapManager : IManager
    {
        private bool _active = false;
        private DateTime _controlJustPressed = DateTime.MinValue;
        public BigMapManager() { }

        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public void OnTick(object sender, EventArgs e) 
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
        //public void Activate()
        //{
        //    Enabled = true;
        //}
        //public void Deactivate()
        //{
        //    Enabled = false;
        //}
        //public bool Toggle()
        //{
        //    Enabled = !Enabled;
        //    return Enabled;
        //}
    }
}
