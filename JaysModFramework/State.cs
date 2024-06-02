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
                if (_worldspace != "" && _map != "")
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
                if (_worldspace != "" && _map != "")
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
            File.Create(stateFile);
            TextWriter writer = new StreamWriter(stateFile);
            XmlSerializer stateSerializer = new XmlSerializer(typeof(State));
            stateSerializer.Serialize(writer, this);
            writer.Close();
        }
        #endregion
        #region Load
        public bool Load(string saveId)
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
                    _loadMenu = new Menu("Load Game", "Load Save Game", Framework.ObjectPool);
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
            if (!Load(saveId))
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
                    _saveMenu = new Menu("Save Game", "Load Save Game", Framework.ObjectPool);
                    _saveMenu.Opening += AddSaveGames;
                }
                return _saveMenu;
            }
        }
        private void AddSaveGames(object sender, CancelEventArgs e)
        {
            Directory.CreateDirectory(BaseSaveDirectory);
            _saveMenu.Clear();
            _saveMenu.Add(new MenuItem("Create New Save"));
            foreach (string directory in Directory.GetDirectories(BaseSaveDirectory))
            {
                MenuItem item = new MenuItem(Path.GetFileName(directory));
                item.Activated += SaveGameHandler; ;
                _saveMenu.Add(item);
            }
        }

        private void SaveGameHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion Menus
    }
}
