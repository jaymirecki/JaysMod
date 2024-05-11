using JMF.Interiors;
using JMF.Math;
using JMF.Native;
using System.Collections.Generic;
using OOD.Collections;

namespace JMF
{
    namespace Modules
    {
        public class IPLLoader: InternalModule<IPLLoaderSettings>
        {
            internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
            public override string ModuleName { get; } = "IPL Loader";
            public override string ModuleDescription { get; } = "Loads IPLs";
            public override IPLLoaderSettings Settings { get { return Global.Config.IPLLoaderSettings; } }
            private XMLDatabaseTable<string, IPL> IPLs { get; set; }
            public override void OnActivate()
            {
                Function.Call(Hash.OnEnterMp);
                IPLs = new MemoryXMLDatabaseTable<string, IPL>(Global.Config.DataPath, "IPL");
                IPLs.GetValue("FacilityExterior").Load();
                IPL facility = IPLs.GetValue("FacilityInterior");
                List<string> entitySets  = new List<string>() { "Shell", "CannonOff", "LoungePrestige" };
                facility.Load(entitySets, "Utility");
                //facility.Load(entitySets, "Utility");
                Vector3 position = facility.Position;
                //position.Z = position.Z + 1;
                Game.Player.Character.Position = position;
                //Thread.Sleep(5000);
                //Game.Player.Character.Position = position;
            }
            public override void OnDeactivate()
            {
                if (IPLs == null)
                {
                    return;
                }
                foreach (IPL ipl in IPLs)
                {
                    ipl.Unload();
                }
            }
            public bool Load(string iplId)
            {
                if (IPLs != null && IPLs.TryGetValue(iplId, out IPL ipl))
                {
                    ipl.Load();
                    return true;
                }
                return false;
            }

        }
        public class IPLLoaderSettings : ModuleSettings
        {
            public bool AircraftCarrierEnabled = false;
            public bool CasinoEnabled = false;
            public bool FacilityEnabled = false;
            public bool CargoShipEnabled = true;
        }
    }
}
