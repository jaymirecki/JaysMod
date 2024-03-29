﻿using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using NativeUI;
using System.Collections.Generic;
using JaysModFramework;
using System.Collections;

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

            KeyDown += OnKeyDown;
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

            DebugMenu();
        }
        private void LoadMenu(bool modEnabled)
        {
            List<object> games = new State().FindSaves();

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
            List<object> games = new State().FindSaves();
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
        void OnKeyDown(object sender, KeyEventArgs e)
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
            originalPosition = new Vector3(playerPed.Position);
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
                Game.Player.Character.Position = originalPosition.BaseVector;
                Game.Player.Character.Heading = originalHeading;
                World.CurrentDate = originalDateTime;
            }
        }
        void DebugMenu()
        {
            UIMenu submenu = modMenuPool.AddSubMenu(MainMenu, "Debug");
            UIMenuItem currPos = new UIMenuItem("Current Position");
            submenu.AddItem(currPos);
            UIMenuItem vehPos = new UIMenuItem("Vehicle Position");
            submenu.AddItem(vehPos);
            UIMenuItem vehName = new UIMenuItem("Vehicle Model Name");
            submenu.AddItem(vehName);
            UIMenuItem zoneId = new UIMenuItem("Zone ID");
            submenu.AddItem(zoneId);
            submenu.RefreshIndex();

            submenu.OnItemSelect += (sender, item, index) =>
            {
                Ped player = Game.Player.Character;
                if (item == currPos)
                    Debug.Log("Current Position: " + player.Position.ToString() + " H:" + player.Heading.ToString(), true);
                else if (item == vehPos)
                    Debug.Log("Vehicle Position: " + player.CurrentVehicle.Position.ToString() + " H:" + player.CurrentVehicle.Heading.ToString(), true);
                else if (item == vehName)
                    Debug.Log("Vehicle Name: " + player.CurrentVehicle.Model.ToString(), true);
                else if (item == zoneId)
                {
                    Vector3 position = new Vector3(Game.Player.Character.Position);
                    int zone = Function.Call<int>(Hash.GET_ZONE_AT_COORDS, position.X, position.Y, position.Z);
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_armybase");
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "restrictedareas");
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_armybase");
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_lossantosintl");
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prison");
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prisonvanbreak");
                    Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_doors");
                    Function.Call(Hash._REMOVE_AIR_DEFENSE_ZONE, zone);
                }
            };
        }
    }
}
