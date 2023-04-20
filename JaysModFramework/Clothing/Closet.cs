//using GTA;
//using NativeUI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JaysModFramework
//{
//    public static class Closet
//    {
//        private const string Title = "Closet";
//        private const string Description = "Manage your outfit";
//        private static Outfit DefaultOutfit;
//        private static UIMenu ClosetMenu;
//        private static UIMenu OutfitMenu;
//        public static UIMenu Menu(NPC player, MenuPool menuPool)
//        {
//            ClosetMenu = new UIMenu(Title, Description);
//            AddMenuItems(ClosetMenu, player, menuPool);

//            return ClosetMenu;
//        }
//        public static void SubMenu(NPC player, MenuPool menuPool, UIMenu menu)
//        {
//            ClosetMenu = menuPool.AddSubMenu(menu, Title, Description);
//            AddMenuItems(ClosetMenu, player, menuPool);
//        }
//        private static void AddMenuItems(UIMenu menu, NPC player, MenuPool menuPool)
//        {
//            AddOutfitMenu(menu, player, menuPool);

//            UIMenuItem accept = new UIMenuItem("Accept", "Save the current outfit");
//            UIMenuItem cancel = new UIMenuItem("Cancel", "Cancel outfit changes");
//            menu.AddItem(accept);
//            menu.AddItem(cancel);

//            menu.OnItemSelect += (UIMenu sender, UIMenuItem selectedItem, int index) =>
//            {
//                if (selectedItem == accept)
//                {
//                    AcceptOutfit(player);
//                }
//                else if (selectedItem == cancel)
//                {
//                    CancelOutfit(player);
//                }
//            };

//            menu.OnMenuOpen += (UIMenu sender) =>
//            {
//                if (DefaultOutfit == null)
//                {
//                    AcceptOutfit(player);
//                }
//            };

//            menu.OnMenuClose += (UIMenu sender) =>
//            {
//                OnClose(player);
//            };
//        }
//        private static void OnClose(NPC player)
//        {
//            if (!AreAnyMenusOpen())
//            {
//                CancelOutfit(player);
//            }
//        }
//        private static bool AreAnyMenusOpen()
//        {
//            return (ClosetMenu != null && ClosetMenu.Visible) ||
//                (OutfitMenu != null && OutfitMenu.Visible);
//        }
//        private static void AcceptOutfit(NPC player)
//        {
//            DefaultOutfit = player.Outfit.Copy();
//        }
//        private static void CancelOutfit(NPC player)
//        {
//            player.Outfit = DefaultOutfit.Copy();
//        }
//        private static void AddOutfitMenu(UIMenu superMenu, NPC player, MenuPool menuPool)
//        {
//            Dictionary<string,Outfit> outfitMap = new Dictionary<string, Outfit>();
//            outfitMap.Add("Casual", MaleOutfitTemplates.Casual);
//            outfitMap.Add("Formal", MaleOutfitTemplates.Formal);
//            outfitMap.Add("Combat", MaleOutfitTemplates.Combat);
//            outfitMap.Add("Scuba", MaleOutfitTemplates.Scuba);
//            outfitMap.Add("Beach", MaleOutfitTemplates.Beach);
//            outfitMap.Add("Bike", MaleOutfitTemplates.Bike);
//            outfitMap.Add("Navy Casual", MaleOutfitTemplates.NavyCasual);
//            outfitMap.Add("Navy Combat", MaleOutfitTemplates.NavyCombat);
//            outfitMap.Add("Test Pilot", MaleOutfitTemplates.TestPilot);

//            OutfitMenu = menuPool.AddSubMenu(superMenu, "Outfits", "Choose an outfit");

//            foreach(string name in outfitMap.Keys)
//            {
//                OutfitMenu.AddItem(new UIMenuItem(name));
//            }

//            OutfitMenu.OnItemSelect += (UIMenu sender, UIMenuItem selectedItem, int index) =>
//            {
//                player.Outfit = outfitMap.Values.ToArray()[index].Copy();
//            };

//            OutfitMenu.OnMenuClose += (UIMenu sender) =>
//            {
//                OnClose(player);
//            };
//        }
//    }
//}
