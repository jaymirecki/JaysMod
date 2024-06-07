using System;
using System.Xml.Serialization;
using JMF.Native;

namespace JMF
{
    public class Vehicle : Entity, IEquatable<Vehicle>
    {
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        [XmlIgnore]
        public VehicleClass Class
        {
            get { return (VehicleClass)Function.Call<int>(Hash.GetVehicleClass, Handle); }
        }
        private bool _sirenOn = false;
        public bool SirenOn
        {
            get
            {
                return GetPropertyOrDefault(Hash.IsVehicleSirenOn, _sirenOn);
            }
            set
            {
                SetProperty(Hash.SetVehicleSiren, value);
                _sirenOn = value;
            }
        }
        private bool _sirenAudioOn = true;
        public bool SirenAudioOn
        {
            get
            {
                return GetPropertyOrDefault(Hash.IsVehicleSirenAudioOn, _sirenAudioOn);
            }
            set
            {
                SetProperty(Hash.SetVehicleHasMutedSirens, !value);
                _sirenAudioOn = value;
            }
        }
        private float _speed = 0;
        public override float Speed
        {
            get { return GetPropertyOrDefault(Hash.GetEntitySpeed, _speed); }
            set
            {
                SetProperty(Hash.SetVehicleForwardSpeed, value);
                _speed = value;
            }
        }
        #endregion Properties
        #region Methods
        public override void Spawn()
        {
            Debug.Log(DebugSeverity.Error, World.GetNearbyVehicles(Position, 3f).Count);
            foreach(Vehicle v in World.GetNearbyVehicles(Position, 3f))
            {
                v.Delete();
            }
            Model.Request();
            Handle = Function.Call<int>(Hash.CreateVehicle, Model.Hash, 0, 0, 0, Heading, false, true);
            Model.NotNeeded();
            SirenOn = _sirenOn;
            SirenAudioOn = _sirenAudioOn;

            OnEntitySpawn();
        }
        public void ToggleSirenNoise()
        {
            if (SirenOn)
            {
                SirenAudioOn = !SirenAudioOn;
            }
        }
        #endregion Methods
        #region Constructors
        public Vehicle(int handle)
        {
            Handle = handle;
        }
        public Vehicle() { }
        public Vehicle(Model model) {
            Model = model;
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
            return !(other is null) && Handle == other.Handle;
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
        #endregion Operators
    }
}
