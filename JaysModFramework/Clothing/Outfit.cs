using GTA;
using JaysModFramework.Clothing.Components;
using OOD.Collections;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public class Outfit: IXMLDatabaseItem<string>
    {
        public string Name;
        private static readonly ComponentKey StaticKey = new ComponentKey();
        [XmlIgnore]
        public Hair Hair;
        public ComponentKey HairKey
        {
            get { return StaticKey.FromComponent(Hair); }
            set { Hair = StaticKey.ToComponent(value, Global.Presets.MaleHairs); }
        }
        [XmlIgnore]
        public Mask Mask;
        public ComponentKey MaskKey
        {
            get { return StaticKey.FromComponent(Mask); }
            set { Mask = StaticKey.ToComponent(value, Global.Presets.MaleMasks); }
        }
        [XmlIgnore]
        public Hands Hands;
        public ComponentKey HandsKey
        {
            get { return StaticKey.FromComponent(Hands); }
            set { Hands = StaticKey.ToComponent(value, Global.Presets.MaleHands); }
        }
        [XmlIgnore]
        public Lower Lower;
        public ComponentKey LowerKey
        {
            get { return StaticKey.FromComponent(Lower); }
            set { Lower = StaticKey.ToComponent(value, Global.Presets.MaleLowers); }
        }
        [XmlIgnore]
        public Parachute Parachute;
        public ComponentKey ParachuteKey
        {
            get { return StaticKey.FromComponent(Parachute); }
            set { Parachute = StaticKey.ToComponent(value, Global.Presets.MaleParachutes); }
        }
        [XmlIgnore]
        public Shoes Shoes;
        public ComponentKey ShoesKey
        {
            get { return StaticKey.FromComponent(Shoes); }
            set { Shoes = StaticKey.ToComponent(value, Global.Presets.MaleShoes); }
        }
        [XmlIgnore]
        public Accessory Accessory;
        public ComponentKey AccessoryKey
        {
            get { return StaticKey.FromComponent(Accessory); }
            set { Accessory = StaticKey.ToComponent(value, Global.Presets.MaleAccessories); }
        }
        [XmlIgnore]
        public Vest Vest;
        public ComponentKey VestKey
        {
            get { return StaticKey.FromComponent(Vest); }
            set { Vest = StaticKey.ToComponent(value, Global.Presets.MaleVests); }
        }
        [XmlIgnore]
        public Neck Neck;
        public ComponentKey NeckKey
        {
            get { return StaticKey.FromComponent(Neck); }
            set { Neck = StaticKey.ToComponent(value, Global.Presets.MaleNecks); }
        }
        [XmlIgnore]
        public ShirtOverlay ShirtOverlay;
        public ComponentKey ShirtOverlayKey
        {
            get { return StaticKey.FromComponent(ShirtOverlay); }
            set { ShirtOverlay = StaticKey.ToComponent(value, Global.Presets.MaleShirtOverlays); }
        }
        [XmlIgnore]
        public Hat Hat;
        public ComponentKey HatKey
        {
            get { return StaticKey.FromComponent(Hat); }
            set { Hat = StaticKey.ToComponent(value, Global.Presets.MaleHats); }
        }
        [XmlIgnore]
        public Glasses Glasses;
        public ComponentKey GlassesKey
        {
            get { return StaticKey.FromComponent(Glasses); }
            set { Glasses = StaticKey.ToComponent(value, Global.Presets.MaleGlasses); }
        }
        [XmlIgnore]
        public Ears Ears;
        public ComponentKey EarsKey
        {
            get { return StaticKey.FromComponent(Ears); }
            set { Ears = StaticKey.ToComponent(value, Global.Presets.MaleEars); }
        }
        [XmlIgnore]
        public Watch Watch;
        public ComponentKey WatchKey
        {
            get { return StaticKey.FromComponent(Watch); }
            set { Watch = StaticKey.ToComponent(value, Global.Presets.MaleWatches); }
        }
        public string ID
        {
            get { return Name; }
        }
        public Outfit()
        {
            Name = "Default Name";
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
        public Outfit(string name, Mask mask, Hands upper, Lower lower, Parachute parachute, Shoes shoes, Accessory accessory, Vest vest, Neck neck, ShirtOverlay shirtOverlay, Hat hat, Glasses glasses, Ears ears, Watch watch)
        {
            Name = name;
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
            Components.Components.SetComponent(ped, Hair);
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
