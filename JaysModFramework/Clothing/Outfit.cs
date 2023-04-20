using GTA;
using JaysModFramework.Clothing.Components;

namespace JaysModFramework.Clothing
{
    public struct Outfit
    {
        public Mask Mask;
        public OutfitColor MaskColor;
        public Outfit(Mask mask, OutfitColor maskColor)
        {
            Mask = mask;
            MaskColor = maskColor;
        }
        public void FromPed(Ped ped)
        {

        }
        public void ToPed(Ped ped, bool preserveHair = true)
        {
            Components.Components.SetMask(ped, Mask, MaskColor);
        }
    }
}
