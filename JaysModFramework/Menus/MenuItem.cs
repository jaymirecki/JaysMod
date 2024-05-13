using LemonUI.Menus;
using System;

namespace JMF.Menus
{
    public interface IMenuItem
    {
        event SelectedEventHandler Selected;
        event EventHandler Activated;
        bool Enabled { get; set; }
    }
    public class MenuItem: IMenuItem
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
            add { _nativeItem.Selected += (object sender, LemonUI.Menus.SelectedEventArgs e) => value(sender, (SelectedEventArgs)e); }
            remove { _nativeItem.Selected -= (object sender, LemonUI.Menus.SelectedEventArgs e) => value(sender, (SelectedEventArgs)e); }
        }

        private void _nativeItem_Selected(object sender, SelectedEventArgs e)
        {
            throw new NotImplementedException();
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
