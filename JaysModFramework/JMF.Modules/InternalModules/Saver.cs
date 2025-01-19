﻿using JMF.Menus;
using JMF.Native;
using JMF.UI;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace JMF.Modules
{
    public class Saver : InternalModule<ModuleSettings>
    {
        private const string BaseSaveDirectory = "./JMF/Saves";
        private const string BaseTemplateDirectory = "./JMF/Templates";
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Saver";
        public override string ModuleDescription { get; } = "Enables use of larger radar from MP";
        public override ModuleSettings Settings { get { return Framework.Config.BigMapSettings; } }
        private Menu GameMenu;
        private Menu NewMenu;
        private Menu LoadMenu;
        private Menu SaveMenu;
        private MenuItem NewSave;
        public Saver() : base() {
            CreateMenu();
            InteractionMenu.AddMenu(GameMenu);
        }
        private void CreateMenu()
        {
            GameMenu = new Menu("Game", "Game", "Save or load games");
            NewMenu = new Menu("New Game", "New Game", "Start a new game");
            LoadMenu = new Menu("Load Game", "Load Game", "Load an existing save game");
            SaveMenu = new Menu("Save Game", "Save Game", "Save the current game");

            GameMenu.Add(NewMenu);
            AddGames(BaseTemplateDirectory, NewMenu, NewGameHandler);
            GameMenu.Add(LoadMenu);
            AddGames(BaseSaveDirectory, LoadMenu, LoadGameHandler);
            GameMenu.Add(SaveMenu);
            AddGames(BaseSaveDirectory, SaveMenu, SaveGameHandler);
            NewSave = new MenuItem("Create a new save");
            NewSave.Activated += SaveGameHandler;
            SaveMenu.Add(NewSave);
        }
        private void AddGames(string directory, Menu menu, EventHandler handler)
        {
            Directory.CreateDirectory(directory);
            menu.Clear();
            foreach (string d in Directory.GetDirectories(directory))
            {
                MenuItem item = new MenuItem(Path.GetFileName(d));
                item.Activated += handler;
                menu.Add(item);
            }
        }
        private void NewGameHandler(object sender, EventArgs e)
        {
            MenuItem item = NewMenu.SelectedItem;
            string templateId = item.Title;
            if (!LoadGame(Path.Combine(BaseTemplateDirectory, templateId)))
            {
                Debug.Log(DebugSeverity.Error, "Failed to load template " + templateId);
            }
        }
        private void LoadGameHandler(object sender, EventArgs e)
        {
            MenuItem item = LoadMenu.SelectedItem;
            string saveId = item.Title;
            if (!LoadGame(Path.Combine(BaseSaveDirectory, saveId)))
            {
                Debug.Log(DebugSeverity.Error, "Failed to load game " + saveId);
            }
        }
        private void SaveGameHandler(object sender, EventArgs e)
        {
            MenuItem item = SaveMenu.SelectedItem;
            string saveId;
            Debug.Log(DebugSeverity.Error, item.Title);
            if (item == NewSave)
            {
                KeyboardResult result = Screen.GetKeyboardInput();
                if (result.Status != KeyboardResultStatus.Accept)
                {
                    return;
                }
                saveId = result.Input;
            }
            else
            {
                saveId = item.Title;
            }
            Debug.Log(DebugSeverity.Error, saveId);

            if (!SaveGame(Path.Combine(BaseSaveDirectory, saveId)))
            {
                Debug.Log(DebugSeverity.Error, "Failed to save game " + saveId);
            }
        }
        private bool LoadGame(string path)
        {
            return false;
        }
        private bool SaveGame(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                Serialize(path, "World", new State());
            }
            catch (Exception ex)
            {
                Debug.Log(DebugSeverity.Error, ex);
                return false;
            }
            return true;
        }
        private void Serialize<T>(string path, string filename, T value)
        {
            FileStream stream = File.Create(Path.Combine(path, filename + ".xml"));
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, value);
        }
    }
}
