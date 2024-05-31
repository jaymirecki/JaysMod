using JMF.Interiors;
using OOD.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public class Map: IXMLDatabaseItem<string>
        {
            public string ID { get; set; } = "";
            public string Name { get; set; } = "";
            public bool IsOverworld { get; set; } = true;
            public List<IPLSettings> IPLs { get; set; } = new List<IPLSettings>();
            private List<Portal> _portals = new List<Portal>();
            public List<Portal> Portals
            {
                get
                {
                    foreach (Portal portal in _portals)
                    {
                        portal.Map = this;
                    }
                    return _portals;
                }
                set { _portals = value; }
            }
            [XmlIgnore]
            public Worldspace Worldspace = new Worldspace();
            public ValidationState Validate()
            {
                foreach (IPLSettings iplSettings in IPLs)
                {
                    if (!Framework.Database.IPLs.TryGetValue(iplSettings.ID, out IPL ipl))
                    {
                        return new ValidationState(false, "Map " + ID + " could not load IPL " + iplSettings.ID);
                    }
                    if (!IsOverworld && ipl.IsOverworld)
                    {
                        return new ValidationState(false, "Map " + ID + " is not marked overworld but IPL " + ipl.ID + " is");
                    }
                }
                return new ValidationState(true);
            }
            public void Load()
            {
                foreach (IPLSettings iplSettings in IPLs)
                {
                    if (Framework.Database.IPLs.TryGetValue(iplSettings.ID, out IPL ipl))
                    {
                        ipl.Load(iplSettings.EntitySets, iplSettings.Theme);
                    }
                    else
                    {
                        Debug.Log(DebugSeverity.Error, "Map " + ID + " could not load IPL " + iplSettings.ID);
                    }
                }
                for (int i = 0; i < Portals.Count; i++)
                {
                    Portal portal = Portals[i];
                    portal.Map = this;
                    //Portals[i] = portal;
                }
            }
            public void Unload()
            {

            }
            public void ManagePortals()
            {
                foreach (Portal portal in Portals)
                {
                    portal.Draw();
                    portal.Teleport();
                }
            }
            public bool TryGetPortal(string portalId, out Portal portal)
            {
                foreach (Portal p in Portals)
                {
                    if (p.ID == portalId)
                    {
                        portal = p;
                        portal.Map = this;
                        return true;
                    }
                }
                portal = new Portal();
                return false;
            }
        }
    }
}
