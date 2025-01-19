using LemonUI.Menus;

namespace JMF.Menus
{
    public class SubmenuItem : NativeSubmenuItem, IMenuItem
    {
        public SubmenuItem(NativeMenu menu, NativeMenu parent) : base(menu, parent)
        {
        }

        public SubmenuItem(NativeMenu menu, NativeMenu parent, string endLabel) : base(menu, parent, endLabel)
        {
        }
    }
}
