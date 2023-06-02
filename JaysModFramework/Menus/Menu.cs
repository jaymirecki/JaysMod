using LemonUI.Menus;
using System;
using System.ComponentModel;

namespace JaysModFramework.Menus
{
    public class Menu
    {
        internal NativeMenu _nativeMenu;

        public Menu(string title, ObjectPool pool)
        {
            _nativeMenu = new NativeMenu(title);
            pool.Add(_nativeMenu);
        }
        public Menu(string title, string subtitle, ObjectPool pool)
        {
            _nativeMenu = new NativeMenu(title, subtitle);
            pool.Add(_nativeMenu);
        }
        public Menu(string title, string subtitle, string description, ObjectPool pool)
        {
            _nativeMenu = new NativeMenu(title, subtitle, description);
            pool.Add(_nativeMenu);
        }
        public void Add(MenuItem menuItem)
        {
            _nativeMenu.Add(menuItem._nativeItem);
        }
        public void Add<T>(MenuListItem<T> menuItem)
        {
            _nativeMenu.Add(menuItem._nativeItem);
        }
        public SubmenuItem Add(Menu menu)
        {
            return new SubmenuItem(_nativeMenu.AddSubMenu(menu._nativeMenu));
        }
        public void Open()
        {
            Visible = true;
        }
        public void Clear()
        {
            _nativeMenu.Clear();
        }
        public event CancelEventHandler Opening
        {
            add { _nativeMenu.Opening += value; }
            remove { _nativeMenu.Opening -= value; }
        }
        public bool Visible
        {
            get { return _nativeMenu.Visible; }
            set { _nativeMenu.Visible = value; }
        }
        public event EventHandler Shown
        {
            add { _nativeMenu.Shown += value; }
            remove { _nativeMenu.Shown -= value; }
        }
    }
}