using LemonUI.Menus;

namespace JaysModFramework.Menus
{
    public class ObjectPool : LemonUI.ObjectPool { }
    public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);
    public class SelectedEventArgs : LemonUI.Menus.SelectedEventArgs
    {
        public SelectedEventArgs(int index, int screen) : base(index, screen) { }
    }
    public delegate void ItemChangedEventHandler<T>(object sender, ItemChangedEventArgs<T> e);
    public class ItemChangedEventArgs<T>
    {
        //
        // Summary:
        //     The new object.
        public T Object { get; set; }

        //
        // Summary:
        //     The index of the object.
        public int Index { get; }

        //
        // Summary:
        //     The direction of the Item Changed event.
        public Direction Direction { get; }

        internal ItemChangedEventArgs(T obj, int index, Direction direction)
        {
            Object = obj;
            Index = index;
            Direction = direction;
        }
    }
}