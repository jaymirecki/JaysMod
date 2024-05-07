using JMF.Native;

namespace JMF
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
        private static Clock _clock;
        public static Clock Clock
        {
            get
            {
                if (_clock == null)
                {
                    _clock = new Clock();
                }
                return _clock;
            }
        }
    }
}
