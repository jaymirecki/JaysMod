using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using GVehicle = GTA.Vehicle;

namespace JaysModFramework
{
    public class Vehicle: IEquatable<Vehicle>
    {
        public GVehicle BaseVehicle { get; set; }
        public string ID
        {
            get { return _id; }
            set
            {
                SpawnedVehicles.TryRemove(_id);
                SpawnedVehicles.TryAdd(value, this);
                _id = value;
            }
        }
        private string _id;
        internal static OOD.Collections.Dictionary<string, Vehicle> SpawnedVehicles = new OOD.Collections.Dictionary<string, Vehicle>();
        #region Helpers
        public static Vehicle SpawnVehicle(VehicleHash modelHash, Vector3 position, float heading)
        {
            GVehicle newVehicle = SpawnVehicle(modelHash, position);
            Vehicle vehicle = new Vehicle(newVehicle);
            vehicle.Heading = heading;
            return vehicle;
        }
        private static GVehicle SpawnVehicle(VehicleHash modelHash, Vector3 position = new Vector3())
        {
            Model model = new Model(modelHash);
            GVehicle[] vehicles = GTA.World.GetNearbyVehicles(position.BaseVector, 3f, model);
            if (vehicles.Length > 0)
            {
                for (int i = 0; i < vehicles.Length; i++)
                {
                    DeleteVehicle(vehicles[i]);
                }
            }

            return GTA.World.CreateVehicle(model, position.BaseVector);
        }
        public static bool DeleteVehicle(string targetId)
        {
            if (SpawnedVehicles.TryGetValue(targetId, out Vehicle target))
            {
                SpawnedVehicles.TryRemove(targetId);
                return HardDeleteVehicle(target);
            }
            return false;
        }
        public static bool DeleteVehicle(Vehicle target)
        {
            return DeleteVehicle(target.ID) || HardDeleteVehicle(target);
        }
        public static bool DeleteVehicle(GVehicle target, bool tryDeleteNPC = true)
        {
            Vehicle targetVehicle = null;
            if (target == null)
            {
                return false;
            }
            foreach (Vehicle v in SpawnedVehicles.Values)
            {
                if (v.BaseVehicle != null && v.BaseVehicle == target)
                {
                    targetVehicle = v;
                    break;
                }
            }
            if (targetVehicle is null)
            {
                target.Delete();
                return true;
            }
            return DeleteVehicle(targetVehicle);
        }
        private static bool HardDeleteVehicle(Vehicle target)
        {
            if (target.BaseVehicle is null)
            {
                return false;
            }
            if (target.BaseVehicle.Exists())
            {
                target.BaseVehicle.Delete();
                return true;
            }
            return false;
        }
        public static void DeleteAllVehicles()
        {
            List<string> npcs = new List<string>(SpawnedVehicles.Keys);
            foreach (string targetId in npcs)
            {
                DeleteVehicle(targetId);
            }
        }
        private static void CopyVehicle(GVehicle source, GVehicle destination)
        {
            destination.Heading = source.Heading;
            destination.IsEngineRunning = source.IsEngineRunning;
            destination.IsSirenActive = source.IsSirenActive;
            destination.IsSirenSilent = Function.Call<bool>((Hash)0xB5CC40FBCB586380, source);
            destination.IsTaxiLightOn = source.IsTaxiLightOn;
            destination.LandingGearState = source.LandingGearState;
            destination.Position = source.Position;
            destination.Speed = source.Speed;
            destination.Health = source.Health;
            destination.MaxHealth = source.MaxHealth;
            destination.DirtLevel = source.DirtLevel;
            destination.Mods.PrimaryColor = source.Mods.PrimaryColor;
            destination.Mods.SecondaryColor = source.Mods.SecondaryColor;
            SetDoorOpenStatus(destination.Doors, VehicleDoorIndex.FrontLeftDoor, GetDoorOpenStatus(source.Doors, VehicleDoorIndex.FrontLeftDoor));
            SetDoorOpenStatus(destination.Doors, VehicleDoorIndex.FrontRightDoor, GetDoorOpenStatus(source.Doors, VehicleDoorIndex.FrontRightDoor));
            SetDoorOpenStatus(destination.Doors, VehicleDoorIndex.BackLeftDoor, GetDoorOpenStatus(source.Doors, VehicleDoorIndex.BackLeftDoor));
            SetDoorOpenStatus(destination.Doors, VehicleDoorIndex.BackRightDoor, GetDoorOpenStatus(source.Doors, VehicleDoorIndex.BackRightDoor));
            SetDoorOpenStatus(destination.Doors, VehicleDoorIndex.Trunk, GetDoorOpenStatus(source.Doors, VehicleDoorIndex.Trunk));
        }
        private static bool GetDoorOpenStatus(VehicleDoorCollection doorCollection, VehicleDoorIndex door)
        {
            if (doorCollection.Contains(door))
            {
                return doorCollection[door].IsOpen;
            }
            return false;
        }
        private static void SetDoorOpenStatus(VehicleDoorCollection doorCollection, VehicleDoorIndex door, bool isOpen)
        {
            if (doorCollection.Contains(door))
            {
                if (isOpen)
                {
                    doorCollection[door].Open();
                }
                else
                {
                    doorCollection[door].Close();
                }
            }
        }
        #endregion
        #region Base Properties
        public bool HasSiren
        {
            get { return BaseVehicle.HasSiren; }
        }
        public float Heading
        {
            get { return BaseVehicle.Heading; }
            set { BaseVehicle.Heading = value; }
        }
        public bool IsEngineRunning
        {
            get { return BaseVehicle.IsEngineRunning; }
            set {
                Debug.Notify("Before: " + BaseVehicle.IsEngineRunning);
                BaseVehicle.IsEngineRunning = value;
                if (value)
                {
                    //Debug.Log("Jet Engine");
                    //Function.Call(GTA.Native.Hash._SET_VEHICLE_JET_ENGINE_ON, BaseVehicle.Handle, value);
                }
                Debug.Notify("After: " + BaseVehicle.IsEngineRunning);
            }
        }
        public bool IsSirenActive
        {
            get { return BaseVehicle.IsSirenActive; }
            set { BaseVehicle.IsSirenActive = value; }
        }
        public bool IsSirenSilent
        {
            get { return !Function.Call<bool>((Hash)0xB5CC40FBCB586380, BaseVehicle); } //_IS_VEHICLE_SIREN_SOUND_ON
            set { BaseVehicle.IsSirenSilent = value; }
        }
        public bool IsTaxiLightOn
        {
            get { return BaseVehicle.IsTaxiLightOn; }
            set { BaseVehicle.IsTaxiLightOn = value; }
        }
        public VehicleLandingGearState LandingGearState
        {
            get { return BaseVehicle.LandingGearState; }
            set { BaseVehicle.LandingGearState = value; }
        }
        public Model Model
        {
            get { return BaseVehicle.Model; }
        }
        public Vector3 Position
        {
            get { return new Vector3(BaseVehicle.Position); }
            set { BaseVehicle.Position = value.BaseVector; }
        }
        public Vector3 Rotation
        {
            get { return new Vector3(BaseVehicle.Rotation); }
            set { BaseVehicle.Rotation = value.BaseVector; }
        }
        public float Speed
        {
            get { return BaseVehicle.Speed; }
            set { BaseVehicle.Speed = value; }
        }
        public float WheelSpeed
        {
            get { return BaseVehicle.WheelSpeed; }
        }
        public int Health
        {
            get { return BaseVehicle.Health;  }
            set { BaseVehicle.Health = value; }
        }
        public int MaxHealth
        {
            get { return BaseVehicle.MaxHealth; }
            set { BaseVehicle.MaxHealth = value; }
        }
        public float DirtLevel
        {
            get { return BaseVehicle.DirtLevel; }
            set { BaseVehicle.DirtLevel = value; }
        }
        public VehicleHash Hash
        {
            get {
                return (VehicleHash)BaseVehicle.Model.Hash; }
            set
            {
                GVehicle newVehicle = SpawnVehicle(value, new Vector3(BaseVehicle.Position));
                CopyVehicle(BaseVehicle, newVehicle);
                BaseVehicle.Delete();
                BaseVehicle = newVehicle;
            }
        }
        #region Doors
        public VehicleDoorCollection Doors
        {
            get { return BaseVehicle.Doors; }
        }
        public bool FrontLeftDoorOpen
        {
            get { return GetDoorOpenStatus(Doors, VehicleDoorIndex.FrontLeftDoor); }
            set { SetDoorOpenStatus(Doors, VehicleDoorIndex.FrontLeftDoor, value); }
        }
        public bool FrontRightDoorOpen
        {
            get { return GetDoorOpenStatus(Doors, VehicleDoorIndex.FrontRightDoor); }
            set { SetDoorOpenStatus(Doors, VehicleDoorIndex.FrontRightDoor, value); }
        }
        public bool BackLeftDoorOpen
        {
            get { return GetDoorOpenStatus(Doors, VehicleDoorIndex.BackLeftDoor); }
            set { SetDoorOpenStatus(Doors, VehicleDoorIndex.BackLeftDoor, value); }
        }
        public bool BackRightDoorOpen
        {
            get { return GetDoorOpenStatus(Doors, VehicleDoorIndex.BackRightDoor); }
            set { SetDoorOpenStatus(Doors, VehicleDoorIndex.BackRightDoor, value); }
        }
        public bool TrunkOpen
        {
            get { return GetDoorOpenStatus(Doors, VehicleDoorIndex.Trunk); }
            set { SetDoorOpenStatus(Doors, VehicleDoorIndex.Trunk, value); }
        }
        #endregion Doors
        #endregion BaseVehicleProperties
        #region Base Vehicle Mods
        public VehicleModCollection Mods
        {
            get { return BaseVehicle.Mods; }
        }
        public VehicleColor PrimaryColor
        {
            get { return Mods.PrimaryColor; }
            set { Mods.PrimaryColor = value; }
        }
        public VehicleColor SecondaryColor
        {
            get { return Mods.SecondaryColor; }
            set { Mods.SecondaryColor = value; }
        }
        #endregion BaseVehicleMods
        #region Base Methods
        
        #endregion Base Methods
        #region Extension Properties
        public string ModelName { get { return BaseVehicle.LocalizedName; } }
        public string Name;
        #endregion
        #region Extension Methods
        public void ToggleSirenNoise()
        {
            if (HasSiren && IsSirenActive)
            {
                IsSirenSilent = !IsSirenSilent;
            }
        }
        #endregion
        #region Constructors
        public Vehicle(GVehicle baseVehicle)
        {
            this.BaseVehicle = baseVehicle;
            Name = "";
            ID = "";// IDGenerator.VehicleID(this);
            SpawnedVehicles.TryAdd(ID, this);
        }
        public Vehicle()
        {
            BaseVehicle = SpawnVehicle(VehicleHash.Akuma, new Vector3());
            Name = "Generic";
            ID = "";//IDGenerator.VehicleID(this);
        }
        #endregion Constructors
        #region Operators
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is Vehicle ? Equals((Vehicle)obj) : base.Equals(obj);
        }
        public bool Equals(Vehicle other)
        {
            return !(other is null) && ID == other.ID;
        }
        public static bool operator ==(Vehicle veh1, Vehicle veh2)
        {
            if (veh1 is null)
            {
                return veh2 is null;
            }
            return (veh1.Equals(veh2));
        }
        public static bool operator !=(Vehicle veh1, Vehicle veh2)
        {
            return !(veh1 == veh2);
        }
        public static implicit operator GVehicle(Vehicle v) => v.BaseVehicle;
        public static explicit operator Vehicle(GVehicle v) => new Vehicle(v);
        #endregion Operators
    }
}
