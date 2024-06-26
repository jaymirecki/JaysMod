﻿using JMF.Menus;
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
        public override ModuleSettings Settings { get { return Framework.Config.InteractionMenuSettings; } }
        private static List<Menu> menuList = new List<Menu>();
        public static void AddMenu(Menu menu)
        {
            menuList.Add(menu);
        }
        public static bool RemoveMenu(Menu menu)
        {
            return menuList.Remove(menu);
        }
        private Menu _menu;
        public InteractionMenu() : base()
        {
            AddMenu(Framework.State.LoadMenu);
            AddMenu(Framework.State.SaveMenu);
            AddMenu(Framework.State.NewMenu);
        }

        public override void OnControlReleased(Control control) {
            if (control == Control.SelectCharacterMultiplayer)
            {
                _menu = new Menu("Interactions", Framework.ObjectPool);
                foreach(Menu menu in menuList)
                {
                    _menu.Add(menu);
                }
                _menu.Open();
            }
        }
        public override void OnTick() 
        {
            Framework.ObjectPool.Process();
        }
        public override void OnControlHeld(Control control)
        {
            if (control == Control.CharacterWheel)
            {
                _menu = new Menu("Interactions", Framework.ObjectPool);
                foreach (Menu menu in menuList)
                {
                    _menu.Add(menu);
                }
                _menu.Open();
            }
        }
    }
}
