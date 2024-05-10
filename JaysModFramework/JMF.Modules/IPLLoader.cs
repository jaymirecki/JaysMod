using JMF.Interiors;
using JMF.Math;
using JMF.Native;
using System.Collections.Generic;

namespace JMF
{
    namespace Modules
    {
        public class IPLLoader: InternalModule<IPLLoaderSettings>
        {
            internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
            public override string ModuleName { get; } = "IPL Loader";
            public override string ModuleDescription { get; } = "Loads IPLs";
            public override IPLLoaderSettings Settings { get { return Global.Config.IPLLoaderSettings; } }
            public IPL AircraftCarrier = new IPL(
                new Vector3(3082.3117f, -4717.1191f, 15.2622f),
                new List<string>(){
                    "hei_carrier",
                    "hei_carrier_int1",
                    "hei_carrier_int1_lod",
                    "hei_carrier_int2",
                    "hei_carrier_int2_lod",
                    "hei_carrier_int3",
                    "hei_carrier_int3_lod",
                    "hei_carrier_int4",
                    "hei_carrier_int4_lod",
                    "hei_carrier_int5",
                    "hei_carrier_int5_lod",
                    "hei_carrier_int6",
                    "hei_carrier_int6_lod",
                    "hei_carrier_lod",
                    "hei_carrier_slod",
                }
                );
            public IPL Casino = new IPL(
                new Vector3(1100.000f, 220.000f, -50.000f),
                new List<string>(){
                    "vw_casino_main﻿ ",
                    "vw_casino_garage",
                    "vw_casino_carpark",
                    "vw_casino_penthouse" 
                }
                );
            public IPL Facility = new IPL(
                new Vector3(345.00000000f, 4842.00000000f, -60.00000000f),
                new List<string>(){
                    //Exteriors
                    "xm_hatch_01_cutscene", 
                    "xm_hatch_02_cutscene",
                    "xm_hatch_03_cutscene", 
                    "xm_hatch_04_cutscene", 
                    "xm_hatch_06_cutscene",
                    "xm_hatch_07_cutscene",
                    "xm_hatch_08_cutscene",
                    "xm_hatch_09_cutscene",
                    "xm_hatch_10_cutscene",
                    "xm_hatch_closed",
                    "xm_siloentranceclosed_x17",
                    "xm_bunkerentrance_door",
                    "xm_hatches_terrain",
                    "xm_hatches_terrain_lod",
                    //Interiors
                    "xm_x17dlc_int_placement_interior_33_x17dlc_int_02_milo_",
                }
                );
            public IPL CargoShip = new IPL(
                new Vector3(-344.4349f, -4062.832f, 17.000f),
                new List<string>(){
                    "m23_2_cargoship﻿",
                    "m23_2_cargoship_bridge",
                }
                );
            public override void OnActivate()
            {
                Function.Call(Hash.OnEnterMp);
                if (Settings.AircraftCarrierEnabled)
                {
                    AircraftCarrier.Load();
                }
                if (Settings.CasinoEnabled)
                {
                    Casino.Load();
                }
                if (Settings.FacilityEnabled)
                {
                    Facility.Load();
                    Function.Call(Hash.ActivateInteriorEntitySet, 269313, "set_int_02_shell");
                    Function.Call(Hash.SetInteriorEntitySetColor, 269313, "set_int_02_shell", 1);
                    Function.Call(Hash.ActivateInteriorEntitySet, 269313, "set_int_02_cannon");
                    Function.Call(Hash.SetInteriorEntitySetColor, 269313, "set_int_02_cannon", 1);
                    Function.Call(Hash.ActivateInteriorEntitySet, 269313, "set_int_02_security");
                    Function.Call(Hash.SetInteriorEntitySetColor, 269313, "set_int_02_security", 1);
                    Function.Call(Hash.ActivateInteriorEntitySet, 269313, "set_int_02_sleep");
                    Function.Call(Hash.SetInteriorEntitySetColor, 269313, "set_int_02_sleep", 1);
                    //Function.Call(Hash.ActivateInteriorEntitySet, 269313, "set_int_02_lounge1");
                    //Function.Call(Hash.SetInteriorEntitySetColor, 269313, "set_int_02_lounge1", 1);
                    Function.Call(Hash.ActivateInteriorEntitySet, 269313, "set_int_02_decal_01");
                    Function.Call(Hash.SetInteriorEntitySetColor, 269313, "set_int_02_decal_01", 1);
                    Function.Call(Hash.ActivateInteriorEntitySet, 269313, "Set_Int_02_outfit_paramedic");
                    //Game.Player.Character.Position = Facility.Position;

                }
                if (Settings.CargoShipEnabled)
                {
                    CargoShip.Load();
                }
            }
            public override void OnDeactivate()
            {
                if (AircraftCarrier.Loaded)
                {
                    AircraftCarrier.Unload();
                }
                if (Casino.Loaded)
                {
                    Casino.Unload();
                }
                if (Facility.Loaded)
                {
                    Facility.Unload();
                }
                if (CargoShip.Loaded)
                {
                    CargoShip.Unload();
                }
            }

        }
        public class IPLLoaderSettings : ModuleSettings
        {
            public bool AircraftCarrierEnabled = false;
            public bool CasinoEnabled = false;
            public bool FacilityEnabled = false;
            public bool CargoShipEnabled = true;
        }
    }
}
