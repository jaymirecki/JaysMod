using GTA;
using JaysModFramework.Menus;
using System;

namespace JaysModFramework
{
    public abstract class Module : Script
    {
        private const string DeactivatedString = "Deactivated";
        private const string ActivatedString = "Activated";
        public abstract string ModuleName { get; }
        public abstract string ModuleDescription { get; }
        public bool IsActive { get; private set; }
        public abstract bool DefaultActivationState { get; }
        private MenuListItem<string> _menuItem;
        public MenuListItem<string> MenuItem
        {
            get { return GenerateMenuItem(); }
        }
        public Module()
        {
            ModuleManager.AddModule(this);
            IsActive = DefaultActivationState;
        }
        public virtual void OnTick() { }
        public virtual void OnControlReleased(GTA.Control control) { }
        public virtual void OnControlHeld(GTA.Control control) { }
        public virtual void OnControlDoublePressed(GTA.Control control) { }
        //public abstract void ControlPressed(Control control);
        private void CheckControlPressed()
        {
            Control[] controls = (Control[])Enum.GetValues(typeof(Control));
            foreach (Control control in controls)
            {
                if (GTA.Game.IsEnabledControlPressed(control))
                {

                }
            }
        }
        #region Activation/Deactivation
        public void Activate()
        {
            if (!IsActive)
            {
                Debug.Log(DebugSeverity.Info, ModuleName + " " + ActivatedString.ToLower());
                IsActive = true;
            }
        }
        public void Deactivate()
        {
            if (IsActive)
            {
                Debug.Log(DebugSeverity.Info, ModuleName + " " + DeactivatedString.ToLower());
                IsActive = false;
            }
        }
        public void Toggle()
        {
            IsActive = !IsActive;
        }
        #endregion Activation/Deactivation
        #region MenuItem
        private MenuListItem<string> GenerateMenuItem()
        {
            if (_menuItem == null)
            {
                _menuItem = new MenuListItem<string>(ModuleName, ModuleDescription, DeactivatedString, ActivatedString);
                _menuItem.SelectedItem = IsActive ? ActivatedString : DeactivatedString;
                Debug.Log(DebugSeverity.Info, ModuleName);
                _menuItem.ItemChanged += ItemChanged;
                _menuItem.Selected += Selected;
            }
            return _menuItem;
        }

        private void Selected(object sender, SelectedEventArgs e)
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

        private void ItemChanged(object sender, ItemChangedEventArgs<string> e)
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
        #endregion MenuItem
    }
}
