using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace JaysMod
{
    [ScriptAttributes(NoDefaultInstance = true)]
    class Charter : Script
    {
        bool asked;
        Vector3 taxi1, taxi2, taxi3;
        Ped pilot;
        Vehicle plane;

        public Charter()
        {
            //JaysMod.Debug("Unparameterized");
            pilot = null;
            plane = null;
            //asked = chartering = false;
            //pilot_heading = 43.63f;
            //plane_heading = 315.443f;
            //pilot_spawn = new Vector3(1503.31f, 3104.58f, 41f);
            //plane_spawn = new Vector3(1500.399f, 3099.608f, 42.69702f);
            //activated = false;
            //Tick += onTick;
            //KeyDown += onKeyDown;
        }

        //public Charter(Vehicle plane, Ped pilot)
        //{
        //    JaysMod.Debug("first check");
        //    //this.plane = plane;
        //    //this.pilot = pilot;
        //    //Tick += onTick;
        //    //KeyDown += onKeyDown;
        //}

        ~Charter()
        {
            //Unload();
        }

        void charter(RunwayID destination)
        {
            Ped player = Game.Player.Character;

            while (!player.IsInVehicle(plane))
                Yield();
            player.Task.WarpIntoVehicle(plane, VehicleSeat.ExtraSeat4);
            //Wait(1000);
            pilot.Task.EnterVehicle(plane, VehicleSeat.Driver);
            while (!pilot.IsInVehicle(plane)) Yield();
            plane.Doors[VehicleDoorIndex.FrontLeftDoor].Close();
            plane.IsTaxiLightOn = true;
            Function.Call(Hash._SET_VEHICLE_DESIRED_VERTICAL_FLIGHT_PHASE, plane, 0);
            plane.IsEngineRunning = true;
            Wait(5000);

            // Taxi
            taxi1 = new Vector3(1591, 3190, 41);
            taxi2 = new Vector3(1586, 3210, 41f);
            taxi3 = new Vector3(1554, 3210, 41);
            Vector3 takeoff = new Vector3(1057.491f, 3074.598f, 70);
            Vector3 cruise = new Vector3(-3621, 1973, 2250);
            PlaneSpeed charterSpeeds;
            Drive.PlaneSpeeds.TryGetValue(plane.Model.Hash, out charterSpeeds);
            Drive.Normal(pilot, plane, taxi1, charterSpeeds.Taxi);
            //Drive.Normal(pilot, plane, taxi2, charterSpeeds.Taxi);
            //Drive.Normal(pilot, plane, taxi3, charterSpeeds.Taxi);
            Drive.PlaneTakeoff(pilot, plane, RunwayID.SSRA27);

            Drive.PlaneFly(pilot, plane, cruise);
            GTA.UI.Screen.ShowSubtitle("We have now reached our cruising altitude. Please enjoy your flight.");

            //Drive.PlaneFlyWarp(pilot, plane);

            Drive.PlaneLand(pilot, plane, destination);

            // Taxi
            pilot.Task.LeaveVehicle(plane, false);
            while (plane.Speed > 0) Yield();
            plane.IsTaxiLightOn = false;
            plane.IsEngineRunning = false;
            plane.Doors[VehicleDoorIndex.FrontLeftDoor].Open();
            GTA.UI.Screen.ShowHelpTextThisFrame("done");
        }

        void onTick(object sender, EventArgs e)
        {
            //if (!activated && !hasBeenActivated)
            //    return;
            //if (!activated && hasBeenActivated)
            //{
            //    hasBeenActivated = false;
            //    Unload();
            //    return;
            //}
            //if (activated && !hasBeenActivated)
            //{
            //    hasBeenActivated = true;
            //    Load();
            //}

            //Ped player = Game.Player.Character;
            if (canAsk())
            {
                //asked = true;
                GTA.UI.Screen.ShowSubtitle("Hello sir, would you like to charter a flight?", 2000);
                GTA.UI.Screen.ShowHelpTextThisFrame("Press 'G' to charter this flight");
            }
            //else
            //    asked = false;

            //if (!chartering)
            //{
            //    plane.Doors[VehicleDoorIndex.FrontLeftDoor].Open();
            //}

            //if (chartering && player.IsInVehicle(plane))
            //{
            //    player.Task.WarpIntoVehicle(plane, VehicleSeat.ExtraSeat4);
            //    Wait(1000);
            //    pilot.Task.EnterVehicle(plane, VehicleSeat.Driver);
            //    while (!pilot.IsInVehicle(plane)) Wait(100);
            //    plane.Doors[VehicleDoorIndex.FrontLeftDoor].Close();
            //    plane.IsTaxiLightOn = true;
            //    Function.Call(Hash._SET_VEHICLE_DESIRED_VERTICAL_FLIGHT_PHASE, plane, 0);
            //    plane.IsEngineRunning = true;
            //    Wait(5000);

            //    // Taxi
            //    taxi1 = new Vector3(1591, 3190, 41);
            //    taxi2 = new Vector3(1586, 3210, 41f);
            //    taxi3 = new Vector3(1554, 3210, 41);
            //    Vector3 takeoff = new Vector3(1057.491f, 3074.598f, 70);
            //    Vector3 cruise = new Vector3(-3621, 1973, 2250);
            //    PlaneSpeed charterSpeeds;
            //    Drive.PlaneSpeeds.TryGetValue(plane.Model.Hash, out charterSpeeds);
            //    Drive.Normal(pilot, plane, taxi1, charterSpeeds.Taxi);
            //    Drive.Normal(pilot, plane, taxi2, charterSpeeds.Taxi);
            //    Drive.Normal(pilot, plane, taxi3, charterSpeeds.Taxi);
            //    Drive.PlaneTakeoff(pilot, plane, RunwayID.SSRA27);

            //    Drive.PlaneFly(pilot, plane, cruise);
            //    GTA.UI.Screen.ShowSubtitle("We have now reached our crusing altitude. Please enjoy your flight.");

            //    //Drive.PlaneFlyWarp(pilot, plane);

            //    Drive.PlaneLand(pilot, plane, RunwayID.USSL34);

            //    // Taxi
            //    pilot.Task.LeaveVehicle(plane, false);
            //    while (plane.Speed > 0) Yield();
            //    plane.IsTaxiLightOn = false;
            //    plane.IsEngineRunning = false;
            //    plane.Doors[VehicleDoorIndex.FrontLeftDoor].Open();
            //    chartering = false;
            //    GTA.UI.Screen.ShowHelpTextThisFrame("done");
            //}

        }

        void onKeyDown(object sender, KeyEventArgs e)
        {
            if (pilot == null)
                return;
            Ped player = GTA.Game.Player.Character;
            if (e.KeyCode == Keys.G && player.Position.DistanceTo(pilot.Position) < 5f)
            {
                player.Task.EnterVehicle(plane, VehicleSeat.ExtraSeat4, 5000, 1f, EnterVehicleFlags.None);
                charter(RunwayID.LSIA12);
            }
        }

        public void Load(Vehicle plane, Ped pilot)
        {
            //asked = chartering = false;
            //pilot = Maps.Functions.SpawnPed("s_m_m_pilot_01", pilot_spawn, pilot_heading);

            //plane = Maps.Functions.SpawnVehicle("nimbus", plane_spawn, plane_heading, 112, 40, -1);
            //plane.Doors[VehicleDoorIndex.FrontLeftDoor].Open();
            //plane.DirtLevel = 0;
            JaysMod.Debug("first check");
            this.plane = plane;
            this.pilot = pilot;
            Tick += onTick;
            KeyDown += onKeyDown;
        }

        public void Unload()
        {
            asked = false;
            if (pilot != null)
                Maps.Functions.DeletePed(pilot);
            if (plane != null)
                Maps.Functions.DeleteVehicle(plane);
        }

        private bool canAsk()
        {
            Ped player = Game.Player.Character;
            return !asked && player.Position.DistanceTo(pilot.Position) < 5f && !player.IsInVehicle() && !pilot.IsInVehicle(plane);
        }

    }
}
