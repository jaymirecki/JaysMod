using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public struct TravelPoint
        {
            const float MainlandXMed = 403;
            const float MainlandXMax = MainlandXMed + 6500;
            const float MainlandXMin = MainlandXMed - 6500;
            const float MainlandYMed = 2024;
            const float MainlandYMax = MainlandYMed + 7500;
            const float MainlandYMin = MainlandYMed - 9000;

            const float IslandXMed = 403;
            const float IslandXMax = IslandXMed + 6500;
            const float IslandXMin = IslandXMed - 6500;
            const float IslandYMed = 2024;
            const float IslandYMax = IslandYMed + 5000;
            const float IslandYMin = IslandYMed - 5000;

            const float Z = 200;

            public string ID;
            public TravelPointPosition Position;
            [XmlIgnore]
            public Worldspace ParentWorldspace;
            public string ConnectedWorldspaceID;
            private Worldspace _connectedWorldspace;
            [XmlIgnore]
            public Worldspace ConnectedWorldspace
            {
                get
                {
                    if (_connectedWorldspace == null)
                    {
                        Framework.Database.Worldspaces.TryGetValue(ConnectedWorldspaceID, out _connectedWorldspace);
                    }
                    return _connectedWorldspace;
                }
            }
            public string ConnectedTravelPointID;

            public TravelPoint(
                string id = "",
                TravelPointPosition position = TravelPointPosition.MainlandNorth,
                string parentWorldspaceID = "",
                string connectWorldspaceID = "",
                string connectedTravelPointID = ""
                )
            {
                ID = id;
                Position = position;
                ConnectedWorldspaceID = connectWorldspaceID;
                ConnectedTravelPointID = connectedTravelPointID;
                ParentWorldspace = new Worldspace();
                _connectedWorldspace = null;
            }

            public void OnTick()
            {
                if (Game.Player.Character.IsInAnyVehicle && Game.Player.Character.CurrentVehicle.Class == VehicleClass.Planes)
                {
                    Vector3 vector = VectorPosition(Position);
                    World.DrawMarker(vector);
                    if (Game.Player.Character.Position.DistanceTo2D(vector) < 50f)
                    {
                        if (ConnectedWorldspace.TryGetTravelPoint(ConnectedTravelPointID, out TravelPoint connectedTravelPoint))
                        {
                            Vector3 connectedVector = VectorPosition(connectedTravelPoint.Position);
                            Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                            float speed = vehicle.Speed;
                            float heading = connectedVector.HeadingTo(new Vector3(403));
                            vehicle.Heading = heading;
                            Framework.State.Worldspace = ConnectedWorldspaceID;
                            vehicle.Position = connectedVector.Offset(100f, heading);
                            vehicle.Speed = speed;
                        }
                    }
                }
            }
            private static Vector3 VectorPosition(TravelPointPosition position)
            {
                Vector3 vector;
                switch (position)
                {
                    case TravelPointPosition.MainlandNorth:
                        vector = new Vector3(MainlandXMed, MainlandYMax, Z);
                        break;
                    case TravelPointPosition.MainlandNortheast:
                        vector = new Vector3(MainlandXMax, MainlandYMax, Z);
                        break;
                    case TravelPointPosition.MainlandEast:
                        vector = new Vector3(MainlandXMax, MainlandYMed, Z);
                        break;
                    case TravelPointPosition.MainlandSoutheast:
                        vector = new Vector3(MainlandXMax, MainlandYMin, Z);
                        break;
                    case TravelPointPosition.MainlandSouth:
                        vector = new Vector3(MainlandXMed, MainlandYMin, Z);
                        break;
                    case TravelPointPosition.MainlandSouthwest:
                        vector = new Vector3(MainlandXMin, MainlandYMin, Z);
                        break;
                    case TravelPointPosition.MainlandWest:
                        vector = new Vector3(MainlandXMin, MainlandYMed, Z);
                        break;
                    case TravelPointPosition.MainlandNorthwest:
                        vector = new Vector3(MainlandXMin, MainlandYMax, Z);
                        break;
                    case TravelPointPosition.IslandNorth:
                        vector = new Vector3(IslandXMed, IslandYMax, Z);
                        break;
                    case TravelPointPosition.IslandNortheast:
                        vector = new Vector3(IslandXMax, IslandYMax, Z);
                        break;
                    case TravelPointPosition.IslandEast:
                        vector = new Vector3(IslandXMax, IslandYMed, Z);
                        break;
                    case TravelPointPosition.IslandSoutheast:
                        vector = new Vector3(IslandXMax, IslandYMin, Z);
                        break;
                    case TravelPointPosition.IslandSouth:
                        vector = new Vector3(IslandXMed, IslandYMin, Z);
                        break;
                    case TravelPointPosition.IslandSouthwest:
                        vector = new Vector3(IslandXMin, IslandYMin, Z);
                        break;
                    case TravelPointPosition.IslandWest:
                        vector = new Vector3(IslandXMin, IslandYMed, Z);
                        break;
                    case TravelPointPosition.IslandNorthwest:
                        vector = new Vector3(IslandXMin, IslandYMax, Z);
                        break;
                    default:
                        vector = new Vector3(MainlandXMed, MainlandYMax, Z);
                        break;
                }
                return vector;
            }
        }
    }
}
