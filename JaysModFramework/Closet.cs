using GTA;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public static class Closet
    {
        private const string Title = "Closet";
        private const string Description = "Choose an outfit";
        private static Outfit DefaultOutfit;
        public static UIMenu Menu(NPC player)
        {
            UIMenu closetMenu = new UIMenu(Title, Description);
            AddMenuItems(closetMenu, player);

            return closetMenu;
        }
        public static void SubMenu(NPC player, MenuPool menuPool, UIMenu menu)
        {
            UIMenu closetMenu = menuPool.AddSubMenu(menu, Title, Description);
            AddMenuItems(closetMenu, player);
        }
        private static void AddMenuItems(UIMenu menu, NPC player)
        {
            List<Outfit> outfits = new List<Outfit>() { 
                MaleOutfitTemplates.Casual, 
                MaleOutfitTemplates.Formal,
                MaleOutfitTemplates.Combat,
                MaleOutfitTemplates.Scuba,
                MaleOutfitTemplates.Beach,
                MaleOutfitTemplates.Bike,
                MaleOutfitTemplates.NavyCasual,
                MaleOutfitTemplates.NavyCombat,
                MaleOutfitTemplates.TestPilot,
            };
            List<object> outfitNames = new List<object>() { 
                "Casual", "Formal", "Combat", "Scuba", "Beach", "Bike", "Navy Casual", "Navy Combat", "Test Pilot",
            };
            UIMenuListItem outfitList = new UIMenuListItem("Outfits", outfitNames, 0);
            player.Outfit = outfits[0].Copy();
            outfitList.OnListChanged += (UIMenuListItem sender, int newIndex) =>
            {
                player.Outfit = outfits[newIndex].Copy();
            };

            UIMenuItem accept = new UIMenuItem("Accept", "Save outfit changes");
            UIMenuItem cancel = new UIMenuItem("Cancel", "Discard outfit changes");

            menu.AddItem(outfitList);
            menu.AddItem(accept);
            menu.AddItem(cancel);

            menu.OnItemSelect += (UIMenu sender, UIMenuItem selectedItem, int index) =>
            {
                if (selectedItem == accept)
                {
                    DefaultOutfit = player.Outfit;
                }
                else if (selectedItem == cancel && DefaultOutfit != null)
                {
                    player.Outfit = DefaultOutfit;
                }
            };

            menu.OnMenuOpen += (UIMenu sender) =>
            {
                DefaultOutfit = player.Outfit;
            };
        }
    }
}
