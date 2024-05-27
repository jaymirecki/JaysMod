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
            private IPLLoader IPLLoader
            {
                get { return ModuleManager.GetModule<IPLLoader>(typeof(IPLLoader)); }
            }
            public ValidationState Validate()
            {
                return new ValidationState();
            }
            public void Load()
            {
                foreach (string ipl in IPLs)
                {
                    if (!IPLLoader.Load(ipl))
                    {
                        Debug.Log(DebugSeverity.Error, "Map " + ID + " could not load IPL " + ipl);
                    }
                }
            }
            public void Unload()
            {
                World.Weather = WeatherType.Clear;
            }
        }
    }
}
