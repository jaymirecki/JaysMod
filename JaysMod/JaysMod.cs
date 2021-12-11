using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;
using JaysMod.Framework;

namespace JaysMod
{
    [ScriptAttributes(NoDefaultInstance = true)]
    public partial class JaysMod : Script
    {
        private MenuPool ModMenuPool;
        private UIMenu planeMenu;
        SaveAndLoad SaverLoader;
        private string SaveId;

        private Dictionary<Vehicle, bool> Sirens;

        private HUD Hud;
        private NPC PlayerNPC;
        private Weather Weather
        {
            get { return GTA.World.Weather; }
            set { GTA.World.Weather = value; }
        }
        
        private int Minutes;

        private static bool DEBUG = true;

        public JaysMod()
        {
            ModMenuPool = new MenuPool();
            Sirens = new Dictionary<Vehicle, bool>();

            Hud = InstantiateScript<HUD>();

            Tick += onTick;
            KeyDown += onKeyDown;
        }

        public void Load(string saveId)
        {
            SetupGame();
            SaveId = saveId;
            PlayerNPC.Load(SaverLoader, SaveId, "player");
            World.CurrentDate = new DateTime(SaverLoader.Load(SaveId, "time", 432500000000));
        }
        public void Load()
        {
            Load(SaveId);
        }
        private void SetupGame()
        {
            Minutes = DateTime.Now.Minute;
            World.IsClockPaused = true;
            OutfitTemplates.SetupOutfits();
            SaverLoader = new SaveAndLoad("JaysMod.ini");
            LoadModel(1885233650);
            PlayerNPC = new NPC("player", Game.Player.Character);
            Weather = Weather.ExtraSunny;
            JaysModFramework.RestrictedAreas.SetEnabledFortZancudo(false);
        }
        public void New(string saveId)
        {
            SetupGame();
            SaveId = saveId;
            PlayerNPC.Outfit = OutfitTemplates.NavyCombat;
            World.CurrentDate = new DateTime(432500000000);
        }
        public void Save()
        {
            PlayerNPC.Save(SaverLoader, SaveId, "player");
            SaverLoader.Save(SaveId, "time", GTA.World.CurrentDate.Ticks);

            SaverLoader.Save();
        }

        public void Unload()
        {
            Hud.Abort();
            Hud = null;
            JaysModFramework.RestrictedAreas.SetEnabledFortZancudo(true);

            World.IsClockPaused = false;
        }

        public static void Debug(string message)
        {
            if (DEBUG)
                GTA.UI.Notification.Show(message);
        }
        public static void Debug<T>(T message)
        {
            Debug(message.ToString());
        }
        void onTick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute != Minutes)
            {
                World.CurrentDate = World.CurrentDate.AddMinutes(1);
                Minutes = DateTime.Now.Minute;
            }
            if (ModMenuPool != null)
                ModMenuPool.ProcessMenus();
            if ((Game.Player.IsDead || PlayerNPC.Health == 0 || PlayerNPC.IsDead))
            {
                Respawn();
            }
        }
        private void Respawn()
        {
            // Prevent default death behavior
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "respawn_controller");
            Ped playerPed = Game.Player.Character;
            Function.Call(Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, playerPed);
            Function.Call(Hash.NETWORK_RESURRECT_LOCAL_PLAYER, playerPed.Position.X, playerPed.Position.Y, playerPed.Position.Z, 0f, 0f, 0f);
            playerPed.IsInvincible = true;
            Function.Call(Hash.IGNORE_NEXT_RESTART, true);
            GTA.UI.Screen.StopEffects();

            // Custom death behavior
            //Function.Call(Hash._RESET_LOCALPLAYER_STATE);
            Function.Call(Hash.RESET_PLAYER_ARREST_STATE, playerPed);
            //playerPed.Ragdoll();
            Wait(5000);

            // Fade and respawn
            GTA.UI.Screen.FadeOut(2000);
            while (!GTA.UI.Screen.IsFadedOut)
                Yield();
            playerPed.CancelRagdoll();
            Function.Call(Hash.DISPLAY_HUD, true);
            Function.Call(Hash.DISPLAY_RADAR, true);
            GTA.Game.TimeScale = 1f;
            Load();
            Wait(2000);
            playerPed.IsInvincible = false;
            GTA.UI.Screen.FadeIn(2000);
        }

        public static void LoadModel(string ModelName) {
            int modelHash = Function.Call<int>(Hash.GET_HASH_KEY, ModelName);
            LoadModel(modelHash);
        }

        public static void LoadModel(int ModelHash)
        {
            var characterModel = new Model(ModelHash);
            characterModel.Request(500);
            if (characterModel.IsInCdImage && characterModel.IsValid)
            {
                // If the model isn't loaded, wait until it is
                while (!characterModel.IsLoaded) Yield();

                // Set the player's model
                Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);
                Function.Call(Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, Game.Player.Character.Handle);
                characterModel.MarkAsNoLongerNeeded();
            }
        }

        void onKeyDown(object sender, KeyEventArgs e)
        {
            Ped player = Game.Player.Character;
            if (e.KeyCode == Keys.J)
            {
                if (player.IsInVehicle())
                {
                    Vehicle vehicle = new Vehicle(player.CurrentVehicle);
                    if (vehicle.HasSiren && Sirens.ContainsKey(vehicle))
                    {
                        bool silent;
                        Sirens.TryGetValue(vehicle, out silent);
                        vehicle.IsSirenSilent = !silent;
                        Sirens.Remove(vehicle);
                        Sirens.Add(vehicle, !silent);
                    }
                    else if (vehicle.HasSiren)
                    {
                        bool silent = true;
                        vehicle.IsSirenSilent = silent;
                        Sirens.Add(vehicle, silent);
                    }
                }
            }
            else if (e.KeyCode == Keys.E)
            {
                //if (player.IsInVehicle() && 
                //    player.CurrentVehicle == playerPlane.BaseVehicle && 
                //    !ModMenuPool.IsAnyMenuOpen())
                //{
                //    planeMenu.Visible = true;
                //}
            }
        }

        //void Modules()
        //{
        //    UIMenu submenu = modMenuPool.AddSubMenu(mainMenu, "Modules");

        //    //OutfitsMenu(submenu);
        //    LoadoutsMenu(submenu);

        //    UIMenuItem charterMenu = new UIMenuItem("Charter Flights (v0.1)");
        //    submenu.AddItem(charterMenu);
        //    UIMenuItem atc = new UIMenuItem("ATC");
        //    submenu.AddItem(atc);
        //    submenu.RefreshIndex();

        //    submenu.OnItemSelect += (sender, item, index) =>
        //    {
        //        if (item == charterMenu) {
        //            if (charter == null) {
        //                float pilot_heading = 43.63f;
        //                float plane_heading = 315.443f;
        //                Vector3 pilot_spawn = new Vector3(1503.31f, 3104.58f, 41f);
        //                Vector3 plane_spawn = new Vector3(1500.399f, 3099.608f, 42.69702f);
        //                Ped pilot = Maps.Functions.SpawnPed("s_m_m_pilot_01", pilot_spawn, pilot_heading);
        //                Vehicle plane = Maps.Functions.SpawnVehicle("nimbus", plane_spawn, plane_heading, 112, 40, -1);
        //                while(!plane.Exists() || !pilot.Exists())
        //                {
        //                    Debug("not loaded");
        //                    Yield();
        //                }
        //                Debug(plane.Position);
        //                Debug(pilot.Position);
        //                //new Charter();
        //                charter = InstantiateScript<Charter>();
        //                charter.Load(plane, pilot);
        //                GTA.UI.Notification.Show("Charter Flights Activated");
        //            } else {
        //                charter.Unload();
        //                charter.Abort();
        //                charter = null;
        //                GTA.UI.Notification.Show("Charter Flights Deactivated");
        //            }
        //        }
        //        else if (item == atc)
        //        {
        //            ATC.Run();
        //        }
        //    };
        //}

        void LoadoutsMenu(UIMenu submenu)
        {
            UIMenu menu = ModMenuPool.AddSubMenu(submenu, "Loadouts");

            UIMenuItem clean = new UIMenuItem("Clean");
            UIMenuItem echoAssault = new UIMenuItem("Echo Assault");
            UIMenuItem LSPDPatrol = new UIMenuItem("LSPD Patrol");
            menu.AddItem(clean);
            menu.AddItem(echoAssault);
            menu.AddItem(LSPDPatrol);
            menu.RefreshIndex();

            menu.OnItemSelect += (sender, item, index) =>
            {
                Ped player = Game.Player.Character;

                if (item == clean)
                {
                    Loadout.setLoadout(player, Loadout.LoadoutID.Clean);
                }
                else if (item == echoAssault)
                {
                    Loadout.setLoadout(player, Loadout.LoadoutID.EchoAssault);
                }
                else if (item == LSPDPatrol)
                {
                    Loadout.setLoadout(player, Loadout.LoadoutID.LSPDPatrol);
                }

            };
        }

        void createPlaneMenu()
        {
            planeMenu = new UIMenu("Private Plane", "SELECT AN OPTION");

            UIMenu outfitMenu = ModMenuPool.AddSubMenu(planeMenu, "Outfits");
            //playerOutfit.OutfitsMenu(outfitMenu);

            ModMenuPool.Add(planeMenu);
        }
    }
}
