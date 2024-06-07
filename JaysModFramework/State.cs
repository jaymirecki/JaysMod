using JMF.Menus;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace JMF
{
    public class State
    {
        private const string BaseSaveDirectory = "./JMF/Saves";
        private const string BaseTempalteDirectory = "./JMF/Templates";
        #region Properties
        public DateTime Date
        {
            get { return Game.Clock.Date; }
            set { Game.Clock.Date = value; }
        }
        public WeatherType Weather
        {
            get { return World.Weather; }
            set { World.Weather = value; }
        }
        private string _worldspace = "";
        public string Worldspace
        {
            get
            {
                if (Universe.Truth.CurrentWorldspace != null)
                {

                    _worldspace = Universe.Truth.CurrentWorldspace.ID;
                }
                return _worldspace;
            }
            set
            {
                _worldspace = value;
                if (_worldspace != "")
                {
                    Universe.Truth.ChangeWorldspace(_worldspace, _map);
                }
            }
        }
        private string _map = "";
        public string Map
        {
            get
            {
                if (Universe.Truth.CurrentWorldspace != null)
                {
                    _map = Universe.Truth.CurrentWorldspace.CurrentMap;
                }
                return _map;
            }
            set
            {
                _map = value;
                if (_worldspace != "")
                {
                    Universe.Truth.ChangeWorldspace(_worldspace, _map);
                }
            }
        }
        #endregion
        #region Constructors
        public State()
        {
        }
        #endregion
        #region Save
        public void Save(string saveId)
        {
            string saveDirectory = Path.GetFullPath(Path.Combine(BaseSaveDirectory, saveId));
            Directory.CreateDirectory(saveDirectory);
            string stateFile = Path.Combine(saveDirectory, "state.xml");
            Debug.Log(DebugSeverity.Error, stateFile);
            FileStream stream = File.Create(stateFile);
            stream.Close();
            TextWriter writer = new StreamWriter(stateFile);
            XmlSerializer stateSerializer = new XmlSerializer(typeof(State));
            stateSerializer.Serialize(writer, this);
            writer.Close();
        }
        #endregion
        #region Load
        public bool LoadSave(string saveId)
        {
            string saveDirectory = Path.GetFullPath(Path.Combine(BaseSaveDirectory, saveId));
            Directory.CreateDirectory(saveDirectory);
            string stateFile = Path.Combine(saveDirectory, "state.xml");
            if (!File.Exists(stateFile))
            {
                return false;
            }

            Load(stateFile, saveDirectory);

            return true;
        }
        public bool LoadTemplate(string templateId)
        {
            string saveDirectory = Path.GetFullPath(Path.Combine(BaseTempalteDirectory, templateId));
            Directory.CreateDirectory(saveDirectory);
            string stateFile = Path.Combine(saveDirectory, "state.xml");
            if (!File.Exists(stateFile))
            {
                return false;
            }

            Load(stateFile, saveDirectory);

            return true;
        }
        private void Load(string stateFile, string saveDirectory)
        {
            TextReader reader = new StreamReader(stateFile);
            XmlSerializer stateSerializer = new XmlSerializer(typeof(State));
            stateSerializer.Deserialize(reader);
            reader.Close();
        }
        #endregion Load
        ///////////////////////////////////////////////////////////////////////
        //                                Menus                              //
        ///////////////////////////////////////////////////////////////////////
        #region Menus
        private Menu _loadMenu = null;
        public Menu LoadMenu
        {
            get
            {
                if (_loadMenu == null)
                {
                    _loadMenu = new Menu("Load Game", "Load Game", Framework.ObjectPool);
                    _loadMenu.Opening += AddLoadGames;
                }
                return _loadMenu;
            }
        }
        private void AddLoadGames(object sender, CancelEventArgs e)
        {
            Directory.CreateDirectory(BaseSaveDirectory);
            _loadMenu.Clear();
            foreach (string directory in Directory.GetDirectories(BaseSaveDirectory))
            {
                MenuItem item = new MenuItem(Path.GetFileName(directory));
                item.Activated += LoadGameHandler;
                _loadMenu.Add(item);
            }
        }

        private void LoadGameHandler(object sender, EventArgs e)
        {
            MenuItem item = _loadMenu.SelectedItem;
            string saveId = item.Title;
            if (!LoadSave(saveId))
            {
                Debug.Log(DebugSeverity.Error, "Failed to load saved game " + saveId);
            }
        }
        private Menu _saveMenu = null;
        public Menu SaveMenu
        {
            get
            {
                if (_saveMenu == null)
                {
                    _saveMenu = new Menu("Save Game", "Save Game", Framework.ObjectPool);
                    _saveMenu.Opening += AddSaveGames;
                }
                return _saveMenu;
            }
        }
        private void AddSaveGames(object sender, CancelEventArgs e)
        {
            Directory.CreateDirectory(BaseSaveDirectory);
            _saveMenu.Clear();
            MenuItem newSave = new MenuItem("Create New Save");
            newSave.Activated += SaveGameHandler;
            _saveMenu.Add(newSave);
            foreach (string directory in Directory.GetDirectories(BaseSaveDirectory))
            {
                MenuItem item = new MenuItem(Path.GetFileName(directory));
                item.Activated += SaveGameHandler;
                _saveMenu.Add(item);
            }
        }

        private void SaveGameHandler(object sender, EventArgs e)
        {
            MenuItem item = _saveMenu.SelectedItem;
            Debug.Log(DebugSeverity.Error, item.Title);
            if (item.Title == "Create New Save")
            {
                Save("new save");
            }
            else
            {
                Save(item.Title);
            }
        }
        private Menu _newMenu = null;
        public Menu NewMenu
        {
            get
            {
                if (_newMenu == null)
                {
                    _newMenu = new Menu("New Game", "New Game", Framework.ObjectPool);
                    _newMenu.Opening += AddTemplates; ;
                }
                return _newMenu;
            }
        }

        private void AddTemplates(object sender, CancelEventArgs e)
        {
            Directory.CreateDirectory(BaseTempalteDirectory);
            _newMenu.Clear();
            foreach (string directory in Directory.GetDirectories(BaseTempalteDirectory))
            {
                MenuItem item = new MenuItem(Path.GetFileName(directory));
                item.Activated += NewGameHandler;
                _newMenu.Add(item);
            }
        }
        private void NewGameHandler(object sender, EventArgs e)
        {
            MenuItem item = _newMenu.SelectedItem;
            Debug.Log(DebugSeverity.Error, item.Title);
            LoadTemplate(item.Title);
        }
        #endregion Menus
    }
}
