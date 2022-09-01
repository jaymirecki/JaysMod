using GTA;
using GTA.Math;
using GTA.Native;
using System.Collections.Generic;

namespace JaysModFramework
{
    internal class IPLDetails
    {
        private readonly Vector3 Position;
        private readonly List<string> IPLNames;
        public readonly bool IsDLC;
        private Blip Blip;
        private readonly BlipSprite BlipSprite;
        private readonly string BlipName;
        private readonly bool HasBlip;
        public bool Loaded { get { return loaded; } }
        private bool loaded;

        public IPLDetails(Vector3 position, List<string> iplNames) : this(position, iplNames, false, false, BlipSprite.Standard, "") { }
        public IPLDetails(Vector3 position, List<string> iplNames, BlipSprite blipSprite, string blipName) : this(position, iplNames, false, true, blipSprite, blipName) { }
        public IPLDetails(Vector3 position, List<string> iplNames, bool isDLC) : this(position, iplNames, isDLC, false, BlipSprite.Standard, "") { }
        public IPLDetails(Vector3 position, List<string> iplNames, bool isDLC, BlipSprite blipSprite, string blipName) : this(position, iplNames, isDLC, true, blipSprite, blipName) { }
        private IPLDetails(Vector3 position, List<string> iplNames, bool isDLC, bool hasBlip, BlipSprite blipSprite, string blipName)
        {
            Position = position;
            IPLNames = iplNames;
            IsDLC = isDLC;
            HasBlip = hasBlip;
            if (hasBlip)
            {
                BlipSprite = blipSprite;
                BlipName = blipName;
            }
        }

        public void Load(bool showBlip = true, BlipSprite overrideBlipSprite = BlipSprite.Invisible, string overrideBlipName = "")
        {
            if (Loaded)
            {
                return;
            }
            if (IsDLC)
            {
                Function.Call((Hash)0x0888C3502DBBEEF5); //_LOAD_MP_DLC_Maps
            }
            int interiorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Position.X, Position.Y, Position.Z);
            Function.Call(Hash.DISABLE_INTERIOR, interiorID, false);
            foreach (string iplName in IPLNames)
            {
                Function.Call(Hash.REQUEST_IPL, iplName);
            }
            if (HasBlip && showBlip)
            {
                Blip = World.CreateBlip(Position.BaseVector);
                Blip.Sprite = overrideBlipSprite == BlipSprite.Invisible ? BlipSprite : overrideBlipSprite;
                Blip.Name = overrideBlipName == "" ? BlipName : overrideBlipName;
            }
            Function.Call(Hash.REFRESH_INTERIOR, interiorID);
            loaded = true;
        }
        public void Unload()
        {
            if (!Loaded)
            {
                return;
            }
            int interiorID = Function.Call<int>(Hash.GET_INTERIOR_AT_COORDS, Position.X, Position.Y, Position.Z);
            Function.Call(Hash.DISABLE_INTERIOR, interiorID, true);
            Function.Call(Hash.REFRESH_INTERIOR, interiorID);
            if (Blip != null)
            {
                Blip.Delete();
            }
            loaded = false;
        }
        public void Reload()
        {
            Unload();
            Load();
        }
    }
}
