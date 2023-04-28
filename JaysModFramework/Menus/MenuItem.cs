using LemonUI.Menus;
using System;

namespace JaysModFramework.Menus
{
    public class MenuItem
    {
        internal NativeItem _nativeItem;
        public MenuItem(string title)
        {
            _nativeItem = new NativeItem(title);
        }
        public MenuItem(string title, string description)
        {
            _nativeItem = new NativeItem(title, description);
        }
        public MenuItem(string title, string description, string subtitle)
        {
            _nativeItem = new NativeItem(title, description, subtitle);
        }
        public event SelectedEventHandler Selected
        {
            add { _nativeItem.Selected += value; }
            remove { _nativeItem.Selected -= value; }
        }
        public event EventHandler Activated
        {
            add { _nativeItem.Activated += value; }
            remove { _nativeItem.Activated -= value; }
        }
    }
}
