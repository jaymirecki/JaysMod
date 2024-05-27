using JMF.Menus;
using JMF.Modules;
using System;
using System.Collections.Generic;

[assembly: Rage.Attributes.Plugin("JMF Module Manager", Description = "Manages lifecycles for other JMF Modules")]
namespace JMF
{
    using ModuleListItem = MenuListItem<string>;
    public static class ModuleManager
    {
        private static readonly List<Module> moduleList = new List<Module>();
        private static readonly List<InternalModule> internalModuleList = new List<InternalModule>();
        internal static List<Module> Modules
        {
            get { return moduleList; }
        }

        // Properties for managing control cycle
        private static OOD.Collections.Dictionary<Control, DateTime> _controlJustReleased = new OOD.Collections.Dictionary<Control, DateTime>();
        private static OOD.Collections.Dictionary<Control, DateTime> _controlPressed = new OOD.Collections.Dictionary<Control, DateTime>();
        private static bool _controlsInitialized = false;
        #region Menu
        private static Menu _moduleMenu;
        public static Menu ModuleMenu()
        {
            if (_moduleMenu == null)
            {
                _moduleMenu = new Menu("Modules", "Modules", Global.ObjectPool);
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
            moduleList.Add(module);
            moduleList.Sort();
        }
        public static void AddInternalModule(InternalModule module)
        {
            internalModuleList.Add(module);
            internalModuleList.Sort();
        }
        private static void RefreshMenu()
        {
            if (_moduleMenu == null)
            {
                return;
            }
            _moduleMenu.Clear();
            foreach (Module module in moduleList)
            {
                _moduleMenu.Add(module.Menu);
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
            foreach (InternalModule module in internalModuleList)
            {
                if (module.IsActive)
                {
                    module.OnTick();
                }
            }
        }
        public static void OnControlReleased(Control control)
        {
            foreach (Module module in moduleList)
            {
                if (module.IsActive)
                {
                    module.OnControlReleased(control);
                }
            }
        }
        public static void OnControlHeld(Control control)
        {
            foreach (Module module in moduleList)
            {
                if (module.IsActive)
                {
                    module.OnControlHeld(control);
                }
            }
        }
        public static void OnControlDoublePressed(Control control)
        {
            foreach (Module module in moduleList)
            {
                if (module.IsActive)
                {
                    module.OnControlDoublePressed(control);
                }
            }
        }
        #endregion Life cycle events
        #region Tick cycle
        internal static void Tick()
        {
            ModuleManager.OnTick();
            ModuleManager.InitializeControls();
            Control[] controls = (Control[])Enum.GetValues(typeof(Control));
            foreach (Control control in controls)
            {
                ModuleManager.CheckEnabledControlJustReleased(control);
                ModuleManager.CheckEnabledControlPressed(control);
            }
        }

        private static void InitializeControls()
        {
            if (!_controlsInitialized)
            {
                Control[] controls = (Control[])Enum.GetValues(typeof(Control));
                foreach (Control control in controls)
                {
                    _controlJustReleased.Add(control, DateTime.MinValue);
                    _controlPressed.Add(control, DateTime.MaxValue);
                }
                _controlsInitialized = true;
            }
        }
        private static void CheckEnabledControlJustReleased(Control control)
        {
            if (ModuleManager.IsEnabledControlJustReleased(control))
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
        }
        private static void CheckEnabledControlPressed(Control control)
        {
            if (ModuleManager.IsEnabledControlPressed(control))
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
                        _controlPressed[control] = DateTime.MaxValue;
                    }
                }
            }
        }
        private static bool IsEnabledControlJustReleased(Control control)
        {
            return
                Native.Function.Call<bool>(Native.Hash.IsControlEnabled, 0, (int)control) &&
                Native.Function.Call<bool>(Native.Hash.IsControlJustReleased, 1, (int)control);
        }
        private static bool IsEnabledControlJustPressed(Control control)
        {
            return
                Native.Function.Call<bool>(Native.Hash.IsControlEnabled, 0, (int)control) &&
                Native.Function.Call<bool>(Native.Hash.IsControlJustPressed, 1, (int)control);
        }
        private static bool IsEnabledControlPressed(Control control)
        {
            return
                Native.Function.Call<bool>(Native.Hash.IsControlEnabled, 0, (int)control) &&
                Native.Function.Call<bool>(Native.Hash.IsControlPressed, 1, (int)control);
        }
        #endregion Tick cycle
        public static T GetModule<T>(Type moduleType) where T: Module
        {

            foreach (Module module in ModuleManager.Modules)
            {
                if (module.GetType() == moduleType)
                {
                    return (T)module;
                }
            }
            return null;
        }
    }
    public class RageModuleScriptRunner
    {

        public static void Main()
        {
            InteractionMenu.AddMenu(ModuleManager.ModuleMenu());
            InteractionMenu.AddMenu(Debug.Menu());
            new InteractionMenu();
            new SilentSirens();
            new BigMap();
            new MatterOfTime();
            new FreemodePlayer();
            new Respawner();
            new IPLLoader();
            new Yacht();
            new PortalManager();
            new WorldspaceLoader();
            while (true)
            {
                ModuleManager.Tick();
                Rage.GameFiber.Yield();
            }
        }
    }
}
