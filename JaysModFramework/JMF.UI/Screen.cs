
using JMF.Native;

namespace JMF
{
    namespace UI
    {
        public static class Screen
        {
            ///////////////////////////////////////////////////////////////////
            //                           Properties                          //
            ///////////////////////////////////////////////////////////////////
            #region Properties
            public static bool IsFadedOut
            {
                get { return Function.Call<bool>(Hash.IsScreenFadedOut); }
            }
            public static bool IsFadedIn
            {
                get { return Function.Call<bool>(Hash.IsScreenFadedIn); }
            }
            #endregion Properties
            ///////////////////////////////////////////////////////////////////
            //                            Methods                            //
            ///////////////////////////////////////////////////////////////////
            #region Methods
            public static void StopAllEffects()
            {
                Function.Call(Hash.AnimpostfxStopAll);
            }
            public static void FadeOut(int duration)
            {
                Function.Call(Hash.DoScreenFadeOut, duration);
            }
            public static void FadeIn(int duration)
            {
                Function.Call(Hash.DoScreenFadeIn, duration);
            }
            #endregion Methods
        }
    }
}
