//using Rage;
//using Rage.Native;
//using System.Collections.Generic;

//namespace JaysModFramework
//{
//    public enum IPL
//    {
//        AircraftCarrier,
//        DiamondCasino,
//        FIB,
//    }
//    public class IPLLoader
//    {
//        private static bool IPLsSetup = false;
//        private static Dictionary<IPL, IPLDetails> IPLs = new Dictionary<IPL, IPLDetails>();
//        private static void SetupIPLs()
//        {
//            if (IPLsSetup)
//            {
//                return;
//            }
//            IPLs.Add(
//                IPL.AircraftCarrier,
//                new IPLDetails(
//                    new Vector3(3082.3117f, -4717.1191f, 15.2622f),
//                    new List<string>(){ 
//                        "hei_carrier", 
//                        "hei_carrier_DistantLights", 
//                        "hei_Carrier_int1", 
//                        "hei_Carrier_int2", 
//                        "hei_Carrier_int3", 
//                        "hei_Carrier_int4", 
//                        "hei_Carrier_int5", 
//                        "hei_Carrier_int6", 
//                        "hei_carrier_LODLights" },
//                    true,
//                    BlipSprite.Jet,
//                    "Aircraft Carrier"
//                    )
//                );
//            IPLs.Add(
//                IPL.DiamondCasino,
//                new IPLDetails(
//                    new Vector3(1100.000f, 220.000f, -50.000f),
//                    new List<string>(){
//                        "vw_casino_main﻿ ",
//                        "vw_casino_garage",
//                        "vw_casino_carpark",
//                        "vw_casino_penthouse" },
//                    true,
//                    BlipSprite.Casino,
//                    "Diamond Casino"
//                    )
//                );
//            IPLsSetup = true;
//        }
//        public static void Load(IPL ipl, bool showBlip = true, BlipSprite overrideBlipSprite = BlipSprite.Invisible, string overrideBlipName = "")
//        {
//            SetupIPLs();
//            if (IPLs.TryGetValue(ipl, out IPLDetails value))
//            {
//                value.Load(showBlip, overrideBlipSprite, overrideBlipName);
//            }
//        }
//        public static void Unload(IPL ipl, bool tryUnloadDLCs = false)
//        {
//            SetupIPLs();
//            if (IPLs.TryGetValue(ipl, out IPLDetails value))
//            {
//                value.Unload();
//                if (value.IsDLC)
//                {
//                    UnloadDLC(true);
//                }
//            }
//        }
//        public static void Reload(IPL ipl)
//        {
//            SetupIPLs();
//            if (IPLs.TryGetValue(ipl, out IPLDetails value))
//            {
//                value.Unload();
//                value.Load();
//            }
//        }
//        public static void UnloadDLC(bool checkUnloadedFirst = false)
//        {
//            bool isDLCUnloaded = true;
//            if (checkUnloadedFirst)
//            {
//                foreach (IPLDetails ipl in IPLs.Values)
//                {
//                    if (ipl.Loaded && ipl.IsDLC)
//                    {
//                        isDLCUnloaded = false;
//                    }
//                }
//            }
//            if (isDLCUnloaded) {
//                Function.Call((Hash)0xD7C10C4A637992C9); // _UNLOAD_MP_DLC_MAP
//            }
//        }
//        public static void UnloadAll()
//        {
//            foreach (IPLDetails ipl in IPLs.Values)
//            {
//                ipl.Unload();
//            }
//            //UnloadDLC();
//        }
//    }
//}
