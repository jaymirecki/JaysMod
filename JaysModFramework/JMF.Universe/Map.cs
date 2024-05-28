using JMF.Interiors;
using OOD.Collections;
using System.Collections.Generic;

namespace JMF
{
    namespace Universe
    {
        public class Map: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<IPLSettings> IPLs { get; set; }
            public List<Portal> Portals { get; set; }
            public ValidationState Validate()
            {
                foreach (IPLSettings iplSettings in IPLs)
                {
                    if (!Framework.Database.IPLs.Contains(iplSettings.ID))
                    {
                        Debug.Log(DebugSeverity.Error, "Map " + ID + " could not load IPL " + iplSettings.ID);
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
                        ipl.Load();
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
