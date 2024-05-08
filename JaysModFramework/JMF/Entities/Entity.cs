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
            get { return Function.Call<Vector3>(Hash.GetEntityCoords, Handle); }
            set { Function.Call(Hash.SetEntityCoords, Handle, value.X, value.Y, value.Z); }
        }
        public Model Model
        {
            get { return new Model(Function.Call<uint>(Hash.GetEntityModel, Handle)); }
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
