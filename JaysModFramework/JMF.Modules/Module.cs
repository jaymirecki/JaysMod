﻿using JMF.Menus;
using JMF.Modules;
using Rage;
using System;
using System.Collections.Generic;

namespace JMF
{
    public abstract class Module : IComparable<Module>
    {
        private const string DeactivatedString = "Deactivated";
        private const string ActivatedString = "Activated";
        public abstract string ModuleName { get; }
        public abstract string ModuleDescription { get; }
        internal abstract SemanticVersion Version { get; }

        private string _moduleLogName { get => ModuleName + ":" + Version; }
        public bool IsActive { get; private set; }
        protected List<Type> Dependencies = new List<Type>();
        protected List<IMenuItem> MenuItems = new List<IMenuItem>();
        public Module()
        {
            ModuleManager.AddModule(this);
        }
        #region Life cycle events
        public void Tick()
        {
            while (true)
            {
                OnTick();
                Rage.GameFiber.Yield();
            }
        }
        public virtual void OnTick() { }
        public virtual void OnControlReleased(Control control) { }
        public virtual void OnControlHeld(Control control) { }
        public virtual void OnControlDoublePressed(Control control) { }
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
        #endregion Life cycle events
        #region Activation/Deactivation
        public void Activate()
        {
            if (!IsActive && CheckDependencies())
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
        private bool CheckDependencies()
        {
            foreach (Type dependency in Dependencies)
            {
                bool isFound = false;
                foreach (Module module in ModuleManager.Modules)
                {
                    if (module.GetType() == dependency)
                    {
                        if (!module.IsActive)
                        {
                            Debug.Log(DebugSeverity.Warning, ModuleName + ": Dependency " + module.ModuleName + " is not active");
                            return false;
                        }
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    Debug.Log(DebugSeverity.Warning, ModuleName + ": Dependency " + dependency.Name + " is not installed");
                    return false;
                }
            }
            return true;
        }
        #endregion Activation/Deactivation
        #region MenuItem
        private Menu _menu;
        private MenuListItem<string> enabledItem;
        public Menu Menu
        {
            get { return GenerateMenu(); }
        }
        private Menu GenerateMenu()
        {
            if (_menu == null)
            {
                _menu = new Menu("Modules", ModuleName, ModuleDescription, Framework.ObjectPool);
                enabledItem = new MenuListItem<string>(ModuleName, ModuleDescription, DeactivatedString, ActivatedString);
                enabledItem.SelectedItem = IsActive ? ActivatedString : DeactivatedString;
                enabledItem.ItemChanged += ItemChanged;
                enabledItem.Selected += Selected;
                _menu.Add(enabledItem);
                AddMenuItems();
            }
            return _menu;
        }
        protected virtual void AddMenuItems() { }

        private void Selected(object sender, SelectedEventArgs e)
        {
            if (IsActive)
            {
                enabledItem.SelectedItem = ActivatedString;
            }
            else
            {
                enabledItem.SelectedItem = DeactivatedString;
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
            LemonUI.Menus.NativeListItem<string> listItem = sender as LemonUI.Menus.NativeListItem<string>;
            if (listItem!= null)
            {
                listItem.SelectedItem = IsActive ? ActivatedString : DeactivatedString;
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
    public abstract class Module<T> : Module where T : IModuleSettings
    {
        public virtual T Settings { get; private set; }
        public Module(): base()
        {
            if (Settings.Enabled)
            {
                Activate();
            }
        }
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
