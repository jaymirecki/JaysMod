using GTA;
using JaysModFramework.Menus;
using System;

namespace JaysModFramework
{
    public abstract class Module : Script, IComparable<Module>
    {
        private const string DeactivatedString = "Deactivated";
        private const string ActivatedString = "Activated";
        public abstract string ModuleName { get; }
        public abstract string ModuleDescription { get; }
        internal abstract SemanticVersion Version { get; }

        private string _moduleLogName { get => ModuleName + Version; }
        public bool IsActive { get; private set; }
        public abstract bool DefaultActivationState { get; }
        public Module()
        {
            ModuleManager.AddModule(this);
            if (DefaultActivationState)
            {
                Activate();
            }
        }
        #region Life cycle events
        public virtual void OnTick() { }
        public virtual void OnControlReleased(GTA.Control control) { }
        public virtual void OnControlHeld(GTA.Control control) { }
        public virtual void OnControlDoublePressed(GTA.Control control) { }
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
        #endregion Life cycle events
        #region Activation/Deactivation
        public void Activate()
        {
            if (!IsActive)
            {
                Debug.Log(DebugSeverity.Info, _moduleLogName + " " + ActivatedString.ToLower());
                IsActive = true;
                OnActivate();
            }
        }
        public void Deactivate()
        {
            if (IsActive)
            {
                Debug.Log(DebugSeverity.Info, _moduleLogName + " " + DeactivatedString.ToLower());
                IsActive = false;
                OnDeactivate();
            }
        }
        public void Toggle()
        {
            if (IsActive)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }
        #endregion Activation/Deactivation
        #region MenuItem
        private MenuListItem<string> _menuItem;
        public MenuListItem<string> MenuItem
        {
            get { return GenerateMenuItem(); }
        }
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
        #region IComparer
        private readonly System.Collections.Generic.List<string> _ignoredFirstWords = new System.Collections.Generic.List<string>() { "the", "a" };
        private string _moduleAlphabetizedName
        {
            get
            {
                string alphabetizedName = ModuleName;
                if (string.IsNullOrEmpty(alphabetizedName))
                {
                    return alphabetizedName;
                }
                while (_ignoredFirstWords.Contains(alphabetizedName.Split(' ')[0].ToLower()))
                {
                    string firstWord = alphabetizedName.Split(' ')[0];
                    string modifiedAlphabetizedName = alphabetizedName.Replace(firstWord, "");
                    alphabetizedName = modifiedAlphabetizedName.Replace(" ", "");
                }
                return alphabetizedName;
            }
        }
        public int CompareTo(Module other)
        {
            return string.Compare(_moduleAlphabetizedName, other._moduleAlphabetizedName);
        }
        #endregion IComparer
    }

    struct SemanticVersion
    {
        public int Major;
        public int Minor;
        public int Patch;

        public SemanticVersion(int major = 1, int minor = 0, int patch = 0)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public override string ToString()
        {
            return "v" + Major + "." + Minor + "." + Patch;
        }
    }
}
