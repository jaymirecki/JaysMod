using JMF.Interiors;
using JMF.Menus;
using JMF.Native;
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
            public override IPLLoaderSettings Settings { get { return Framework.Config.IPLLoaderSettings; } }
            public override void OnActivate()
            {
                Function.Call(Hash.OnEnterMp);
            }
            protected override void AddMenuItems()
            {
                foreach (IPL ipl in Framework.Database.IPLs)
                {
                    Menu.Add(ipl.MenuItem);
                }
            }
            public bool Load(string iplId)
            {
                if (Framework.Database.IPLs.TryGetValue(iplId, out IPL ipl))
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
