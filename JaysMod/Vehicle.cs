using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using GTA.Math;
using GTA.Native;

using GVehicle = GTA.Vehicle;

namespace JaysMod
{
    class Vehicle: IEquatable<Vehicle>
    {
        public GVehicle BaseVehicle { get; }

        internal string ID { get; }
        private static Dictionary<string, Vehicle> vehicles;

        private static void Add(Vehicle vehicle)
        {
            if (vehicles is null)
            {
                vehicles = new Dictionary<string, Vehicle>();
            }
            vehicles.Add(vehicle.ID, vehicle);
        }

        public static void SaveAll(ScriptSettings ini, string savePrefix)
        {
            string ids = "";
            foreach (Vehicle vehicle in vehicles.Values)
            {
                ids += vehicle.ID;
                vehicle.Save(ini, savePrefix);
            }
        }

        #region Vehicle Properties
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
            get { return BaseVehicle.Model;  }
        }

        #region Mods
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
        public Vector3 Position
        {
            get { return BaseVehicle.Position; }
            set { BaseVehicle.Position = value; }
        }
        public Vector3 Rotation
        {
            get { return BaseVehicle.Rotation; }
            set { BaseVehicle.Rotation = value; }
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
        #endregion BaseVehicleProperties

        #region Save Prefix
        private enum Pref
        {
            Model,
            Position,
            PrimaryColor,
            SecondaryColor,
            Rotation,
            Heading,
        }
        private string Prefix(Pref pref)
        {
            return ID + "-" + Enum.GetName(typeof(Pref), pref);
        }
        #endregion Save Prefix

        #region Constructors
        public Vehicle(): this(System.Guid.NewGuid().ToString()) {}
        public Vehicle(string id)
        {
            ID = id;
        }
        public Vehicle(GVehicle baseVehicle): this()
        {
            this.BaseVehicle = baseVehicle;
        }
        public Vehicle(ScriptSettings ini, string id, string savePrefix): this(id)
        {
            Load(ini, savePrefix);
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
            return ID == other.ID;
        }
        public static bool operator ==(Vehicle veh1, Vehicle veh2)
        {
            return veh1.Equals(veh2);
        }
        public static bool operator !=(Vehicle veh1, Vehicle veh2)
        {
            return !(veh1 == veh2);
        }
        public static implicit operator GVehicle(Vehicle v) => v.BaseVehicle;
        public static explicit operator Vehicle(GVehicle v) => new Vehicle(v);
        #endregion Operators

        public void Save(ScriptSettings ini, string savePrefix)
        {
            string section = "Save-" + savePrefix;
            ini.SetValue(section, Prefix(Pref.Model), Model.Hash);
            ini.SetValue(section, Prefix(Pref.Position), Position);
            ini.SetValue(section, Prefix(Pref.Heading), Heading);
            ini.SetValue(section, Prefix(Pref.PrimaryColor), PrimaryColor);
            ini.SetValue(section, Prefix(Pref.SecondaryColor), SecondaryColor);
            ini.SetValue(section, Prefix(Pref.Rotation), Rotation);
        }
        public void Load(ScriptSettings ini, string savePrefix)
        {
            string section = "Save-" + savePrefix;
            Model model = new Model(ini.GetValue(section, Prefix(Pref.Model), VehicleHash.Seminole));
            Vector3 position = ini.GetValue(section, Prefix(Pref.Position), new Vector3(6, 9, 70.5f));
            float heading = ini.GetValue(section, Prefix(Pref.Heading), 251);
            World.CreateVehicle(model, position, heading);

            PrimaryColor = ini.GetValue(section, Prefix(Pref.PrimaryColor), VehicleColor.Blue);
            SecondaryColor = ini.GetValue(section, Prefix(Pref.SecondaryColor), VehicleColor.Blue);
            Rotation = ini.GetValue(section, Prefix(Pref.Rotation), new Vector3());
        }
    }
}
