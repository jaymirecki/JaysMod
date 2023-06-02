using JaysModFramework.Menus;
using System;
using System.Collections.Generic;

namespace JaysModFramework
{
    using ModuleListItem = MenuListItem<string>;
    public static class ModuleManager
    {
        private static readonly List<Module> _moduleList = new List<Module>();
        //private static readonly List<ModuleListItem> _moduleMenuItemList = new List<ModuleListItem>();
        #region Menu
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
            _moduleList.Sort();
        }
        private static void RefreshMenu()
        {
            if (_moduleMenu == null)
            {
                return;
            }
            _moduleMenu.Clear();
            Debug.Log(DebugSeverity.Info, _moduleList.Count.ToString());
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
        #endregion Menu

        #region Life cycle events
        public static void OnTick()
        {
            foreach (Module module in _moduleList)
            {
                module.OnTick();
            }
        }
        public static void OnControlReleased(GTA.Control control)
        {
            foreach (Module module in _moduleList)
            {
                module.OnControlReleased(control);
            }
        }
        public static void OnControlHeld(GTA.Control control)
        {
            foreach (Module module in _moduleList)
            {
                module.OnControlHeld(control);
            }
        }
        public static void OnControlDoublePressed(GTA.Control control)
        {
            foreach (Module module in _moduleList)
            {
                module.OnControlDoublePressed(control);
            }
        }
        #endregion Life cycle events
    }
    public class ModuleScriptRunner : GTA.Script
    {
        private OOD.Collections.Dictionary<GTA.Control, DateTime> _controlJustReleased = new OOD.Collections.Dictionary<GTA.Control, DateTime>();
        private OOD.Collections.Dictionary<GTA.Control, DateTime> _controlPressed = new OOD.Collections.Dictionary<GTA.Control, DateTime>();

        public ModuleScriptRunner()
        {
            Tick += ModuleRunner_Tick;

            GTA.Control[] controls = (GTA.Control[])Enum.GetValues(typeof(GTA.Control));
            foreach (GTA.Control control in controls)
            {
                _controlJustReleased.Add(control, DateTime.MinValue);
                _controlPressed.Add(control, DateTime.MaxValue);
            }
        }
        private void ModuleRunner_Tick(object sender, EventArgs e)
        {
            ModuleManager.OnTick();

            GTA.Control[] controls = (GTA.Control[])Enum.GetValues(typeof(GTA.Control));
            foreach (GTA.Control control in controls)
            {
                if (GTA.Game.IsEnabledControlJustReleased(control))
                {
                    ModuleManager.OnControlReleased(control);

                    TimeSpan duration = DateTime.UtcNow - _controlJustReleased[control];
                    if (duration.TotalSeconds < 1)
                    {
                        ModuleManager.OnControlDoublePressed(control);
                        _controlJustReleased[control] = DateTime.MinValue;
                    }
                    else
                    {
                        _controlJustReleased[control] = DateTime.UtcNow;
                    }

                    _controlPressed[control] = DateTime.MaxValue;
                }
                else if (GTA.Game.IsEnabledControlPressed(control))
                {
                    if (_controlPressed[control] == DateTime.MaxValue)
                    {
                        _controlPressed[control] = DateTime.UtcNow;
                    }
                    else
                    {
                        TimeSpan duration = DateTime.UtcNow - _controlPressed[control];
                        if (duration.TotalSeconds > 1 && duration.TotalSeconds < 2)
                        {
                            ModuleManager.OnControlHeld(control);
                            _controlPressed[control] = DateTime.MinValue;
                        }
                    }
                }
            }
        }
    }
}
