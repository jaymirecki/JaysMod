using LemonUI.Menus;
using System;

namespace JMF.Menus
{
    public class MenuListItem<T> : NativeListItem<T>, IMenuItem
    {
        public MenuListItem(string title, params T[] objs) : base(title, objs)
        {
        }

        public MenuListItem(string title, string subtitle, params T[] objs) : base(title, subtitle, objs)
        {
        }
    }
}
