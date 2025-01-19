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
                dlcMenus.Add(new Menu("Afterhours", "Afterhours"));
                dlcMenus.Add(new Menu("Bikers", "Bikers"));
                dlcMenus.Add(new Menu("Casino", "Casino"));
                dlcMenus.Add(new Menu("CayoPerico", "CayoPerico"));
                dlcMenus.Add(new Menu("ChopShop", "ChopShop"));
                dlcMenus.Add(new Menu("Contract", "Contract"));
                dlcMenus.Add(new Menu("Doomsday", "Doomsday"));
                dlcMenus.Add(new Menu("Drugwars", "Drugwars"));
                dlcMenus.Add(new Menu("Executive", "Executive"));
                dlcMenus.Add(new Menu("Finance", "Finance"));
                dlcMenus.Add(new Menu("GTA", "GTA"));
                dlcMenus.Add(new Menu("Gunrunning", "Gunrunning"));
                dlcMenus.Add(new Menu("HeistCasino", "HeistCasino"));
                dlcMenus.Add(new Menu("Heists", "Heists"));
                dlcMenus.Add(new Menu("Import", "Import"));
                dlcMenus.Add(new Menu("Smugglers", "Smugglers"));

                Menu other = new Menu("Other", "Other");
                Menu all = new Menu("All", "All");

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
