using JMF.Interiors;
using JMF.Native;
using System.Collections.Generic;

namespace JMF.Modules
{
    public class Yacht : InternalModule<ModuleSettings>
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "YachtPoC";
        public override string ModuleDescription { get; } = "PoC for Galaxy Super Yacht";
        public override ModuleSettings Settings { get { return new ModuleSettings(); } }
        private Vector3 position = new Vector3(-777.4865f, 6566.9072f, 5.430f);
        private Vector3 playerPosition = new Vector3(-777.4865f, 6566.9072f, 15.430f);
        public override void OnActivate()
        {
            new IPL("Yacht", position, new List<string>() { "apa_yacht_grp12_2" }).Load();
            new IPL("Yacht", position, new List<string>() { "apa_yacht_grp12_2_int" }).Load();
            new IPL("Yacht", position, new List<string>() { "apa_yacht_grp12_2_lod" }).Load();
            Thread.Sleep(1000);
            int yachtHandle = Function.Call<int>(Hash.GetClosestObjectOfType, position.X, position.Y, position.Z, 100f, 0x4FCAD2E0);
            Function.Call(Hash.SetObjectTextureVariation, yachtHandle, 15);
            List<string> models = new List<string>() { "apa_mp_apa_yacht_o3_rail_a", "apa_mp_apa_yacht_option3", "apa_mp_apa_yacht_option3_cola", "apa_mp_apa_yacht_option3_colb", "apa_mp_apa_yacht_option3_colc", "apa_mp_apa_yacht_option3_cold", "apa_mp_apa_yacht_option3_cole" };
            foreach (string m in models)
            {
                CreateAndAttachProp(m, yachtHandle, new Vector3(0.0032f, 0.0028f, 14.5700f), new Vector3());
            }
            List<KeyValuePair<Vector3, Vector3>> doorPositions = new List<KeyValuePair<Vector3, Vector3>>() {
                new KeyValuePair<Vector3, Vector3>(new Vector3(0.01894f, -3.3871f, 6.6600f), new Vector3(0.0000f, 0.0000f, 90.2950f) ),
                new KeyValuePair<Vector3, Vector3>(new Vector3(0.0046f, -0.6018f, 6.6600f), new Vector3(0.0000f, 0.0000f, -89.7050f) ),
                new KeyValuePair<Vector3, Vector3>(new Vector3(-36.8202f, -1.2778f, 0.6500f), new Vector3(0.0000f, 0.0000f, -89.9550f) )
            };
            foreach(KeyValuePair<Vector3, Vector3> d in doorPositions)
            {
                CreateAndAttachProp("apa_mp_apa_yacht_door\0", yachtHandle, d.Key, d.Value);
            }
            CreateAndAttachProp("apa_mp_apa_yacht_jacuzzi_ripple1", yachtHandle, new Vector3(-50.8033f, -1.9774f, 0.1368f), new Vector3());

            Game.Player.Character.Position = playerPosition;
        }
        private void CreateAndAttachProp(string model, int yachtHandle, Vector3 position, Vector3 rotation)
        {
            uint modelHash = Function.Call<uint>(Hash.GetHashKey, model);
            int modelHandle = Function.Call<int>(Hash.GetClosestObjectOfType, position.X, position.Y, position.Z, 100f, modelHash);
            //Function.Call(Hash.DeleteObject, modelHandle);
            Function.Call(Hash.RequestModel, modelHash);
            Thread.Sleep(1000);
            modelHandle = Function.Call<int>(Hash.CreateObject, modelHash, 0f, 0f, 0f, false);
            Function.Call(Hash.AttachEntityToEntity, modelHandle, yachtHandle, 0, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, false, false, false, false, 2, true);
            Function.Call(Hash.SetObjectTextureVariation, modelHandle, 14);
        }
        public override void OnDeactivate()
        {

        }
    }
}
