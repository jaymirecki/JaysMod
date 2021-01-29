using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using NativeUI;
using System.Diagnostics;

namespace JaysMod
{
    [ScriptAttributes(NoDefaultInstance = true)]
    partial class Outfits : Script
    {
        public Ped Ped;
        private int currentHat, currentHatColor;
        public enum HeadOverlay { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
        public enum OutfitComponent { Beard = 1, Hair = 2, Upper = 3, Lower = 4, Hands = 5, Shoes = 6, AccOne = 8, AccTwo = 9, Shirt = 11 };
        public enum Prop { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
        public enum OutfitID { Casual, Formal, Combat, Bike, Beach, Scuba, NavyAtEase, NavyCombat, PilotInformal, TestPilot, Extra }
        private struct Outfit {
            public int Beard, BeardColor,
                Hair, HairColor,
                Upper, UpperColor,
                Lower, LowerColor,
                Hands, HandsColor,
                Shoes, ShoesColor,
                Teeth, TeethColor,
                AccOne, AccOneColor,
                AccTwo, AccTwoColor,
                Shirt, ShirtColor,
                Hat, HatColor,
                Glasses, GlassesColor,
                Ears, EarsColor,
                Watch, WatchColor;
            public Outfit(Outfit a)
            {
                Beard = a.Beard; BeardColor = a.BeardColor;
                Hair = a.Hair; HairColor = a.HairColor;
                Upper = a.Upper; UpperColor = a.UpperColor;
                Lower = a.Lower; LowerColor = a.LowerColor;
                Hands = a.Hands; HandsColor = a.HandsColor;
                Shoes = a.Shoes; ShoesColor = a.ShoesColor;
                Teeth = a.Teeth; TeethColor = a.TeethColor;
                AccOne = a.AccOne; AccOneColor = a.AccOneColor;
                AccTwo = a.AccTwo; AccTwoColor = a.AccTwoColor;
                Shirt = a.Shirt; ShirtColor = a.ShirtColor;
                Hat = a.Hat; HatColor = a.HatColor;
                Glasses = a.Glasses; GlassesColor = a.GlassesColor;
                Ears = a.Ears; EarsColor = a.EarsColor;
                Watch = a.Watch; WatchColor = a.WatchColor;
            }
        }
        private static Outfit blankOutfit()
        {
            return new Outfit
            {
                Beard = 0, BeardColor = 0,
                Hair = 0, HairColor = 0,
                Upper = 0, UpperColor = 0,
                Lower = 0, LowerColor = 0,
                Hands = 0, HandsColor = 0,
                Shoes = 0, ShoesColor = 0,
                Teeth = 0, TeethColor = 0,
                AccOne = 0, AccOneColor = 0,
                AccTwo = 0, AccTwoColor = 0,
                Shirt = 0, ShirtColor = 0,
                Hat = -1, HatColor = 0,
                Glasses = -1, GlassesColor = 0,
                Ears = -1, EarsColor = 0,
                Watch = -1, WatchColor = 0
            };
        }
        public enum FatigueColor { Navy }

        public OutfitID CurrentOutfit;

        private Outfit[] AllOutfits;

        private static int palette = 0;

        public Outfits()
        {
            currentHat = -1;
            currentHatColor = 0;
            AllOutfits = new Outfit[Enum.GetValues(typeof(OutfitID)).Length];
            setOutfits(0, 0);
        }
        public void FromIni(ScriptSettings ini, Ped ped)
        {
            Ped = ped;
            CurrentOutfit = ini.GetValue<OutfitID>("Save", "outfit", OutfitID.Casual);
            JaysMod.Debug<OutfitID>(CurrentOutfit);
            LoadOutfit(CurrentOutfit);

            int beard = ini.GetValue<int>("Save", "beard", GetComponent(Ped, OutfitComponent.Beard));
            int hair = ini.GetValue<int>("Save", "hair", GetComponent(Ped, OutfitComponent.Hair));
            int upper = ini.GetValue<int>("Save", "upper", GetComponent(Ped, OutfitComponent.Upper));
            int lower = ini.GetValue<int>("Save", "lower", GetComponent(Ped, OutfitComponent.Lower));
            int hands = ini.GetValue<int>("Save", "hands", GetComponent(Ped, OutfitComponent.Hands));
            int shoes = ini.GetValue<int>("Save", "shoes", GetComponent(Ped, OutfitComponent.Shoes));
            int accone = ini.GetValue<int>("Save", "accone", GetComponent(Ped, OutfitComponent.AccOne));
            int acctwo = ini.GetValue<int>("Save", "acctwo", GetComponent(Ped, OutfitComponent.AccTwo));
            int shirt = ini.GetValue<int>("Save", "shirt", GetComponent(Ped, OutfitComponent.Shirt));
            SetComponent(Ped, OutfitComponent.Beard, beard);
            SetComponent(Ped, OutfitComponent.Hair, hair);
            SetComponent(Ped, OutfitComponent.Upper, upper);
            SetComponent(Ped, OutfitComponent.Lower, lower);
            SetComponent(Ped, OutfitComponent.Hands, hands);
            SetComponent(Ped, OutfitComponent.Shoes, shoes);
            SetComponent(Ped, OutfitComponent.AccOne, accone);
            SetComponent(Ped, OutfitComponent.AccTwo, acctwo);
            SetComponent(Ped, OutfitComponent.Shirt, shirt);

            int beardColor = ini.GetValue<int>("Save", "beardcolor", GetComponentColor(Ped, OutfitComponent.Beard));
            int hairColor = ini.GetValue<int>("Save", "haircolor", GetComponentColor(Ped, OutfitComponent.Hair));
            int upperColor = ini.GetValue<int>("Save", "uppercolor", GetComponentColor(Ped, OutfitComponent.Upper));
            int lowerColor = ini.GetValue<int>("Save", "lowercolor", GetComponentColor(Ped, OutfitComponent.Lower));
            int handsColor = ini.GetValue<int>("Save", "handscolor", GetComponentColor(Ped, OutfitComponent.Hands));
            int shoesColor = ini.GetValue<int>("Save", "shoescolor", GetComponentColor(Ped, OutfitComponent.Shoes));
            int accOneColor = ini.GetValue<int>("Save", "acconecolor", GetComponentColor(Ped, OutfitComponent.AccOne));
            int accTwoColor = ini.GetValue<int>("Save", "acctwocolor", GetComponentColor(Ped, OutfitComponent.AccTwo));
            int shirtColor = ini.GetValue<int>("Save", "shirtcolor", GetComponentColor(Ped, OutfitComponent.Shirt));
            SetComponentColor(Ped, OutfitComponent.Beard, beardColor);
            SetComponentColor(Ped, OutfitComponent.Hair, hairColor);
            SetComponentColor(Ped, OutfitComponent.Upper, upperColor);
            SetComponentColor(Ped, OutfitComponent.Lower, lowerColor);
            SetComponentColor(Ped, OutfitComponent.Hands, handsColor);
            SetComponentColor(Ped, OutfitComponent.Shoes, shoesColor);
            SetComponentColor(Ped, OutfitComponent.AccOne, accOneColor);
            SetComponentColor(Ped, OutfitComponent.AccTwo, accTwoColor);
            SetComponentColor(Ped, OutfitComponent.Shirt, shirtColor);

            setOutfits(hair, hairColor);

            int hat = ini.GetValue<int>("Save", "hat", GetProp(Ped, Prop.Hat));
            int glasses = ini.GetValue<int>("Save", "glasses", GetProp(Ped, Prop.Glasses));
            int ears = ini.GetValue<int>("Save", "ears", GetProp(Ped, Prop.Ears));
            int watch = ini.GetValue<int>("Save", "watch", GetProp(Ped, Prop.Watch));
            SetProp(Ped, Prop.Hat, hat);
            SetProp(Ped, Prop.Glasses, glasses);
            SetProp(Ped, Prop.Ears, ears);
            SetProp(Ped, Prop.Watch, watch);

            int hatColor = ini.GetValue<int>("Save", "hatcolor", GetPropColor(Ped, Prop.Hat));
            int glassesColor = ini.GetValue<int>("Save", "glassescolor", GetPropColor(Ped, Prop.Glasses));
            int earsColor = ini.GetValue<int>("Save", "earscolor", GetPropColor(Ped, Prop.Ears));
            int watchColor = ini.GetValue<int>("Save", "watchcolor", GetPropColor(Ped, Prop.Watch));
            SetPropColor(Ped, Prop.Hat, hatColor);
            SetPropColor(Ped, Prop.Glasses, glassesColor);
            SetPropColor(Ped, Prop.Ears, earsColor);
            SetPropColor(Ped, Prop.Watch, watchColor);

            int color = 9;
            SetOverlay(Ped, HeadOverlay.FacialHair, 10, color);
            SetHair(Ped, 19, color);
        }
        public void ToIni(ScriptSettings ini)
        {
            ini.SetValue<int>("Save", "model", Ped.Model.Hash);
            ini.SetValue<int>("Save", "beard", GetComponent(Ped, OutfitComponent.Beard));
            ini.SetValue<int>("Save", "hair", GetComponent(Ped, OutfitComponent.Hair));
            ini.SetValue<int>("Save", "upper", GetComponent(Ped, OutfitComponent.Upper));
            ini.SetValue<int>("Save", "lower", GetComponent(Ped, OutfitComponent.Lower));
            ini.SetValue<int>("Save", "hands", GetComponent(Ped, OutfitComponent.Hands));
            ini.SetValue<int>("Save", "shoes", GetComponent(Ped, OutfitComponent.Shoes));
            ini.SetValue<int>("Save", "accone", GetComponent(Ped, OutfitComponent.AccOne));
            ini.SetValue<int>("Save", "acctwo", GetComponent(Ped, OutfitComponent.AccTwo));
            ini.SetValue<int>("Save", "shirt", GetComponent(Ped, OutfitComponent.Shirt));

            ini.SetValue<int>("Save", "beardcolor", GetComponentColor(Ped, OutfitComponent.Beard));
            ini.SetValue<int>("Save", "haircolor", GetComponentColor(Ped, OutfitComponent.Hair));
            ini.SetValue<int>("Save", "uppercolor", GetComponentColor(Ped, OutfitComponent.Upper));
            ini.SetValue<int>("Save", "lowercolor", GetComponentColor(Ped, OutfitComponent.Lower));
            ini.SetValue<int>("Save", "handscolor", GetComponentColor(Ped, OutfitComponent.Hands));
            ini.SetValue<int>("Save", "shoescolor", GetComponentColor(Ped, OutfitComponent.Shoes));
            ini.SetValue<int>("Save", "acconecolor", GetComponentColor(Ped, OutfitComponent.AccOne));
            ini.SetValue<int>("Save", "acctwocolor", GetComponentColor(Ped, OutfitComponent.AccTwo));
            ini.SetValue<int>("Save", "shirtcolor", GetComponentColor(Ped, OutfitComponent.Shirt));

            ini.SetValue<int>("Save", "hat", GetProp(Ped, Prop.Hat));
            ini.SetValue<int>("Save", "glasses", GetProp(Ped, Prop.Glasses));
            ini.SetValue<int>("Save", "ears", GetProp(Ped, Prop.Ears));
            ini.SetValue<int>("Save", "watch", GetProp(Ped, Prop.Watch));

            ini.SetValue<int>("Save", "hatcolor", GetPropColor(Ped, Prop.Hat));
            ini.SetValue<int>("Save", "glassescolor", GetPropColor(Ped, Prop.Glasses));
            ini.SetValue<int>("Save", "earscolor", GetPropColor(Ped, Prop.Ears));
            ini.SetValue<int>("Save", "watchcolor", GetPropColor(Ped, Prop.Watch));
        }
        public static void SetOverlay(Ped ped, HeadOverlay overlay, int index, int color)
        {
            Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, ped.Handle, 0, 0, 0, 0, 0, 0, 0.5, 0.5, 0, true);
            Function.Call(Hash.SET_PED_HEAD_OVERLAY, ped.Handle, (int)overlay, index, 1f);
            Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, ped.Handle, (int)overlay, 1, color, color);
        }
        public static void SetHair(Ped ped, int index, int color)
        {
            SetComponent(ped, OutfitComponent.Hair, index);
            Function.Call(Hash._SET_PED_HAIR_COLOR, ped, color, color);
        }
        public static int GetOverlay(Ped ped, HeadOverlay overlay)
        {
            return Function.Call<int>(Hash._GET_PED_HEAD_OVERLAY_VALUE, ped, (int)overlay);
        }
        public void SetPed(Ped ped)
        {
            Ped = ped;
            setOutfits(0, 0);
        }

        public void setOutfits(int hairIndex, int hairColor)
        {
            Outfit casual = blankOutfit();
            casual.Hair = hairIndex;
            casual.HairColor = hairColor;
            casual.Lower = 10;
            casual.Shoes = 1;
            casual.AccOne = 15;
            casual.Shirt = 16;
            casual.ShirtColor = 1;
            casual.Ears = 2;
            casual.Watch = 1;
            AllOutfits[(int)OutfitID.Casual] = casual;

            Outfit formal = blankOutfit();
            formal.Hair = hairIndex;
            formal.HairColor = hairColor;
            formal.Upper = 1;
            formal.Lower = 28;
            formal.LowerColor = 8;
            formal.Shoes = 20;
            formal.Shoes = 3;
            formal.AccOne = 73;
            formal.AccOneColor = 1;
            formal.Shirt = 11;
            formal.ShirtColor = 1;
            formal.Ears = 2;
            AllOutfits[(int)OutfitID.Formal] = formal;

            Outfit combat = blankOutfit();
            combat.Beard = 35;
            combat.Upper = 17;
            combat.Lower = 33;
            combat.Shoes = 24;
            combat.AccOne = 15;
            combat.AccTwo = 11;
            combat.AccTwoColor = 1;
            combat.Shirt = 49;
            combat.Hat = 117;
            AllOutfits[(int)OutfitID.Combat] = combat;

            Outfit bike = blankOutfit();
            bike.Hair = hairIndex;
            bike.HairColor = hairColor;
            bike.Beard = 0;
            bike.Upper = 31;
            bike.Lower = 4;
            bike.LowerColor = 4;
            bike.Shoes = 4;
            bike.ShoesColor = 2;
            bike.AccOne = 142;
            bike.Shirt = 64;
            bike.Hat = 70;
            AllOutfits[(int)OutfitID.Bike] = bike;


            Outfit scuba = blankOutfit();
            scuba.Beard = 122;
            scuba.Upper = 96;
            scuba.Lower = 94;
            scuba.Shoes = 67;
            scuba.AccOne = 123;
            scuba.Shirt = 49;
            scuba.Glasses = 26;
            AllOutfits[(int)OutfitID.Scuba] = scuba;

            Outfit beach = blankOutfit();
            beach.Hair = hairIndex;
            beach.HairColor = hairColor;
            beach.Upper = 15;
            beach.Lower = 6;
            beach.LowerColor = 1;
            beach.Shoes = 34;
            beach.AccOne = 15;
            beach.Shirt = 252;
            AllOutfits[(int)OutfitID.Beach] = beach;

            Outfit navyAtEase = blankOutfit();
            navyAtEase.Hair = hairIndex;
            navyAtEase.HairColor = hairColor;
            navyAtEase.Upper = 11;
            navyAtEase.Lower = 87;
            navyAtEase.Shoes = 35;
            navyAtEase.AccOne = 15;
            navyAtEase.Shirt = 222;
            navyAtEase.Hat = 107;
            AllOutfits[(int)OutfitID.NavyAtEase] = navyAtEase;

            Outfit navyCombat = new Outfit(navyAtEase);
            navyCombat.Upper = 152;
            navyCombat.AccTwo = 26;
            navyCombat.AccTwoColor = 5;
            navyCombat.Shirt = 221;
            navyCombat.Hat = 117;
            navyCombat.HatColor = 18;
            AllOutfits[(int)OutfitID.NavyCombat] = navyCombat;

            Outfit testPilot = new Outfit();
            testPilot.Beard = 122;
            testPilot.Upper = 165;
            testPilot.UpperColor = 3;
            testPilot.Lower = 92;
            testPilot.LowerColor = 1;
            testPilot.Shoes = 87;
            testPilot.ShoesColor = 3;
            testPilot.AccOne = 15;
            testPilot.Shirt = 228;
            testPilot.ShirtColor = 1;
            testPilot.Hat = 111;
            testPilot.HatColor = 5;
            AllOutfits[(int)OutfitID.TestPilot] = testPilot;

            Outfit pilotInformal = new Outfit(navyAtEase);
            pilotInformal.Lower = 10;
            pilotInformal.Shoes = 10;
            pilotInformal.Teeth = 38;
            pilotInformal.Shirt = 318;
            pilotInformal.ShirtColor = 5;
            pilotInformal.Watch = 10;
            AllOutfits[(int)OutfitID.PilotInformal] = pilotInformal;
            
            CurrentOutfit = OutfitID.Casual;
        }

        public void OnTick(object sender, EventArgs e)
        {
            Ped player = Game.Player.Character;
            if (CurrentOutfit == OutfitID.Scuba)
            {
                if (GetComponent(player, OutfitComponent.AccOne) != AllOutfits[(int)OutfitID.Scuba].AccOne)
                {
                    SetComponent(player, OutfitComponent.AccOne, AllOutfits[(int)OutfitID.Casual].AccOne);
                    SetComponent(player, OutfitComponent.Shoes, AllOutfits[(int)OutfitID.Scuba].Shoes + 2);
                }
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad7)
            {
                RaiseLowerVisor();
            }
            else if (e.KeyCode == Keys.NumPad8)
            {
                AddRemoveHelmet();
            }
        }

        public void OutfitsMenu(UIMenu outfitMenu)
        {

            List<dynamic> outfitList = new List<dynamic>() {
                "Casual", "Formal", "Combat", "Bike", "Beach", "Scuba",
                "Navy, At Ease", "Navy, Combat", "Pilot, Informal", "Test Pilot" };
            UIMenuListItem outfit = new UIMenuListItem("Outfit", outfitList, 0);
            outfitMenu.AddItem(outfit);

            outfitMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == outfit)
                    LoadOutfit((OutfitID)outfit.Index);
            };
        }

        public void RaiseLowerVisor()
        {
            int helmet = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 0);
            int helmetColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 0);
            Ped player = Game.Player.Character;
            switch (helmet)
            {
                case -1:
                    return;
                case 70:
                    SetProp(player, Prop.Hat, 52);
                    SetPropColor(player, Prop.Hat, helmetColor);
                    return;
                case 52:
                    SetProp(player, Prop.Hat, 70);
                    SetPropColor(player, Prop.Hat, helmetColor);
                    return;
                case 91:
                    SetProp(player, Prop.Hat, 92);
                    SetPropColor(player, Prop.Hat, helmetColor);
                    return;
                case 92:
                    SetProp(player, Prop.Hat, 91);
                    SetPropColor(player, Prop.Hat, helmetColor);
                    return;
                case 116:
                    SetProp(player, Prop.Hat, 117);
                    SetPropColor(player, Prop.Hat, helmetColor);
                    Function.Call(Hash.SET_NIGHTVISION, false);
                    return;
                case 117:
                    SetProp(player, Prop.Hat, 116);
                    SetPropColor(player, Prop.Hat, helmetColor);
                    Function.Call(Hash.SET_NIGHTVISION, true);
                    return;
            }
        }

        public void AddRemoveHelmet()
        {
            int helmet = GetProp(Ped, Prop.Hat);
            if (helmet == -1) {
                SetProp(Ped, Prop.Hat, currentHat);
                SetPropColor(Ped, Prop.Hat, currentHatColor);
                return;
            }
            currentHat = GetProp(Ped, Prop.Hat);
            currentHatColor = GetPropColor(Ped, Prop.Hat);
            int glasses = GetProp(Ped, Prop.Glasses);
            int ears = GetProp(Ped, Prop.Ears);
            int watch = GetProp(Ped, Prop.Watch);
            // Clear props
            Function.Call(Hash.CLEAR_ALL_PED_PROPS,
                    Ped);
            Function.Call(Hash.SET_NIGHTVISION, false);
            SetProp(Ped, Prop.Glasses, glasses);
            SetProp(Ped, Prop.Ears, ears);
            SetProp(Ped, Prop.Watch, watch);
            return;
        }

        ///////////////////////////////////////////////////////////////////////
        //                      Set and Get Components                       //
        ///////////////////////////////////////////////////////////////////////
        #region
        private static void SetComponentAndColor(Ped ped, OutfitComponent componentId, int component, int color)
        {
            if (Function.Call<bool>(Hash.IS_PED_COMPONENT_VARIATION_VALID, ped, (int)componentId, component, color, palette))
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, (int)componentId, component, color, palette);
        }
        private static void SetComponent(Ped ped, OutfitComponent componentId, int component)
        {
            int color = GetComponentColor(ped, componentId);
            SetComponentAndColor(ped, componentId, component, color);
        }

        private static int GetComponent(Ped ped, OutfitComponent componentId)
        {
            return Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, (int)componentId);
        }
        private static int GetComponentColor(Ped ped, OutfitComponent componentId)
        {
            return Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, (int)componentId);
        }
        private static void SetComponentColor(Ped ped, OutfitComponent componentId, int color)
        {
            int component = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, componentId);
            SetComponentAndColor(ped, componentId, component, color);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //                        Set and Get Props                          //
        ///////////////////////////////////////////////////////////////////////
        #region
        private static void SetPropAndColor(Ped ped, Prop propId, int propIndex, int color)
        {
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, (int)propId, propIndex, color, 0);
        }
        private static int GetProp(Ped ped, Prop propId)
        {
            return Function.Call<int>(Hash.GET_PED_PROP_INDEX, ped, (int)propId);
        }
        private static int GetPropColor(Ped ped, Prop propId)
        {
            int color = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, ped, (int)propId);
            return Math.Max(color, 0);
        }
        private static void SetProp(Ped ped, Prop propId, int propIndex)
        {
            SetPropAndColor(ped, propId, propIndex, GetPropColor(ped, propId));
        }
        private static void SetPropColor(Ped ped, Prop propId, int color)
        {
            SetPropAndColor(ped, propId, GetProp(ped, propId), color);
        }
        #endregion

        public void LoadOutfit(OutfitID Outfit) {
            CurrentOutfit = Outfit;
            Outfit outfit = AllOutfits[(int)CurrentOutfit];
            // Beard
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 1, outfit.Beard, outfit.BeardColor, 2);
            // Hair
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 2, outfit.Hair, outfit.HairColor, 2);
            // Upper
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 3, outfit.Upper, outfit.UpperColor, 2);
            // Lower
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 4, outfit.Lower, outfit.LowerColor, 2);
            // Hands
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 5, outfit.Hands, outfit.HandsColor, 2);
            // Shoes
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 6, outfit.Shoes, outfit.ShoesColor, 2);
            // AccOne
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 8, outfit.AccOne, outfit.AccOneColor, 2);
            // AccTwo
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 9, outfit.AccTwo, outfit.AccTwoColor, 2);
            // Shirt
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        Ped, 11, outfit.Shirt, outfit.ShirtColor, 2);
            // Clear props
            Function.Call(Hash.CLEAR_ALL_PED_PROPS,
                    Ped);
            Function.Call(Hash.SET_NIGHTVISION, false);
            // Hat
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Ped, 0, outfit.Hat, outfit.HatColor, 2);
            // Glasses
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Ped, 1, outfit.Glasses, outfit.GlassesColor, 2);
            // Ears
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Ped, 2, outfit.Ears, outfit.EarsColor, 2);
            // Watch
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Ped, 3, outfit.Watch, outfit.WatchColor, 2);
        }
    }
}
