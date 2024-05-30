using OOD.Collections;
using System;
using System.Collections.Generic;

namespace JMF
{
    namespace Universe
    {
        public class Worldspace: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<string> Maps { get; set; } = new List<string>();
            private List<Map> _loadedMaps = new List<Map>();
            public List<TravelPoint> TravelPoints { get; set; } = new List<TravelPoint>();
            public List<WeatherType> WeatherTypes { get; set; } = new List<WeatherType>();
            private static Random random = new Random();
            public ValidationState Validate()
            {
                if (WeatherTypes.Count < 1)
                {
                    return new ValidationState(false, "Worldspace " + ID + " has no WeatherTypes");
                }
                return new ValidationState();
            }
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
                foreach (string m in Maps)
                {
                    if (Framework.Database.Maps.TryGetValue(m, out Map map) && map.IsOverworld)
                    {
                        LoadMap(map);
                    }
                }
            }
            private void UnloadMaps()
            {
                List<Map> mapsToUnload = _loadedMaps;
                _loadedMaps = new List<Map>();
                foreach (Map map in mapsToUnload)
                {
                    map.Unload();
                }
                //mapsToUnload.Clear();
            }
            public bool LoadMap(string mapId, out Map outMap)
            {
                if (!Maps.Contains(mapId))
                {
                    Debug.Log(DebugSeverity.Error, "Worldspace " + ID + " does not contain Map " + mapId);
                    outMap = null;
                    return false;
                }

                foreach (string m in Maps)
                {
                    if (Framework.Database.Maps.TryGetValue(m, out Map map) && map.ID == mapId)
                    {
                        UnloadMaps();
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
            private void LoadMap(Map map)
            {
                if (!Maps.Contains(map.ID))
                {
                    return;
                }
                map.Worldspace = this;
                map.Load();
                _loadedMaps.Add(map);
            }
            public void Unload()
            {
                World.Weather = WeatherType.Clear;
            }
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
