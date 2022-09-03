using GTA;
using GTA.Native;
using System;
using JaysModFramework.MaleOutfitPieces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using JaysModFramework.Clothing;

namespace JaysModFramework
{
    #region SlotEnums
    public enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    public enum OutfitComponents { Beard = 1, Hair = 2, Torso = 3, Lower = 4, Hands = 5, Shoes = 6, Undershirt = 8, AccTwo = 9, Shirt = 11 };
    public enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    #endregion SlotEnums
    public struct Outfit
    {
        #region Apply
        #endregion Apply
        #region Properties
        Shirt Shirt;
        #endregion Properties
        #region Colors
        string ShirtColor;
        #endregion Colors
        #region Constructors
        public Outfit(Shirt shirt, string shirtColor) {
            Shirt = shirt;
            ShirtColor = shirtColor;
        }
        public Outfit(Shirt shirt) : this(shirt, "") { }
        #endregion
        #region Ped Management
        public void ApplyToPed(Ped ped, bool preserveHair)
        {
            SetComponent(ped, OutfitComponents.Torso, Shirt.Torso, Shirt.TorsoColor(ShirtColor));
            SetComponent(ped, OutfitComponents.Undershirt, Shirt.Undershirt, Shirt.UndershirtColor(ShirtColor));
            SetComponent(ped, OutfitComponents.Shirt, Shirt.ShirtOverlay, Shirt.ShirtOverlayColor(ShirtColor));

            //int hair = Hair;
            //int hairColor = HairColor;
            //if (preserveHair)
            //{
            //    hair = GetComponent(ped, OutfitComponents.Hair);
            //    hairColor = GetComponentColor(ped, OutfitComponents.Hair);
            //}
            //if (Beard == (int)Masks.ScubaHood)
            //{
            //    hair = 0;
            //}
            //// Set components
            //SetComponent(ped, OutfitComponents.Beard, Beard, BeardColor);
            //SetComponent(ped, OutfitComponents.Hair, hair, hairColor);
            //SetHairColor(ped, hairColor);
            //SetComponent(ped, OutfitComponents.Upper, Upper, UpperColor);
            //SetComponent(ped, OutfitComponents.Lower, Lower, LowerColor);
            //SetComponent(ped, OutfitComponents.Hands, Hands, HandsColor);
            //SetComponent(ped, OutfitComponents.Shoes, Shoes, ShoesColor);
            //SetComponent(ped, OutfitComponents.AccOne, AccOne, AccOneColor);
            //SetComponent(ped, OutfitComponents.AccTwo, AccTwo, AccTwoColor);
            //SetComponent(ped, OutfitComponents.Shirt, Shirt, ShirtColor);

            //// Clear props
            //Function.Call(Hash.CLEAR_ALL_PED_PROPS, ped);
            //Function.Call(Hash.SET_NIGHTVISION, false);

            //// Set props
            //SetProp(ped, Props.Hat, Hat, HatColor);
            //SetProp(ped, Props.Glasses, Glasses, GlassesColor);
            //SetProp(ped, Props.Ears, Ears, EarsColor);
            //SetProp(ped, Props.Watch, Watch, WatchColor);
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
    }
}
