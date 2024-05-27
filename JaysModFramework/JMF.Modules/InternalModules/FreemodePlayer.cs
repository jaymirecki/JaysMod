using System;

namespace JMF.Modules
{
    public class FreemodePlayer : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Freemode Player";
        public override string ModuleDescription { get; } = "Converts the player to male freemode ped";
        public override ModuleSettings Settings { get { return Framework.Config.FreemodePlayerSettings; } }
        private Vector3 originalPosition;
        private float originalHeading;
        private DateTime originalTime;
        public FreemodePlayer() : base() {
            Dependencies.Add(typeof(Respawner));
        }
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
