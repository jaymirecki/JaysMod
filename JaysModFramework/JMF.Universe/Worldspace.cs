using JMF.Interiors;
using OOD.Collections;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public class Worldspace: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<string> MapIDs { get; set; } = new List<string>();
            private List<Map> _loadedMaps = new List<Map>();
            public List<TravelPoint> TravelPoints { get; set; } = new List<TravelPoint>();
            public List<WeatherType> WeatherTypes { get; set; } = new List<WeatherType>();
            [XmlIgnore]
            public string CurrentMap { get; private set; } = "";
            private static Random random = new Random();
            ///////////////////////////////////////////////////////////////////
            //                          Validation                           //
            ///////////////////////////////////////////////////////////////////
            #region Validation
            public ValidationState Validate()
            {
                foreach (string mapId in MapIDs)
                {
                    if (!ValidateMap(mapId, out ValidationState validationState))
                    {
                        return validationState;
                    }
                }
                if (WeatherTypes.Count < 1)
                {
                    return new ValidationState(false, "Worldspace " + ID + " has no WeatherTypes");
                }
                return new ValidationState(true);
            }
            private bool ValidateMap(string mapId, out ValidationState validationState)
            {
                // Validate that map with id exists in Worldspace
                if (!GetMap(mapId, out Map map))
                {
                    validationState = new ValidationState(false, "Worldspace " + ID + " cannot load Map " + mapId + " because it does not exist in database");
                    return false;
                }
                // Validate that map's parent Worldspace is this Worldspace
                if (map.Worldspace != this)
                {
                    validationState = new ValidationState(false, "Map " + mapId + " does not list Worldspace " + ID + " as parent but is its child");
                    return false;
                }
                // Validate map's portals
                foreach (Portal portal in map.Portals)
                {
                    if (!ValidatePortal(portal, map, out validationState))
                    {
                        return false;
                    }
                }
                validationState = new ValidationState(true);
                return true;
            }
            private bool ValidatePortal(Portal portal, Map map, out ValidationState validationState)
            {
                // Warn if portal has either IPL or RoomPortal but not both
                if (portal.IPL != null && portal.IPL != "" && (portal.RoomPortalID == null || portal.RoomPortalID == ""))
                {
                    Debug.Log(DebugSeverity.Warning, "Map " + map.ID + " Portal " + portal.ID + " has IPL but no RoomPortal");
                }
                else if (portal.RoomPortalID != null && portal.RoomPortalID != "" && (portal.IPL == null || portal.IPL == ""))
                {
                    Debug.Log(DebugSeverity.Warning, "Map " + map.ID + " Portal " + portal.ID + " has RoomPortal but no IPL");
                }
                // Validate that portals connect to other maps in this Worldspace
                if (!MapIDs.Contains(portal.ConnectedPortalMap)) {
                    validationState = new ValidationState(false, "Portal " + portal.ID + " in Map " + map.ID + " is invalid because connected Map " + portal.ConnectedPortalMap + " does not exist in Worldspace " + ID);
                    return false;
                }
                // Validate that portal's parent Map is the Map that listed it
                if (portal.Map != map)
                {
                    validationState = new ValidationState(false, "Portal " + portal.ID + " does not list Map " + map.ID + " as parent but is its child");
                    return false;
                }
                // Validate that portal's parent Worldspace is this Worldspace
                if (portal.Worldspace != this)
                {
                    validationState = new ValidationState(false, "Portal " + portal.ID + " does not list Worldspace " + map.ID + " parent but is its child");
                    return false;
                }
                // Validate that portal IPL link exists within Map and RoomPortal exists within IPL
                if (portal.IPL != null && portal.IPL != "")
                {
                    bool found = false;
                    foreach (IPLSettings iplSettings in map.IPLs)
                    {
                        if (iplSettings.ID == portal.IPL)
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        validationState = new ValidationState(false, "Portal " + portal.ID + " is invalid because IPL " + portal.IPL + " does not exist in Map " + map.ID);
                        return false;
                    }
                    if (Framework.Database.IPLs.TryGetValue(portal.IPL, out IPL ipl) && !ipl.TryGetRoomPortal(portal.RoomPortalID, out RoomPortal roomPortal))
                    {
                        validationState = new ValidationState(false, "Portal " + portal.ID + " is invalid because RoomPortal " + portal.RoomPortalID + " does not exist in IPL " + portal.IPL);
                        return false;
                    }
                }
                validationState = new ValidationState(true);
                return true;
            }
            #endregion Validation
            ///////////////////////////////////////////////////////////////////
            //                     Loading and Unloading                     //
            ///////////////////////////////////////////////////////////////////
            #region Loading and Unloading
            public bool LoadWorldspace(string mapId)
            {
                if (!LoadMap(mapId, out Map map))
                {
                    return false;
                }

                World.Weather = WeatherTypes[random.Next(WeatherTypes.Count)];
                return true;
            }
            private void LoadOverworld()
            {
                foreach (string m in MapIDs)
                {
                    if (GetMap(m, out Map map) && map.IsOverworld)
                    {
                        LoadMap(map);
                    }
                }
            }
            private void UnloadMaps()
            {
                // We switch out these lists before unloading the maps so that the tick cycle doesn't reference invalid indices
                List<Map> mapsToUnload = _loadedMaps;
                _loadedMaps = new List<Map>();
                foreach (Map map in mapsToUnload)
                {
                    map.Unload();
                }
            }
            public bool LoadMap(string mapId, out Map outMap)
            {
                if (!MapIDs.Contains(mapId))
                {
                    Debug.Log(DebugSeverity.Error, "Worldspace " + ID + " does not contain Map " + mapId);
                    outMap = null;
                    return false;
                }

                foreach (string m in MapIDs)
                {
                    if (GetMap(m, out Map map) && map.ID == mapId)
                    {
                        UnloadMaps();
                        CurrentMap = map.ID;
                        outMap = map;
                        if (map.IsOverworld)
                        {
                            LoadOverworld();
                            return true; ;
                        }
                        else
                        {
                            LoadMap(map);
                            return true;
                        }
                    }
                }
                Debug.Log(DebugSeverity.Error, "Worldspace " + ID + " cannot load Map " + mapId + " because it does not exist in database");
                outMap = null;
                return false;
            }
            public bool GetMap(string mapId, out Map map)
            {
                if (Framework.Database.Maps.TryGetValue(mapId, out map))
                {
                    map.Worldspace = this;
                    return true;
                }
                return false;
            }
            private void LoadMap(Map map)
            {
                if (!MapIDs.Contains(map.ID))
                {
                    return;
                }
                map.Load();
                _loadedMaps.Add(map);
            }
            public void Unload()
            {
                World.Weather = WeatherType.Clear;
            }
            #endregion Loading and Unloading
            public void ManagePortals()
            {
                foreach (Map map in _loadedMaps)
                {
                    map.ManagePortals();
                }
            }
        }
    }
}
