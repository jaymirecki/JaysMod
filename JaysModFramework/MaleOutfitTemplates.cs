using GTA;
using GTA.Native;
using System;
using JaysModFramework.MaleOutfitPieces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public static class MaleOutfitTemplates
    {
        public static Outfit Casual = new Outfit();
        public static Outfit Formal = new Outfit();
        public static Outfit Combat = new Outfit();
        public static Outfit Bike = new Outfit();
        public static Outfit Scuba = new Outfit();
        public static Outfit Beach = new Outfit();
        public static Outfit NavyCasual = new Outfit();
        public static Outfit NavyCombat = new Outfit();
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
            Combat.AccTwo = AccTwo.CombatVest;
            Combat.AccTwoColor = 1;
            Combat.Shirt = Shirts.Combat;
            Combat.Hat = Hats.CombatHelmetVisorUp;

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
            Beach.AccOne = AccOne.Casual;
            Beach.Shirt = Shirts.Beach;

            NavyCasual.Upper = Uppers.FatiguesCasual;
            NavyCasual.Lower = Lowers.Fatigues;
            NavyCasual.Shoes = Shoes.Fatigues;
            NavyCasual.AccOne = AccOne.Casual;
            NavyCasual.Shirt = Shirts.FatiguesCasual;
            NavyCasual.Hat = Hats.FatiguesCasual;

            NavyCombat = NavyCasual.Copy();
            NavyCombat.Upper = Uppers.FatiguesCombat;
            NavyCombat.AccTwo = AccTwo.MilitaryVest;
            NavyCombat.AccTwoColor = 5;
            NavyCombat.Shirt = Shirts.FatiguesCombat;
            NavyCombat.Hat = Hats.CombatHelmetVisorUp;
            NavyCombat.HatColor = 18;

            TestPilot.Beard = Beards.Scuba;
            TestPilot.Upper = Uppers.TestPilot;
            TestPilot.UpperColor = 3;
            TestPilot.Lower = Lowers.TestPilot;
            TestPilot.LowerColor = 1;
            TestPilot.Shoes = Shoes.TestPilot;
            TestPilot.ShoesColor = 3;
            TestPilot.AccOne = AccOne.Casual;
            TestPilot.Shirt = Shirts.TestPilot;
            TestPilot.ShirtColor = 1;
            TestPilot.Hat = Hats.TestPilot;
            TestPilot.HatColor = 5;

            PilotCasual = NavyCasual.Copy();
            PilotCasual.Lower = Lowers.Casual;
            PilotCasual.Shoes = Shoes.PilotCasual;
            //PilotCasual.Teeth = Lowers.PilotCasual;
            PilotCasual.Shirt = Shirts.PilotCasual;
            PilotCasual.ShirtColor = 5;
            PilotCasual.Watch = Watches.PilotCasual;
        }
    }
}
