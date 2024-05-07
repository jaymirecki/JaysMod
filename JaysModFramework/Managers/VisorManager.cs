//using JaysModFramework.MaleOutfitPieces;
//using System.Windows.Forms;

//namespace JaysModFramework
//{
//    public class VisorManager : Script
//    {
//        private static bool Enabled = false;
//        private static NPC PlayerNPC;
//        public VisorManager()
//        {
//            Tick += OnTick;
//            KeyDown += OnKeyDown;
//        }

//        private void OnTick(object sender, System.EventArgs e)
//        {
//            if (Game.IsNightVisionActive && Enabled && PlayerNPC != null && PlayerNPC.Outfit.Hat != (int)Hats.CombatHelmetVisorDown)
//            {
//                Game.IsNightVisionActive = false;
//            }
//        }

//        private void OnKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.NumPad7 && Enabled && PlayerNPC != null)
//            {
//                switch (PlayerNPC.Outfit.Hat)
//                {
//                    case (int)Hats.CombatHelmetVisorUp:
//                        SwitchHelmet(Hats.CombatHelmetVisorDown);
//                        Game.IsNightVisionActive = true;
//                        break;
//                    case (int)Hats.CombatHelmetVisorDown:
//                        SwitchHelmet(Hats.CombatHelmetVisorUp);
//                        break;
//                    case (int)Hats.MotorcycleVisorUp:
//                        SwitchHelmet(Hats.MotorcycleVisorDown);
//                        break;
//                    case (int)Hats.MotorcycleVisorDown:
//                        SwitchHelmet(Hats.MotorcycleVisorUp);
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//        private static void SwitchHelmet(Hats hat)
//        {
//            Outfit newOutfit = PlayerNPC.Outfit.Copy();
//            newOutfit.SetHat(hat);
//            PlayerNPC.Outfit = newOutfit;
//        }
//        public static void Activate(NPC targetNPC)
//        {
//            Enabled = true;
//            PlayerNPC = targetNPC;
//        }
//        public static void Deactivate()
//        {
//            Enabled = false;
//            PlayerNPC = null;
//            Game.IsNightVisionActive = false;
//        }
//    }
//}
