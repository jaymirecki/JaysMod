using GTA;
using JaysModFramework.Clothing.Components;
using OOD.Collections;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public struct Outfit: IXMLDatabaseItem<string>
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        [XmlIgnore]
        public Mask Mask;
        public string MaskID
        {
            get { return Mask.ID; }
            set {  Mask = Global.Database.Masks[value]; }
        }
        [XmlIgnore]
        public Torso Torso;
        public string TorsoID
        {
            get { return Torso.ID; }
            set { Torso = Global.Database.Torsos[value]; }
        }
        [XmlIgnore]
        public Legs Legs;
        public string LegsID
        {
            get { return Legs.ID; }
            set { Legs = Global.Database.Legs[value]; }
        }
        [XmlIgnore]
        public Accessory Accessory;
        public string AccessoryID
        {
            get { return Accessory.ID; }
            set { Accessory = Global.Database.Accessory[value]; }
        }
        public int VestColor { get; set; }
        [XmlIgnore]
        public Parachute Parachute;
        public string ParachuteID
        {
            get { return Parachute.ID; }
            set { Parachute = Global.Database.Parachute[value]; }
        }
        [XmlIgnore]
        public Hat Hat;
        public string HatID
        {
            get { return Hat.ID; }
            set { Hat = Global.Database.Hats[value]; }
        }
        [XmlIgnore]
        public Glasses Glasses;
        public string GlassesID
        {
            get { return Glasses.ID; }
            set { Glasses = Global.Database.Glasses[value]; }
        }
        [XmlIgnore]
        public Ears Ears;
        public string EarsID
        {
            get { return Ears.ID; }
            set { Ears = Global.Database.Ears[value]; }
        }
        [XmlIgnore]
        public Wrist Wrist;
        public string WristID
        {
            get { return Wrist.ID; }
            set { Wrist = Global.Database.Wrists[value]; }
        }
        #endregion Components
        #endregion Properties
        #region Constructors
        public Outfit(string name, PedHash model, Mask mask, Torso torso, Legs legs, Accessory accessory, int vestColor, Parachute parachute, Hat hat, Glasses glasses, Ears ears, Wrist wrist)
        {
            Name = name;
            Model = model;
            Mask = mask;
            Torso = torso;
            Legs = legs;
            Accessory = accessory;
            VestColor = vestColor;
            Parachute = parachute;
            Hat = hat;
            Glasses = glasses;
            Ears = ears;
            Wrist = wrist;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Mask.SetToPed(ped);
            Torso.SetToPed(ped);
            Legs.SetToPed(ped);
            Accessory.SetToPed(ped);
            if (VestColor > -1)
            {
                Components.Components.SetComponent(ped, new OutfitComponent(Torso.Vest, new string[] { }, VestColor, true), OutfitComponents.Vest);
            }
            Parachute.SetToPed(ped, Torso);

            Hat.SetToPed(ped);
            Glasses.SetToPed(ped);
            Ears.SetToPed(ped);
            Wrist.SetToPed(ped);
        }
    }
}
