using System;
using JMF.Native;

namespace JMF
{
    public class Ped : Entity, IEquatable<Ped>
    {
        ///////////////////////////////////////////////////////////////////////
        //                            Constructors                           //
        ///////////////////////////////////////////////////////////////////////
        #region Constructors
        public Ped(int handle)
        {
            Handle = handle;
        }
        #endregion Constructors
        ///////////////////////////////////////////////////////////////////////
        //                             Operators                             //
        ///////////////////////////////////////////////////////////////////////
        #region Operators
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is Ped ? Equals((Ped)obj) : base.Equals(obj);
        }
        public bool Equals(Ped other)
        {
            return !(other is null) && Handle == other.Handle;
        }
        public static bool operator ==(Ped ped1, Ped ped2)
        {
            if (ped1 is null)
            {
                return ped2 is null;
            }
            return (ped1.Equals(ped2));
        }
        public static bool operator !=(Ped ped1, Ped ped2)
        {
            return !(ped1 == ped2);
        }
        #endregion Operators
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        public bool IsInAnyVehicle
        {
            get { return Function.Call<bool>(Hash.IsPedInAnyVehicle, Handle, true);  }
        }
        private Vehicle _currentVehicle = null;
        public Vehicle CurrentVehicle
        {
            get
            {
                if (IsInAnyVehicle)
                {
                    int handle = Function.Call<int>(Hash.GetVehiclePedIsIn, Handle);
                    if (_currentVehicle == null || _currentVehicle.Handle != handle)
                    {
                        _currentVehicle = new Vehicle(handle);
                    }
                }
                return _currentVehicle;
            }
        }
        #endregion Properties
    }
}
