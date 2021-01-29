using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;
using System.Drawing;

namespace JaysMod
{
    public partial class JaysMod : Script
    {
        private void SaveGame()
        {
            playerOutfit.ToIni(ini);
            playerLoadout.ToIni(ini);
            Ped player = Game.Player.Character;

            ini.SetValue<float>("Save", "heading", player.Heading);
            Vector3 position = player.Position;
            ini.SetValue<float>("Save", "positionX", position.X);
            ini.SetValue<float>("Save", "positionY", position.Y);
            ini.SetValue<float>("Save", "positionZ", position.Z);
            ini.SetValue<long>("Save", "time", GTA.World.CurrentDate.Ticks);

            ini.Save();
        }
        private void LoadGame()
        {
            Game.Player.IsInvincible = true;
            FromIni(ini);
            playerOutfit.FromIni(ini, Game.Player.Character);
            playerLoadout.FromIni(ini, Game.Player.Character);

            //playerPlane = Maps.Functions.SpawnVehicle("nimbus", new Vector3(1506, 3107, 41), 318, (int)VehicleColor.MatteWhite, (int)VehicleColor.MatteDarkRed, 0);
            //Blip blip = playerPlane.AddBlip();
            //blip.Sprite = BlipSprite.Plane;
            //blip.Name = "Private Plane";
            //LoadOutfit();
            //LoadLoadout();

            //createPlaneMenu();

            //World.PauseClock(true);
            Minutes = DateTime.Now.Minute;
            //Game.Player.IsInvincible = false;
        }
        private void FromIni(ScriptSettings ini)
        {
            int multiplayerModel = Function.Call<int>(Hash.GET_HASH_KEY, "mp_m_freemode_01");
            int playerModel = ini.GetValue<int>("Save", "model", multiplayerModel);
            LoadModel(playerModel);

            Ped player = Game.Player.Character;

            player.Heading = ini.GetValue<float>("Save", "heading", 0);
            player.Position = ini.GetValue<Vector3>("Save", "position", new Vector3(0, 0, 72));

            World.CurrentDate = new DateTime(ini.GetValue<long>("Save", "time", 432500000000));
        }
    }
}
