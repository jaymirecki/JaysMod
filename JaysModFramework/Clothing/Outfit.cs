using GTA;
using JaysModFramework.Clothing.Components;
using OOD.Collections;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public struct Outfit: IXMLDatabaseItem<string>
    {
        #region Properties
        public string ID
        {
            get { return Name; }
        }
        public string Name;
        #region Components
        [XmlIgnore]
        public Mask Mask;
        public string MaskID
        {
            get { return Mask.ID; }
        }
        [XmlIgnore]
        public Torso Torso;
        public string TorsoID
        {
            get { return Torso.ID; }
        }
        [XmlIgnore]
        public Legs Legs;
        public string LegsID
        {
            get { return Legs.ID; }
        }
        [XmlIgnore]
        public Accessory Accessory;
        public string AccessoryID
        {
            get { return Accessory.ID; }
        }
        public int VestColor;
        [XmlIgnore]
        public Parachute Parachute;
        public string ParachuteID
        {
            get { return Parachute.ID; }
        }
        #endregion Components
        #endregion Properties
        #region Constructors
        public Outfit(string name, Mask mask, Torso torso, Legs legs, Accessory accessory, int vestColor, Parachute parachute)
        {
            Name = name;
            Mask = mask;
            Torso = torso;
            Legs = legs;
            Accessory = accessory;
            VestColor = vestColor;
            Parachute = parachute;
        }
        #endregion Constructors
        public void ToPed(Ped ped)
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
        }
    }
}
