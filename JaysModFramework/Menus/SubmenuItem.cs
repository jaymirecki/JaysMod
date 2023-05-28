using LemonUI.Menus;
using System;

namespace JaysModFramework.Menus
{
    public class SubmenuItem: IMenuItem
    {
        internal NativeSubmenuItem _nativeItem;
        public SubmenuItem(NativeSubmenuItem nativeItem)
        {
            _nativeItem = nativeItem;
        }
        public event SelectedEventHandler Selected
        {
            add { _nativeItem.Selected += (object sender, LemonUI.Menus.SelectedEventArgs e) => value(sender, (SelectedEventArgs)e); }
            remove { _nativeItem.Selected -= (object sender, LemonUI.Menus.SelectedEventArgs e) => value(sender, (SelectedEventArgs)e); }
        }
        public event EventHandler Activated
        {
            add { _nativeItem.Activated += value; }
            remove { _nativeItem.Activated -= value; }
        }
        public bool Enabled
        {
            get { return _nativeItem.Enabled; }
            set { _nativeItem.Enabled = value; }
        }
    }
}
