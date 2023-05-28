using LemonUI.Menus;
using System;

namespace JaysModFramework.Menus
{
    public class MenuListItem<T>: IMenuItem
    {
        internal NativeListItem<T> _nativeItem;
        public string Title
        {
            get { return _nativeItem.Title; }
            set { _nativeItem.Title = value; }
        }
        public MenuListItem(string title, params T[] objs)
        {
            _nativeItem = new NativeListItem<T>(title, objs);
        }
        public MenuListItem(string title, string subtitle, params T[] objs)
        {
            _nativeItem = new NativeListItem<T>(title, subtitle, objs);
        }
        public event SelectedEventHandler Selected
        {
            add
            {
                _nativeItem.Selected += (
                    object sender,
                    LemonUI.Menus.SelectedEventArgs e) => value(sender, new SelectedEventArgs(e.Index, e.OnScreen));
            }
            remove
            {
                _nativeItem.Selected -= (
                    object sender,
                    LemonUI.Menus.SelectedEventArgs e) => value(sender, new SelectedEventArgs(e.Index, e.OnScreen));
            }
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
        public event ItemChangedEventHandler<T> ItemChanged
        {
            add
            {
                _nativeItem.ItemChanged += (
                    object sender,
                    LemonUI.Menus.ItemChangedEventArgs<T> e) => value(sender, new ItemChangedEventArgs<T>(e.Object, e.Index, e.Direction));
            }
            remove
            {
                _nativeItem.ItemChanged -= (
                    object sender,
                    LemonUI.Menus.ItemChangedEventArgs<T> e) => value(sender, new ItemChangedEventArgs<T>(e.Object, e.Index, e.Direction));
            }
        }
        public T SelectedItem
        {
            get { return _nativeItem.SelectedItem; }
            set { _nativeItem.SelectedItem = value; }
        }
        public int SelectedIndex
        {
            get { return _nativeItem.SelectedIndex; }
            set { _nativeItem.SelectedIndex = value; }
        }
    }
}
