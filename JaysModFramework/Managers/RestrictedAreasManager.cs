using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    public class RestrictedAreasManager: Script
    {
        private static bool ZancudoDisabled, LSIADisabled, PrisonDisabled, MerryweatherDisabled;

        public RestrictedAreasManager()
        {
            EnableAll();

            Tick += OnTick;
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (ZancudoDisabled)
            {
                //Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_armybase");
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_armybase");
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "restrictedareas");
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_doors");
            }
            if (LSIADisabled)
            {
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_lossantosintl");
            }
            if (PrisonDisabled)
            {
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prison");
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prisonvanbreak");
            }
            if (MerryweatherDisabled)
            {
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "restrictedareas");
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_doors");
            }
        }
        public static void EnableAll()
        {
            ZancudoDisabled = false;
            LSIADisabled = false;
            PrisonDisabled = false;
            MerryweatherDisabled = false;
        }
        public static void DisableAll()
        {
            ZancudoDisabled = true;
            LSIADisabled = true;
            PrisonDisabled = true;
            MerryweatherDisabled = true;
        }
        public static void SetEnabledFortZancudo(bool enabled)
        {
            ZancudoDisabled = !enabled;
        }
        public static void SetEnabledLSIA(bool enabled)
        {
            LSIADisabled = !enabled;
        }
        public static void SetEnabledPrison(bool enabled)
        {
            PrisonDisabled = !enabled;
        }
        public static void SetEnabledMerryweather(bool enabled)
        {
            MerryweatherDisabled = !enabled;
        }
    }
}
