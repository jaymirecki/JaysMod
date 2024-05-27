using OOD.Collections;
using System;
using System.Collections.Generic;

namespace JMF
{
    namespace Universe
    {
        public class Worldspace: IXMLDatabaseItem<string>
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public List<TravelPoint> TravelPoints { get; set; } = new List<TravelPoint>();
            public List<WeatherType> WeatherTypes { get; set; } = new List<WeatherType>();
            private static Random random = new Random();
            public ValidationState Validate()
            {
                if (WeatherTypes.Count < 1)
                {
                    return new ValidationState(false, "Worldspace " + ID + " has no WeatherTypes");
                }
                return new ValidationState();
            }
            public void Load()
            {
                World.Weather = WeatherTypes[random.Next(WeatherTypes.Count)];
            }
            public void Unload()
            {
                World.Weather = WeatherType.Clear;
            }
        }
    }
}
