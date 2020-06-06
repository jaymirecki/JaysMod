using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace JaysMod
{
    class ATC : Script
    {
        static Vector3 taxi1, taxi2, taxi3, taxi4, taxi5, taxi6, taxi7, taxi8, taxi9, taxi10, taxi11, taxi12, taxi13;
        static Vehicle plane1, plane2, plane3, plane4, plane5, plane6, plane7, plane8, plane9, plane10, plane11;
        static Ped pilot;
        public ATC()
        {
            taxi1 = new Vector3(1553.52f, 3152.32f, 41);
            taxi2 = new Vector3(1581.87f, 3180.46f, 41);
            taxi3 = new Vector3(1597.28f, 3195.98f, 41);
            taxi4 = new Vector3(1589.36f, 3209.58f, 41);
            taxi5 = new Vector3(1584.29f, 3217.86f, 41);
            taxi6 = new Vector3(1557.37f, 3210.96f, 41);
            taxi7 = new Vector3(1093.63f, 3086.28f, 41);
            taxi8 = new Vector3(1076.24f, 3068.01f, 41);
            taxi9 = new Vector3(1072.58f, 3043.75f, 41);
            taxi10 = new Vector3(1089.83f, 3026.19f, 41);
            taxi11 = new Vector3(1117.68f, 3025.00f, 41);
            taxi12 = new Vector3(1480.10f, 3122.45f, 41);
            taxi13 = new Vector3(1519.33f, 3133.06f, 41);

            string model = "cuban800";
            plane1 = Maps.Functions.SpawnVehicle(model, taxi1, 314.79f, (int)VehicleColor.MatteWhite, (int)VehicleColor.MatteDarkRed, 0);
            //plane2 = Maps.Functions.SpawnVehicle(model, taxi2, 314.79f, 0, 0, 0);
            //plane3 = Maps.Functions.SpawnVehicle(model, taxi4, 30.035f, 0, 0, 0);
            //plane4 = Maps.Functions.SpawnVehicle(model, taxi6, 105.28f, 0, 0, 0);
            //plane5 = Maps.Functions.SpawnVehicle(model, taxi7, 105.28f, 0, 0, 0);
            //plane6 = Maps.Functions.SpawnVehicle(model, taxi8, 150.49f, 0, 0, 0);
            //plane7 = Maps.Functions.SpawnVehicle(model, taxi9, 195.70f, 0, 0, 0);
            //plane8 = Maps.Functions.SpawnVehicle(model, taxi10, 240.455f, 0, 0, 0);
            //plane9 = Maps.Functions.SpawnVehicle(model, taxi11, 285.21f, 0, 0, 0);
            //plane10 = Maps.Functions.SpawnVehicle(model, taxi12, 285.21f, 0, 0, 0);
            //plane11 = Maps.Functions.SpawnVehicle(model, taxi13, 285.21f, 0, 0, 0);
        }

        static public void Run()
        {
            pilot = Maps.Functions.SpawnPed("s_m_m_pilot_01", new Vector3(1536.25f, 3161.26f, 41), 0f);
            pilot.Task.WarpIntoVehicle(plane1, VehicleSeat.Driver);
            Game.Player.Character.Task.WarpIntoVehicle(plane1, VehicleSeat.Passenger);
            while (!pilot.IsSittingInVehicle()) Yield();
            Drive.PlaneTaxi(pilot, plane1, taxi2);
            Drive.PlaneTaxi(pilot, plane1, taxi3);
            Drive.PlaneTaxi(pilot, plane1, taxi4);
            Drive.PlaneTaxi(pilot, plane1, taxi5);
            Drive.PlaneTaxi(pilot, plane1, taxi6);
            Drive.PlaneTaxi(pilot, plane1, taxi7);
            Drive.PlaneTaxi(pilot, plane1, taxi8);
            Drive.PlaneTaxi(pilot, plane1, taxi9);
            Drive.PlaneTaxi(pilot, plane1, taxi10);
            Drive.PlaneTaxi(pilot, plane1, taxi11);
            //Drive.PlaneTaxi(pilot, plane1, taxi12);
            //Drive.PlaneTaxi(pilot, plane1, taxi13);
            //Drive.PlaneTaxi(pilot, plane1, taxi1);
            //Drive.PlaneTaxi(pilot, plane1, taxi2);
            //Drive.PlaneTaxi(pilot, plane1, taxi3);
            Unload();
        }

        static public void Unload()
        {
            Maps.Functions.DeletePed(pilot);
            Maps.Functions.DeleteVehicle(plane1);
            //Maps.Functions.DeleteVehicle(plane2);
            //Maps.Functions.DeleteVehicle(plane3);
            //Maps.Functions.DeleteVehicle(plane4);
            //Maps.Functions.DeleteVehicle(plane5);
            //Maps.Functions.DeleteVehicle(plane6);
            //Maps.Functions.DeleteVehicle(plane7);
            //Maps.Functions.DeleteVehicle(plane8);
            //Maps.Functions.DeleteVehicle(plane9);
            //Maps.Functions.DeleteVehicle(plane10);
            //Maps.Functions.DeleteVehicle(plane11);
        }
    }
}
