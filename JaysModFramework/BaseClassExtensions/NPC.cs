using GTA;
using GTA.Native;
using System.Xml;

namespace JaysModFramework
{
    public class NPC: IXmlSerializable
    {
        internal static XmlDictionary<string,NPC> SpawnedNPCs = new XmlDictionary<string,NPC>();
        private Ped BasePed;
        private const string PlayerID = "JMFNPCPlayer";
        public static NPC PlayerNPC
        {
            get
            {
                NPC player;
                if (SpawnedNPCs.TryGetValue(PlayerID, out player))
                {
                    return player;
                }
                return new NPC(Game.Player.Character);
            }
        }
        public static void ClearPlayerNPC()
        {
            SpawnedNPCs.Remove(PlayerID);
        }
        #region Base Values
        public Vector3 Position
        {
            get { return new Vector3(BasePed.Position); }
            set { BasePed.Position = value.BaseVector; }
        }
        public float Heading
        {
            get { return BasePed.Heading; }
            set { BasePed.Heading = value; }
        }
        public int Health
        {
            get { return BasePed.Health; }
            set { BasePed.Health = value; }
        }
        public int MaxHealth
        {
            get { return BasePed.MaxHealth; }
            set { BasePed.MaxHealth = value; }
        }
        public bool IsDead
        {
            get { return BasePed.IsDead; }
            set
            {
                if (value)
                {
                    BasePed.Kill();
                }
                else
                {
                    BasePed.Resurrect();
                }
            }
        }
        public bool IsPlayer
        {
            get { return BasePed.IsPlayer; }
        }
        public float HeightAboveGround
        {
            get { return BasePed.HeightAboveGround; }
        }
        public Gender Gender
        {
            get { return BasePed.Gender; }
            //set 
            //{
            //    if (value != BasePed.Gender) {
            //        if (value == Gender.Male)
            //        {
            //            BasePed.Delete();.Model = new Model(PedHash.FreemodeMale01);
            //        }
            //        else
            //        {

            //        }
            //    }
            //}
        }
        #endregion Base Values
        #region BaseMethods
        public bool IsInVehicle()
        {
            return BasePed.IsInVehicle();
        }
        #endregion
        #region Extension Values
        public string ID
        {
            get { return _id; }
            set 
            { 
                SpawnedNPCs.TryRemove(_id);
                SpawnedNPCs.TryAdd(value, this);
                _id = value;
            }
        }
        private string _id;
        public string Name;
        public Outfit Outfit
        {
            get
            {
                Outfit outfit = new Outfit();
                outfit.FromPed(BasePed);
                return outfit;
            }
            set
            {
                value.Hair = Hair;
                value.HairColor = HairColor;
                value.ApplyToPed(BasePed, false);
            }
        }
        #endregion
        #region ExtensionMethods
        public bool IsScuba
        {
            get { return Outfit.GetComponent(BasePed, OutfitComponents.AccOne) == (int)MaleOutfitPieces.Undershirts.ScubaTank; }
        }
        public bool IsAccOneDefaulted
        {
            get { return Outfit.GetComponent(BasePed, OutfitComponents.AccOne) == 0; }
        }
        #endregion
        public int Hair = 19;
        public int HairColor = 1;
        #region Constructors
        public NPC(Ped ped): this()
        {
            BasePed = ped;
            Outfit = MaleOutfitTemplates.Casual;
            Outfit.ApplyToPed(BasePed, false);

            if (IsPlayer)
            {
                ID = PlayerID;
            }
            else
            {
                ID = IDGenerator.NPCID(this);
            }
            if (!SpawnedNPCs.TryAdd(ID, this))
            {
                BasePed.Delete();
            }
        }
        public NPC()
        {
        }
        #endregion
        #region XMLSerialization
        public void WriteXml(XmlWriter writer)
        {
            //writer.WriteAttributeString("ID", ID);
            XmlSerialization.WriteElement(writer, "ID", ID);
            XmlSerialization.WriteComplexElement(writer, "Position", Position);
            XmlSerialization.WriteElement(writer, "Heading", Heading);
            XmlSerialization.WriteElement(writer, "Health", Health);
            XmlSerialization.WriteElement(writer, "MaxHealth", MaxHealth);
            XmlSerialization.WriteElement(writer, "IsDead", IsDead);
            XmlSerialization.WriteComplexElement(writer, "Outfit", Outfit);
        }
        public void ReadXml(XmlReader reader)
        {
            //ID = reader.GetAttribute("ID");
            ID = XmlSerialization.ReadElement<string>(reader, "ID");
            SetModelBasedOnId();
            Position = XmlSerialization.ReadComplexElement<Vector3>(reader, "Position");
            Heading = XmlSerialization.ReadElement<float>(reader, "Heading");
            Health = XmlSerialization.ReadElement<int>(reader, "Health");
            MaxHealth = XmlSerialization.ReadElement<int>(reader, "MaxHealth");
            IsDead = XmlSerialization.ReadElement<bool>(reader, "IsDead");
            Outfit = XmlSerialization.ReadComplexElement<Outfit>(reader, "Outfit");
        }
        private void SetModelBasedOnId()
        {
            //Debug.Log(ID);
            if (ID == PlayerID)
            {
                BasePed = Game.Player.Character;
            }
            else
            {
                int modelHash = Function.Call<int>(Hash.GET_HASH_KEY, "mp_m_freemode_01");
                //Model newModel = new Model(PedHash.FreemodeFemale01);
                //BasePed = World.CreatePed(newModel, new GTA.Math.Vector3());
                BasePed = SpawnPed(PedHash.FreemodeFemale01, new Vector3(), 0f);
            }
        }
        #endregion
        private Ped SpawnPed(PedHash ped, Vector3 position, float heading)
        {
            return World.CreatePed(new Model(ped), position.BaseVector, heading);
        }
    }
}
