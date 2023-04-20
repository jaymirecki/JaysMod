//using GTA;
//using JaysModFramework.MaleOutfitPieces;
//using System.Windows.Forms;

//namespace JaysModFramework
//{
//    public class HelmetManager : Script
//    {
//        private static bool Enabled = false;
//        private static NPC PlayerNPC;
//        private static bool WearingHat;
//        private static int Hat;
//        private static int DefaultHat = (int)Hats.Default;
//        public HelmetManager()
//        {
//            Tick += OnTick;
//            KeyDown += OnKeyDown;
//        }

//        private void OnTick(object sender, System.EventArgs e)
//        {
//            if (Enabled && PlayerNPC != null && PlayerNPC.IsInVehicle())
//            {

//            }
//        }

//        private void OnKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.NumPad9 && Enabled && PlayerNPC != null)
//            {
//                SwitchHat();
//            }
//        }
//        private static void SwitchHat()
//        {
//            Outfit newOutfit = PlayerNPC.Outfit.Copy();
//            if (newOutfit.Hat != DefaultHat)
//            {
//                Hat = newOutfit.Hat;
//                newOutfit.SetHat((Hats)DefaultHat);
//                PlayerNPC.Outfit = newOutfit;
//            }
//            else if (Hat != DefaultHat)
//            {
//                newOutfit.SetHat((Hats)DefaultHat);
//                PlayerNPC.Outfit = newOutfit;
//            }
//        }
//        public static void Activate(NPC targetNPC)
//        {
//            Enabled = true;
//            PlayerNPC = targetNPC;
//            WearingHat = PlayerNPC.Outfit.Hat != DefaultHat;
//            Hat = PlayerNPC.Outfit.Hat;
//        }
//        public static void Deactivate()
//        {
//            Enabled = false;
//            PlayerNPC = null;
//            WearingHat = false;
//            Hat = -1;
//        }
//    }
//}
