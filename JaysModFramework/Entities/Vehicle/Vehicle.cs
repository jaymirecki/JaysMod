using System;
using JaysModFramework.Native;

namespace JaysModFramework
{
    public class Vehicle : Entity, IEquatable<Vehicle>
    {
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        public bool SirenOn
        {
            get { return Function.Call<bool>(Hash.IsVehicleSirenOn, Handle); }
            set { Function.Call(Hash.SetVehicleSiren, Handle, value); }
        }
        public bool SirenAudioOn
        {
            get { return Function.Call<bool>(Hash.IsVehicleSirenAudioOn, Handle); }
            set { Function.Call(Hash.SetVehicleHasMutedSirens, Handle, !value); }
        }
        #endregion Properties
        #region Methods
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
