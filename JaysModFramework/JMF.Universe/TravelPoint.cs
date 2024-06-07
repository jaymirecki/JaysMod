using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public struct TravelPoint
        {
            public string ID;
            public Vector3 Position;
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
                Vector3 position = new Vector3(),
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
                    World.DrawMarker(Position);
                    if (Game.Player.Character.Position.DistanceTo2D(Position) < 50f)
                    {
                        Debug.Log(DebugSeverity.Warning, "Player in range");
                        if (ConnectedWorldspace.TryGetTravelPoint(ConnectedTravelPointID, out TravelPoint connectedTravelPoint))
                        {
                            Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                            float speed = vehicle.Speed;
                            float heading = connectedTravelPoint.Position.HeadingTo(new Vector3(403));
                            Debug.Log(DebugSeverity.Error, heading);
                            vehicle.Heading = heading;
                            Debug.Log(DebugSeverity.Error, "moving player to " + connectedTravelPoint.Position.Offset(100f, heading).ToString());
                            Framework.State.Worldspace = ConnectedWorldspaceID;
                            vehicle.Position = connectedTravelPoint.Position.Offset(100f, heading);
                            vehicle.Speed = speed;
                        }
                    }
                }
            }
        }
    }
}
