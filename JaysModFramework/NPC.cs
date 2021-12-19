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
    public class NPC
    {
        private Ped BasePed;
        public string ID;
        #region Base Values
        public Vector3 Position
        {
            get { return BasePed.Position; }
            set { BasePed.Position = value; }
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
        }
        public float HeightAboveGround
        {
            get { return BasePed.HeightAboveGround; }
        }
        #endregion Base Values
        public Outfit Outfit
        {
            get {
                Outfit outfit = new Outfit();
                outfit.FromPed(BasePed);
                return outfit; }
            set {
                value.Hair = Hair;
                value.HairColor = HairColor;
                value.ApplyToPed(BasePed, false);
            }
        }
        public bool IsScuba
        {
            get { return Outfit.GetComponent(BasePed, OutfitComponents.AccOne) == (int)MaleOutfitPieces.AccOne.ScubaTank; }
        }
        public bool IsAccOneDefaulted
        {
            get { return Outfit.GetComponent(BasePed, OutfitComponents.AccOne) == 0; }
        }
        public int Hair = 19;
        public int HairColor = 1;
        public NPC(string id, Ped ped): this(ped)
        {
            ID = id;
        }
        public NPC(Ped ped)
        {
            ID = "null";
            BasePed = ped;
            Outfit = MaleOutfitTemplates.Casual;
            Outfit.ApplyToPed(BasePed, false);
        }

        public void Save(SaveAndLoad save, string saveId, string prefix)
        {
            string myPrefix = SaveAndLoad.CombinePrefix(prefix, "npc_" + ID);
            save.SaveVector(saveId, myPrefix, Position);
            save.Save(saveId, myPrefix + "_heading", Heading);
            save.Save(saveId, myPrefix + "_health", Health);
            Outfit.Save(save, saveId, myPrefix);
        }
        public void Load(SaveAndLoad load, string saveId, string prefix)
        {
            string myPrefix = SaveAndLoad.CombinePrefix(prefix, "npc_" + ID);
            Position = load.LoadVector(saveId, myPrefix);
            Heading = load.Load(saveId, myPrefix + "_heading", 0);
            Health = load.Load(saveId, myPrefix + "_health", MaxHealth);
            Outfit.Load(load, saveId, myPrefix);
            Outfit.ApplyToPed(BasePed, false);
        }
    }
}
