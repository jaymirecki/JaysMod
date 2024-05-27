using JMF.Native;

namespace JMF
{
    public static class World
    {
        public static float SnowLevel
        {
            get { return Function.Call<float>(Hash.GetSnowLevel); }
            set { Function.Call(Hash.SetSnowLevel, value); }
        }
        public static WeatherType Weather
        {
            set { Function.Call(Hash.SetWeatherTypeNowPersist, value.ToString().ToUpper()); }
        }
    }
}
