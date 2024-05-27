using JMF.Menus;
using JMF.Native;
using OOD.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public class Worldspace: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<TravelPoint> TravelPoints { get; set; } = new List<TravelPoint>();
            public bool Validate()
            {
                return true;
            }
        }
    }
}
