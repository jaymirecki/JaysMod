using JMF.Math;
using JMF.Native;
using System.Collections.Generic;

namespace JMF
{
    namespace Interiors
    {
        public class IPL
        {
            public Vector3 Position { get; private set; }
            private readonly List<string> IPLNames;
            public bool Loaded { get; private set; }

            public IPL(Vector3 position, List<string> iplNames)
            {
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
