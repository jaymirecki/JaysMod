using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;

namespace JaysMod
{
    public struct PlaneSpeed
    {
        public float Taxi, Ground, Air;
    };
    
    public struct Runway
    {
        public Vector3 Start, End, Cruising;
        public float Heading;
    };

    public enum RunwayID { SSRA27, SSRA9, LSIA3, LSIA21, LSIA12, LSIA30, USSL34, USSL16 };

    class Drive : Script
    {
        public static SortedDictionary<int, PlaneSpeed> PlaneSpeeds = new SortedDictionary<int, PlaneSpeed>();
        public static Runway[] Runways = new Runway[8];

        public Drive()
        {
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "nimbus"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "volatol"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "titan"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 87 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "tula"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 110 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "pyro"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "besra"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "lazer"), new PlaneSpeed { Taxi = 20, Ground = 60, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "strikeforce"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "miljet"), new PlaneSpeed { Taxi = 20, Ground = 100, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "cuban800"), new PlaneSpeed { Taxi = 20, Ground = 70, Air = 150 });
            PlaneSpeeds.Add(Function.Call<int>(Hash.GET_HASH_KEY, "hydra"), new PlaneSpeed { Taxi = 20, Ground = 60, Air = 150 });
            Runways[(int)RunwayID.SSRA27] = new Runway
            {
                Start = new Vector3(1554, 3210, 41),
                End = new Vector3(1057, 3075, 41),
                Heading = 105
            };
            Runways[(int)RunwayID.SSRA9] = new Runway
            {
                Start = new Vector3(1057, 3075, 41),
                End = new Vector3(1554, 3210, 41),
                Heading = 75
            };
            Runways[(int)RunwayID.LSIA3] = new Runway
            {
                Start = new Vector3(-1631, -2722, 15),
                End = new Vector3(-1383, -2291, 15),
                Heading = 330
            };
            Runways[(int)RunwayID.LSIA21] = new Runway
            {
                Start = new Vector3(-1383, -2291, 15),
                End = new Vector3(-1631, -2722, 15),
                Heading = 150
            };
            Runways[(int)RunwayID.LSIA12] = new Runway
            {
                Start = new Vector3(-1501, -2854, 15),
                End = new Vector3(-989, -3149, 14),
                Heading = 240
            };
            Runways[(int)RunwayID.LSIA30] = new Runway
            {
                Start = new Vector3(-989, -3149, 14),
                End = new Vector3(-1501, -2854, 15),
                Heading = 60
            };
            Runways[(int)RunwayID.USSL34] = new Runway
            {
                Start = new Vector3(3087, -4783, -20),
                End = new Vector3(3024, -4648, -20),
                Heading = 25
            };
            Runways[(int)RunwayID.USSL16] = new Runway
            {
                Start = Runways[(int)RunwayID.USSL34].End,
                End = Runways[(int)RunwayID.USSL34].Start,
                Heading = Runways[(int)RunwayID.USSL34].Heading + 180
            };
        }

        public static float ToMPH(float mps)
        {
            return mps / 0.44704f;
        }

        public static float ToMPS(float mph)
        {
            return mph * 0.44704f;
        }

        public static void DriveToCoord(Ped driver, Vehicle vehicle, Vector3 destination, float speed, DrivingStyle drivingStyle)
        {
            Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, driver, vehicle,
                          destination.X, destination.Y, destination.Z, speed * 0.44704f, 5f, vehicle.GetHashCode(), drivingStyle, 1f, true);
        }

        public static void Normal(Ped driver, Vehicle vehicle, Vector3 destination, float speed)
        {
            GTA.UI.Notification.Show("Drive normal to " + destination.ToString());
            DriveToCoord(driver, vehicle, destination, speed, DrivingStyle.Normal);
            while (vehicle.Position.DistanceTo2D(destination) > 5) Yield();
        }

        public static void PlaneTaxi(Ped pilot, Vehicle plane, Vector3 destination)
        {
            while (plane.Position.DistanceTo2D(destination) > 30)
            {
                DriveToCoord(pilot, plane, destination, PlaneSpeeds[plane.Model.Hash].Taxi, DrivingStyle.Normal);
                Yield();
            }
            JaysMod.Debug("Close Taxiing");
            while (plane.Position.DistanceTo2D(destination) > 5)
            {
                DriveToCoord(pilot, plane, destination, PlaneSpeeds[plane.Model.Hash].Taxi / 2, DrivingStyle.Normal);
                Yield();
            }
        }

        public static void PlaneTakeoff(Ped pilot, Vehicle plane, RunwayID runwayID)
        {
            Runway runway = Runways[(int)runwayID];
            Vector3 start = plane.Position;
            float startDistance = start.DistanceTo2D(runway.End);
            PlaneSpeed planeSpeed;
            PlaneSpeeds.TryGetValue(plane.Model.Hash, out planeSpeed);
            float airSpeed = planeSpeed.Air * 1;
            float groundSpeed = planeSpeed.Ground;
            float initSpeed = planeSpeed.Taxi;
            float initDistance = plane.Position.DistanceTo2D(runway.End);
            float distance = plane.Position.DistanceTo2D(runway.End);
            while (distance > 10f)
            {
                float distanceTraveled = initDistance - distance;
                float speed = distanceTraveled / initDistance * (groundSpeed - initSpeed) * 2 + initSpeed;
                speed = Math.Min(speed, groundSpeed);
                Vector3 end = runway.End;
                if (distance < 130f)
                {
                    end.Z = end.Z + 30;
                    speed = airSpeed;
                }
                DriveToCoord(pilot, plane, end, speed, DrivingStyle.Normal);
                distance = plane.Position.DistanceTo2D(runway.End);
                Yield();
            }
            plane.LandingGearState = VehicleLandingGearState.Retracting;
        }

        public static void PlaneFly(Ped pilot, Vehicle plane, Vector3 destination)
        {
            PlaneSpeed planeSpeed;
            PlaneSpeeds.TryGetValue(plane.Model.Hash, out planeSpeed);
            float speed = planeSpeed.Air;
            DriveToCoord(pilot, plane, destination, speed, DrivingStyle.Normal);
            while (plane.Position.DistanceTo(destination) > 10f) Yield();
        }
        
        public static void PlaneFlySlow(Ped pilot, Vehicle plane, Vector3 destination, float proximity)
        {
            PlaneSpeed planeSpeed;
            PlaneSpeeds.TryGetValue(plane.Model.Hash, out planeSpeed);
            float slowSpeed = 60f;
            float initSpeed = planeSpeed.Air - slowSpeed;
            float initDistance = plane.Position.DistanceTo(destination);
            while (plane.Position.DistanceTo2D(destination) > proximity)
            {
                float distance = plane.Position.DistanceTo2D(destination);
                float speed = initSpeed * distance / initDistance + slowSpeed;
                DriveToCoord(pilot, plane, destination, speed, DrivingStyle.Normal);
                Yield();
            }
        }

        public static void PlaneFlyWarp(Ped pilot, Vehicle plane)
        {
            Vector3 destination = new Vector3(-7000, 600, 2250);
            float speed = 500;
            Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, pilot, plane, destination.X, destination.Y, destination.Z, speed * 0.44704f, 5f, plane.GetHashCode(), 16777216, 1f, true);
            while (plane.Position.DistanceTo(destination) > 25f) Wait(100);
        }

        public static void PlaneLand(Ped pilot, Vehicle plane, RunwayID runwayId)
        {
            Runway runway = Runways[(int)runwayId];
            Vector3 touchdown = (runway.Start + runway.End) / 2;
            Vector3 start = runway.Start;
            start.Z = start.Z + 30;

            KeyValuePair<Vector3, Vector3> approach = calculateApproach(runway.Start, runway.End);
            //GTA.UI.Screen.FadeOut(2000);
            //Wait(2000);
            plane.Position = approach.Key;
            plane.Speed = 180;
            plane.Heading = runway.Heading;
            PlaneFlySlow(pilot, plane, approach.Value, 20f);
            //GTA.UI.Screen.FadeIn(2000);
            plane.LandingGearState = VehicleLandingGearState.Deploying;
            pilot.Task.LandPlane(runway.Start, runway.End, plane);
            while(plane.WheelSpeed == 0)
            {
                Yield();
            }
            Normal(pilot, plane, runway.End, 30f);
        }

        private static KeyValuePair<Vector3, Vector3> calculateApproach(Vector3 start, Vector3 end)
        {
            float length = start.DistanceTo2D(end);
            float multiplier = 2000 / length;
            Vector3 diff = end - start;
            Vector3 approach = start - (diff * multiplier);
            approach.Z = start.Z + 400;

            multiplier = 700 / length;
            Vector3 landing = start - (diff * multiplier);
            landing.Z = landing.Z + 50;
            return new KeyValuePair<Vector3, Vector3>(approach, landing);
        }
    }
}
