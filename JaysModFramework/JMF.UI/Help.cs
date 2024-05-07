
using JMF.Native;

namespace JMF
{
    namespace UI
    {
        public static class Help
        {
            public static void Show(TextComponent text, bool beep = true, int duration = -1, int shape = 0)
            {
                Function.Call(Hash.BeginTextCommandDisplayHelp, "STRING");
                Function.Call(Hash.AddTextComponentSubstringPlayerName, text.ToString());
                Function.Call(Hash.EndTextCommandDisplayHelp, shape, false, beep, duration);
            }
        }
    }
}
