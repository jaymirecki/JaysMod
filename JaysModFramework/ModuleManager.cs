using JaysModFramework.Menus;
using System.Collections.Generic;

namespace JaysModFramework
{
    using ModuleListItem = MenuListItem<string>;
    public static class ModuleManager
    {
        private static readonly List<Module> _moduleList = new List<Module>();
        //private static readonly List<ModuleListItem> _moduleMenuItemList = new List<ModuleListItem>();
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
        public static void AddModule(Module module)
        {
            _moduleList.Add(module);
            //_moduleMenuItemList.Add(module.MenuItem);
        }
        private static void RefreshMenu()
        {
            if (_moduleMenu == null)
            {
                return;
            }
            _moduleMenu.Clear();
            _moduleList.Sort(ModuleSorter);
            List<ModuleListItem> itemList = _moduleList.ConvertAll((Module m) => m.MenuItem);
            foreach (ModuleListItem item in itemList)
            {
                _moduleMenu.Add(item);
            }
        }
        private static int ModuleSorter(Module a, Module b)
        {
            return string.Compare(a.ModuleName, b.ModuleName);
        }
    }
}
