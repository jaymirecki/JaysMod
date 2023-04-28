using LemonUI;
using LemonUI.Menus;
using System;

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
        public void Add(Menu menu)
        {
            _nativeMenu.Add(menu._nativeMenu);
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