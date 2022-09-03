using GTA;
using JaysModFramework.MaleOutfitPieces;

namespace JaysModFramework
{
    public class ScubaManager : Script
    {
        private static bool Enabled = false;
        private static NPC PlayerNPC;
        private static bool IsScuba = false;

        public ScubaManager()
        {
            Tick += OnTick;
        }

        private void OnTick(object sender, System.EventArgs e)
        {
            if (Enabled && PlayerNPC != null)
            {
                //if (IsScuba && !PlayerNPC.IsScuba)
                //{
                //    if (PlayerNPC.IsAccOneDefaulted)
                //    {
                //        Debug.Log("set notisscuba");
                //        SetScubaLand();
                //        IsScuba = false;
                //    }
                //    else
                //    {
                //        Debug.Log("notisscuba");
                //        IsScuba = false;
                //    }
                //}
                //if (IsScuba && PlayerNPC.Outfit.AccOne != (int)Undershirts.ScubaTank)
                //{
                //    Debug.Log("notisscuba");
                //    SetScubaLand();
                //    IsScuba = false;
                //}
                //else if (!IsScuba && PlayerNPC.IsScuba)
                //{
                //    Debug.Log("isscuba");
                //    IsScuba = true;
                //}
            }
        }

        private void ResetScuba()
        {
            Debug.Log("resetscuba");
            //PlayerNPC.Outfit = MaleOutfitTemplates.Scuba.Copy();
        }
        private void SetScubaLand()
        {
            //PlayerNPC.Outfit = MaleOutfitTemplates.ScubaLand.Copy();
        }
        public static void Activate(NPC playerNPC)
        {
            Enabled = true;
            PlayerNPC = playerNPC;
        }
        public static void Deactivate()
        {
            Enabled = false;
            PlayerNPC = null;
        }
    }
}
