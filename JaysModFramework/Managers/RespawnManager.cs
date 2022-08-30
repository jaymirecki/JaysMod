using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public class RespawnManager: Script
    {
        private static NPC PlayerNPC;
        private static bool Activated;
        private static Vector3 OriginalPosition;
        private static float OriginalHeading;
        private static DateTime OriginalDateTime;
        public RespawnManager()
        {
            Tick += OnTick;
        }
        public void OnTick(object sender, EventArgs e)
        {
            if (Activated && (PlayerNPC != null) && (Game.Player.IsDead || PlayerNPC.Health == 0 || PlayerNPC.IsDead))
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
            Load();
            Wait(2000);
            playerPed.IsInvincible = false;
            GTA.UI.Screen.FadeIn(2000);
        }
        public static void Activate(NPC targetNPC)
        {
            Activated = true;
            PlayerNPC = targetNPC;
            OriginalHeading = PlayerNPC.Heading;
            OriginalPosition = PlayerNPC.Position;
            OriginalDateTime = World.CurrentDate;
        }
        public static void Deactivate()
        {
            Activated = false;
            PlayerNPC = null;
        }
        private static void Load()
        {
            PlayerNPC.Heading = OriginalHeading;
            PlayerNPC.Position = OriginalPosition;
            World.CurrentDate = OriginalDateTime;
        }
    }
}
