using JMF.Math;
using JMF.Native;

namespace JMF.Modules
{
    public class Yacht : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "YachtPoC";
        public override string ModuleDescription { get; } = "PoC for Galaxy Super Yacht";
        public override ModuleSettings Settings { get { return new ModuleSettings(); } }
        private Vector3 position = new Vector3(-3542f, 1488f, 5.4f);
        private Vector3 playerPosition = new Vector3(-3542f, 1488f, 15.4f);
        public override void OnActivate()
        {
            int yachtHandle = Function.Call<int>(Hash.GetClosestObjectOfType, -3542f, 1488f, 5.4f, 3f, 0x4FCAD2E0);
            int model = Function.Call<int>(Hash.CreateObject, "apa_mp_apa_yacht_option3", 0f, 0f, 0f, false, false, false);
            Function.Call(Hash.AttachEntityToEntity, model, yachtHandle, 0, 0.0032f, 0.0028f, 14.5700f, 0f, 0f, 0f, false, false, false, false, 2, true);
            Game.Player.Character.Position = playerPosition;    
            Debug.Log(DebugSeverity.Warning, new Vector3((Rage.Vector3)Rage.Native.NativeFunction.Call((ulong)Hash.GetEntityCoords, typeof(Rage.Vector3), yachtHandle)).ToString());
        }
        public override void OnDeactivate()
        {

        }
    }
}
