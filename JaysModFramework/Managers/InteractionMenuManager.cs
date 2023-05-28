using GTA;
using GTA.Native;
using JaysModFramework.Menus;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Managers
{
    using Menu = Menus.Menu;
    public class InteractionMenuManager : IManager
    {
        private bool _active = false;
        private DateTime _controlPressed = DateTime.MaxValue;
        private ObjectPool _pool;
        private Menu _menu;
        public InteractionMenuManager() 
        {
            _pool = new ObjectPool();
            _menu = new Menu("Interactions", _pool);
            _menu.Add(ModuleManager.ModuleMenu(_pool));
            _menu.Add(Debug.Menu(_pool));
            _controlPressed = DateTime.MaxValue;
        }

        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public void OnTick(object sender, EventArgs e) 
        {
            _pool.Process();
            if (Game.IsControlPressed(GTA.Control.CharacterWheel))
            {
                if (_controlPressed == DateTime.MaxValue)
                {
                    _controlPressed = DateTime.UtcNow;
                }
                else
                {
                    TimeSpan duration = DateTime.UtcNow - _controlPressed;
                    if (duration.TotalSeconds > 1)
                    {
                        _menu.Open();
                    }
                }
            }
            else if (Game.IsControlJustReleased(GTA.Control.CharacterWheel))
            {
                _controlPressed = DateTime.MaxValue;
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
