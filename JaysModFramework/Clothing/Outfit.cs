using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public class Outfit: IJMFDatabaseItem<string>
    {
        private static readonly Presets Presets = new Presets();
        [XmlIgnore]
        public Mask Mask;
        public ComponentKey MaskKey
        {
            get { return new ComponentKey().FromComponent(Mask); }
            set
            {
                Mask = Presets.MaleMasks[value.ID];
                Mask.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Hands Hands;
        public ComponentKey HandsKey
        {
            get { return new ComponentKey().FromComponent(Hands); }
            set
            {
                Hands = Presets.MaleHands[value.ID];
                Hands.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Lower Lower;
        public ComponentKey LowerKey
        {
            get { return new ComponentKey().FromComponent(Lower); }
            set
            {
                Lower = Presets.MaleLowers[value.ID];
                Lower.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Parachute Parachute;
        public ComponentKey ParachuteKey
        {
            get { return new ComponentKey().FromComponent(Parachute); }
            set
            {
                Parachute = Presets.MaleParachutes[value.ID];
                Parachute.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Shoes Shoes;
        public ComponentKey ShoesKey
        {
            get { return new ComponentKey().FromComponent(Shoes); }
            set
            {
                Shoes = Presets.MaleShoes[value.ID];
                Shoes.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Accessory Accessory;
        public ComponentKey AccessoryKey
        {
            get { return new ComponentKey().FromComponent(Accessory); }
            set
            {
                Accessory = Presets.MaleAccessories[value.ID];
                Accessory.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Vest Vest;
        public ComponentKey VestKey
        {
            get { return new ComponentKey().FromComponent(Vest); }
            set
            {
                Vest = Presets.MaleVests[value.ID];
                Vest.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Neck Neck;
        public ComponentKey NeckKey
        {
            get { return new ComponentKey().FromComponent(Neck); }
            set
            {
                Neck = Presets.MaleNecks[value.ID];
                Neck.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public ShirtOverlay ShirtOverlay;
        public ComponentKey ShirtOverlayKey
        {
            get { return new ComponentKey().FromComponent(ShirtOverlay); }
            set
            {
                ShirtOverlay = Presets.MaleShirtOverlays[value.ID];
                ShirtOverlay.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Hat Hat;
        //public ComponentKey HatKey
        //{
        //    get { return new ComponentKey().FromComponent(Hat); }
        //    set
        //    {
        //        Hat = Presets.MaleHats[value.ID];
        //        Hat.CurrentColor = value.CurrentColor;
        //    }
        //}
        [XmlIgnore]
        public Glasses Glasses;
        //public ComponentKey GlassesKey
        //{
        //    get { return new ComponentKey().FromComponent(Glasses); }
        //    set
        //    {
        //        Glasses = Presets.MaleGlasses[value.ID];
        //        Glasses.CurrentColor = value.CurrentColor;
        //    }
        //}
        [XmlIgnore]
        public Ears Ears;
        //public ComponentKey EarsKey
        //{
        //    get { return new ComponentKey().FromComponent(Ears); }
        //    set
        //    {
        //        Ears = Presets.MaleEars[value.ID];
        //        Ears.CurrentColor = value.CurrentColor;
        //    }
        //}
        [XmlIgnore]
        public Watch Watch;
        //public ComponentKey WatchKey
        //{
        //    get { return new ComponentKey().FromComponent(Watch); }
        //    set
        //    {
        //        Watch = Presets.MaleWatches[value.ID];
        //        Watch.CurrentColor = value.CurrentColor;
        //    }
        //}
        public string ID
        {
            get { return Mask.Name; }
        }
        public Outfit()
        {
            Mask = new Mask();
            Hands = new Hands();
            Lower = new Lower();
            Parachute = new Parachute();
            Shoes = new Shoes();
            Accessory = new Accessory();
            Vest = new Vest();
            Neck = new Neck();
            ShirtOverlay = new ShirtOverlay();
            Glasses = new Glasses();
            Ears = new Ears();
            Hat = new Hat();
            Glasses = new Glasses();
            Ears = new Ears();
            Watch = new Watch();
        }
        public Outfit(Mask mask, Hands upper, Lower lower, Parachute parachute, Shoes shoes, Accessory accessory, Vest vest, Neck neck, ShirtOverlay shirtOverlay, Hat hat, Glasses glasses, Ears ears, Watch watch)
        {
            Mask = mask;
            Hands = upper;
            Lower = lower;
            Parachute = parachute;
            Shoes = shoes;
            Accessory = accessory;
            Vest = vest;
            Neck = neck;
            ShirtOverlay = shirtOverlay;
            Glasses = glasses;
            Ears = ears;
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
            Components.Components.SetComponent(ped, Hands);
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
