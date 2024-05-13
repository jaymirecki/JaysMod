using JMF.Menus;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace JMF.Modules
{
    using Menu = Menus.Menu;
    public class InteractionMenu : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Interaction Menu for JMF";
        public override string ModuleDescription { get; } = "Creates an Interaction Menu. WARNING: Disabling this module may prevent correct use of other modules.";
        public override ModuleSettings Settings { get { return Global.Config.InteractionMenuSettings; } }
        private static List<Menu> menuList = new List<Menu>();
        public static void AddMenu(Menu menu)
        {
            menuList.Add(menu);
        }
        private Menu _menu;
        public InteractionMenu() : base()
        {
        }

        public override void OnControlReleased(Control control) {
            if (control == Control.SelectCharacterMultiplayer)
            {
                _menu = new Menu("Interactions", Global.ObjectPool);
                foreach(Menu menu in menuList)
                {
                    _menu.Add(menu);
                }
                _menu.Open();
            }
        }
        public override void OnTick() 
        {
            Global.ObjectPool.Process();
        }
        public override void OnControlHeld(Control control)
        {
            if (control == Control.CharacterWheel)
            {
                _menu.Open();
            }
        }
    }
}
