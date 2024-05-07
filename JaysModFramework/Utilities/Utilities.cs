using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework.Utilities
{
    internal class Utilities
    {
        internal static Rage.Vehicle[] GetNearbyVehicles(Rage.Vector3 position, float radius) {
            return (Rage.Vehicle[]) World.GetEntities(position, radius, GetEntitiesFlags.ConsiderAllVehicles);
        }
    }
}
