using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JaysModFramework.Native;

namespace JaysModFramework
{
    public static class Game
    {
        private static Player _player;
        public static Player Player
        {
            get
            {
                int handle = Function.Call<int>(Hash.PlayerId);
                if (_player == null || _player.Handle != handle)
                {
                    _player = new Player(handle);
                }
                return _player;
            }
        }
    }
}
