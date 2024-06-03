using JMF.Native;
using System;

namespace JMF
{
    public abstract class Entity
    {
        public int Handle { get; protected set; } = -1;

        //
        // Properties implemented by Entity
        //
        #region Properties
        protected T GetPropertyOrDefault<T>(Hash hash, T value)
        {
            if (Handle == -1)
            {
                return value;
            }
            return Function.Call<T>(hash, Handle);
        }
        protected bool SetProperty(Hash hash, Rage.Native.NativeArgument value)
        {
            if (Handle != -1)
            {
                Function.Call(hash, Handle, value);
                return true;
            }
            return false;
        }
        private float _heading;
        public float Heading
        {
            get
            {
                return GetPropertyOrDefault(Hash.GetEntityHeading, _heading);
            }
            set
            {
                SetProperty(Hash.SetEntityHeading, value);
                _heading = value;
            }
        }
        private Vector3 _position;
        public Vector3 Position
        {
            get 
            {
                if (Handle == -1)
                {
                    return _position;
                }
                return new Vector3(
                    (Rage.Vector3)Rage.Native.NativeFunction.Call(
                        (ulong)Hash.GetEntityCoords, 
                        typeof(Rage.Vector3),
                        Handle
                        )
                    ); 
            }
            set
            {
                if (Handle != -1)
                {
                    Function.Call(Hash.SetEntityCoordsNoOffset, Handle, value.X, value.Y, value.Z, false, false, true);
                }
                _position = value;
            }
        }
        private uint _model = 0;
        public Model Model
        {
            get 
            { 
                return new Model(GetPropertyOrDefault(Hash.GetEntityModel, _model)); 
            }
            set
            {
                _model = value.Hash;
                if (Handle != -1)
                {
                    Spawn();
                }
            }
        }
        private bool _isInvincible;
        public bool IsInvincible
        {
            get
            {
                return _isInvincible;
            }
            set
            {
                SetProperty(Hash.SetEntityInvincible, value);
                _isInvincible = value;
            }
        }
        public bool IsDead
        {
            get { return Function.Call<bool>(Hash.IsEntityDead, Handle); }
        }
        private int _health;
        public int Health
        {
            get
            {
                return GetPropertyOrDefault(Hash.GetEntityHealth, _health);
            }
            set
            {
                SetProperty(Hash.SetEntityHealth, value);
                _health = value;
            }
        }
        public float HeightAboveGround
        {
            get { return Function.Call<float>(Hash.GetEntityHeightAboveGround, Handle); }
        }
        public string Worldspace { get; set; } = "";
        public string Map { get; set; } = "";
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
        public void OnEntitySpawn()
        {
            Heading = _heading;
            Position = _position;
            IsInvincible = _isInvincible;
            Health = _health;
        }
        public void Delete()
        {
            if (Function.Call<bool>(Hash.DoesEntityExist, Handle))
            {
                Debug.Log(DebugSeverity.Warning, "exists");
                int handle = Handle;
                Rage.Native.NativeFunction.Natives.DeleteEntity(ref handle);
            }
        }
        #endregion Methods

        //
        // Abstract methods implemented by descendant classes
        //
        #region Abstract Methods
        public abstract void Spawn();
        #endregion Abstract Methods
    }
}
