using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;

namespace JaysModFramework.Clothing
{
    public class Outfit: IJMFDatabaseItem<string>
    {
        public Mask Mask;
        public Hands Upper;
        public Lower Lower;
        public Parachute Parachute;
        public Shoes Shoes;
        public Accessory Accessory;
        public Vest Vest;
        public Neck Neck;
        public ShirtOverlay ShirtOverlay;
        public Hat Hat;
        public Glasses Glasses;
        public Ears Ears;
        public Watch Watch;
        public string ID
        {
            get { return Mask.Name; }
        }
        public Outfit()
        {

        }
        public Outfit(Mask mask, Hands upper, Lower lower, Parachute parachute, Shoes shoes, Accessory accessory, Vest vest, Neck neck, ShirtOverlay shirtOverlay, Hat hat, Glasses glasses, Ears ears, Watch watch)
        {
            Mask = mask;
            Upper = upper;
            Lower = lower;
            Parachute = parachute;
            Shoes = shoes;
            Accessory = accessory;
            Vest = vest;
            Neck = neck;
            ShirtOverlay = shirtOverlay;
            Glasses = glasses;
            Ears = ears;
            Watch = watch;
            Hat = hat;
            Glasses = glasses;
            Ears = ears;
            Watch = watch;
        }
        public void FromPed(Ped ped)
        {

        }
        public void ToPed(Ped ped, bool preserveHair = true)
        {
            Components.Components.SetComponent(ped, Mask);
            Components.Components.SetComponent(ped, Upper);
            Components.Components.SetComponent(ped, Lower);
            Components.Components.SetComponent(ped, Parachute);
            Components.Components.SetComponent(ped, Shoes);
            Components.Components.SetComponent(ped, Accessory);
            Components.Components.SetComponent(ped, Vest);
            Components.Components.SetComponent(ped, Neck);
            Components.Components.SetComponent(ped, ShirtOverlay);
            Components.Components.SetProp(ped, Hat);
            Components.Components.SetProp(ped, Glasses);
            Components.Components.SetProp(ped, Ears);
            Components.Components.SetProp(ped, Watch);
        }
    }
}
