using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;
using JaysModFramework;
using System.IO;

namespace JaysMod
{
    using Vector3 = JaysModFramework.Vector3;
    using Vehicle = JaysModFramework.Vehicle;

    [ScriptAttributes(NoDefaultInstance = true)]
    public partial class JaysMod : Script
    {
        private MenuPool ModMenuPool;
        private UIMenu planeMenu;
        private UIMenu ClosetMenu;
        SaveAndLoad SaverLoader;
        private string SaveId;

        private HUD Hud;
        private NPC PlayerNPC;
        private Weather Weather
        {
            get { return GTA.World.Weather; }
            set { GTA.World.Weather = value; }
        }
        
        private int Minutes = 0;

        private static bool DEBUG = true;

        public JaysMod()
        {
            ModMenuPool = new MenuPool();
            ModMenuPool.ResetCursorOnOpen = true;

            Hud = InstantiateScript<HUD>();

            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        public void Load(string saveId)
        {
            SaveId = saveId;
            SetupGame();
        }
        public void Load()
        {
            Load(SaveId);
        }
        private void SetupGame()
        {
            Function.Call(Hash.SET_MINIMAP_HIDE_FOW, true);
            Minutes = DateTime.Now.Minute;
            World.IsClockPaused = true;
            MaleOutfitTemplates.SetupOutfits();
            LoadModel("mp_m_freemode_01");
            TextReader reader = new StreamReader("./scripts/JMF/Saves/test1.xml");
            new State().DeserializeFromXML(reader);
            PlayerNPC = NPC.PlayerNPC;
            RestrictedAreas.DisableAll();
            ActivateManagers();
            ClosetMenu = Closet.Menu(PlayerNPC, ModMenuPool);
            ModMenuPool.Add(ClosetMenu);
            SpawnCarrier();
        }
        private void ActivateManagers()
        {
            RealTimeDuration.Activate();
            BigMapManager.Activate();
            SirenManager.Activate();
            RespawnManager.Activate(PlayerNPC);
            VisorManager.Activate(PlayerNPC);
            ScubaManager.Activate(PlayerNPC);
        }
        private void DeactivateManagers()
        {
            RealTimeDuration.Deactivate();
            BigMapManager.Deactivate();
            SirenManager.Deactivate();
            RespawnManager.Deactivate();
            VisorManager.Deactivate();
            ScubaManager.Deactivate();
        }

        private void SpawnCarrier()
        {
            IPLLoader.Load(IPL.AircraftCarrier);
            IPLLoader.Load(IPL.DiamondCasino);
        }
        public void New(string saveId)
        {
            SaveId = saveId;
            SetupGame();
            //Vehicle.SpawnVehicle(VehicleHash.Nimbus, new Vector3(1544.28f, 3143.25f, 41.5f), 313.05f);
        }
        public void Save()
        {
            TextWriter writer = new StreamWriter("./scripts/JMF/Saves/test1.xml");
            new State().SerializeToXml(writer);
        }

        public void Unload()
        {
            Hud.Abort();
            Hud = null;
            Function.Call(Hash.SET_MINIMAP_HIDE_FOW, false);
            RestrictedAreas.EnableAll();
            DeactivateManagers();

            World.IsClockPaused = false;
            IPLLoader.UnloadAll();
        }
        void OnTick(object sender, EventArgs e)
        {
            if (ModMenuPool != null)
                ModMenuPool.ProcessMenus();
            Function.Call(Hash.SET_RADAR_AS_INTERIOR_THIS_FRAME, 1076883030, 1700f, 2580f, 0, 0); // Show Prison Map
            Function.Call(Hash.SET_RADAR_AS_INTERIOR_THIS_FRAME, -1827983545, -2250f, 3100f, 0, 0); // Show Fort Zancudo Map
            Function.Call(Hash.SET_RADAR_AS_EXTERIOR_THIS_FRAME);
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

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            Ped player = Game.Player.Character;
            if (e.KeyCode == Keys.E)
            {
                //if (player.IsInVehicle() && 
                //    player.CurrentVehicle == playerPlane.BaseVehicle && 
                //    !ModMenuPool.IsAnyMenuOpen())
                //{
                //    planeMenu.Visible = true;
                //}
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (ModMenuPool.IsAnyMenuOpen())
                {
                    ModMenuPool.CloseAllMenus();
                }
                else
                {
                    ClosetMenu.Visible = true;
                }
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
