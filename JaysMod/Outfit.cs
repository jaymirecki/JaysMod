﻿using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysMod
{
    #region HeadOverlay
    enum Blemishes
    {
        Default = 0,
    }

    enum FacialHair
    {
        Default = 0,
    }
    enum Eyebrows
    {
        Default = 0,
    }
    #endregion HeadOverlay
    #region OutfitComponents
    enum Beards
    {
        Default = 0,
        Combat = 35,
        Scuba = 122,
    }
    enum Hair
    {
        Default = 19,
    }
    enum Uppers
    {
        Default = 0,
        Formal = 1,
        Combat = 17,
        Bike = 31,
        Scuba = 96,
        Beach = 15,
        FatiguesCasual = 11,
        FatiguesCombat = 152,
        TestPilot = 165,
    }
    enum Lowers
    {
        Default = 0,
        Casual = 10,
        Formal = 28,
        Combat = 33,
        Bike = 4,
        Scuba = 94,
        Beach = 6,
        Fatigues = 87,
        TestPilot = 92,
        PilotCasual = 10,
    }
    enum Hands
    {
        Default = 0,
    }
    enum Shoes
    {
        Default = 0,
        Casual = 1,
        Formal = 3,
        Combat = 24,
        Bike = 4,
        Scuba = 67,
        Beach = 34,
        Fatigues = 35,
        TestPilot = 87,
        PilotCasual = 10,
    }
    enum AccOne
    {
        Default = 0,
        Casual = 15,
        Formal = 73,
        Combat = 15,
        Bike = 142,
        Scuba = 123,
        Fatigues = 15,
    }
    enum AccTwo
    {
        Default = 0,
        Combat = 11,
        FatiguesCombat = 126,
    }
    enum Shirts
    {
        Default = 0,
        Casual = 16,
        Formal = 11,
        Combat = 49,
        Bike = 64,
        Beach = 252,
        FatiguesCasual = 222,
        FatiguesCombat = 221,
        TestPilot = 228,
        PilotCasual = 318,
    }
    #endregion OutfitComponents
    #region Props
    enum Hats
    {
        Default = -1,
        Combat = 117,
        Bike = 70,
        FatiguesCasual = 107,
        FatiguesCombat = 117,
        TestPilot = 111,
    }
    enum Glasses
    {
        Default = -1,
        Scuba = 26,
    }
    enum Ears
    {
        Default = -1,
        Casual = 2,
    }
    enum Watches
    {
        Default = -1,
        Casual = 1,
        PilotCasual = 10,
    }
    #endregion Props
    #region SlotEnums
    public enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    public enum OutfitComponents { Beard = 1, Hair = 2, Upper = 3, Lower = 4, Hands = 5, Shoes = 6, AccOne = 8, AccTwo = 9, Shirt = 11 };
    public enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    #endregion SlotEnums
    class Outfit
    {
        public Blemishes Blemishes = Blemishes.Default;
        public FacialHair FacialHair = FacialHair.Default;
        public Eyebrows Eyebrows = Eyebrows.Default;
        public Beards Beard = Beards.Default;
        public Hair Hair = Hair.Default;
        public Uppers Upper = Uppers.Default;
        public Lowers Lower = Lowers.Default;
        public Hands Hands = Hands.Default;
        public Shoes Shoes = Shoes.Default;
        public AccOne AccOne = AccOne.Default;
        public AccTwo AccTwo = AccTwo.Default;
        public Shirts Shirt = Shirts.Default;
        public Hats Hat = Hats.Default;
        public Glasses Glasses = Glasses.Default;
        public Ears Ears = Ears.Default;
        public Watches Watch = Watches.Default;
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

        public Outfit() { }
        public Outfit(
            Beards beard, int beardColor, Hair hair, int hairColor, Uppers upper, int upperColor, 
            Lowers lower, int lowerColor, Hands hands, int handsColor, Shoes shoes, int shoesColor, 
            AccOne accOne, int accOneColor, AccTwo accTwo, int accTwoColor, Shirts shirt, int shirtColor, 
            Hats hat, int hatColor, Glasses glasses, int glassesColor, Ears ears, int earsColor, Watches watch, int watchColor
            )
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

        public void ApplyToPed(Ped ped, bool preserveHair)
        {
            Hair hair = Hair;
            int hairColor = HairColor;
            if (preserveHair)
            {
                hair = GetComponent<Hair>(ped, OutfitComponents.Hair);
                hairColor = GetComponentColor(ped, OutfitComponents.Hair);
            }

            // Set components
            JaysMod.Debug(Shirt);
            SetComponent(ped, OutfitComponents.Beard, (int)Beard, BeardColor);
            SetComponent(ped, OutfitComponents.Hair, (int)Hair, HairColor);
            SetHairColor(ped, HairColor);
            SetComponent(ped, OutfitComponents.Upper, (int)Upper, UpperColor);
            SetComponent(ped, OutfitComponents.Lower, (int)Lower, LowerColor);
            SetComponent(ped, OutfitComponents.Hands, (int)Hands, HandsColor);
            SetComponent(ped, OutfitComponents.Shoes, (int)Shoes, ShoesColor);
            SetComponent(ped, OutfitComponents.AccOne, (int)AccOne, AccOneColor);
            SetComponent(ped, OutfitComponents.AccTwo, (int)AccTwo, AccTwoColor);
            SetComponent(ped, OutfitComponents.Shirt, (int)Shirt, ShirtColor);

            // Clear props
            Function.Call(Hash.CLEAR_ALL_PED_PROPS, ped);
            Function.Call(Hash.SET_NIGHTVISION, false);

            // Set props
            SetProp(ped, Props.Hat, (int)Hat, HatColor);
            SetProp(ped, Props.Glasses, (int)Glasses, GlassesColor);
            SetProp(ped, Props.Ears, (int)Ears, EarsColor);
            SetProp(ped, Props.Watch, (int)Watch, WatchColor);
        }
        public void Save(SaveAndLoad save, string saveId, string prefix)
        {
            string myPrefix = prefix + "_outfit";
            save.Save(saveId, myPrefix + "_beard", Beard);
            save.Save(saveId, myPrefix + "_hair", Hair);
            save.Save(saveId, myPrefix + "_upper", Upper);
            save.Save(saveId, myPrefix + "_lower", Lower);
            save.Save(saveId, myPrefix + "_hands", Hands);
            save.Save(saveId, myPrefix + "_shoes", Shoes);
            save.Save(saveId, myPrefix + "_accone", AccOne);
            save.Save(saveId, myPrefix + "_acctwo", AccTwo);
            save.Save(saveId, myPrefix + "_shirt", Shirt);
            save.Save(saveId, myPrefix + "_hat", Hat);
            save.Save(saveId, myPrefix + "_glasses", Glasses);
            save.Save(saveId, myPrefix + "_ears", Ears);
            save.Save(saveId, myPrefix + "_watch", Watch);
            save.Save(saveId, myPrefix + "_beardColor", BeardColor);
            save.Save(saveId, myPrefix + "_hairColor", HairColor);
            save.Save(saveId, myPrefix + "_upperColor", UpperColor);
            save.Save(saveId, myPrefix + "_lowerColor", LowerColor);
            save.Save(saveId, myPrefix + "_handsColor", HandsColor);
            save.Save(saveId, myPrefix + "_shoesColor", ShoesColor);
            save.Save(saveId, myPrefix + "_acconeColor", AccOneColor);
            save.Save(saveId, myPrefix + "_acctwoColor", AccTwoColor);
            save.Save(saveId, myPrefix + "_shirtColor", ShirtColor);
            save.Save(saveId, myPrefix + "_hatColor", HatColor);
            save.Save(saveId, myPrefix + "_glassesColor", GlassesColor);
            save.Save(saveId, myPrefix + "_earsColor", EarsColor);
            save.Save(saveId, myPrefix + "_watchColor", WatchColor);
        }

        public void Load(SaveAndLoad load, string saveId, string prefix)
        {
            string myPrefix = prefix + "_outfit";
            Beard = load.Load(saveId, myPrefix + "_beard", Beard);
            Hair = load.Load(saveId, myPrefix + "_hair", Hair);
            Upper = load.Load(saveId, myPrefix + "_upper", Upper);
            Lower = load.Load(saveId, myPrefix + "_lower", Lower);
            Hands = load.Load(saveId, myPrefix + "_hands", Hands);
            Shoes = load.Load(saveId, myPrefix + "_shoes", Shoes);
            AccOne = load.Load(saveId, myPrefix + "_accone", AccOne);
            AccTwo = load.Load(saveId, myPrefix + "_acctwo", AccTwo);
            Shirt = load.Load(saveId, myPrefix + "_shirt", Shirt);
            Hat = load.Load(saveId, myPrefix + "_hat", Hat);
            Glasses = load.Load(saveId, myPrefix + "_glasses", Glasses);
            Ears = load.Load(saveId, myPrefix + "_ears", Ears);
            Watch = load.Load(saveId, myPrefix + "_watch", Watch);
            BeardColor = load.Load(saveId, myPrefix + "_beardColor", BeardColor);
            HairColor = load.Load(saveId, myPrefix + "_hairColor", HairColor);
            UpperColor = load.Load(saveId, myPrefix + "_upperColor", UpperColor);
            LowerColor = load.Load(saveId, myPrefix + "_lowerColor", LowerColor);
            HandsColor = load.Load(saveId, myPrefix + "_handsColor", HandsColor);
            ShoesColor = load.Load(saveId, myPrefix + "_shoesColor", ShoesColor);
            AccOneColor = load.Load(saveId, myPrefix + "_acconeColor", AccOneColor);
            AccTwoColor = load.Load(saveId, myPrefix + "_acctwoColor", AccTwoColor);
            ShirtColor = load.Load(saveId, myPrefix + "_shirtColor", ShirtColor);
            HatColor = load.Load(saveId, myPrefix + "_hatColor", HatColor);
            GlassesColor = load.Load(saveId, myPrefix + "_glassesColor", GlassesColor);
            EarsColor = load.Load(saveId, myPrefix + "_earsColor", EarsColor);
            WatchColor = load.Load(saveId, myPrefix + "_watchColor", WatchColor);
        }

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
        private static T GetComponent<T>(Ped ped, OutfitComponents componentId)
        {
            return Function.Call<T>(Hash.GET_PED_DRAWABLE_VARIATION, ped, (int)componentId);
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
        private static T GetProp<T>(Ped ped, Props propId)
        {
            return Function.Call<T>(Hash.GET_PED_PROP_INDEX, ped, (int)propId);
        }
        #endregion Set/Get Props
    }

    static class OutfitTemplates
    {
        public static Outfit Casual = new Outfit();
        public static Outfit Formal = new Outfit();
        public static Outfit Combat = new Outfit();
        public static Outfit Bike = new Outfit();
        public static Outfit Scuba = new Outfit();
        public static Outfit Beach = new Outfit();
        public static Outfit FatiguesCasual = new Outfit();
        public static Outfit FatiguesCombat = new Outfit();
        public static Outfit TestPilot = new Outfit();
        public static Outfit PilotCasual = new Outfit();

        public static void SetupOutfits()
        {
            Casual.Lower = Lowers.Casual;
            Casual.Shoes = Shoes.Casual;
            Casual.AccOne = AccOne.Casual;
            Casual.Shirt = Shirts.Casual;
            Casual.ShirtColor = 1;
            Casual.Ears = Ears.Casual;
            Casual.Watch = Watches.Casual;

            Formal.Upper = Uppers.Formal;
            Formal.Lower = Lowers.Formal;
            Formal.LowerColor = 8;
            Formal.Shoes = Shoes.Formal;
            Formal.AccOne = AccOne.Formal;
            Formal.AccOneColor = 1;
            Formal.Shirt = Shirts.Formal;
            Formal.ShirtColor = 1;
            Formal.Ears = Ears.Casual;

            Combat.Beard = Beards.Combat;
            Combat.Upper = Uppers.Combat;
            Combat.Lower = Lowers.Combat;
            Combat.Shoes = Shoes.Combat;
            Combat.AccOne = AccOne.Combat;
            Combat.AccTwo = AccTwo.Combat;
            Combat.AccTwoColor = 1;
            Combat.Shirt = Shirts.Combat;
            Combat.Hat = Hats.Combat;

            Bike.Upper = Uppers.Bike;
            Bike.Lower = Lowers.Bike;
            Bike.LowerColor = 4;
            Bike.Shoes = Shoes.Bike;
            Bike.ShoesColor = 2;
            Bike.AccOne = AccOne.Bike;
            Bike.Shirt = Shirts.Bike;
            Bike.Hat = Hats.Bike;

            Scuba.Beard = Beards.Scuba;
            Scuba.Upper = Uppers.Scuba;
            Scuba.Lower = Lowers.Scuba;
            Scuba.Shoes = Shoes.Scuba;
            Scuba.AccOne = AccOne.Scuba;
            Scuba.Shirt = Shirts.Combat;
            Scuba.Glasses = Glasses.Scuba;

            Beach.Upper = Uppers.Beach;
            Beach.Lower = Lowers.Beach;
            Beach.LowerColor = 1;
            Beach.Shoes = Shoes.Beach;
            Beach.AccOne = AccOne.Fatigues;
            Beach.Shirt = Shirts.Beach;

            FatiguesCasual.Upper = Uppers.FatiguesCasual;
            FatiguesCasual.Lower = Lowers.Fatigues;
            FatiguesCasual.Shoes = Shoes.Fatigues;
            FatiguesCasual.AccOne = AccOne.Fatigues;
            FatiguesCasual.Shirt = Shirts.FatiguesCasual;
            FatiguesCasual.Hat = Hats.FatiguesCasual;

            FatiguesCombat = FatiguesCasual.Copy();
            FatiguesCombat.Upper = Uppers.FatiguesCombat;
            FatiguesCombat.AccTwo = AccTwo.FatiguesCombat;
            FatiguesCombat.AccTwoColor = 5;
            FatiguesCombat.Shirt = Shirts.FatiguesCombat;
            FatiguesCombat.Hat = Hats.FatiguesCombat;
            FatiguesCombat.HatColor = 18;

            TestPilot.Beard = Beards.Scuba;
            TestPilot.Upper = Uppers.TestPilot;
            TestPilot.UpperColor = 3;
            TestPilot.Lower = Lowers.TestPilot;
            TestPilot.LowerColor = 1;
            TestPilot.Shoes = Shoes.TestPilot;
            TestPilot.ShoesColor = 3;
            TestPilot.AccOne = AccOne.Fatigues;
            TestPilot.Shirt = Shirts.TestPilot;
            TestPilot.ShirtColor = 1;
            TestPilot.Hat = Hats.TestPilot;
            TestPilot.HatColor = 5;

            PilotCasual = FatiguesCasual.Copy();
            PilotCasual.Lower = Lowers.PilotCasual;
            PilotCasual.Shoes = Shoes.PilotCasual;
            //PilotCasual.Teeth = Lowers.PilotCasual;
            PilotCasual.Shirt = Shirts.PilotCasual;
            PilotCasual.ShirtColor = 5;
            PilotCasual.Watch = Watches.PilotCasual;
        }
    }
}
