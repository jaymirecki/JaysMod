using GTA;
using GTA.Native;

namespace JaysModFramework.Clothing.Components
{
    public enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    public enum OutfitComponents { Mask = 1, Hair = 2, Upper = 3, Lower = 4, Hands = 5, Shoes = 6, AccOne = 8, AccTwo = 9, Shirt = 11 };
    public enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    internal static partial class Components
    {
        private static void SetComponent(Ped ped, OutfitComponents slot, int componentId, int componentColorId)
        {
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, slot, componentId, componentColorId, 2);
        }
        public static void SetMask(Ped ped, Mask mask, OutfitColor color)
        {
            SetComponent(ped, OutfitComponents.Mask, mask.ID, color.ID);
        }
    }
}
