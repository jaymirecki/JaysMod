using JMF.Interiors;
using JMF.Native;
using System.Collections.Generic;

namespace JMF.Modules
{
    public class PortalManager : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Portal Manager";
        public override string ModuleDescription { get; } = "Enables use of larger radar from MP";
        public override ModuleSettings Settings { get { return new ModuleSettings(true); } }
        private List<Portal> portals = new List<Portal>();
        public PortalManager() : base() { }
        public override void OnActivate()
        {
            Portal test = new Portal("test", "AircraftCarrier", "One", "SubmarineKosatka", "One");
            portals.Add(test);
        }
        public override void OnTick()
        {
            foreach (Portal portal in portals)
            {
                RoomPortal inPortal = Framework.Database.IPLs.GetValue(portal.InPortalIPL).Portals.Find((RoomPortal p) => p.ID == portal.InPortalID);
                RoomPortal outPortal = Framework.Database.IPLs.GetValue(portal.OutPortalIPL).Portals.Find((RoomPortal p) => p.ID == portal.OutPortalID);
                Function.Call(Hash.DrawMarker, 1, inPortal.InPosition.X, inPortal.InPosition.Y, inPortal.InPosition.Z - 1, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f, 1f, 255, 128, 0, 50, false, false, 2, false, 0, 0, false);
                Function.Call(Hash.DrawMarker, 1, outPortal.InPosition.X, outPortal.InPosition.Y, outPortal.InPosition.Z - 1, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f, 1f, 255, 128, 0, 50, false, false, 2, false, 0, 0, false);
                if (Game.Player.Character.Position.DistanceTo(inPortal.InPosition) < .5f)
                {
                    Game.Player.Character.Position = outPortal.OutPosition;
                    Game.Player.Character.Heading = outPortal.OutHeading;
                }
                else if (Game.Player.Character.Position.DistanceTo(outPortal.InPosition) < .5f)
                {
                    Game.Player.Character.Position = inPortal.OutPosition;
                    Game.Player.Character.Heading = inPortal.OutHeading;
                }
            }
        }
    }
}
