using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace JaysMod
{
    class Launcher : Script
    {
        private MenuPool modMenuPool;
        private UIMenu mainMenu;
        private JaysMod mod;

        private int originalModel;
        private Vector3 originalPosition;
        private float originalHeading;
        private DateTime originalDateTime;

        public Launcher()
        {
            mod = null;

            modMenuPool = new MenuPool();
            mainMenu = new UIMenu("Jay's Mod", "SELECT AN OPTION");
            modMenuPool.Add(mainMenu);
            UIMenuItem save = new UIMenuItem("Save Game");
            UIMenuItem load = new UIMenuItem("Load Game");
            UIMenuItem quit = new UIMenuItem("Quit Game");
            mainMenu.AddItem(save);
            mainMenu.AddItem(load);
            mainMenu.AddItem(quit);
            mainMenu.RefreshIndex();

            mainMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == save)
                    SaveGame();
                else if (item == load)
                {
                    if (mod == null)
                    {
                        originalModel = Game.Player.Character.Model.Hash;
                        originalPosition = Game.Player.Character.Position;
                        originalHeading = Game.Player.Character.Heading;
                        originalDateTime = World.CurrentDate;
                    }
                    LoadGame();
                }
                else if (item == quit)
                    QuitGame();
            };

            Debug();

            KeyDown += onKeyDown;
            Tick += onTick;
        }
        void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && !modMenuPool.IsAnyMenuOpen())
                mainMenu.Visible = !mainMenu.Visible;
            else if (e.KeyCode == Keys.F5)
                modMenuPool.CloseAllMenus();
        }
        void onTick(object sender, EventArgs e)
        {
            modMenuPool.ProcessMenus();
        }
        public void SaveGame()
        {
            if (mod != null)
            {
                mod.Save();
            }
        }
        public void LoadGame()
        {
            if (mod != null)
                QuitGame();
            mod = InstantiateScript<JaysMod>();
            mod.Load();
            originalDateTime = World.CurrentDate;
        }
        public void QuitGame()
        {
            if (mod != null)
            {
                mod.Unload();
                mod.Abort();
                mod = null;
                JaysMod.LoadModel(originalModel);
                Game.Player.Character.Position = originalPosition;
                Game.Player.Character.Heading = originalHeading;
                World.CurrentDate = originalDateTime;
            }
        }
        void Debug()
        {
            UIMenu submenu = modMenuPool.AddSubMenu(mainMenu, "Debug");
            UIMenuItem currPos = new UIMenuItem("Current Position");
            submenu.AddItem(currPos);
            UIMenuItem vehPos = new UIMenuItem("Vehicle Position");
            submenu.AddItem(vehPos);
            UIMenuItem vehName = new UIMenuItem("Vehicle Model Name");
            submenu.AddItem(vehName);
            submenu.RefreshIndex();

            submenu.OnItemSelect += (sender, item, index) =>
            {
                Ped player = Game.Player.Character;
                if (item == currPos)
                    JaysMod.Debug("Current Position: " + player.Position.ToString() + " H:" + player.Heading.ToString());
                else if (item == vehPos)
                    JaysMod.Debug("Vehicle Position: " + player.CurrentVehicle.Position.ToString() + " H:" + player.CurrentVehicle.Heading.ToString());
                else if (item == vehName)
                    JaysMod.Debug("Vehicle Name: " + player.CurrentVehicle.Model.ToString());
            };
        }
    }
}
