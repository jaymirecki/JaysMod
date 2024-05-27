using JMF.Native;

namespace JMF
{
    public class World
    {
        public WeatherType Weather
        {
            set { Function.Call(Hash.SetWeatherTypeNowPersist, value.ToString()); }
        }
    }
}
