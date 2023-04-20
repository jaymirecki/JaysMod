using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework
{
    internal static class IDGenerator
    {
        public static string NPCID(NPC npc)
        {
            string baseId = "";
            if (npc.Name != "")
            {
                baseId = npc.Name;
            }
            return GenerateID("NPC", NPC.SpawnedNPCs.Keys, baseId);
        }
        internal static string VehicleID(Vehicle vehicle)
        {
            string baseId = vehicle.ModelName;
            return GenerateID("Vehicle", Vehicle.SpawnedVehicles.Keys, baseId);
        }
        private static string GenerateID<TVal>(string type, Dictionary<string, TVal>.KeyCollection collisions, string baseId = "")
        {
            int counter = 1;
            string ID = "JMF" + type;
            if (!string.IsNullOrEmpty(baseId))
            {
                return ID + baseId;
            }
            while (!IsValidID(IDPlusCounter(ID, counter), collisions))
            {
                counter++;
            }
            return ID + counter.ToString("D8");
        }
        private static string IDPlusCounter(string id, int counter)
        {
            return id + counter.ToString("D8");
        }
        private static bool IsValidID<TVal>(string id, Dictionary<string, TVal>.KeyCollection collisions)
        {
            return !collisions.Contains(id);
        }
    }
}
