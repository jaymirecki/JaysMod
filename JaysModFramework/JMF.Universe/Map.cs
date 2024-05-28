using JMF.Interiors;
using OOD.Collections;
using System.Collections.Generic;

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
            public List<Portal> Portals { get; set; } = new List<Portal>();
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
                return new ValidationState();
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
            }
            public void Unload()
            {

            }
        }
    }
}
