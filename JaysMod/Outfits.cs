using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using NativeUI;

namespace JaysMod
{
    static partial class Outfits
    {
        private static bool activated;
        private static int currentHat, currentHatColor;
        public enum OutfitID {Casual, Formal, Combat, Bike, Beach, Scuba, NavyAtEase, NavyCombat, PilotInformal, Extra}
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

        public static OutfitID CurrentOutfit;

        private static Outfit[] AllOutfits;

        private static int palette = 0;

        public static void setOutfits()
        {
            currentHat = -1;
            currentHatColor = 0;
            AllOutfits = new Outfit[10];

            Outfit casual = blankOutfit();
            casual.Hair = 19;
            casual.HairColor = 1;
            casual.Lower = 10;
            casual.Shoes = 1;
            casual.AccOne = 15;
            casual.Shirt = 16;
            casual.ShirtColor = 1;
            casual.Ears = 2;
            casual.Watch = 1;
            AllOutfits[(int)OutfitID.Casual] = casual;

            Outfit formal = blankOutfit();
            formal.Hair = 19;
            formal.HairColor = 1;
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
            bike.Hair = 19;
            bike.HairColor = 1;
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
            beach.Hair = 19;
            beach.HairColor = 1;
            beach.Upper = 15;
            beach.Lower = 6;
            beach.LowerColor = 1;
            beach.Shoes = 34;
            beach.AccOne = 15;
            beach.Shirt = 252;
            AllOutfits[(int)OutfitID.Beach] = beach;

            Outfit navyAtEase = blankOutfit();
            navyAtEase.Hair = 19;
            navyAtEase.HairColor = 1;
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

            Outfit pilotInformal = new Outfit(navyAtEase);
            pilotInformal.Lower = 10;
            pilotInformal.Shoes = 10;
            pilotInformal.Teeth = 38;
            pilotInformal.Shirt = 318;
            pilotInformal.ShirtColor = 5;
            pilotInformal.Watch = 10;
            AllOutfits[(int)OutfitID.PilotInformal] = pilotInformal;
            
            CurrentOutfit = OutfitID.Casual;
            activated = false;
        }

        public static void OnTick()
        {
            Ped player = Game.Player.Character;
            if (CurrentOutfit == OutfitID.Scuba)
            {
                if (GetAccOneComponent(player) != AllOutfits[(int)OutfitID.Scuba].AccOne)
                {
                    SetAccOneComponent(player, AllOutfits[(int)OutfitID.Casual].AccOne);
                    SetShoesComponent(player, AllOutfits[(int)OutfitID.Scuba].Shoes + 2);
                }
            }
        }

        public static void OnKeyDown(Keys k)
        {
            if (k == Keys.NumPad7)
            {
                RaiseLowerVisor();
            }
            else if (k == Keys.NumPad8)
            {
                AddRemoveHelmet();
            }
        }

        public static void OutfitsMenu(UIMenu outfitMenu)
        {

            List<dynamic> outfitList = new List<dynamic>() {
                "Casual", "Formal", "Combat", "Bike", "Beach", "Scuba",
                "Navy, At Ease", "Navy, Combat", "Pilot, Informal" };
            UIMenuListItem outfit = new UIMenuListItem("Outfit", outfitList, 0);
            outfitMenu.AddItem(outfit);

            outfitMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == outfit)
                    LoadOutfit((OutfitID)outfit.Index);
            };
        }

        public static void RaiseLowerVisor()
        {
            int helmet = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 0);
            int helmetColor = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, Game.Player.Character, 0);
            Ped player = Game.Player.Character;
            switch (helmet)
            {
                case -1:
                    return;
                case 70:
                    SetHatProp(player, 52);
                    SetHatPropColor(player, helmetColor);
                    return;
                case 52:
                    SetHatProp(player, 70);
                    SetHatPropColor(player, helmetColor);
                    return;
                case 91:
                    Function.Call(Hash.SET_PED_PROP_INDEX,
                            Game.Player.Character,
                            0, 92, 5, 2);
                    return;
                case 92:
                    Function.Call(Hash.SET_PED_PROP_INDEX,
                            Game.Player.Character,
                            0, 91, 5, 2);
                    return;
                case 116:
                    Function.Call(Hash.SET_PED_PROP_INDEX,
                            Game.Player.Character,
                            0, 117, helmetColor, 2);
                    Function.Call(Hash.SET_NIGHTVISION, false);
                    return;
                case 117:
                    Function.Call(Hash.SET_PED_PROP_INDEX,
                            Game.Player.Character,
                            0, 116, helmetColor, 2);
                    Function.Call(Hash.SET_NIGHTVISION, true);
                    return;
            }
        }

        public static void AddRemoveHelmet()
        {
            int helmet = GetHatProp(Game.Player.Character);
            if (helmet == -1) {
                SetHatProp(Game.Player.Character, currentHat);
                SetHatPropColor(Game.Player.Character, currentHatColor);
                return;
            }
            currentHat = GetHatProp(Game.Player.Character);
            currentHatColor = GetHatPropColor(Game.Player.Character);
            int glasses = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 1);
            int ears = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 2);
            int watch = Function.Call<int>(Hash.GET_PED_PROP_INDEX, Game.Player.Character, 3);
            // Clear props
            Function.Call(Hash.CLEAR_ALL_PED_PROPS,
                    Game.Player.Character);
            Function.Call(Hash.SET_NIGHTVISION, false);
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Game.Player.Character, 1, glasses, 0, 2);
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Game.Player.Character, 2, ears, 0, 2);
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    Game.Player.Character, 3, watch, 0, 2);
            return;
        }
        
        public static void LoadOutfit(OutfitID Outfit)
        {
            Ped player = Game.Player.Character;
            LoadOutfit(player, Outfit);
        }

        private static void SetComponentAndColor(Ped ped, int componentId, int component, int color)
        {
            if (Function.Call<bool>(Hash.IS_PED_COMPONENT_VARIATION_VALID, ped, componentId, component, color, palette))
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, componentId, component, color, palette);
        }

        ///////////////////////////////////////////////////////////////////////
        //                      Set and Get Components                       //
        ///////////////////////////////////////////////////////////////////////
        #region

        private static void SetComponent(Ped ped, int componentId, int component)
        {
            int color = Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, componentId);
            SetComponentAndColor(ped, componentId, component, color);
        }

        public static int GetBeardComponent(Ped ped)
        {
            int beard = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 1);
            return beard;
        }

        public static void SetBeardComponent(Ped ped, int component)
        {
            SetComponent(ped, 1, component);
        }

        public static int GetHairComponent(Ped ped)
        {
            int hair = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 2);
            return hair;
        }

        public static void SetHairComponent(Ped ped, int component)
        {
            SetComponent(ped, 2, component);
        }

        public static int GetUpperComponent(Ped ped)
        {
            int upper = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 3);
            return upper;
        }

        public static void SetUpperComponent(Ped ped, int component)
        {
            SetComponent(ped, 3, component);
        }

        public static int GetLowerComponent(Ped ped)
        {
            int lower = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 4);
            return lower;
        }

        public static void SetLowerComponent(Ped ped, int component)
        {
            SetComponent(ped, 4, component);
        }

        public static int GetHandsComponent(Ped ped)
        {
            int hands = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 5);
            return hands;
        }

        public static void SetHandsComponent(Ped ped, int component)
        {
            SetComponent(ped, 5, component);
        }

        public static int GetShoesComponent(Ped ped)
        {
            int shoes = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 6);
            return shoes;
        }

        public static void SetShoesComponent(Ped ped, int component)
        {
            SetComponent(ped, 6, component);
        }

        public static int GetAccOneComponent(Ped ped)
        {
            int acc = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 8);
            return acc;
        }

        public static void SetAccOneComponent(Ped ped, int component)
        {
            SetComponent(ped, 8, component);
        }

        public static int GetAccTwoComponent(Ped ped)
        {
            int acc = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 9);
            return acc;
        }

        public static void SetAccTwoComponent(Ped ped, int component)
        {
            SetComponent(ped, 9, component);
        }

        public static int GetShirtComponent(Ped ped)
        {
            int shirt = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, 11);
            return shirt;
        }

        public static void SetShirtComponent(Ped ped, int component)
        {
            SetComponent(ped, 11, component);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //                        Component Colors                           //
        ///////////////////////////////////////////////////////////////////////
        #region
        private static int GetComponentColor(Ped ped, int componentId)
        {
            return Function.Call<int>(Hash.GET_PED_TEXTURE_VARIATION, ped, componentId);
        }
        private static void SetComponentColor(Ped ped, int componentId, int color)
        {
            int component = Function.Call<int>(Hash.GET_PED_DRAWABLE_VARIATION, ped, componentId);
            SetComponentAndColor(ped, componentId, component, color);
        }
        public static void SetBeardComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 1, color);
        }
        public static int GetBeardComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 1);
        }
        public static void SetHairComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 2, color);
        }
        public static int GetHairComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 2);
        }
        public static void SetUpperComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 3, color);
        }
        public static int GetUpperComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 3);
        }
        public static void SetLowerComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 4, color);
        }
        public static int GetLowerComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 4);
        }
        public static void SetHandsComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 5, color);
        }
        public static int GetHandsComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 5);
        }
        public static void SetShoesComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 6, color);
        }
        public static int GetShoesComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 6);
        }
        public static void SetAccOneComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 8, color);
        }
        public static int GetAccOneComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 8);
        }
        public static void SetAccTwoComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 9, color);
        }
        public static int GetAccTwoComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 9);
        }
        public static void SetShirtComponentColor(Ped ped, int color)
        {
            SetComponentColor(ped, 11, color);
        }
        public static int GetShirtComponentColor(Ped ped)
        {
            return GetComponentColor(ped, 11);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////
        //                        Set and Get Props                          //
        ///////////////////////////////////////////////////////////////////////
        #region
        private static void SetPropAndColor(Ped ped, int propId, int prop, int color)
        {
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, propId, prop, color, 0);
        }
        private static int GetProp(Ped ped, int propId)
        {
            return Function.Call<int>(Hash.GET_PED_PROP_INDEX, ped, propId);
        }
        private static int GetPropColor(Ped ped, int propId)
        {
            int color = Function.Call<int>(Hash.GET_PED_PROP_TEXTURE_INDEX, ped, propId);
            return Math.Max(color, 0);
        }
        private static void SetProp(Ped ped, int propId, int prop)
        {
            SetPropAndColor(ped, propId, prop, GetPropColor(ped, propId));
        }
        private static void SetPropColor(Ped ped, int propId, int color)
        {
            SetPropAndColor(ped, propId, GetProp(ped, propId), color);
        }
        public static void SetHatProp(Ped ped, int prop)
        {
            SetProp(ped, 0, prop);
        }
        public static int GetHatProp(Ped ped)
        {
            return GetProp(ped, 0);
        }
        public static void SetHatPropColor(Ped ped, int color)
        {
            SetPropColor(ped, 0, color);
        }
        public static int GetHatPropColor(Ped ped)
        {
            return GetPropColor(ped, 0);
        }
        public static void SetGlassesProp(Ped ped, int prop)
        {
            SetProp(ped, 1, prop);
        }
        public static int GetGlassesProp(Ped ped)
        {
            return GetProp(ped, 1);
        }
        public static void SetGlassesPropColor(Ped ped, int color)
        {
            SetPropColor(ped, 1, color);
        }
        public static int GetGlassesPropColor(Ped ped)
        {
            return GetPropColor(ped, 1);
        }
        public static void SetEarProp(Ped ped, int prop)
        {
            SetProp(ped, 2, prop);
        }
        public static int GetEarProp(Ped ped)
        {
            return GetProp(ped, 2);
        }
        public static void SetEarPropColor(Ped ped, int color)
        {
            SetPropColor(ped, 2, color);
        }
        public static int GetEarPropColor(Ped ped)
        {
            return GetPropColor(ped, 2);
        }
        public static void SetWatchProp(Ped ped, int prop)
        {
            SetProp(ped, 3, prop);
        }
        public static int GetWatchProp(Ped ped)
        {
            return GetProp(ped, 3);
        }
        public static void SetWatchPropColor(Ped ped, int color)
        {
            SetPropColor(ped, 3, color);
        }
        public static int GetWatchPropColor(Ped ped)
        {
            return GetPropColor(ped, 3);
        }
        #endregion

        public static void LoadOutfit(Ped ped, OutfitID Outfit) {
            CurrentOutfit = Outfit;
            Outfit outfit = AllOutfits[(int)CurrentOutfit];
            // Beard
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 1, outfit.Beard, outfit.BeardColor, 2);
            // Hair
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 2, outfit.Hair, outfit.HairColor, 2);
            // Upper
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 3, outfit.Upper, outfit.UpperColor, 2);
            // Lower
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 4, outfit.Lower, outfit.LowerColor, 2);
            // Hands
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 5, outfit.Hands, outfit.HandsColor, 2);
            // Shoes
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 6, outfit.Shoes, outfit.ShoesColor, 2);
            // AccOne
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 8, outfit.AccOne, outfit.AccOneColor, 2);
            // AccTwo
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 9, outfit.AccTwo, outfit.AccTwoColor, 2);
            // Shirt
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION,
                        ped, 11, outfit.Shirt, outfit.ShirtColor, 2);
            // Clear props
            Function.Call(Hash.CLEAR_ALL_PED_PROPS,
                    ped);
            Function.Call(Hash.SET_NIGHTVISION, false);
            // Hat
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, 0, outfit.Hat, outfit.HatColor, 2);
            // Glasses
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, 1, outfit.Glasses, outfit.GlassesColor, 2);
            // Ears
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, 2, outfit.Ears, outfit.EarsColor, 2);
            // Watch
            Function.Call(Hash.SET_PED_PROP_INDEX,
                    ped, 3, outfit.Watch, outfit.WatchColor, 2);

            activated = true;
        }
    }
}
