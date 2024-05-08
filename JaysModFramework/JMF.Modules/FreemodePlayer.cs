using JMF.Math;
using JMF.Native;
using JMF.UI;
using System;

namespace JMF.Modules
{
    public class FreemodePlayer : InternalModule
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Freemode Player";
        public override string ModuleDescription { get; } = "Converts the player to male freemode ped";
        public override bool DefaultActivationState { get { return false; } }
        private bool respawning = false;
        private Vector3 originalPosition;
        private float originalHeading;
        private DateTime originalTime;
        public FreemodePlayer() : base() { }
        private Model originalModel;
        public override void OnActivate()
        {
            Ped playerPed = Game.Player.Character;
            originalModel = playerPed.Model;
            originalPosition = playerPed.Position;
            originalHeading = playerPed.Heading;
            originalTime = Game.Clock.Date;
            Game.Player.SetModel(PedHash.FreemodeMale01);
            //Game.FadeOutAfterDeath = false;
        }
        public override void OnDeactivate()
        {
            Game.Player.SetModel(originalModel);
            Ped playerPed = Game.Player.Character;
            Debug.Notify(originalPosition, true);
            Debug.Notify(originalHeading, true);
            playerPed.Position = originalPosition;
            playerPed.Heading = originalHeading;
            //Game.Clock.Date = originalTime;
            //Game.FadeOutAfterDeath = true;
        }
        public override void OnTick()
        {
            Ped playerPed = Game.Player.Character;
            Function.Call(Hash.TerminateAllScriptsWithThisName, "respawn_controller");
            if (Game.Player.IsDead)
            {
                Respawn();
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
            Debug.Notify("begin fadeout", true);
            Screen.FadeOut(2000);
            while (!Screen.IsFadedOut)
                Thread.Yield();
            Debug.Notify("faded out", true);
            Function.Call(Hash.NetworkResurrectLocalPlayer, playerPed.Position.X, playerPed.Position.Y, playerPed.Position.Z, 0f, 0, false);
            playerPed.CancelRagdoll();
            Game.TimeScale = 1f;
            playerPed.Position = originalPosition;
            playerPed.Heading = originalHeading;
            Game.Clock.Date = originalTime;
            Debug.Notify("begin sleep", true);
            Thread.Sleep(2000);
            Debug.Notify("end sleep", true);
            playerPed.IsInvincible = false;
            Debug.Notify("begin fadein", true);
            Screen.FadeIn(2000);
            while (!Screen.IsFadedIn)
                Thread.Yield();
            Debug.Notify("faded in", true);
            Thread.Sleep(5000);
            Debug.Notify("display hud", true);
            Function.Call(Hash.DisplayHud, true);
            Function.Call(Hash.DisplayRadar, true);
            respawning = false;
        }
    }
}
