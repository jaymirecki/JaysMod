using JMF.Native;
using JMF.UI;

namespace JMF.Modules
{
    public class Respawner : InternalModule<ModuleSettings>
    {
        const int POSTWAIT = 5000;
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Respawner";
        public override string ModuleDescription { get; } = "Handles the respawn cycle for non-SP peds";
        public override ModuleSettings Settings { get { return Framework.Config.RespawnerSettings; } }
        private bool respawning = false;
        public Respawner() : base() { }
        public override void OnTick()
        {
            Ped playerPed = Game.Player.Character;
            if (IsSPPed(playerPed))
            {
                return;
            }
            Function.Call(Hash.TerminateAllScriptsWithThisName, "respawn_controller");
            if (Game.Player.IsDead && !respawning)
            {
                Respawn();
            }
        }
        private bool IsSPPed(Ped playerPed)
        {
            switch (playerPed.Model.Hash)
            {
                case (uint)PedHash.Michael:
                    return true;
                case (uint)PedHash.Franklin:
                    return true;
                case (uint)PedHash.Trevor:
                    return true;
                default:
                    return false;
            }
        }
        private void Respawn()
        {
            respawning = true;
            // Prevent default death behavior
            Ped playerPed = Game.Player.Character;
            Function.Call(Hash.NetworkRequestControlOfEntity, playerPed.Handle);
            playerPed.IsInvincible = true;
            Function.Call(Hash.IgnoreNextRestart, true);
            Screen.StopAllEffects();

            // Custom death behavior
            Function.Call(Hash.ResetPlayerArrestState, playerPed.Handle);
            //playerPed.Ragdoll();
            Thread.Sleep(5000);

            // Fade and respawn
            Screen.FadeOut(2000);
            while (!Screen.IsFadedOut)
                Thread.Yield();
            Function.Call(Hash.NetworkResurrectLocalPlayer, playerPed.Position.X, playerPed.Position.Y, playerPed.Position.Z, 0f, 0, false);
            Game.Player.IsInvincible = true;
            playerPed.CancelRagdoll();
            Game.TimeScale = 1f;
            Vector3 position = playerPed.Position;
            position.Z = playerPed.Position.Z - playerPed.HeightAboveGround + 1;
            playerPed.Position = position;
            //playerPed.Heading = playerPed.Heading;
            Thread.Sleep(2000);
            Screen.FadeIn(2000);
            while (!Screen.IsFadedIn)
                Thread.Yield();
            Thread.Sleep(POSTWAIT);
            Game.Player.IsInvincible = false;
            Function.Call(Hash.DisplayHud, true);
            Function.Call(Hash.DisplayRadar, true);
            respawning = false;
        }
    }
}
