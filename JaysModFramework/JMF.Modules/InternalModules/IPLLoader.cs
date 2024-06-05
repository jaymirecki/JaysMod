using JMF.Interiors;
using JMF.Menus;
using JMF.Native;
using OOD.Collections;
using System.Collections.Generic;

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
            public IPLLoader()
            {
                Dependencies.Add(typeof(InteractionMenu));
            }
            public override void OnActivate()
            {
                Function.Call(Hash.OnEnterMp);
            }
            protected override void AddMenuItems()
            {
                List<Menu> dlcMenus = new List<Menu>();
                dlcMenus.Add(new Menu("Afterhours", "Afterhours", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Bikers", "Bikers", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Casino", "Casino", Framework.ObjectPool));
                dlcMenus.Add(new Menu("CayoPerico", "CayoPerico", Framework.ObjectPool));
                dlcMenus.Add(new Menu("ChopShop", "ChopShop", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Contract", "Contract", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Doomsday", "Doomsday", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Drugwars", "Drugwars", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Executive", "Executive", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Finance", "Finance", Framework.ObjectPool));
                dlcMenus.Add(new Menu("GTA", "GTA", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Gunrunning", "Gunrunning", Framework.ObjectPool));
                dlcMenus.Add(new Menu("HeistCasino", "HeistCasino", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Heists", "Heists", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Import", "Import", Framework.ObjectPool));
                dlcMenus.Add(new Menu("Smugglers", "Smugglers", Framework.ObjectPool));

                Menu other = new Menu("Other", "Other", Framework.ObjectPool);
                Menu all = new Menu("All", "All", Framework.ObjectPool);

                Debug.Log(DebugSeverity.Error, Framework.Database.IPLs.Count);

                foreach (IPL ipl in Framework.Database.IPLs)
                {
                    all.Add(ipl.MenuItem);
                    bool added = false;
                    foreach (Menu m in dlcMenus)
                    {
                        if (m.Name == ipl.DLC)
                        {
                            m.Add(ipl.MenuItem);
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                    {
                        other.Add(ipl.MenuItem);
                    }
                }
                foreach(Menu m in dlcMenus)
                {
                    Menu.Add(m);
                }
                Menu.Add(other);
                Menu.Add(all);
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
