using JaysModFramework.Menus;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Managers
{
    using Menu = Menus.Menu;
    public class InteractionMenuModule : Module
    {
        public override string ModuleName { get; } = "InteractionMenuModule";
        public override string ModuleDescription { get; } = "Creates an Interaction Menu. WARNING: Disabling this module may prevent correct use of other modules.";
        public override bool DefaultActivationState { get { return Global.Config.InteractionMenuModuleEnabled; } }
        private DateTime _controlPressed = DateTime.MaxValue;
        private ObjectPool _pool = new ObjectPool();
        private Menu _menu;
        public InteractionMenuModule() : base()
        {
            _menu = new Menu("Interactions", _pool);
            _menu.Add(ModuleManager.ModuleMenu(_pool));
            _menu.Add(Debug.Menu(_pool));
        }

        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public override void OnTick(object sender, EventArgs e) 
        {
            _pool.Process();
            if (GTA.Game.IsControlPressed(GTA.Control.CharacterWheel))
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
            else if (GTA.Game.IsControlJustReleased(GTA.Control.CharacterWheel))
            {
                _controlPressed = DateTime.MaxValue;
            }
        }
    }
}
