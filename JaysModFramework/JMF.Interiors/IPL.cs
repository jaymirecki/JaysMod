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
            public string ID { get; set; } = "";
            public string DLC { get; set; } = "";
            public int InteriorID { get; set; } = 0;
            public bool IsOverworld { get; set; } = false;
            private Vector3 _position = new Vector3();
            public Vector3 Position
            {
                get { return _position; }
                set
                {
                    _position = value;
                    int interiorId = Function.Call<int>(Hash.GetInteriorAtCoords, _position.X, _position.Y, _position.Z);
                    if (interiorId != 0 && interiorId != InteriorID)
                    {
                        InteriorID = interiorId;
                        Debug.Log(DebugSeverity.Info, "Set new InteriorID for IPL " + ID + ": " + interiorId);
                    }
                }
            }
            public List<string> IPLNames { get; set; }
            public List<IPLEntitySet> EntitySets { get; set; } = new List<IPLEntitySet>();
            public List<IPLTheme> Themes { get; set; } = new List<IPLTheme>();
            public List<string> DefaultEntitySets { get; set; } = new List<string>();
            public List<RoomPortal> Portals { get; set; } = new List<RoomPortal>();
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
                Debug.Notify(ID + ": " + IsOverworld, true);
                Unload();
                if (includeEntitySets == null)
                {
                    includeEntitySets = new List<string>();
                }
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
                Function.Call(Hash.CapInterior, InteriorID, false);
                foreach (string iplName in IPLNames)
                {
                    Function.Call(Hash.RequestIpl, iplName);
                    if (!Function.Call<bool>(Hash.IsIplActive, iplName))
                    {
                        Debug.Log(DebugSeverity.Warning, iplName + " is not loaded");
                    }
                }
                LoadEntitySets(theme, includeEntitySets);
                Function.Call(Hash.RefreshInterior, InteriorID);
            }
            private void LoadEntitySets(int theme, List<string> includeEntitySets)
            {
                int loadedEntities = 0;
                foreach (IPLEntitySet entitySet in EntitySets)
                {
                    if (includeEntitySets.Count == 0 || includeEntitySets.Contains(entitySet.HumanName))
                    {
                        Function.Call(Hash.ActivateInteriorEntitySet, InteriorID, entitySet.GameName);
                        Function.Call(Hash.SetInteriorEntitySetColor, InteriorID, entitySet.GameName, theme);
                        if (Function.Call<bool>(Hash.IsInteriorEntitySetActive, InteriorID, entitySet.GameName))
                        {
                            loadedEntities++;
                        }
                    }
                }
                ValidateEntitySets(includeEntitySets, loadedEntities);
            }
            private void ValidateEntitySets(List<string> includeEntitySets, int loadedEntities)
            {
                if (includeEntitySets.Count > 0 && loadedEntities != includeEntitySets.Count)
                {
                    foreach (string name in includeEntitySets)
                    {
                        bool found = false;
                        if (name == "Null") found = true;
                        foreach (IPLEntitySet entitySet in EntitySets)
                        {
                            if (entitySet.HumanName == name)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            Debug.Log(DebugSeverity.Warning, "Entity set " + name + " does not exist for interior " + ID);
                        }
                    }
                }
            }
            public void Unload()
            {
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
            }
            public void Reload()
            {
                Unload();
                Load();
            }
            public ValidationState Validate()
            {
                return new ValidationState();
            }
        }
    }
}
