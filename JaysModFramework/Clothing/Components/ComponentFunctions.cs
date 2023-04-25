using GTA;
using GTA.Native;

namespace JaysModFramework.Clothing.Components
{
    internal static partial class Components
    {
        private static void SetComponent(Ped ped, OutfitComponents slot, int componentId, int componentColorId)
        {
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, (int)slot, componentId, componentColorId, 2);
        }
        public static void SetComponent(Ped ped, OutfitComponent component, OutfitComponents slot)
        {
            Debug.Notify(slot.ToString(), component.Index, true);
            SetComponent(ped, slot, component.Index, component.CurrentColor);
        }
        private static void SetProp(Ped ped, Props slot, int propId, int propColorId)
        {
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, slot, propId, propColorId, 2);
        }
        public static void SetProp(Ped ped, OutfitComponent prop, Props slot)
        {
            SetProp(ped, slot, prop.Index, prop.CurrentColor);
        }
    }
}
