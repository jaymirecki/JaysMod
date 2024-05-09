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
            Debug.Log(DebugSeverity.Info, "time: " + Game.Clock.Date);
            originalTime = Game.Clock.Date;
            Game.Player.SetModel(PedHash.FreemodeMale01);
            Game.Player.SpecialAbilityEnabled = false;
        }
        public override void OnDeactivate()
        {
            Game.Player.SetModel(originalModel);
            Ped playerPed = Game.Player.Character;
            Debug.Notify(originalPosition, true);
            Debug.Notify(originalHeading, true);
            playerPed.Position = originalPosition;
            playerPed.Heading = originalHeading;
            Game.Clock.Date = originalTime;
            Game.Player.SpecialAbilityEnabled = true;
        }
    }
}
