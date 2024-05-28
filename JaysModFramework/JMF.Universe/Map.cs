using JMF.Interiors;
using JMF.Modules;
using OOD.Collections;
using System;
using System.Collections.Generic;

namespace JMF
{
    namespace Universe
    {
        public class Map: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<string> IPLs { get; set; }
            public List<Portal> Portals { get; set; }
            public ValidationState Validate()
            {
                foreach (string iplId in IPLs)
                {
                    if (!Framework.Database.IPLs.Contains(iplId))
                    {
                        Debug.Log(DebugSeverity.Error, "Map " + ID + " could not load IPL " + iplId);
                    }
                }
                return new ValidationState();
            }
            public void Load()
            {
                foreach (string iplId in IPLs)
                {
                    if (Framework.Database.IPLs.TryGetValue(iplId, out IPL ipl))
                    {
                        ipl.Load();
                    }
                    else
                    {
                        Debug.Log(DebugSeverity.Error, "Map " + ID + " could not load IPL " + iplId);
                    }
                }
            }
            public void Unload()
            {

            }
        }
    }
}
