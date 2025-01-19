
using JMF.Native;

namespace JMF
{
    namespace UI
    {
        public enum KeyboardResultStatus
        {
            Accept,
            Cancel,
            Error,
        }
        public struct KeyboardResult
        {
            public KeyboardResultStatus Status;
            public string Input;
        }
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
            public static KeyboardResult GetKeyboardInput()
            {
                const string keyboardTitle = "new_save_title_input";
                //Native.Function.Call(Hash.AddTextEntry, keyboardTitle, "Enter name for new save:");
                Function.Call(Hash.DisplayOnscreenKeyboard, 0, keyboardTitle, "", "", "", "", "", 30);
                while (Function.Call<int>(Hash.UpdateOnscreenKeyboard) == 0)
                {
                    Thread.Yield();
                }
                int keyboardResult = Function.Call<int>(Hash.UpdateOnscreenKeyboard);
                if (keyboardResult == 1)
                {
                    return new KeyboardResult() { Status = KeyboardResultStatus.Accept, Input = Function.Call<string>(Hash.GetOnscreenKeyboardResult) };
                }
                else if (keyboardResult == 2)
                {
                    return new KeyboardResult() { Status = KeyboardResultStatus.Cancel };
                }
                else
                {
                    return new KeyboardResult() { Status = KeyboardResultStatus.Error };
                }
            }
            #endregion Methods
        }
    }
}
