using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace JaysMod
{
    static class Loadout
    {
        public enum LoadoutID { Clean, LSPDPatrol, EchoAssault }

        public static void setLoadout(Ped ped, LoadoutID loadout)
        {
            ped.Weapons.RemoveAll();
            if (loadout == LoadoutID.LSPDPatrol)
                giveLSPDPatrol(ped);
            else if (loadout == LoadoutID.EchoAssault)
                giveEchoAssault(ped);
        }

        public static void giveLSPDPatrol(Ped ped)
        {
            Weapon pistol = ped.Weapons.Give(WeaponHash.Pistol, 24, false, false);
            pistol.Tint = WeaponTint.LSPD;

            ped.Weapons.Give(WeaponHash.Flashlight, 0, false, false);
            ped.Weapons.Give(WeaponHash.Nightstick, 0, false, false);
        }
        public static void giveEchoAssault(Ped ped)
        {
            Weapon knife = ped.Weapons.Give(WeaponHash.Knife, 0, false, false);

            Weapon pistol = ped.Weapons.Give(WeaponHash.Pistol, 24, false, false);
            pistol.Components.GetSuppressorComponent().Active = true;
            pistol.Components.GetFlashLightComponent().Active = true;

            Weapon rifle = ped.Weapons.Give(WeaponHash.CarbineRifle, 90, false, false);
            rifle.Components.GetSuppressorComponent().Active = true;
            rifle.Components.GetFlashLightComponent().Active = true;
            rifle.Components.GetScopeComponent(0).Active = true;

            Weapon sniper = ped.Weapons.Give(WeaponHash.HeavySniper, 12, false, false);
            //sniper.Components.GetSuppressorComponent().Active = true;
            sniper.Components.GetScopeComponent(0).Active = true;

            Weapon grenade = ped.Weapons.Give(WeaponHash.Grenade, 5, false, false);

            ped.Weapons.Give(WeaponHash.Parachute, 2, false, false);
            ped.Weapons.Give(WeaponHash.NightVision, 0, false, false);
            Game.Player.PrimaryParachuteTint = ParachuteTint.Shadow;
            Game.Player.ReserveParachuteTint = ParachuteTint.Shadow;
        }
        public static bool HasFlashLight(WeaponHash weapon)
        {
            try
            {
                if (Game.Player.Character.Weapons[weapon].Components.GetFlashLightComponent().Active)
                    return true;
                else return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool HasSuppressor(WeaponHash weapon)
        {
            try
            {
                if (Game.Player.Character.Weapons[weapon].Components.GetSuppressorComponent().Active)
                    return true;
                else return true;
            }
            catch
            {
                return false;
            }
        }
        //public static bool HasFlashLight(WeaponHash weapon)
        //{
        //    return weapon != WeaponHash.SniperRifle &&
        //           weapon != WeaponHash.FireExtinguisher &&
        //           weapon != WeaponHash.CompactGrenadeLauncher &&
        //           weapon != WeaponHash.Snowball &&
        //           weapon != WeaponHash.VintagePistol &&
        //           weapon != WeaponHash.HeavySniper &&
        //           weapon != WeaponHash.SweeperShotgun &&
        //           weapon != WeaponHash.Wrench &&
        //           weapon != WeaponHash.Ball &&
        //           weapon != WeaponHash.Molotov &&
        //           weapon != WeaponHash.StickyBomb &&
        //           weapon != WeaponHash.PetrolCan &&
        //           weapon != WeaponHash.StunGun &&
        //           weapon != WeaponHash.Minigun &&
        //           weapon != WeaponHash.GolfClub &&
        //           weapon != WeaponHash.FlareGun &&
        //           weapon != WeaponHash.Flare &&
        //           weapon != WeaponHash.Hammer &&
        //           weapon != WeaponHash.Gusenberg &&
        //           weapon != WeaponHash.CompactRifle &&
        //           weapon != WeaponHash.HomingLauncher &&
        //           weapon != WeaponHash.Nightstick &&
        //           weapon != WeaponHash.Railgun &&
        //           weapon != WeaponHash.SawnOffShotgun &&
        //           weapon != WeaponHash.Firework &&
        //           weapon != WeaponHash.CombatMG &&
        //           weapon != WeaponHash.Crowbar &&
        //           weapon != WeaponHash.Flashlight &&
        //           weapon != WeaponHash.Dagger &&
        //           weapon != WeaponHash.Grenade &&
        //           weapon != WeaponHash.PoolCue &&
        //           weapon != WeaponHash.Bat &&
        //           weapon != WeaponHash.Knife &&
        //           weapon != WeaponHash.MG &&
        //           weapon != WeaponHash.BZGas &&
        //           weapon != WeaponHash.Unarmed &&
        //           weapon != WeaponHash.Musket &&
        //           weapon != WeaponHash.ProximityMine &&
        //           weapon != WeaponHash.RPG &&
        //           weapon != WeaponHash.PipeBomb &&
        //           weapon != WeaponHash.MiniSMG &&
        //           weapon != WeaponHash.SNSPistol &&
        //           weapon != WeaponHash.Revolver &&
        //           weapon != WeaponHash.BattleAxe &&
        //           weapon != WeaponHash.KnuckleDuster &&
        //           weapon != WeaponHash.MachinePistol &&
        //           weapon != WeaponHash.MarksmanPistol &&
        //           weapon != WeaponHash.Machete &&
        //           weapon != WeaponHash.SwitchBlade &&
        //           weapon != WeaponHash.DoubleBarrelShotgun &&
        //           weapon != WeaponHash.Hatchet &&
        //           weapon != WeaponHash.Bottle &&
        //           weapon != WeaponHash.Parachute &&
        //           weapon != WeaponHash.SmokeGrenade;
        //}
        //public static bool HasSuppressor(WeaponHash weapon)
        //{
        //    return weapon != WeaponHash.FireExtinguisher &&
        //           weapon != WeaponHash.CompactGrenadeLauncher &&
        //           weapon != WeaponHash.Snowball &&
        //           weapon != WeaponHash.CombatPDW &&
        //           weapon != WeaponHash.HeavySniper &&
        //           weapon != WeaponHash.SweeperShotgun &&
        //           weapon != WeaponHash.Wrench &&
        //           weapon != WeaponHash.PumpShotgun &&
        //           weapon != WeaponHash.Ball &&
        //           weapon != WeaponHash.Molotov &&
        //           weapon != WeaponHash.StickyBomb &&
        //           weapon != WeaponHash.PetrolCan &&
        //           weapon != WeaponHash.StunGun &&
        //           weapon != WeaponHash.Minigun &&
        //           weapon != WeaponHash.GolfClub &&
        //           weapon != WeaponHash.FlareGun &&
        //           weapon != WeaponHash.Flare &&
        //           weapon != WeaponHash.Hammer &&
        //           weapon != WeaponHash.Gusenberg &&
        //           weapon != WeaponHash.CompactRifle &&
        //           weapon != WeaponHash.HomingLauncher &&
        //           weapon != WeaponHash.Nightstick &&
        //           weapon != WeaponHash.Railgun &&
        //           weapon != WeaponHash.SawnOffShotgun &&
        //           weapon != WeaponHash.Firework &&
        //           weapon != WeaponHash.CombatMG &&
        //           weapon != WeaponHash.Crowbar &&
        //           weapon != WeaponHash.Flashlight &&
        //           weapon != WeaponHash.Dagger &&
        //           weapon != WeaponHash.Grenade &&
        //           weapon != WeaponHash.PoolCue &&
        //           weapon != WeaponHash.Bat &&
        //           weapon != WeaponHash.Knife &&
        //           weapon != WeaponHash.MG &&
        //           weapon != WeaponHash.BZGas &&
        //           weapon != WeaponHash.Unarmed &&
        //           weapon != WeaponHash.GrenadeLauncher &&
        //           weapon != WeaponHash.Musket &&
        //           weapon != WeaponHash.ProximityMine &&
        //           weapon != WeaponHash.RPG &&
        //           weapon != WeaponHash.PipeBomb &&
        //           weapon != WeaponHash.MiniSMG &&
        //           weapon != WeaponHash.SNSPistol &&
        //           weapon != WeaponHash.Revolver &&
        //           weapon != WeaponHash.BattleAxe &&
        //           weapon != WeaponHash.KnuckleDuster &&
        //           weapon != WeaponHash.MarksmanPistol &&
        //           weapon != WeaponHash.Machete &&
        //           weapon != WeaponHash.SwitchBlade &&
        //           weapon != WeaponHash.DoubleBarrelShotgun &&
        //           weapon != WeaponHash.Hatchet &&
        //           weapon != WeaponHash.Bottle &&
        //           weapon != WeaponHash.Parachute &&
        //           weapon != WeaponHash.SmokeGrenade;
    //}
    }
}
