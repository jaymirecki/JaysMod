using JMF.Native;

namespace JMF
{
    public class Player
    {
        ///////////////////////////////////////////////////////////////////////
        //                             Properties                            //
        ///////////////////////////////////////////////////////////////////////
        #region Properties
        public int Handle;
        private Ped _character;
        public Ped Character
        {
            get
            {
                int handle = Function.Call<int>(Hash.GetPlayerPed, Handle);
                if (_character == null || handle != _character.Handle)
                {
                    _character = new Ped(handle);
                }
                return _character;
            }
        }
        public bool IsDead
        {
            get { return Function.Call<bool>(Hash.IsPlayerDead, Handle); }
        }
        public bool SpecialAbilityEnabled
        {
            get { return Function.Call<bool>(Hash.IsSpecialAbilityEnabled, Handle); }
            set { Function.Call(Hash.EnableSpecialAbility, Handle, value); }
        }
        public bool IsInvincible
        {
            set { Function.Call(Hash.SetPlayerInvincible, Handle, value); }
        }
        #endregion Properties
        ///////////////////////////////////////////////////////////////////////
        //                            Constructors                           //
        ///////////////////////////////////////////////////////////////////////
        #region Constructors
        public Player(int handle)
        {
            Handle = handle;
        }
        #endregion Constructors
        ///////////////////////////////////////////////////////////////////////
        //                              Methods                              //
        ///////////////////////////////////////////////////////////////////////
        #region Methods
        public bool SetModel(uint hash)
        {
            return SetModel(new Model(hash));
        }
        public bool SetModel(Model model)
        {
            if (model.Request())
            {
                Debug.Notify("model loaded", true);
                Function.Call(Hash.SetPlayerModel, Handle, model.Hash);
                Debug.Notify("marking model as not needed. model loaded: " + model.IsLoaded, true);
                Function.Call(Hash.SetPedDefaultComponentVariation, Character.Handle);
                Function.Call(Hash.SetModelAsNoLongerNeeded, model.Hash);
                return true;
            }
            return false;
        }
        public bool SetModel(PedHash hash)
        {
            return SetModel((uint)hash);
        }
        #endregion Methods
    }
}
