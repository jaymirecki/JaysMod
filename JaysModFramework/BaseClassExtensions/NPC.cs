﻿//using System.Collections.Generic;

//namespace JaysModFramework
//{
//    public class NPC
//    {
//        internal static OOD.Collections.Dictionary<string, NPC> SpawnedNPCs = new OOD.Collections.Dictionary<string, NPC>();
//        private Ped BasePed;
//        private const string PlayerID = "JMFNPCPlayer";
//        #region Helpers
//        public static NPC PlayerNPC
//        {
//            get
//            {
//                NPC player;
//                if (SpawnedNPCs.TryGetValue(PlayerID, out player))
//                {
//                    return player;
//                }
//                return new NPC(Game.LocalPlayer.Character);
//            }
//        }
//        public static void ClearPlayerNPC()
//        {
//            SpawnedNPCs.TryRemove(PlayerID);
//        }
//        public static void DeleteAllNPCs()
//        {
//            List<string> npcs = new List<string>(SpawnedNPCs.Keys);
//            foreach (string targetId in npcs)
//            {
//                DeleteNPC(targetId);
//            }
//        }
//        public static void DeleteNPC(Ped target, bool tryDeleteNPC = true)
//        {
//            NPC targetNPC = null;
//            foreach (NPC npc in SpawnedNPCs.Values)
//            {
//                if (npc.BasePed == target)
//                {
//                    targetNPC = npc;
//                    break;
//                }
//            }
//            if (targetNPC is null && target.Exists())
//            {
//                target.Delete();
//                return;
//            }
//            DeleteNPC(targetNPC);
//        }
//        public static void DeleteNPC(NPC target)
//        {
//            DeleteNPC(target.ID);
//            HardDeleteNPC(target);
//        }
//        public static void DeleteNPC(string targetId)
//        {
//            if (SpawnedNPCs.TryGetValue(targetId, out NPC target))
//            {
//                HardDeleteNPC(target);
//                SpawnedNPCs.TryRemove(targetId);
//            }
//        }
//        private static void HardDeleteNPC(NPC target)
//        {
//            if (target.BasePed is null)
//            {
//                return;
//            }
//            if (target.BasePed.Exists())
//            {
//                target.BasePed.Delete();
//            }
//        }
//        private static void CopyPedValues(Ped source, Ped destination)
//        {
//            destination.Position = source.Position;
//            destination.Heading = source.Heading;
//            destination.Health = source.Health;
//            destination.MaxHealth = source.MaxHealth;
//            destination.Armor = source.Armor;
//        }
//        private static Ped SpawnPed(PedHash ped, Vector3 position = new Vector3(), float heading = 0)
//        {
//            return World.CreatePed(new Model((uint)ped), position.BaseVector, heading);
//        }
//        #endregion
//        #region Base Values
//        public Vector3 Position
//        {
//            get { return new Vector3(BasePed.Position); }
//            set { BasePed.Position = value.BaseVector; }
//        }
//        public float Heading
//        {
//            get { return BasePed.Heading; }
//            set { BasePed.Heading = value; }
//        }
//        public int Health
//        {
//            get { return BasePed.Health; }
//            set { BasePed.Health = value; }
//        }
//        public int MaxHealth
//        {
//            get { return BasePed.MaxHealth; }
//            set { BasePed.MaxHealth = value; }
//        }
//        public bool IsDead
//        {
//            get { return BasePed.IsDead; }
//            set
//            {
//                if (value)
//                {
//                    BasePed.Kill();
//                }
//                else
//                {
//                    BasePed.Resurrect();
//                }
//            }
//        }
//        public bool IsPlayer
//        {
//            get { return BasePed.IsPlayer; }
//        }
//        public float HeightAboveGround
//        {
//            get { return BasePed.HeightAboveGround; }
//        }
//        public Model Model { get { return BasePed.Model; } }
//        #endregion Base Values
//        #region BaseMethods
//        public bool IsInVehicle()
//        {
//            return BasePed.IsInVehicle();
//        }
//        #endregion
//        #region Extension Values
//        public string ID
//        {
//            get { return _id; }
//            set
//            {
//                if (value == _id)
//                {
//                    return;
//                }
//                if (value == PlayerID)
//                {
//                    SwapPlayerPedToBasePed();
//                }
//                SpawnedNPCs.TryRemove(_id);
//                DeleteNPC(value);
//                SpawnedNPCs.TryAdd(value, this);
//                _id = value;
//            }
//        }
//        private void SwapPlayerPedToBasePed()
//        {
//            Game.LocalPlayer.Model = new Rage.Model(Model.BaseModel.Hash);
//            CopyPedValues(BasePed, Game.LocalPlayer.Character);
//            BasePed.Delete();
//            BasePed = Game.LocalPlayer.Character;
//        }
//        private string _id;
//        public string Name;
//        private Gender _gender;
//        public Gender Gender
//        {
//            get { return _gender; }
//            set
//            {
//                if (value != _gender)
//                {
//                    if (value == Gender.Male)
//                    {
//                        SwapGender(PedHash.FreemodeMale01);
//                    }
//                    else
//                    {
//                        SwapGender(PedHash.FreemodeFemale01);
//                    }
//                    _gender = value;
//                }
//            }
//        }
//        private void SwapGender(PedHash model)
//        {
//            Ped newPed = SpawnPed(model);
//            newPed.Position = BasePed.Position;
//            CopyPedValues(BasePed, newPed);
//            if (IsPlayer)
//            {
//                BasePed = newPed;
//                SwapPlayerPedToBasePed();
//            }
//            else
//            {
//                BasePed.Delete();
//                BasePed = newPed;
//            }
//        }
//        //public Outfit Outfit
//        //{
//        //    get
//        //    {
//        //        Outfit outfit = new Outfit();
//        //        outfit.FromPed(BasePed);
//        //        return outfit;
//        //    }
//        //    set
//        //    {
//        //        //value.Hair = Hair;
//        //        //value.HairColor = HairColor;
//        //        value.ToPed(BasePed, false);
//        //    }
//        //}
//        #region Vehicle
//        public string VehicleId
//        {
//            get { return IsInVehicle() ? Vehicle().ID : ""; }
//            set
//            {
//                _vehicleId = value;
//                SetIntoVehicle();
//            }
//        }
//        public VehicleSeat VehicleSeat
//        {
//            get
//            {
//                if (IsInVehicle())
//                {
//                    return BasePed.SeatIndex;
//                }
//                return VehicleSeat.None;
//            }
//            set
//            {
//                _vehicleSeat = value;
//                SetIntoVehicle();
//            }
//        }
//        private string _vehicleId = "";
//        private VehicleSeat _vehicleSeat = VehicleSeat.None;
//        private void SetIntoVehicle()
//        {
//            if (_vehicleId != null && _vehicleSeat != VehicleSeat.None)
//            {
//                if (JaysModFramework.Vehicle.SpawnedVehicles.TryGetValue(_vehicleId, out Vehicle v))
//                {
//                    BasePed.SetIntoVehicle(v, _vehicleSeat);
//                }
//            }
//        }
//        #endregion Vehicle
//        #endregion Extension Values
//        #region ExtensionMethods
//        //public bool IsScuba
//        //{
//        //    get { return Outfit.GetComponent(BasePed, OutfitComponents.AccOne) == (int)MaleOutfitPieces.Undershirts.ScubaTank; }
//        //}
//        //public bool IsAccOneDefaulted
//        //{
//        //    get { return Outfit.GetComponent(BasePed, OutfitComponents.AccOne) == 0; }
//        //}
//        public Vehicle Vehicle()
//        {
//            if (IsInVehicle())
//            {
//                foreach (Vehicle v in JaysModFramework.Vehicle.SpawnedVehicles.Values)
//                {
//                    if (v == BasePed.CurrentVehicle)
//                    {
//                        return v;
//                    }
//                }
//                new Vehicle(BasePed.CurrentVehicle);
//            }
//            return null;
//        }
//        #endregion
//        public int Hair = 19;
//        public int HairColor = 1;
//        #region Constructors
//        public NPC(Ped ped) : this()
//        {
//            BasePed = ped;
//            //Outfit = MaleOutfitTemplates.Casual;
//            //Outfit.ApplyToPed(BasePed, false);

//            if (IsPlayer)
//            {
//                ID = PlayerID;
//            }
//            else
//            {
//                ID = IDGenerator.NPCID(this);
//            }
//        }
//        public NPC()
//        {
//            BasePed = SpawnPed(PedHash.FreemodeMale01);
//            Name = "Generic";
//            _id = IDGenerator.NPCID(this);
//        }
//        #endregion
//    }
//}
