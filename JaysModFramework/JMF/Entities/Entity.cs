using JMF.Math;
using JMF.Native;

namespace JMF
{
    public abstract class Entity
    {
        public int Handle
        {
            get; protected set;
        }

        //
        // Methods implemented by Entity
        //
        #region Properties
        public float Heading
        {
            get { return Function.Call<float>(Hash.GetEntityHeading, Handle); }
            set { Function.Call(Hash.SetEntityHeading, Handle, value); }
        }
        //public Model Model
        //{
        //    get { return Function.Call<uint>(Hash.GetEntityModel, Handle); }
        //}
        public Vector3 Position
        {
            get 
            { 
                return new Vector3(
                    (Rage.Vector3)Rage.Native.NativeFunction.Call(
                        (ulong)Hash.GetEntityCoords, 
                        typeof(Rage.Vector3),
                        Handle
                        )
                    ); 
            }
            set { Function.Call(Hash.SetEntityCoordsNoOffset, Handle, value.X, value.Y, value.Z, false, false, true); }
        }
        public Model Model
        {
            get { return new Model(Function.Call<uint>(Hash.GetEntityModel, Handle)); }
        }
        public bool IsInvincible
        {
            set { Function.Call<bool>(Hash.SetEntityInvincible, Handle, value); }
        }
        public bool IsDead
        {
            get { return Function.Call<bool>(Hash.IsEntityDead, Handle); }
        }
        public int Health
        {
            get { return Function.Call<int>(Hash.GetEntityHealth, Handle); }
            set { Function.Call(Hash.SetEntityHealth, Handle, value); }
        }
        public float HeightAboveGround
        {
            get { return Function.Call<float>(Hash.GetEntityHeightAboveGround, Handle); }
        }
        #endregion Properties

        //
        // Methods implemented by Entity
        //
        #region Methods
        public float DistanceTo(Vector3 position)
        {
            return Position.DistanceTo(position);
        }
        public float DistanceTo2D(Vector3 position)
        {
            return Position.DistanceTo2D(position);
        }
        #endregion Methods

        //
        // Abstract methods implemented by descendant classes
        //
        #region Abstract Methods
        
        #endregion Abstract Methods
    }
}
