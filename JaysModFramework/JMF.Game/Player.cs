using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
