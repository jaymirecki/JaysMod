using LemonUI.Menus;
using System;

namespace JMF.Menus
{
    public interface IMenuItem
    {
        event EventHandler Activated;
        bool Enabled { get; set; }
    }
    public class MenuItem : NativeItem, IMenuItem
    {
        public MenuItem(string title) : base(title)
        {
        }

        public MenuItem(string title, string description) : base(title, description)
        {
        }

        public MenuItem(string title, string description, string altTitle) : base(title, description, altTitle)
        {
        }
    }
}
