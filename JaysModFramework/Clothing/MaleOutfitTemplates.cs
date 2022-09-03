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
    public enum MaleOutfitTemplateIDs
    {
        Casual,
        Formal,
        Combat,
        Bike,
        Scuba,
        ScubaLand,
        Beach,
        NavyCasual,
        NavyCombat,
        ArmyCasual,
        ArmyCombat,
        TestPilot,
        PilotCasual,
}
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
            //    Casual.SetLower(Legs.Casual);
            //    Casual.SetShoes(Shoes.Casual);
            //    Casual.SetAccOne(Undershirts.Casual);
            //    Casual.SetShirt(Shirts.Casual);
            //    Casual.ShirtColor = 1;
            //    Casual.SetEars(Ears.Casual);
            //    Casual.SetWatch(Watches.Casual);
            //    Casual.Template = MaleOutfitTemplateIDs.Casual;

            //    Formal.SetUpper(Torsos.Formal);
            //    Formal.SetLower(Legs.Formal);
            //    Formal.LowerColor = 8;
            //    Formal.SetShoes(Shoes.Formal);
            //    Formal.SetAccOne(Undershirts.Formal);
            //    Formal.AccOneColor = 1;
            //    Formal.SetShirt(Shirts.Formal);
            //    Formal.ShirtColor = 1;
            //    Formal.SetEars(Ears.Casual);
            //    Formal.Template = MaleOutfitTemplateIDs.Formal;

            //    Combat.SetBeard(Masks.Combat);
            //    Combat.SetUpper(Torsos.Combat);
            //    Combat.SetLower(Legs.Combat);
            //    Combat.SetShoes(Shoes.Combat);
            //    Combat.SetAccOne(Undershirts.Combat);
            //    Combat.SetAccTwo(Armors.CombatVest);
            //    Combat.AccTwoColor = 1;
            //    Combat.SetShirt(Shirts.Combat);
            //    Combat.SetHat(Hats.CombatHelmetVisorUp);
            //    Combat.Template = MaleOutfitTemplateIDs.Combat;

            //    Bike.SetUpper(Torsos.Bike);
            //    Bike.SetLower(Legs.Bike);
            //    Bike.LowerColor = 4;
            //    Bike.SetShoes(Shoes.Bike);
            //    Bike.ShoesColor = 2;
            //    Bike.SetAccOne(Undershirts.Bike);
            //    Bike.SetShirt(Shirts.Bike);
            //    Bike.SetHat(Hats.MotorcycleVisorUp);
            //    Bike.HatColor = 9;
            //    Bike.Template = MaleOutfitTemplateIDs.Bike;

            //    Scuba.SetBeard(Masks.ScubaHood);
            //    Scuba.SetUpper(Torsos.Scuba);
            //    Scuba.SetLower(Legs.Scuba);
            //    Scuba.SetShoes(Shoes.ScubaFlippers);
            //    Scuba.SetAccOne(Undershirts.ScubaTank);
            //    Scuba.SetShirt(Shirts.Combat);
            //    Scuba.SetGlasses(Glasses.ScubaMask);
            //    Scuba.Template = MaleOutfitTemplateIDs.Scuba;

            //    ScubaLand = Scuba.Copy();
            //    ScubaLand.SetAccOne(Undershirts.Casual);
            //    ScubaLand.SetShoes(Shoes.ScubaShoes);
            //    ScubaLand.SetGlasses(Glasses.Default);
            //    ScubaLand.Template = MaleOutfitTemplateIDs.ScubaLand;

            //    Beach.SetUpper(Torsos.Beach);
            //    Beach.SetLower(Legs.Beach);
            //    Beach.LowerColor = 1;
            //    Beach.SetShoes(Shoes.Beach);
            //    Beach.SetAccOne(Undershirts.Casual);
            //    Beach.SetShirt(Shirts.Beach);
            //    Beach.Template = MaleOutfitTemplateIDs.Beach;

            //    NavyCasual.SetUpper(Torsos.FatiguesCasual);
            //    NavyCasual.SetLower(Legs.Fatigues);
            //    NavyCasual.SetShoes(Shoes.Fatigues);
            //    NavyCasual.SetAccOne(Undershirts.Casual);
            //    NavyCasual.SetShirt(Shirts.FatiguesCasual);
            //    NavyCasual.SetHat(Hats.FatiguesCasual);
            //    NavyCasual.Template = MaleOutfitTemplateIDs.NavyCasual;

            //    NavyCombat = NavyCasual.Copy();
            //    NavyCombat.SetUpper(Torsos.FatiguesCombat);
            //    NavyCombat.SetAccTwo(Armors.MilitaryVest);
            //    NavyCombat.AccTwoColor = 5;
            //    NavyCombat.SetShirt(Shirts.FatiguesCombat);
            //    NavyCombat.SetHat(Hats.CombatHelmetVisorUp);
            //    NavyCombat.HatColor = 18;
            //    NavyCombat.Template = MaleOutfitTemplateIDs.NavyCombat;

            //    ArmyCasual = NavyCasual.Copy();
            //    ArmyCasual.UpperColor = ArmyCasual.LowerColor = ArmyCasual.ShirtColor = ArmyCasual.HatColor = 3;
            //    ArmyCasual.Template = MaleOutfitTemplateIDs.ArmyCasual;

            //    ArmyCombat = NavyCombat.Copy();
            //    ArmyCombat.UpperColor = ArmyCombat.LowerColor = ArmyCombat.ShirtColor = 3;
            //    ArmyCombat.AccTwoColor = 6;
            //    ArmyCombat.HatColor = 23;
            //    ArmyCombat.Template = MaleOutfitTemplateIDs.ArmyCombat;

            //    TestPilot.SetBeard(Masks.ScubaHood);
            //    TestPilot.SetUpper(Torsos.TestPilot);
            //    TestPilot.UpperColor = 3;
            //    TestPilot.SetLower(Legs.TestPilot);
            //    TestPilot.LowerColor = 1;
            //    TestPilot.SetShoes(Shoes.TestPilot);
            //    TestPilot.ShoesColor = 3;
            //    TestPilot.SetAccOne(Undershirts.Casual);
            //    TestPilot.SetShirt(Shirts.TestPilot);
            //    TestPilot.ShirtColor = 1;
            //    TestPilot.SetHat(Hats.TestPilot);
            //    TestPilot.HatColor = 5;
            //    TestPilot.Template = MaleOutfitTemplateIDs.TestPilot;

            //    PilotCasual = NavyCasual.Copy();
            //    PilotCasual.SetHat(Hats.Default);
            //    PilotCasual.SetLower(Legs.Casual);
            //    PilotCasual.SetShoes(Shoes.PilotCasual);
            //    //PilotCasual.Teeth = Lowers.PilotCasual;
            //    PilotCasual.SetShirt(Shirts.PilotCasual);
            //    PilotCasual.ShirtColor = 5;
            //    PilotCasual.SetWatch(Watches.PilotCasual);
            //    PilotCasual.Template = MaleOutfitTemplateIDs.PilotCasual;
        }
    }
}
