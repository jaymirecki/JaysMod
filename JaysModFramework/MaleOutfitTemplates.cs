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
        public static Outfit ScubaLand = new Outfit();
        public static Outfit Beach = new Outfit();
        public static Outfit NavyCasual = new Outfit();
        public static Outfit NavyCombat = new Outfit();
        public static Outfit ArmyCasual = new Outfit();
        public static Outfit ArmyCombat = new Outfit();
        public static Outfit TestPilot = new Outfit();
        public static Outfit PilotCasual = new Outfit();

        public static void SetupOutfits()
        {
            Casual.SetLower(Lowers.Casual);
            Casual.SetShoes(Shoes.Casual);
            Casual.SetAccOne(AccOne.Casual);
            Casual.SetShirt(Shirts.Casual);
            Casual.ShirtColor = 1;
            Casual.SetEars(Ears.Casual);
            Casual.SetWatch(Watches.Casual);

            Formal.SetUpper(Uppers.Formal);
            Formal.SetLower(Lowers.Formal);
            Formal.LowerColor = 8;
            Formal.SetShoes(Shoes.Formal);
            Formal.SetAccOne(AccOne.Formal);
            Formal.AccOneColor = 1;
            Formal.SetShirt(Shirts.Formal);
            Formal.ShirtColor = 1;
            Formal.SetEars(Ears.Casual);

            Combat.SetBeard(Beards.Combat);
            Combat.SetUpper(Uppers.Combat);
            Combat.SetLower(Lowers.Combat);
            Combat.SetShoes(Shoes.Combat);
            Combat.SetAccOne(AccOne.Combat);
            Combat.SetAccTwo(AccTwo.CombatVest);
            Combat.AccTwoColor = 1;
            Combat.SetShirt(Shirts.Combat);
            Combat.SetHat(Hats.CombatHelmetVisorUp);

            Bike.SetUpper(Uppers.Bike);
            Bike.SetLower(Lowers.Bike);
            Bike.LowerColor = 4;
            Bike.SetShoes(Shoes.Bike);
            Bike.ShoesColor = 2;
            Bike.SetAccOne(AccOne.Bike);
            Bike.SetShirt(Shirts.Bike);
            Bike.SetHat(Hats.MotorcycleVisorUp);
            Bike.HatColor = 9;

            Scuba.SetBeard(Beards.ScubaHood);
            Scuba.SetUpper(Uppers.Scuba);
            Scuba.SetLower(Lowers.Scuba);
            Scuba.SetShoes(Shoes.ScubaFlippers);
            Scuba.SetAccOne(AccOne.ScubaTank);
            Scuba.SetShirt(Shirts.Combat);
            Scuba.SetGlasses(Glasses.ScubaMask);

            ScubaLand = Scuba.Copy();
            ScubaLand.SetAccOne(AccOne.Casual);
            ScubaLand.SetShoes(Shoes.ScubaShoes);
            ScubaLand.SetGlasses(Glasses.Default);

            Beach.SetUpper(Uppers.Beach);
            Beach.SetLower(Lowers.Beach);
            Beach.LowerColor = 1;
            Beach.SetShoes(Shoes.Beach);
            Beach.SetAccOne(AccOne.Casual);
            Beach.SetShirt(Shirts.Beach);

            NavyCasual.SetUpper(Uppers.FatiguesCasual);
            NavyCasual.SetLower(Lowers.Fatigues);
            NavyCasual.SetShoes(Shoes.Fatigues);
            NavyCasual.SetAccOne(AccOne.Casual);
            NavyCasual.SetShirt(Shirts.FatiguesCasual);
            NavyCasual.SetHat(Hats.FatiguesCasual);

            NavyCombat = NavyCasual.Copy();
            NavyCombat.SetUpper(Uppers.FatiguesCombat);
            NavyCombat.SetAccTwo(AccTwo.MilitaryVest);
            NavyCombat.AccTwoColor = 5;
            NavyCombat.SetShirt(Shirts.FatiguesCombat);
            NavyCombat.SetHat(Hats.CombatHelmetVisorUp);
            NavyCombat.HatColor = 18;

            ArmyCasual = NavyCasual.Copy();
            ArmyCasual.UpperColor = ArmyCasual.LowerColor = ArmyCasual.ShirtColor = ArmyCasual.HatColor = 3;

            ArmyCombat = NavyCombat.Copy();
            ArmyCombat.UpperColor = ArmyCombat.LowerColor = ArmyCombat.ShirtColor = 3;
            ArmyCombat.AccTwoColor = 5;
            ArmyCombat.HatColor = 23;

            TestPilot.SetBeard(Beards.ScubaHood);
            TestPilot.SetUpper(Uppers.TestPilot);
            TestPilot.UpperColor = 3;
            TestPilot.SetLower(Lowers.TestPilot);
            TestPilot.LowerColor = 1;
            TestPilot.SetShoes(Shoes.TestPilot);
            TestPilot.ShoesColor = 3;
            TestPilot.SetAccOne(AccOne.Casual);
            TestPilot.SetShirt(Shirts.TestPilot);
            TestPilot.ShirtColor = 1;
            TestPilot.SetHat(Hats.TestPilot);
            TestPilot.HatColor = 5;

            PilotCasual = NavyCasual.Copy();
            PilotCasual.SetLower(Lowers.Casual);
            PilotCasual.SetShoes(Shoes.PilotCasual);
            //PilotCasual.Teeth = Lowers.PilotCasual;
            PilotCasual.SetShirt(Shirts.PilotCasual);
            PilotCasual.ShirtColor = 5;
            PilotCasual.SetWatch(Watches.PilotCasual);
        }
    }
}
