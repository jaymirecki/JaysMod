using GTA;
using GTA.Native;
using JaysModFramework.MaleOutfitPieces;
using System;
using System.Windows.Forms;

namespace JaysModFramework
{
    public class BigMapManager : Script
    {
        private static bool Enabled = false;
        private static bool Active = false;
        public BigMapManager()
        {
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad1 && Enabled)
            {
                Function.Call(Hash.SET_BIGMAP_ACTIVE, !Active);
                Active = !Active;
            }
        }
        public static void Activate()
        {
            Enabled = true;
        }
        public static void Deactivate()
        {
            Enabled = false;
        }
    }
}
