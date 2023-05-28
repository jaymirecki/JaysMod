using JaysModFramework.Menus;
using LemonUI;
using System.Collections.Generic;

namespace JaysModFramework
{
    using ModuleListItem = MenuListItem<string>;
    public static class ModuleManager
    {
        private static readonly List<Module> _moduleList = new List<Module>();
        private static readonly List<ModuleListItem> _moduleMenuItemList = new List<ModuleListItem>();
        private static Menu _moduleMenu;
        public static Menu ModuleMenu(ObjectPool pool)
        {
            if (_moduleMenu == null)
            {
                _moduleMenu = new Menu("Modules", "Modules", pool);
                _moduleMenu.Opening += ModuleMenu_Opening;
            }
            return _moduleMenu;
        }

        private static void ModuleMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshMenu();
        }

        private static int MenuItemComparer(ModuleListItem a, ModuleListItem b)
        {
            return string.Compare(a.Title, b.Title);
        }
        public static void AddModule(Module module)
        {
            _moduleList.Add(module);
            _moduleMenuItemList.Add(module.MenuItem);
        }
        private static void RefreshMenu()
        {
            if (_moduleMenu == null)
            {
                return;
            }
            _moduleMenu.Clear();
            _moduleMenuItemList.Sort(MenuItemComparer);
            foreach (ModuleListItem item in _moduleMenuItemList)
            {
                _moduleMenu.Add(item);
            }
        }
    }
}
