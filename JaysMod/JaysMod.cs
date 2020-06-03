using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace JaysMod
{
    public partial class JaysMod : Script
    {
        private MenuPool modMenuPool;
        private UIMenu mainMenu;
        private Dictionary<Vehicle, bool> sirens;
        private ScriptSettings ini;

        private int OriginalModel;
        private Vector3 OriginalPosition;
        private float OriginalHeading;
        private DateTime OriginalDateTime;

        Charter charter;
        
        private int Minutes;

        private bool GameLoaded;

        private static bool DEBUG = true;

        public JaysMod()
        {
            World.PauseClock(false);
            sirens = new Dictionary<Vehicle, bool>();
            setupIni();

            //GTA.UI.Screen.ShowHelpTextThisFrame("Starting");
            modMenuPool = new MenuPool();
            mainMenu = new UIMenu("Jay's Mod", "SELECT AN OPTION");
            modMenuPool.Add(mainMenu);
            UIMenuItem save = new UIMenuItem("Save Game");
            UIMenuItem load = new UIMenuItem("Load Game");
            UIMenuItem quit = new UIMenuItem("Quit Game");
            mainMenu.AddItem(save);
            mainMenu.AddItem(load);
            mainMenu.AddItem(quit);
            mainMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == save)
                    SaveGame();
                else if (item == load)
                {
                    if (!GameLoaded)
                    {
                        OriginalModel = Game.Player.Character.Model.Hash;
                        OriginalPosition = Game.Player.Character.Position;
                        OriginalHeading = Game.Player.Character.Heading;
                        OriginalDateTime = World.CurrentDate;
                    }
                    LoadGame();
                }
                else if (item == quit)
                    QuitGame();
            };
            Modules();
            Debug();
            mainMenu.RefreshIndex();
            Outfits.setOutfits();
            
            GameLoaded = false;

            Tick += onTick;
            KeyDown += onKeyDown;
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
            if (GameLoaded)
            {
                if (DateTime.Now.Minute != Minutes)
                {
                    World.CurrentDate = World.CurrentDate.AddMinutes(1);
                    Minutes = DateTime.Now.Minute;
                }
                Outfits.OnTick();
            }
            if (modMenuPool != null)
                modMenuPool.ProcessMenus();
            if ((Game.Player.IsDead || Game.Player.Character.Health == 0) &&
                Game.Player.Character.Model.Hash != Function.Call<int>(Hash.GET_HASH_KEY, "player_zero") &&
                Game.Player.Character.Model.Hash != Function.Call<int>(Hash.GET_HASH_KEY, "player_one") &&
                Game.Player.Character.Model.Hash != Function.Call<int>(Hash.GET_HASH_KEY, "player_two"))
            {
                Respawn();
            }
        }
        private void Respawn()
        {
            DateTime time = World.CurrentDate;
            // Prevent default death behavior
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "respawn_controller");
            Ped playerPed = Game.Player.Character;
            Function.Call(Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, playerPed);
            Function.Call(Hash.NETWORK_RESURRECT_LOCAL_PLAYER, playerPed.Position.X, playerPed.Position.Y, playerPed.Position.Z, 0f, 0f, 0f);
            playerPed.IsInvincible = true;
            Function.Call(Hash.IGNORE_NEXT_RESTART, true);
            GTA.UI.Screen.StopEffects();

            // Custom death behavior
            Function.Call(Hash._RESET_LOCALPLAYER_STATE);
            Function.Call(Hash.RESET_PLAYER_ARREST_STATE, playerPed);
            playerPed.Ragdoll();
            Wait(5000);

            // Fade and respawn
            GTA.UI.Screen.FadeOut(2000);
            while (!GTA.UI.Screen.IsFadedOut)
                Yield();
            playerPed.CancelRagdoll();
            Function.Call(Hash.DISPLAY_HUD, true);
            Function.Call(Hash.DISPLAY_RADAR, true);
            GTA.Game.TimeScale = 1f;
            if (GameLoaded)
                LoadGame();
            else
                World.CurrentDate = time;
            Wait(2000);
            playerPed.IsInvincible = false;
            GTA.UI.Screen.FadeIn(2000);
        }
        private void QuitGame()
        {
            LoadModel(OriginalModel);
            Game.Player.Character.Position = OriginalPosition;
            Game.Player.Character.Heading = OriginalHeading;
            World.CurrentDate = OriginalDateTime;
            GameLoaded = false;
            World.PauseClock(false);
        }
        private void setupIni()
        {
            const string nodefstr = "nodef3490457439";
            const int nodefint = 439579345;
            ini = ScriptSettings.Load("JaysMod.ini");

            if (ini.GetValue<string>("General", "name", nodefstr) == nodefstr)
                ini.SetValue<string>("General", "name", "player");
            if (ini.GetValue<int>("General", "funds", nodefint) == nodefint)
                ini.SetValue<int>("General", "funds", 0);
            if (ini.GetValue<int>("General", "outfit", nodefint) == nodefint)
                ini.SetValue<int>("General", "outfit", (int)Outfits.OutfitID.Casual);

            ini.Save();
        }

        void LoadModel(string ModelName) {
            int modelHash = Function.Call<int>(Hash.GET_HASH_KEY, ModelName);
            LoadModel(modelHash);
        }

        void LoadModel(int ModelHash)
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
            if (e.KeyCode == Keys.F5 && !modMenuPool.IsAnyMenuOpen())
                mainMenu.Visible = !mainMenu.Visible;
            else if (e.KeyCode == Keys.F5)
                modMenuPool.CloseAllMenus();
            else if (e.KeyCode == Keys.J)
            {
                if (Game.Player.Character.IsInVehicle())
                {
                    Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                    if (vehicle.HasSiren && sirens.ContainsKey(vehicle))
                    {
                        bool silent;
                        sirens.TryGetValue(vehicle, out silent);
                        vehicle.IsSirenSilent = !silent;
                        sirens.Remove(vehicle);
                        sirens.Add(vehicle, !silent);
                    }
                    else if (vehicle.HasSiren)
                    {
                        bool silent = true;
                        vehicle.IsSirenSilent = silent;
                        sirens.Add(vehicle, silent);
                    }
                }
            }
            else
            {
                Outfits.OnKeyDown(e.KeyCode);
            }
        }

        void Modules()
        {
            UIMenu submenu = modMenuPool.AddSubMenu(mainMenu, "Modules");

            OutfitsMenu(submenu);
            LoadoutsMenu(submenu);

            UIMenuItem charterMenu = new UIMenuItem("Charter Flights (v0.1)");
            submenu.AddItem(charterMenu);
            UIMenuItem atc = new UIMenuItem("ATC");
            submenu.AddItem(atc);
            submenu.RefreshIndex();

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == charterMenu) {
                    if (charter == null) {
                        float pilot_heading = 43.63f;
                        float plane_heading = 315.443f;
                        Vector3 pilot_spawn = new Vector3(1503.31f, 3104.58f, 41f);
                        Vector3 plane_spawn = new Vector3(1500.399f, 3099.608f, 42.69702f);
                        Ped pilot = Maps.Functions.SpawnPed("s_m_m_pilot_01", pilot_spawn, pilot_heading);
                        Vehicle plane = Maps.Functions.SpawnVehicle("nimbus", plane_spawn, plane_heading, 112, 40, -1);
                        while(!plane.Exists() || !pilot.Exists())
                        {
                            Debug("not loaded");
                            Yield();
                        }
                        Debug(plane.Position);
                        Debug(pilot.Position);
                        //new Charter();
                        charter = InstantiateScript<Charter>();
                        charter.Load(plane, pilot);
                        GTA.UI.Notification.Show("Charter Flights Activated");
                    } else {
                        charter.Unload();
                        charter.Abort();
                        charter = null;
                        GTA.UI.Notification.Show("Charter Flights Deactivated");
                    }
                }
                else if (item == atc)
                {
                    ATC.Run();
                }
            };
        }

        void OutfitsMenu(UIMenu submenu)
        {
            UIMenu outfitMenu = modMenuPool.AddSubMenu(submenu, "Outfits");

            Outfits.OutfitsMenu(outfitMenu);
        }

        void LoadoutsMenu(UIMenu submenu)
        {
            UIMenu menu = modMenuPool.AddSubMenu(submenu, "Loadouts");

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

        void Debug()
        {
            UIMenu submenu = modMenuPool.AddSubMenu(mainMenu, "Debug");
            UIMenuItem currPos = new UIMenuItem("Current Position");
            submenu.AddItem(currPos);
            UIMenuItem vehPos = new UIMenuItem("Vehicle Position");
            submenu.AddItem(vehPos);
            UIMenuItem vehName = new UIMenuItem("Vehicle Model Name");
            submenu.AddItem(vehName);
            submenu.RefreshIndex();

            submenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == currPos)
                    GTA.UI.Screen.ShowHelpTextThisFrame("Current Position: " + Game.Player.Character.Position.ToString() + " H:" + Game.Player.Character.Heading.ToString());
                else if (item == vehPos)
                    GTA.UI.Screen.ShowHelpTextThisFrame("Vehicle Position: " + Game.Player.Character.CurrentVehicle.Position.ToString() + " H:" + Game.Player.Character.CurrentVehicle.Heading.ToString());
                else if (item == vehName)
                    GTA.UI.Screen.ShowHelpTextThisFrame("Vehicle Name: " + Game.Player.Character.CurrentVehicle.Model.ToString());
            };
        }
    }
}
