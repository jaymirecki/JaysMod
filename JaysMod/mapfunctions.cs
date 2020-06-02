using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace Maps
{
    public class Functions : Script
    {
        public static int driveNormal;

        public Functions()
        {
            driveNormal = 536871355;
        }

        public static Vehicle SpawnVehicle(string modelName, Vector3 location, float orientation, int pColor, int sColor, int livery)
        {
            Model model = new Model(Function.Call<int>(Hash.GET_HASH_KEY, modelName));
            Vehicle[] vehicles = GTA.World.GetNearbyVehicles(location, 3f, model);
            if (vehicles.Length > 0)
            {
                for (int i = 0; i < vehicles.Length; i++)
                    DeleteVehicle(vehicles[i]);
            }


            Vehicle newVehicle = GTA.World.CreateVehicle(model, location, orientation);
            Function.Call(Hash.SET_VEHICLE_COLOURS, newVehicle, pColor, sColor);
            Function.Call(Hash.SET_VEHICLE_LIVERY, newVehicle, livery);
            return newVehicle;
        }

        public static Ped SpawnPed(string modelName, Vector3 location, float orientation)
        {
            Model model = new Model(Function.Call<int>(Hash.GET_HASH_KEY, modelName));
            Ped[] peds = GTA.World.GetNearbyPeds(location, 3f, model);
            if (peds.Length > 0)
            {
                for (int i = 0; i < peds.Length; i++)
                    DeletePed(peds[i]);
            }
            
            Ped newPed = GTA.World.CreatePed(model, location, orientation);
            return newPed;
        }

        public static Prop SpawnProp(int hash, Vector3 pos, Vector3 rot, bool dynamic)
        {
            int LodDistance = 3000;

            Model model = new Model(hash);
            model.Request(10000);
            Prop prop = GTA.World.CreateProp(model, pos, rot, dynamic, false);
            prop.Position = pos;
            prop.LodDistance = LodDistance;
            if (!dynamic)
                prop.IsPositionFrozen = true;

            return prop;
        }

        public static void DeleteVehicle(Vehicle currVehicle)
        {
            if (currVehicle.Exists())
            {
                //currVehicle.MarkAsNoLongerNeeded();
                currVehicle.Delete();
            }
        }

        public static void DeletePed(Ped currPed)
        {
            if (currPed.Exists())
            {
                currPed.Delete();
            }
        }

        public static void DeleteProp(Prop currProp)
        {
            if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, currProp))
                currProp.Delete();
        }

        public static List<Prop> FillArea(int hash, Vector3 start, float rot, float width, float depth, int wide, int length)
        {
            float radrot;
            Vector3 currpos, nextInRow, nextInColumn, rowpos;
            List<Prop> spawnedProps;
            spawnedProps = new List<Prop>();
            radrot = rot * ((float)Math.PI / 180);

            nextInRow = new Vector3((float)Math.Cos(radrot) * width, (float)Math.Sin(radrot) * width, 0);
            nextInColumn = new Vector3((float)Math.Sin(radrot) * depth, (float)Math.Cos(radrot) * depth, 0);


            currpos = start;
            for (int i = 0; i < length; i++)
            {
                rowpos = currpos;
                for (int j = 0; j < wide; j++)
                {
                    spawnedProps.Add(Maps.Functions.SpawnProp(hash, rowpos, new Vector3(0, 0, rot), false));
                    //UI.Notify(rowpos.ToString());
                    rowpos.X += nextInRow.X;
                    rowpos.Y += nextInRow.Y;
                }
                currpos.X += nextInColumn.X;
                currpos.Y -= nextInColumn.Y;
            }

            return spawnedProps;
        }

        public static List<Prop> FillArea(int hash, Vector3 x1y1, Vector3 x2y2, float rot, float width, float depth)
        {
            int wide, length;
            float radrot;
            Vector3 x1y2, x2y1, currpos, nextInRow, nextInColumn, rowpos;
            List<Prop> spawnedProps;
            spawnedProps = new List<Prop>();
            radrot = rot * ((float)Math.PI / 180);

            // Calculate the rectangle
            x1y2 = new Vector3(x1y1.X, x2y2.Y, x1y1.Z);
            x2y1 = new Vector3(x2y2.X, x1y1.Y, x1y1.Z);
            wide = (int)(x1y1.DistanceTo(x2y1) / width);
            length = (int)(x1y1.DistanceTo(x1y2) / depth);

            GTA.UI.Screen.ShowHelpTextThisFrame(wide.ToString() + " " + length.ToString());

            currpos = x1y1;
            nextInRow = new Vector3((float)Math.Cos(radrot) * width, (float)Math.Sin(radrot) * width, 0);
            nextInColumn = new Vector3((float)Math.Sin(radrot) * depth, (float)Math.Cos(radrot) * depth, 0);

            // Fill in the rectangle
            for (int i = 0; i < wide; i++)
            {
                rowpos = currpos;
                for (int j = 0; j < length; j++)
                {
                    spawnedProps.Add(Maps.Functions.SpawnProp(hash, rowpos, new Vector3(0, 0, rot), false));
                    //UI.Notify(rowpos.ToString());
                    rowpos.X += nextInRow.X;
                    rowpos.Y += nextInRow.Y;
                }
                currpos.X += nextInColumn.X;
                currpos.Y -= nextInColumn.Y;
            }

            return spawnedProps;
        }

        public static float Speed(float value)
        {
            return value * 0.44704f;
        }
    }
}