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
            public List<string> IPLNames;
            [System.Xml.Serialization.XmlIgnoreAttribute]
            public bool Loaded { get; private set; } = false;
            public IPL() { }

            public IPL(string id, Vector3 position, List<string> iplNames)
            {
                ID = id;
                Position = position;
                IPLNames = iplNames;
            }

            public void Load()
            {
                if (Loaded)
                {
                    return;
                }
                int interiorID = Function.Call<int>(Hash.GetInteriorAtCoords, Position.X, Position.Y, Position.Z);
                Function.Call(Hash.DisableInterior, interiorID, false);
                foreach (string iplName in IPLNames)
                {
                    Function.Call(Hash.RequestIpl, iplName);
                }
                Function.Call(Hash.RefreshInterior, interiorID);
                Loaded = true;
            }
            public void Unload()
            {
                if (!Loaded)
                {
                    return;
                }
                int interiorID = Function.Call<int>(Hash.GetInteriorAtCoords, Position.X, Position.Y, Position.Z);
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
