using LemonUI.Menus;

namespace JMF.Menus
{
    public class Menu: NativeMenu
    {
        public Menu(string title): base(title)
        {
            AddToPool(Framework.ObjectPool);
        }
        public Menu(string bannerText, string name): base(bannerText, name)
        {
            AddToPool(Framework.ObjectPool);
        }
        public Menu(string bannerText, string name, string description) : base(bannerText, name, description)
        {
            AddToPool(Framework.ObjectPool);
        }
        public Menu(string bannerText, string name, string description, LemonUI.Elements.I2Dimensional banner) : base(bannerText, name, description, banner)
        {
            AddToPool(Framework.ObjectPool);
        }
        private void AddToPool(LemonUI.ObjectPool pool)
        {
            pool.Add(this);
        }
        public new MenuItem SelectedItem
        {
            get { return (MenuItem)base.SelectedItem; }
        }
    }
}