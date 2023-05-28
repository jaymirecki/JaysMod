using GTA;
using JaysModFramework.Menus;
using System;

namespace JaysModFramework
{
    public abstract class Module : Script
    {
        private const string DeactivatedString = "Deactivated";
        private const string ActivatedString = "Activated";
        public static string ModuleName { get; protected set; } = "";
        public static string ModuleDescription { get; protected set; } = "";
        public static bool IsActive { get; private set; } = false;
        private static MenuListItem<string> _menuItem;
        public MenuListItem<string> MenuItem
        {
            get { return GenerateMenuItem(); }
        }
        public Module()
        {
            SetModuleSpecifics();
            Tick += Module_Tick;
            ModuleManager.AddModule(this);
        }
        protected abstract void SetModuleSpecifics();
        private void Module_Tick(object sender, EventArgs e)
        {
            if (IsActive)
            {
                OnTick(sender, e);
            }
        }

        public abstract void OnTick(object sender, EventArgs e);
        //public abstract void ControlPressed(Control control);
        private static void CheckControlPressed()
        {
            Control[] controls = (Control[])Enum.GetValues(typeof(Control));
            foreach (Control control in controls)
            {
                if (GTA.Game.IsEnabledControlPressed(control))
                {

                }
            }
        }
        public static void Activate()
        {
            if (!IsActive)
            {
                Debug.Log(DebugSeverity.Info, ModuleName + " " + ActivatedString.ToLower());
                IsActive = true;
            }
        }
        public static void Deactivate()
        {
            if (IsActive)
            {
                Debug.Log(DebugSeverity.Info, ModuleName + " " + DeactivatedString.ToLower());
                IsActive = false;
            }
        }
        public static void Toggle()
        {
            IsActive = !IsActive;
        }
        private static MenuListItem<string> GenerateMenuItem()
        {
            if (_menuItem == null)
            {
                _menuItem = new MenuListItem<string>(ModuleName, ModuleDescription, DeactivatedString, ActivatedString);
                _menuItem.ItemChanged += ItemChanged;
                _menuItem.Selected += Selected;
            }
            return _menuItem;
        }

        private static void Selected(object sender, SelectedEventArgs e)
        {
            if (IsActive)
            {
                _menuItem.SelectedItem = ActivatedString;
            }
            else
            {
                _menuItem.SelectedItem = DeactivatedString;
            }
        }

        private static void ItemChanged(object sender, ItemChangedEventArgs<string> e)
        {
            switch (e.Object)
            {
                case DeactivatedString:
                    Deactivate();
                    break;
                case ActivatedString:
                    Activate();
                    break;
                default:
                    return;
            }
        }
    }
}
