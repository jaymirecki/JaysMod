using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GTA;
using GTA.Math;
using GTA.Native;

using GVehicle = GTA.Vehicle;

namespace JaysModFramework
{
    public class Vehicle: IEquatable<Vehicle>, IXmlSerializable
    {
        public GVehicle BaseVehicle { get; set; }

        internal string ID { get; set; }
        internal static XmlDictionary<string, Vehicle> SpawnedVehicles = new XmlDictionary<string, Vehicle>();
        #region Helpers
        public static Vehicle SpawnVehicle(VehicleHash modelHash, Vector3 position, float heading)
        {
            GVehicle newVehicle = SpawnVehicle(modelHash, position);
            Vehicle vehicle = new Vehicle(newVehicle);
            vehicle.Heading = heading;
            return vehicle;
        }
        private static GVehicle SpawnVehicle(VehicleHash modelHash, Vector3 position)
        {
            Model model = new Model(modelHash);
            GVehicle[] vehicles = GTA.World.GetNearbyVehicles(position.BaseVector, 3f, model);
            if (vehicles.Length > 0)
            {
                Debug.Log("Delete " + vehicles.Length + " vehicles");
                for (int i = 0; i < vehicles.Length; i++)
                {
                    DeleteVehicle(vehicles[i]);
                }
            }

            return GTA.World.CreateVehicle(model, position.BaseVector);
        }
        public static bool DeleteVehicle(string id)
        {
            Vehicle target = SpawnedVehicles[id];
            return HardDeleteVehicle(target);
        }
        public static bool DeleteVehicle(Vehicle target)
        {
            return DeleteVehicle(target.ID);
        }
        public static bool DeleteVehicle(GVehicle target)
        {
            Vehicle vehicle = null;
            if (target == null)
            {
                return false;
            }
            foreach (Vehicle v in SpawnedVehicles.Values)
            {
                if (v.BaseVehicle != null && v.BaseVehicle == target)
                {
                    vehicle = v;
                }
            }
            if (vehicle != null)
            {
                //return DeleteVehicle(vehicle);
            }
            return DeleteGTAVehicle(target);
        }
        private static bool HardDeleteVehicle(Vehicle vehicle)
        {
            vehicle.BaseVehicle.Delete();
            return true;
        }
        private static bool DeleteGTAVehicle(GVehicle target)
        {
            if (target.Exists())
            {
                target.Delete();
                return true;
            }
            return false;
        }
        #endregion
        #region Base Properties
        public VehicleDoorCollection Doors
        {
            get { return BaseVehicle.Doors; }
        }
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
            set { BaseVehicle.IsEngineRunning = value; }
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
            ID = IDGenerator.VehicleID(this);
            SpawnedVehicles.TryAdd(ID, this);
        }
        public Vehicle()
        {

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
        #region XML
        public void WriteXml(XmlWriter writer)
        {
            XmlSerialization.WriteElement(writer, "ID", ID);
            XmlSerialization.WriteElement(writer, "ModelHash", Model.Hash);
            XmlSerialization.WriteComplexElement(writer, "Position", Position);
            XmlSerialization.WriteElement(writer, "Heading", Heading);
            XmlSerialization.WriteElement(writer, "Health", Health);
            XmlSerialization.WriteElement(writer, "MaxHealth", MaxHealth);
            XmlSerialization.WriteEnumElement(writer, "PrimaryColor", PrimaryColor);
            XmlSerialization.WriteEnumElement(writer, "SecondaryColor", SecondaryColor);
            XmlSerialization.WriteElement(writer, "DirtLevel", DirtLevel);
            XmlSerialization.WriteElement(writer, "IsEngineRunning", IsEngineRunning);
        }
        public void ReadXml(XmlReader reader)
        {
            Debug.DEBUG = true;
            ID = XmlSerialization.ReadElement<string>(reader, "ID");
            int modelHash = XmlSerialization.ReadElement<int>(reader, "ModelHash");
            Vector3 position = XmlSerialization.ReadComplexElement<Vector3>(reader, "Position");
            BaseVehicle = SpawnVehicle(new Model(modelHash), position);
            Heading = XmlSerialization.ReadElement<float>(reader, "Heading");
            Health = XmlSerialization.ReadElement<int>(reader, "Health");
            MaxHealth = XmlSerialization.ReadElement<int>(reader, "MaxHealth");
            PrimaryColor = XmlSerialization.ReadEnumElement<VehicleColor>(reader, "PrimaryColor");
            SecondaryColor = XmlSerialization.ReadEnumElement<VehicleColor>(reader, "SecondaryColor");
            DirtLevel = XmlSerialization.ReadElement<float>(reader, "DirtLevel");
            IsEngineRunning = XmlSerialization.ReadElement<bool>(reader, "IsEngineRunning");
        }
        #endregion
    }
}
