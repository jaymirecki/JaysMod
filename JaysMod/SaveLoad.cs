using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace JaysMod
{
    public partial class JaysMod : Script
    {
        private void SaveGame()
        {
            SaveOutfit();
            SaveLoadout();
            Ped player = Game.Player.Character;

            ini.SetValue<float>("Save", "heading", player.Heading);
            Vector3 position = player.Position;
            ini.SetValue<float>("Save", "positionX", position.X);
            ini.SetValue<float>("Save", "positionY", position.Y);
            ini.SetValue<float>("Save", "positionZ", position.Z);
            ini.SetValue<long>("Save", "time", GTA.World.CurrentDate.Ticks);

            ini.Save();
        }

        private void SaveOutfit()
        {
            Ped player = Game.Player.Character;
            ini.SetValue<int>("Save", "model", player.Model.Hash);
            ini.SetValue<int>("Save", "beard", Outfits.GetBeardComponent(player));
            ini.SetValue<int>("Save", "hair", Outfits.GetHairComponent(player));
            ini.SetValue<int>("Save", "upper", Outfits.GetUpperComponent(player));
            ini.SetValue<int>("Save", "lower", Outfits.GetLowerComponent(player));
            ini.SetValue<int>("Save", "hands", Outfits.GetHandsComponent(player));
            ini.SetValue<int>("Save", "shoes", Outfits.GetShoesComponent(player));
            ini.SetValue<int>("Save", "accone", Outfits.GetAccOneComponent(player));
            ini.SetValue<int>("Save", "acctwo", Outfits.GetAccTwoComponent(player));
            ini.SetValue<int>("Save", "shirt", Outfits.GetShirtComponent(player));

            ini.SetValue<int>("Save", "beardcolor", Outfits.GetBeardComponentColor(player));
            ini.SetValue<int>("Save", "haircolor", Outfits.GetHairComponentColor(player));
            ini.SetValue<int>("Save", "uppercolor", Outfits.GetUpperComponentColor(player));
            ini.SetValue<int>("Save", "lowercolor", Outfits.GetLowerComponentColor(player));
            ini.SetValue<int>("Save", "handscolor", Outfits.GetHandsComponentColor(player));
            ini.SetValue<int>("Save", "shoescolor", Outfits.GetShoesComponentColor(player));
            ini.SetValue<int>("Save", "acconecolor", Outfits.GetAccOneComponentColor(player));
            ini.SetValue<int>("Save", "acctwocolor", Outfits.GetAccTwoComponentColor(player));
            ini.SetValue<int>("Save", "shirtcolor", Outfits.GetShirtComponentColor(player));

            ini.SetValue<int>("Save", "hat", Outfits.GetHatProp(player));
            ini.SetValue<int>("Save", "glasses", Outfits.GetGlassesProp(player));
            ini.SetValue<int>("Save", "ears", Outfits.GetEarProp(player));
            ini.SetValue<int>("Save", "watch", Outfits.GetWatchProp(player));

            ini.SetValue<int>("Save", "hatcolor", Outfits.GetHatPropColor(player));
            ini.SetValue<int>("Save", "glassescolor", Outfits.GetGlassesPropColor(player));
            ini.SetValue<int>("Save", "earscolor", Outfits.GetEarPropColor(player));
            ini.SetValue<int>("Save", "watchcolor", Outfits.GetWatchPropColor(player));
        }

        private void SaveLoadout()
        {
            WeaponCollection loadout = Game.Player.Character.Weapons;
            foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
            {
                if (loadout.HasWeapon(weapon)) {
                    string name = Enum.GetName(typeof(WeaponHash), weapon);
                    ini.SetValue<bool>("Save", name, true);
                    ini.SetValue<int>("Save", name + "ammo", loadout[weapon].Ammo);
                    ini.SetValue<int>("Save", name + "tint", (int)loadout[weapon].Tint);
                    var components = loadout[weapon].Components;
                    for (int i = 0; i < components.ClipVariationsCount; i++)
                    {
                        ini.SetValue<bool>("Save", name + "clip" + i.ToString(), components.GetClipComponent(i).Active);
                    }
                    if (Loadout.HasFlashLight(weapon))
                        ini.SetValue<bool>("Save", name + "light", components.GetFlashLightComponent().Active);
                    for (int i = 0; i < components.ScopeVariationsCount; i++)
                        ini.SetValue<bool>("Save", name + "scope" + i.ToString(), components.GetScopeComponent(i).Active);
                    if (Loadout.HasSuppressor(weapon))
                        ini.SetValue<bool>("Save", name + "suppressor", components.GetSuppressorComponent().Active);
                }
                else
                    ini.SetValue<bool>("Save", Enum.GetName(typeof(WeaponHash), weapon), false);
            }
        }

        private void LoadGame()
        {
            int multiplayerModel = Function.Call<int>(Hash.GET_HASH_KEY, "mp_m_freemode_01");
            int playerModel = ini.GetValue<int>("Save", "model", multiplayerModel);
            LoadModel(playerModel);

            LoadOutfit();
            LoadLoadout();

            Ped player = Game.Player.Character;

            player.Heading = ini.GetValue<float>("Save", "heading", 0);
            Vector3 position = new Vector3();
            position.X = ini.GetValue<float>("Save", "positionX", 0);
            position.Y = ini.GetValue<float>("Save", "positionY", 0);
            position.Z = ini.GetValue<float>("Save", "positionZ", 71.16f);
            player.Position = position;

            World.PauseClock(true);
            World.CurrentDate = new DateTime(ini.GetValue<long>("Save", "time", 432500000000));
            Minutes = DateTime.Now.Minute;
            GameLoaded = true;
        }
        private void LoadOutfit()
        {
            Ped player = Game.Player.Character;
            int beard = ini.GetValue<int>("Save", "beard", Outfits.GetBeardComponent(player));
            int hair = ini.GetValue<int>("Save", "hair", Outfits.GetHairComponent(player));
            int upper = ini.GetValue<int>("Save", "upper", Outfits.GetUpperComponent(player));
            int lower = ini.GetValue<int>("Save", "lower", Outfits.GetLowerComponent(player));
            int hands = ini.GetValue<int>("Save", "hands", Outfits.GetHandsComponent(player));
            int shoes = ini.GetValue<int>("Save", "shoes", Outfits.GetShoesComponent(player));
            int accone = ini.GetValue<int>("Save", "accone", Outfits.GetAccOneComponent(player));
            int acctwo = ini.GetValue<int>("Save", "acctwo", Outfits.GetAccTwoComponent(player));
            int shirt = ini.GetValue<int>("Save", "shirt", Outfits.GetShoesComponent(player));
            Outfits.SetBeardComponent(player, beard);
            Outfits.SetHairComponent(player, hair);
            Outfits.SetUpperComponent(player, upper);
            Outfits.SetLowerComponent(player, lower);
            Outfits.SetHandsComponent(player, hands);
            Outfits.SetShoesComponent(player, shoes);
            Outfits.SetAccOneComponent(player, accone);
            Outfits.SetAccTwoComponent(player, acctwo);
            Outfits.SetShirtComponent(player, shirt);

            int beardColor = ini.GetValue<int>("Save", "beardcolor", Outfits.GetBeardComponentColor(player));
            int hairColor = ini.GetValue<int>("Save", "haircolor", Outfits.GetHairComponentColor(player));
            int upperColor = ini.GetValue<int>("Save", "uppercolor", Outfits.GetUpperComponentColor(player));
            int lowerColor = ini.GetValue<int>("Save", "lowercolor", Outfits.GetLowerComponentColor(player));
            int handsColor = ini.GetValue<int>("Save", "handscolor", Outfits.GetHandsComponentColor(player));
            int shoesColor = ini.GetValue<int>("Save", "shoescolor", Outfits.GetShoesComponentColor(player));
            int accOneColor = ini.GetValue<int>("Save", "acconecolor", Outfits.GetAccOneComponentColor(player));
            int accTwoColor = ini.GetValue<int>("Save", "acctwocolor", Outfits.GetAccTwoComponentColor(player));
            int shirtColor = ini.GetValue<int>("Save", "shirtcolor", Outfits.GetShirtComponentColor(player));
            Outfits.SetBeardComponentColor(player, beardColor);
            Outfits.SetHairComponentColor(player, hairColor);
            Outfits.SetUpperComponentColor(player, upperColor);
            Outfits.SetLowerComponentColor(player, lowerColor);
            Outfits.SetHandsComponentColor(player, handsColor);
            Outfits.SetShoesComponentColor(player, shoesColor);
            Outfits.SetAccOneComponentColor(player, accOneColor);
            Outfits.SetAccTwoComponentColor(player, accTwoColor);
            Outfits.SetShirtComponentColor(player, shirtColor);

            int hat = ini.GetValue<int>("Save", "hat", Outfits.GetHatProp(player));
            int glasses = ini.GetValue<int>("Save", "glasses", Outfits.GetGlassesProp(player));
            int ears = ini.GetValue<int>("Save", "ears", Outfits.GetEarProp(player));
            int watch = ini.GetValue<int>("Save", "watch", Outfits.GetWatchProp(player));
            Outfits.SetHatProp(player, hat);
            Outfits.SetGlassesProp(player, glasses);
            Outfits.SetEarProp(player, ears);
            Outfits.SetWatchProp(player, watch);

            int hatColor = ini.GetValue<int>("Save", "hatcolor", Outfits.GetHatPropColor(player));
            int glassesColor = ini.GetValue<int>("Save", "glassescolor", Outfits.GetGlassesPropColor(player));
            int earsColor = ini.GetValue<int>("Save", "earscolor", Outfits.GetEarPropColor(player));
            int watchColor = ini.GetValue<int>("Save", "watchcolor", Outfits.GetWatchPropColor(player));
            Outfits.SetHatPropColor(player, hatColor);
            Outfits.SetGlassesPropColor(player, glassesColor);
            Outfits.SetEarPropColor(player, earsColor);
            Outfits.SetWatchPropColor(player, watchColor);
        }
        private void LoadLoadout()
        {
            WeaponCollection loadout = Game.Player.Character.Weapons;
            loadout.RemoveAll();
            foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
            {
                string name = Enum.GetName(typeof(WeaponHash), weapon);
                if (ini.GetValue<bool>("Save", name, false))
                {
                    int ammo = ini.GetValue<int>("Save", name + "ammo", 0);
                    var curr = loadout.Give(weapon, ammo, false, true);
                    curr.Tint = (WeaponTint)ini.GetValue<int>("Save", name + "tint", 0);
                    var components = curr.Components;

                    if (weapon != WeaponHash.Unarmed && weapon != WeaponHash.Parachute)
                    {

                        for (int i = 0; i < components.ClipVariationsCount; i++)
                            components.GetClipComponent(i).Active = ini.GetValue<bool>("Save", name + "clip" + i.ToString(), false);

                        if (Loadout.HasFlashLight(weapon))
                            components.GetFlashLightComponent().Active = ini.GetValue<bool>("Save", name + "light", false);

                        for (int i = 0; i < components.ScopeVariationsCount; i++)
                            components.GetScopeComponent(i).Active = ini.GetValue<bool>("Save", name + "scope" + i.ToString(), false);

                        if (Loadout.HasSuppressor(weapon))
                            components.GetSuppressorComponent().Active = ini.GetValue<bool>("Save", name + "suppressor", false);
                    }
                }
            }
        }
    }
}
