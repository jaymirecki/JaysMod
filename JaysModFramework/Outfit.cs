using GTA;
using GTA.Native;
using System;
using JaysModFramework.MaleOutfitPieces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JaysModFramework
{
    #region SlotEnums
    public enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    public enum OutfitComponents { Beard = 1, Hair = 2, Upper = 3, Lower = 4, Hands = 5, Shoes = 6, AccOne = 8, AccTwo = 9, Shirt = 11 };
    public enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    #endregion SlotEnums
    public class Outfit: IXmlSerializable
    {
        #region Properties
        public MaleOutfitTemplateIDs Template;
        public int Blemishes = (int)MaleOutfitPieces.Blemishes.Default;
        public int FacialHair = (int)MaleOutfitPieces.FacialHair.Default;
        public int Eyebrows = (int)MaleOutfitPieces.Eyebrows.Default;
        public int Beard = (int)MaleOutfitPieces.Masks.Default;
        public void SetBeard(MaleOutfitPieces.Masks beard) { Beard = (int)beard; }
        public int Hair = (int)MaleOutfitPieces.Hair.Default;
        public void SetHair(Hair hair) { Hair = (int)hair; }
        public int Upper = (int)MaleOutfitPieces.Torsos.Default;
        public void SetUpper(MaleOutfitPieces.Torsos upper) { Upper = (int)upper; }
        public int Lower = (int)MaleOutfitPieces.Legs.Default;
        public void SetLower(MaleOutfitPieces.Legs lower) { Lower = (int)lower; }
        public int Hands = (int)MaleOutfitPieces.Bags.Default;
        public void SetHands(Bags hands) { Hands = (int)hands; }
        public int Shoes = (int)MaleOutfitPieces.Shoes.Default;
        public void SetShoes(MaleOutfitPieces.Shoes shoes) { Shoes = (int)shoes; }
        public int AccOne = (int)MaleOutfitPieces.Undershirts.Default;
        public void SetAccOne(MaleOutfitPieces.Undershirts accOne) { AccOne = (int)accOne; }
        public int AccTwo = (int)MaleOutfitPieces.Armors.Default;
        public void SetAccTwo(MaleOutfitPieces.Armors accTwo) { AccTwo = (int)accTwo; }
        public int Shirt = (int)MaleOutfitPieces.Shirts.Default;
        public void SetShirt(MaleOutfitPieces.Shirts shirt) { Shirt = (int)shirt; }
        public int Hat = (int)MaleOutfitPieces.Hats.Default;
        public void SetHat(MaleOutfitPieces.Hats hat) { Hat = (int)hat; }
        public int Glasses = (int)MaleOutfitPieces.Glasses.Default;
        public void SetGlasses(MaleOutfitPieces.Glasses glasses) { Glasses = (int)glasses; }
        public int Ears = (int)MaleOutfitPieces.Ears.Default;
        public void SetEars(MaleOutfitPieces.Ears ears) { Ears = (int)ears; }
        public int Watch = (int)MaleOutfitPieces.Watches.Default;
        public void SetWatch(MaleOutfitPieces.Watches watch) { Watch = (int)watch; }
        #endregion
        #region Colors
        public int BeardColor = 0;
        public int HairColor = 1;
        public int UpperColor = 0;
        public int LowerColor = 0;
        public int HandsColor = 0;
        public int ShoesColor = 0;
        public int AccOneColor = 0;
        public int AccTwoColor = 0;
        public int ShirtColor = 0;
        public int HatColor = 0;
        public int GlassesColor = 0;
        public int EarsColor = 0;
        public int WatchColor = 0;
        #endregion
        #region Constructors
        public Outfit() {

        }
        public Outfit(
            Masks beard, int beardColor, Hair hair, int hairColor, Torsos upper, int upperColor,
            Legs lower, int lowerColor, Bags hands, int handsColor, Shoes shoes, int shoesColor,
            Undershirts accOne, int accOneColor, Armors accTwo, int accTwoColor, Shirts shirt, int shirtColor,
            Hats hat, int hatColor, Glasses glasses, int glassesColor, Ears ears, int earsColor, Watches watch, int watchColor
            ):this(
                (int)beard, beardColor, (int)hair, hairColor, (int)upper, upperColor,
                (int)lower, lowerColor, (int)hands, handsColor, (int)shoes, shoesColor,
                (int)accOne, accOneColor, (int)accTwo, accTwoColor, (int)shirt, shirtColor,
                (int)hat, hatColor, (int)glasses, glassesColor, (int)ears, earsColor, (int)watch, watchColor)
        { }
        public Outfit(
            int beard, int beardColor, int hair, int hairColor, int upper, int upperColor,
            int lower, int lowerColor, int hands, int handsColor, int shoes, int shoesColor,
            int accOne, int accOneColor, int accTwo, int accTwoColor, int shirt, int shirtColor,
            int hat, int hatColor, int glasses, int glassesColor, int ears, int earsColor, int watch, int watchColor
            ): this()
        {
            Beard = beard;
            Hair = hair;
            Upper = upper;
            Lower = lower;
            Hands = hands;
            Shoes = shoes;
            AccOne = accOne;
            AccTwo = accTwo;
            Shirt = shirt;
            Hat = hat;
            Glasses = glasses;
            Ears = ears;
            Watch = watch;

            BeardColor = beardColor;
            HairColor = hairColor;
            UpperColor = upperColor;
            LowerColor = lowerColor;
            HandsColor = handsColor;
            ShoesColor = shoesColor;
            AccOneColor = accOneColor;
            AccTwoColor = accTwoColor;
            ShirtColor = shirtColor;
            HatColor = hatColor;
            GlassesColor = glassesColor;
            EarsColor = earsColor;
            WatchColor = watchColor;
        }
        #endregion
        public Outfit Copy()
        {
            Outfit copy = 
                new Outfit(
                    Beard, BeardColor, Hair, HairColor, Upper, UpperColor,
                    Lower, LowerColor, Hands, HandsColor, Shoes, ShoesColor,
                    AccOne, AccOneColor, AccTwo, AccTwoColor, Shirt, ShirtColor,
                    Hat, HatColor, Glasses, GlassesColor, Ears, EarsColor, Watch, WatchColor);
            return copy;
        }
        private void CopyFrom(Outfit other)
        {
            Beard = other.Beard;
            BeardColor = other.BeardColor;
            Hair = other.Hair;
            HairColor = other.HairColor;
            Upper = other.Upper;
            UpperColor = other.UpperColor;
            Lower = other.Lower;
            LowerColor = other.LowerColor;
            Hands = other.Hands;
            HandsColor = other.HandsColor;
            Shoes = other.Shoes;
            ShoesColor = other.ShoesColor;
            AccOne = other.AccOne;
            AccOneColor = other.AccOneColor;
            AccTwo = other.AccTwo;
            AccTwoColor = other.AccTwoColor;
            Shirt = other.Shirt;
            ShirtColor = other.ShirtColor;
            Hat = other.Hat;
            HatColor = other.HatColor;
            Glasses = other.Glasses;
            GlassesColor = other.GlassesColor;
            Ears = other.Ears;
            EarsColor = other.EarsColor;
            Watch = other.Watch;
            WatchColor = other.WatchColor;
        }
        #region Ped Management
        public void ApplyToPed(Ped ped, bool preserveHair)
        {
            int hair = Hair;
            int hairColor = HairColor;
            if (preserveHair)
            {
                hair = GetComponent(ped, OutfitComponents.Hair);
                hairColor = GetComponentColor(ped, OutfitComponents.Hair);
            }
            if (Beard == (int)Masks.ScubaHood)
            {
                hair = 0;
            }
            // Set components
            SetComponent(ped, OutfitComponents.Beard, Beard, BeardColor);
            SetComponent(ped, OutfitComponents.Hair, hair, hairColor);
            SetHairColor(ped, hairColor);
            SetComponent(ped, OutfitComponents.Upper, Upper, UpperColor);
            SetComponent(ped, OutfitComponents.Lower, Lower, LowerColor);
            SetComponent(ped, OutfitComponents.Hands, Hands, HandsColor);
            SetComponent(ped, OutfitComponents.Shoes, Shoes, ShoesColor);
            SetComponent(ped, OutfitComponents.AccOne, AccOne, AccOneColor);
            SetComponent(ped, OutfitComponents.AccTwo, AccTwo, AccTwoColor);
            SetComponent(ped, OutfitComponents.Shirt, Shirt, ShirtColor);

            // Clear props
            Function.Call(Hash.CLEAR_ALL_PED_PROPS, ped);
            Function.Call(Hash.SET_NIGHTVISION, false);

            // Set props
            SetProp(ped, Props.Hat, Hat, HatColor);
            SetProp(ped, Props.Glasses, Glasses, GlassesColor);
            SetProp(ped, Props.Ears, Ears, EarsColor);
            SetProp(ped, Props.Watch, Watch, WatchColor);
        }
        public void FromPed(Ped ped)
        {
            Beard = GetComponent(ped, OutfitComponents.Beard);
            Hair = GetComponent(ped, OutfitComponents.Hair);
            Upper = GetComponent(ped, OutfitComponents.Upper);
            Lower = GetComponent(ped, OutfitComponents.Lower);
            Hands = GetComponent(ped, OutfitComponents.Hands);
            Shoes = GetComponent(ped, OutfitComponents.Shoes);
            AccOne = GetComponent(ped, OutfitComponents.AccOne);
            AccTwo = GetComponent(ped, OutfitComponents.AccTwo);
            Shirt = GetComponent(ped, OutfitComponents.Shirt);
            Hat = GetProp(ped, Props.Hat);
            Glasses = GetProp(ped, Props.Glasses);
            Ears = GetProp(ped, Props.Ears);
            Watch = GetProp(ped, Props.Watch);

            BeardColor = GetComponentColor(ped, OutfitComponents.Beard);
            HairColor = GetComponentColor(ped, OutfitComponents.Hair);
            UpperColor = GetComponentColor(ped, OutfitComponents.Upper);
            LowerColor = GetComponentColor(ped, OutfitComponents.Lower);
            HandsColor = GetComponentColor(ped, OutfitComponents.Hands);
            ShoesColor = GetComponentColor(ped, OutfitComponents.Shoes);
            AccOneColor = GetComponentColor(ped, OutfitComponents.AccOne);
            AccTwoColor = GetComponentColor(ped, OutfitComponents.AccTwo);
            ShirtColor = GetComponentColor(ped, OutfitComponents.Shirt);
            HatColor = GetPropColor(ped, Props.Hat);
            GlassesColor = GetPropColor(ped, Props.Glasses);
            EarsColor = GetPropColor(ped, Props.Ears);
            WatchColor = GetPropColor(ped, Props.Watch);
        }
        #endregion
        #region String Management
        public override string ToString()
        {
            string str =
                Beard.ToString() + "," +
                Hair.ToString() + "," +
                Upper.ToString() + "," +
                Lower.ToString() + "," +
                Hands.ToString() + "," +
                Shoes.ToString() + "," +
                AccOne.ToString() + "," +
                AccTwo.ToString() + "," +
                Shirt.ToString() + "," +
                Hat.ToString() + "," +
                Glasses.ToString() + "," +
                Ears.ToString() + "," +
                Watch.ToString() + "," +
                BeardColor.ToString() + "," +
                HairColor.ToString() + "," +
                UpperColor.ToString() + "," +
                LowerColor.ToString() + "," +
                HandsColor.ToString() + "," +
                ShoesColor.ToString() + "," +
                AccOneColor.ToString() + "," +
                AccTwoColor.ToString() + "," +
                ShirtColor.ToString() + "," +
                HatColor.ToString() + "," +
                GlassesColor.ToString() + "," +
                EarsColor.ToString() + "," +
                WatchColor.ToString();

            return str;
        }

        private void FromString(string str)
        {
            string[] pieces = str.Split(',');
            Beard = ParseNumOrDefault(pieces[0], Masks.Default);
            Hair = ParseNumOrDefault(pieces[1], MaleOutfitPieces.Hair.Default);
            Upper = ParseNumOrDefault(pieces[2], Torsos.Default);
            Lower = ParseNumOrDefault(pieces[3], Legs.Default);
            Hands = ParseNumOrDefault(pieces[4], MaleOutfitPieces.Bags.Default);
            Shoes = ParseNumOrDefault(pieces[5], MaleOutfitPieces.Shoes.Default);
            AccOne = ParseNumOrDefault(pieces[6], MaleOutfitPieces.Undershirts.Default);
            AccTwo = ParseNumOrDefault(pieces[7], MaleOutfitPieces.Armors.Default);
            Shirt = ParseNumOrDefault(pieces[8], Shirts.Default);
            Hat = ParseNumOrDefault(pieces[9], Hats.Default);
            Glasses = ParseNumOrDefault(pieces[10], MaleOutfitPieces.Glasses.Default);
            Ears = ParseNumOrDefault(pieces[11], MaleOutfitPieces.Ears.Default);
            Watch = ParseNumOrDefault(pieces[12], Watches.Default);
            BeardColor = int.Parse(pieces[13]);
            HairColor = int.Parse(pieces[14]);
            UpperColor = int.Parse(pieces[15]);
            LowerColor = int.Parse(pieces[16]);
            HandsColor = int.Parse(pieces[17]);
            ShoesColor = int.Parse(pieces[18]);
            AccOneColor = int.Parse(pieces[19]);
            AccTwoColor = int.Parse(pieces[20]);
            ShirtColor = int.Parse(pieces[21]);
            HatColor = int.Parse(pieces[22]);
            GlassesColor = int.Parse(pieces[23]);
            EarsColor = int.Parse(pieces[24]);
            WatchColor = int.Parse(pieces[25]);
        }
        private TEnum ParseEnumOrDefault<TEnum>(string str, TEnum def) where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse(str, out result))
            {
                return result;
            }
            return def;
        }
        private int ParseNumOrDefault(string str, int def)
        {
            int result;
            if (int.TryParse(str, out result))
            {
                return result;
            }
            return def;
        }
        private int ParseNumOrDefault<TEnum>(string str, TEnum def) where TEnum: struct
        {
            return ParseNumOrDefault(str, (int)(object)def);
        }
        #endregion
        #region Set/Get Components
        private static void SetComponent(Ped ped, OutfitComponents slot, int component, int color)
        {
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, slot, component, color, 2);
        }
        private static void SetHairColor(Ped ped, int color)
        {
            Function.Call(Hash._SET_PED_HAIR_COLOR, ped, color, color);
        }
        internal static int GetComponent(Ped ped, OutfitComponents componentId)
        {
            return Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, (int)componentId);
        }
        private static int GetComponentColor(Ped ped, OutfitComponents componentId)
        {
            return Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, (int)componentId);
        }
        #endregion Set/Get Components
        #region Set/Get Props
        private static void SetProp(Ped ped, Props propId, int prop, int color)
        {
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, propId, prop, color, 2);
        }
        private static int GetProp(Ped ped, Props propId)
        {
            return Function.Call<int>(Hash.GET_PED_PROP_INDEX, ped, (int)propId);
        }
        private static int GetPropColor(Ped ped, Props propId)
        {
            return Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, ped, (int)propId);
        }
        #endregion Set/Get Props
        #region XML
        public void WriteXml(XmlWriter writer)
        {
            //XmlSerialization.WriteEnumElement(writer, "Beard", (Beards)Beard);
            //XmlSerialization.WriteEnumElement(writer, "Hair", (Hair)Hair);
            //XmlSerialization.WriteEnumElement(writer, "Upper", (Uppers)Upper);
            //XmlSerialization.WriteEnumElement(writer, "Lower", (Lowers)Lower);
            //XmlSerialization.WriteEnumElement(writer, "Hands", (Hands)Hands);
            //XmlSerialization.WriteEnumElement(writer, "Shoes", (Shoes)Shoes);
            //XmlSerialization.WriteEnumElement(writer, "AccOne", (AccOne)AccOne);
            //XmlSerialization.WriteEnumElement(writer, "AccTwo", (AccTwo)AccTwo);
            //XmlSerialization.WriteEnumElement(writer, "Shirt", (Shirts)Shirt);
            //XmlSerialization.WriteEnumElement(writer, "Hat", (Hats)Hat);
            //XmlSerialization.WriteEnumElement(writer, "Glasses", (Glasses)Glasses);
            //XmlSerialization.WriteEnumElement(writer, "Ears", (Ears)Ears);
            //XmlSerialization.WriteEnumElement(writer, "Watch", (Watches)Watch);
            //XmlSerialization.WriteElement(writer, "BeardColor", BeardColor);
            //XmlSerialization.WriteElement(writer, "HairColor", HairColor);
            //XmlSerialization.WriteElement(writer, "UpperColor", UpperColor);
            //XmlSerialization.WriteElement(writer, "LowerColor", LowerColor);
            //XmlSerialization.WriteElement(writer, "HandsColor", HandsColor);
            //XmlSerialization.WriteElement(writer, "ShoesColor", ShoesColor);
            //XmlSerialization.WriteElement(writer, "AccOneColor", AccOneColor);
            //XmlSerialization.WriteElement(writer, "AccTwoColor", AccTwoColor);
            //XmlSerialization.WriteElement(writer, "ShirtColor", ShirtColor);
            //XmlSerialization.WriteElement(writer, "HatColor", HatColor);
            //XmlSerialization.WriteElement(writer, "GlassesColor", GlassesColor);
            //XmlSerialization.WriteElement(writer, "EarsColor", EarsColor);
            //XmlSerialization.WriteElement(writer, "WatchColor", WatchColor);
            XmlSerialization.WriteEnumElement(writer, "Template", Template);
        }
        public void ReadXml(XmlReader reader)
        {
            //Debug.Log("reading outfit");
            //reader.ReadStartElement("OutfitString");
            //FromString(reader.ReadContentAsString());
            //reader.ReadEndElement();
            //Debug.Log("outfit read");

            //SetBeard(XmlSerialization.ReadEnumElement<Beards>(reader, "Beard"));
            //SetHair(XmlSerialization.ReadEnumElement<Hair>(reader, "Hair"));
            //SetUpper(XmlSerialization.ReadEnumElement<Uppers>(reader, "Upper"));
            //SetLower(XmlSerialization.ReadEnumElement<Lowers>(reader, "Lower"));
            //SetHands(XmlSerialization.ReadEnumElement<Hands>(reader, "Hands"));
            //SetShoes(XmlSerialization.ReadEnumElement<Shoes>(reader, "Shoes"));
            //SetAccOne(XmlSerialization.ReadEnumElement<AccOne>(reader, "AccOne"));
            //SetAccTwo(XmlSerialization.ReadEnumElement<AccTwo>(reader, "AccTwo"));
            //SetShirt(XmlSerialization.ReadEnumElement<Shirts>(reader, "Shirt"));
            //SetHat(XmlSerialization.ReadEnumElement<Hats>(reader, "Hat"));
            //SetGlasses(XmlSerialization.ReadEnumElement<Glasses>(reader, "Glasses"));
            //SetEars(XmlSerialization.ReadEnumElement<Ears>(reader, "Ears"));
            //SetWatch(XmlSerialization.ReadEnumElement<Watches>(reader, "Watch"));
            //BeardColor = XmlSerialization.ReadElement<int>(reader, "BeardColor");
            //HairColor = XmlSerialization.ReadElement<int>(reader, "HairColor");
            //UpperColor = XmlSerialization.ReadElement<int>(reader, "UpperColor");
            //LowerColor = XmlSerialization.ReadElement<int>(reader, "LowerColor");
            //HandsColor = XmlSerialization.ReadElement<int>(reader, "HandsColor");
            //ShoesColor = XmlSerialization.ReadElement<int>(reader, "ShoesColor");
            //AccOneColor = XmlSerialization.ReadElement<int>(reader, "AccOneColor");
            //AccTwoColor = XmlSerialization.ReadElement<int>(reader, "AccTwoColor");
            //ShirtColor = XmlSerialization.ReadElement<int>(reader, "ShirtColor");
            //HatColor = XmlSerialization.ReadElement<int>(reader, "HatColor");
            //GlassesColor = XmlSerialization.ReadElement<int>(reader, "GlassesColor");
            //EarsColor = XmlSerialization.ReadElement<int>(reader, "EarsColor");
            //WatchColor = XmlSerialization.ReadElement<int>(reader, "WatchColor");
            Template = XmlSerialization.ReadEnumElement<MaleOutfitTemplateIDs>(reader, "Template");
            SetFromTemplate(Template);
        }
        private void SetFromTemplate(MaleOutfitTemplateIDs template)
        {
            switch (template)
            {
                case MaleOutfitTemplateIDs.Casual:
                    CopyFrom(MaleOutfitTemplates.Casual);
                    break;
                case MaleOutfitTemplateIDs.Formal:
                    CopyFrom(MaleOutfitTemplates.Formal);
                    break;
                case MaleOutfitTemplateIDs.Combat:
                    CopyFrom(MaleOutfitTemplates.Combat);
                    break;
                case MaleOutfitTemplateIDs.Bike:
                    CopyFrom(MaleOutfitTemplates.Bike);
                    break;
                case MaleOutfitTemplateIDs.Scuba:
                    CopyFrom(MaleOutfitTemplates.Scuba);
                    break;
                case MaleOutfitTemplateIDs.ScubaLand:
                    CopyFrom(MaleOutfitTemplates.ScubaLand);
                    break;
                case MaleOutfitTemplateIDs.Beach:
                    CopyFrom(MaleOutfitTemplates.Beach);
                    break;
                case MaleOutfitTemplateIDs.NavyCasual:
                    CopyFrom(MaleOutfitTemplates.NavyCasual);
                    break;
                case MaleOutfitTemplateIDs.NavyCombat:
                    CopyFrom(MaleOutfitTemplates.NavyCombat);
                    break;
                case MaleOutfitTemplateIDs.ArmyCasual:
                    CopyFrom(MaleOutfitTemplates.ArmyCasual);
                    break;
                case MaleOutfitTemplateIDs.ArmyCombat:
                    CopyFrom(MaleOutfitTemplates.ArmyCombat);
                    break;
                case MaleOutfitTemplateIDs.TestPilot:
                    CopyFrom(MaleOutfitTemplates.TestPilot);
                    break;
                case MaleOutfitTemplateIDs.PilotCasual:
                    CopyFrom(MaleOutfitTemplates.PilotCasual);
                    break;
                default:
                    CopyFrom(MaleOutfitTemplates.Casual);
                    break;
            }
        }
        #endregion
    }
}
