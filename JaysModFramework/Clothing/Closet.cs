using GTA;
using JaysModFramework.Menus;
using LemonUI;
using LemonUI.Menus;

namespace JaysModFramework.Clothing
{
    //using Menu = JaysModFramework.Menu.Menu;
    public class Closet
    {
        private const string Title = "Closet";
        private const string Description = "Manage your outfit";
        private Menu ClosetMenu;
        private ObjectPool _pool;
        private Ped _ped;
        public Menu Menu(Ped ped, ObjectPool pool)
        {
            _pool = pool;
            _ped = ped;
            ClosetMenu = new Menu(Title, Description, _pool);
            AddMenuItems();

            return ClosetMenu;
        }
        private void AddMenuItems()
        {
            ClosetMenu.Add(new MenuItem("Empty Item"));
            ClosetMenu.Add(OutfitMenu());
        }

        private Menu OutfitMenu()
        {
            Menu outfitMenu = new Menu("Outfits", "subtitle", "description", _pool);
            foreach (Outfit outfit in Global.Database.Outfits)
            {
                MenuItem outfitItem = new MenuItem(outfit.Name);
                outfitItem.Selected += (sender, args) => outfit.SetToPed(_ped);
                outfitMenu.Add(outfitItem);
            }
            return outfitMenu;
        }
    }
}
