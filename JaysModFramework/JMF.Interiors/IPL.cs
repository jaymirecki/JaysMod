using JMF.Math;
using JMF.Menus;
using JMF.Native;
using OOD.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JMF
{
    namespace Interiors
    {
        public class IPL: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public int InteriorID { get; set; } = 0;
            private Vector3 _position = new Vector3();
            public Vector3 Position
            {
                get { return _position; }
                set
                {
                    _position = value;
                    InteriorID = Function.Call<int>(Hash.GetInteriorAtCoords, _position.X, _position.Y, _position.Z);
                }
            }
            public List<string> IPLNames { get; set; }
            public List<IPLEntitySet> EntitySets { get; set; } = new List<IPLEntitySet>();
            public List<IPLTheme> Themes { get; set; } = new List<IPLTheme>();
            public List<string> DefaultEntitySets { get; set; } = new List<string>();
            [XmlIgnore]
            public bool Loaded { get; private set; } = false;
            [XmlIgnore]
            private MenuListItem<string> _menuItem = null;
            public MenuListItem<string> MenuItem
            {
                get
                {
                    if (_menuItem == null)
                    {
                        _menuItem = new MenuListItem<string>(ID, "", "Load", "Load and Teleport", "Unload");
                        _menuItem.Activated += MenuItemSelected;
                    }
                    return _menuItem;
                }
            }
            private void MenuItemSelected(object sender, System.EventArgs e)
            {
                switch (_menuItem.SelectedItem)
                {
                    case "Load":
                        LoadDefault();
                        return;
                    case "Load and Teleport":
                        LoadDefault();
                        Game.Player.Character.Position = Position;
                        return;
                    case "Unload":
                        Unload();
                        return;
                }
            }
            public IPL() { }

            public IPL(string id, Vector3 position, List<string> iplNames)
            {
                ID = id;
                Position = position;
                IPLNames = iplNames;
            }
            public void LoadDefault()
            {
                Load(DefaultEntitySets);
            }
            public void Load(List<string> includeEntitySets = null, string selectedTheme = "")
            {
                int theme = 1;
                foreach (IPLTheme t in Themes)
                {
                    if (t.Name == selectedTheme)
                    {
                        theme = t.Index;
                        break;
                    }
                }
                Function.Call(Hash.DisableInterior, InteriorID, false);
                foreach (string iplName in IPLNames)
                {
                    Function.Call(Hash.RequestIpl, iplName);
                    if (!Function.Call<bool>(Hash.IsIplActive, iplName))
                    {
                        Debug.Log(DebugSeverity.Warning, iplName + " is not loaded");
                    }
                }
                LoadEntitySets(InteriorID, theme, includeEntitySets);
                Function.Call(Hash.RefreshInterior, InteriorID);
                Loaded = true;
            }
            private void LoadEntitySets(int interiorId, int theme, List<string> includeEntitySets)
            {
                foreach (IPLEntitySet entitySet in EntitySets)
                {
                    if (includeEntitySets != null && !includeEntitySets.Contains(entitySet.HumanName))
                    {
                        continue;
                    }
                    Function.Call(Hash.ActivateInteriorEntitySet, interiorId, entitySet.GameName);
                    Function.Call(Hash.SetInteriorEntitySetColor, interiorId, entitySet.GameName, theme);
                }
            }
            public void Unload()
            {
                if (!Loaded)
                {
                    return;
                }
                foreach (IPLEntitySet entitySet in EntitySets)
                {
                    Function.Call(Hash.DeactivateInteriorEntitySet, InteriorID, entitySet.GameName);
                }
                Function.Call(Hash.RefreshInterior, InteriorID);
                Function.Call(Hash.DisableInterior, InteriorID, true);
                foreach (string iplName in IPLNames)
                {
                    Function.Call(Hash.RemoveIpl, iplName);
                }
                Loaded = false;
            }
            public void Reload()
            {
                Unload();
                Load();
            }
        }
    }
}
