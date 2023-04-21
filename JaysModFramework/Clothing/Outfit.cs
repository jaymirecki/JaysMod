using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public class Outfit: IJMFDatabaseItem<string>
    {
        [XmlIgnore]
        public Mask Mask;
        public ComponentKey MaskKey
        {
            get { return new ComponentKey().FromComponent(Mask); }
            set
            {
                Mask = Global.Presets.MaleMasks[value.ID];
                if (Mask == null)
                {
                    Mask = new Mask(value.ID, value.CurrentColor);
                }
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
                Hands = Global.Presets.MaleHands[value.ID];
                if (Hands == null)
                {
                    Hands = new Hands(value.ID, value.CurrentColor);
                }
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
                Lower = Global.Presets.MaleLowers[value.ID];
                if (Lower == null)
                {
                    Lower = new Lower(value.ID, value.CurrentColor);
                }
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
                Parachute = Global.Presets.MaleParachutes[value.ID];
                if (Parachute == null)
                {
                    Parachute = new Parachute(value.ID, value.CurrentColor);
                }
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
                Shoes shoes = Global.Presets.MaleShoes[value.ID];
                if (shoes == null)
                {
                    shoes = new Shoes(value.ID, value.CurrentColor);
                }
                Shoes = shoes;
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
                Accessory accessories = Global.Presets.MaleAccessories[value.ID];
                if (accessories == null)
                {
                    accessories = new Accessory(value.ID, value.CurrentColor);
                }
                Accessory = accessories;
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
                Vest vest = Global.Presets.MaleVests[value.ID];
                if (vest == null)
                {
                    vest = new Vest(value.ID, value.CurrentColor);
                }
                Vest = vest;
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
                Neck neck = Global.Presets.MaleNecks[value.ID];
                if (neck == null)
                {
                    neck = new Neck(value.ID, value.CurrentColor);
                }
                Neck = neck;
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
                ShirtOverlay shirtOverlay = Global.Presets.MaleShirtOverlays[value.ID];
                if (shirtOverlay == null)
                {
                    shirtOverlay = new ShirtOverlay(value.ID, value.CurrentColor);
                }
                ShirtOverlay = shirtOverlay;
                ShirtOverlay.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Hat Hat;
        public ComponentKey HatKey
        {
            get { return new ComponentKey().FromComponent(Hat); }
            set
            {
                Hat hat = Global.Presets.MaleHats[value.ID];
                if (hat == null)
                {
                    hat = new Hat(value.ID, value.CurrentColor);
                }
                Hat = hat;
                Hat.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Glasses Glasses;
        public ComponentKey GlassesKey
        {
            get { return new ComponentKey().FromComponent(Glasses); }
            set
            {
                Glasses glasses = Global.Presets.MaleGlasses[value.ID];
                if (glasses == null)
                {
                    glasses = new Glasses(value.ID, value.CurrentColor);
                }
                Glasses = glasses;
                Glasses.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Ears Ears;
        public ComponentKey EarsKey
        {
            get { return new ComponentKey().FromComponent(Ears); }
            set
            {
                Ears ears = Global.Presets.MaleEars[value.ID];
                if (ears == null)
                {
                    ears = new Ears(value.ID, value.CurrentColor);
                }
                Ears = ears;
                Ears.CurrentColor = value.CurrentColor;
            }
        }
        [XmlIgnore]
        public Watch Watch;
        public ComponentKey WatchKey
        {
            get { return new ComponentKey().FromComponent(Watch); }
            set
            {
                Watch watch = Global.Presets.MaleWatches[value.ID];
                if (watch == null)
                {
                    watch = new Watch(value.ID, value.CurrentColor);
                }
                Watch = watch;
                Watch.CurrentColor = value.CurrentColor;
            }
        }
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
