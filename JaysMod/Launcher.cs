using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;
using System.Collections.Generic;
using JaysMod.Framework;

namespace JaysMod
{
    class Launcher : Script
    {
        private MenuPool modMenuPool;
        private UIMenu MainMenu;
        private JaysMod mod;

        private int originalModel;
        private Vector3 originalPosition;
        private float originalHeading;
        private DateTime originalDateTime;

        public Launcher()
        {
            mod = null;

            modMenuPool = new MenuPool();
            MainMenu = new UIMenu("Jay's Mod", "SELECT AN OPTION");
            modMenuPool.Add(MainMenu);

            KeyDown += onKeyDown;
            Tick += onTick;
        }
        private void BuildMenu()
        {
            UIMenuItem save = new UIMenuItem("Save Game");
            UIMenuItem newGame = new UIMenuItem("New Game");
            UIMenuItem quit = new UIMenuItem("Quit Game");

            MainMenu.Clear();

            NewMenu(mod != null);
            LoadMenu(mod != null);
            MainMenu.AddItem(save);
            MainMenu.AddItem(quit);
            MainMenu.RefreshIndex();

            MainMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == save)
                {
                    SaveGame();
                }
                else if (item == quit)
                {
                    QuitGame();
                }
            };

            Debug();
        }
        private void LoadMenu(bool modEnabled)
        {
            SaveAndLoad saverLoader = new SaveAndLoad("JaysMod.ini");
            List<object> games = new List<object>(saverLoader.AllSaves());

            string description = "There are no saved games to load.";
            if (games.Count > 0)
            {
                description = "Choose a game to load.";
            }

            UIMenu loadMenu = modMenuPool.AddSubMenu(MainMenu, "Load Game", description);

            if (games.Count > 0)
            {
                UIMenuListItem gameSelect = new UIMenuListItem("Games:", games, 0);
                UIMenuItem loadButton = new UIMenuItem("Load", "Load the selected game");

                loadMenu.AddItem(gameSelect);
                loadMenu.AddItem(loadButton);

                loadMenu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == loadButton)
                    {
                        LoadGame((string)games[gameSelect.Index]);
                    }
                };
            }
        }
        private void NewMenu(bool modEnabled)
        {
            SaveAndLoad saverLoader = new SaveAndLoad("JaysMod.ini");
            List<object> games = new List<object>(saverLoader.AllSaves());
            UIMenuItem newButton = new UIMenuItem("New Game", "Start a new game");
            MainMenu.AddItem(newButton);

            MainMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newButton)
                {
                    string saveId = "SAVE 01";
                    int counter = 1;
                    while (games.Contains(saveId))
                    {
                        counter++;
                        saveId = "SAVE " + counter.ToString("D2");
                    }
                    NewGame(saveId);
                }
            };
        }
        void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && !modMenuPool.IsAnyMenuOpen())
            {
                BuildMenu();
                MainMenu.Visible = !MainMenu.Visible;
            }
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
        public void LoadGame(string saveId)
        {
            if (mod != null)
            {
                QuitGame();
            }
            SaveOriginalState();
            mod = InstantiateScript<JaysMod>();
            mod.Load(saveId);
        }
        public void NewGame(string saveId)
        {
            if (mod != null)
            {
                QuitGame();
            }
            SaveOriginalState();
            mod = InstantiateScript<JaysMod>();
            mod.New(saveId);
        }
        private void SaveOriginalState()
        {
            Ped playerPed = Game.Player.Character;
            originalModel = playerPed.Model;
            originalPosition = playerPed.Position;
            originalHeading = playerPed.Heading;
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
            UIMenu submenu = modMenuPool.AddSubMenu(MainMenu, "Debug");
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
