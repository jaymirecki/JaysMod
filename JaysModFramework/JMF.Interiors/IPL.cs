using JMF.Math;
using JMF.Native;
using System.Collections.Generic;
using OOD.Collections;

namespace JMF
{
    namespace Interiors
    {
        public class IPL: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public Vector3 Position { get; set; }
            public List<string> IPLNames { get; set; }
            public List<IPLEntitySet> EntitySets { get; set; }
            public List<IPLTheme> Themes { get; set; }
            [System.Xml.Serialization.XmlIgnoreAttribute]
            public bool Loaded { get; private set; } = false;
            public IPL() { }

            public IPL(string id, Vector3 position, List<string> iplNames)
            {
                ID = id;
                Position = position;
                IPLNames = iplNames;
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
                foreach (string iplName in IPLNames)
                {
                    Debug.Log(DebugSeverity.Warning, iplName);
                    Function.Call(Hash.RequestIpl, iplName);
                    Debug.Log(DebugSeverity.Warning, Function.Call<bool>(Hash.IsIplActive, iplName).ToString());
                    //while (!Function.Call(Hash.IsIplActive, iplName))
                }
                int interiorID = Function.Call<int>(Hash.GetInteriorAtCoords, Position.X, Position.Y, Position.Z);
                Function.Call(Hash.DisableInterior, interiorID, false);
                LoadEntitySets(interiorID, theme, includeEntitySets);
                Function.Call(Hash.RefreshInterior, interiorID);
                Loaded = true;
            }
            private void LoadEntitySets(int interiorId, int theme, List<string> includeEntitySets)
            {
                Debug.Log(DebugSeverity.Warning, EntitySets.Count.ToString());
                Debug.Log(DebugSeverity.Warning, Position.ToString());
                Debug.Log(DebugSeverity.Warning, interiorId.ToString());
                Debug.Log(DebugSeverity.Warning, theme.ToString());
                foreach (IPLEntitySet entitySet in EntitySets)
                {
                    if (includeEntitySets != null && !includeEntitySets.Contains(entitySet.HumanName))
                    {
                        Debug.Log(DebugSeverity.Warning, "skipping " + entitySet.HumanName);
                        continue;
                    }
                    Debug.Log(DebugSeverity.Warning, entitySet.HumanName);
                    Debug.Log(DebugSeverity.Warning, entitySet.GameName);
                    Debug.Log(DebugSeverity.Warning, "Function.Call(Hash.ActivateInteriorEntitySet, " + interiorId + ", " + entitySet.GameName + ");");
                    Function.Call(Hash.ActivateInteriorEntitySet, interiorId, entitySet.GameName);
                    Function.Call(Hash.SetInteriorEntitySetColor, interiorId, entitySet.GameName, theme);
                }
            }
            public void Unload()
            {
                Debug.Log(DebugSeverity.Warning, "unloading");
                if (!Loaded)
                {
                    return;
                }
                int interiorID = Function.Call<int>(Hash.GetInteriorAtCoords, Position.X, Position.Y, Position.Z);
                foreach (IPLEntitySet entitySet in EntitySets)
                {
                    Function.Call(Hash.DeactivateInteriorEntitySet, interiorID, entitySet.GameName);
                }
                Function.Call(Hash.RefreshInterior, interiorID);
                Function.Call(Hash.DisableInterior, interiorID, true);
                Function.Call(Hash.RefreshInterior, interiorID);
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
