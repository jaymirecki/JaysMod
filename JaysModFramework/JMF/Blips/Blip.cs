using JMF.Native;

namespace JMF
{
    public class IntPtr
    {
        public int Value;
        public IntPtr(int value)
        {
            Value = value;
        }
    }
    public class Blip
    {
        public Blip(
            Vector3 position, 
            BlipSprite sprite = BlipSprite.RadarLevel, 
            bool showDistance = false, 
            BlipColor color = BlipColor.White, 
            bool displayonMap = true, 
            bool displayOnRadar = true
            )
        {
            Handle = Function.Call<int>(Hash.AddBlipForCoord, position.X, position.Y, position.Z);
            SetBlipInfo(sprite, showDistance, color, displayonMap, displayOnRadar);
        }
        public Blip(
            Entity entity,
            BlipSprite sprite = BlipSprite.RadarLevel,
            bool showDistance = false,
            BlipColor color = BlipColor.White,
            bool displayonMap = true,
            bool displayOnRadar = true
            )
        {
            Handle = Function.Call<int>(Hash.AddBlipForEntity, entity.Handle);
            SetBlipInfo(sprite, showDistance, color, displayonMap, displayOnRadar);
        }
        public void Delete()
        {
            IntPtr handle = new IntPtr(Handle);
            Rage.Native.NativeFunction.Natives.RemoveBlip(ref handle.Value);
        }
        private void SetBlipInfo(BlipSprite sprite, bool showDistance, BlipColor color, bool displayonMap, bool displayOnRadar)
        {
            int display;
            if (displayonMap && displayOnRadar)
            {
                display = 2;
            }
            else if (displayonMap)
            {
                display = 3;
            }
            else if (displayOnRadar)
            {
                display = 5;
            }
            else
            {
                display = 0;
            }

            Sprite = sprite;
            ShowDistance = showDistance;
            Color = color;
            Function.Call(Hash.SetBlipDisplay, display);
        }
        public int Handle
        {
            get; protected set;
        }
        public BlipColor Color
        {
            set { Function.Call(Hash.SetBlipColour, (int)value); }
        }
        public BlipSprite Sprite
        {
            set { Function.Call(Hash.SetBlipSprite, (int)value); }
        }
        public bool ShowDistance
        {
            set { Function.Call(Hash.SetBlipCategory, value ? 2 : 1); }
        }
        public string Name
        {
            set
            {
                Function.Call(Hash.BeginTextCommandSetBlipName, "STRING");
                Function.Call(Hash.AddTextComponentSubstringPlayerName, value);
                Function.Call(Hash.EndTextCommandSetBlipName, Handle);
            }
        }
    }
}
