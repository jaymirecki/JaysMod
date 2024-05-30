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
        public static void DrawMarker(Vector3 position)
        {
            Function.Call(Hash.DrawMarker, 1, position.X, position.Y, position.Z, 0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f, 1f, 255, 128, 0, 100, false, false, false, 2, 0, 0, false);
        }
    }
}
