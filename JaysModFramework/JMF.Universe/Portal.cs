using JMF.Interiors;
using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public class Portal
        {
            public string ID;
            public string IPL;
            public string RoomPortalID;
            private Vector3 _position;
            public Vector3 Position
            {
                get
                {
                    if (
                        IPL != null && IPL != ""
                        && Framework.Database.IPLs.TryGetValue(IPL, out IPL ipl) 
                        && ipl.TryGetRoomPortal(RoomPortalID, out RoomPortal roomPortal)
                        )
                    {
                        _position = roomPortal.InPosition;
                    }
                    return _position;
                }
                set { _position = value; }
            }
            private float _heading;
            public float Heading
            {
                get
                {
                    if (
                        IPL != null && IPL != ""
                        && Framework.Database.IPLs.TryGetValue(IPL, out IPL ipl)
                        && ipl.TryGetRoomPortal(RoomPortalID, out RoomPortal roomPortal)
                        )
                    {
                        _heading = roomPortal.OutHeading;
                    }
                    return _heading;
                }
                set { _heading = value; }
            }
            public Vector3 OutPosition
            {
                get { return Position.Offset(1f, Heading); }
            }
            public string ConnectedPortalMap;
            public string ConnectedPortalID;
            [XmlIgnore]
            public Map Map;
            [XmlIgnore]
            public Worldspace Worldspace
            {
                get { return Map.Worldspace; }
            }
            private bool GetConnectedPortal(out Portal connectedPortal)
            {
                if (ConnectedPortalMap != null && ConnectedPortalMap != ""
                    && Worldspace.GetMap(ConnectedPortalMap, out Map map)
                    && map.TryGetPortal(ConnectedPortalID, out connectedPortal)
                    )
                {
                    return true;
                }
                Debug.Log(DebugSeverity.Error, "Portal " + ConnectedPortalMap + ":" + ConnectedPortalID + " does not exist");
                connectedPortal = new Portal();
                return false;
            }
            public Portal() : this("", "", "", new Vector3(), 0f, "", "") { }
            public Portal(
                string id = "",
                Vector3 position = new Vector3(),
                float heading = 0f,
                string connectedPortalMap = "",
                string connectedPortalID = ""
                ) : this(id, "", "", position, heading, connectedPortalMap, connectedPortalID) { }

            public Portal(
                string id = "",
                string ipl = "",
                string roomPortalId = "",
                string connectedPortalMap = "",
                string connectedPortalID = ""
                ) : this(id, ipl, roomPortalId, new Vector3(), 0f, connectedPortalMap, connectedPortalID) { }
            public Portal(
                string id = "",
                string ipl = "",
                string roomPortalId = "",
                Vector3 position = new Vector3(),
                float heading = 0f,
                string connectedPortalMap = "",
                string connectedPortalID = ""
                )
            {
                ID = id;
                IPL = ipl;
                RoomPortalID = roomPortalId;
                _position = position;
                _heading = heading;
                ConnectedPortalMap = connectedPortalMap;
                ConnectedPortalID = connectedPortalID;
                Map = new Map();
            }
            public void Draw()
            {
                World.DrawMarker(Position.Offset(new Vector3(0, 0, -1.1f)));
            }
            public bool ValidateWorldspace()
            {
                if (Map == null)
                {
                    Debug.Log(DebugSeverity.Error, "Portal " + ID + " does not have a map");
                    return false;
                }
                if (Worldspace == null)
                {
                    Debug.Log(DebugSeverity.Error, "Portal " + ID + " does not have a worldspace");
                    return false;
                }
                return true;
            }
            public void Teleport()
            {
                Ped player = Game.Player.Character;
                if (player.Position.DistanceTo(Position) < 2 && GetConnectedPortal(out Portal outPortal))
                {
                    if (!outPortal.ValidateWorldspace())
                    {
                        return;
                    }
                    Worldspace.LoadMap(outPortal.Map.ID, out Map outMap);
                    player.Position = outPortal.Position.Offset(3f, outPortal.Heading);
                    player.Heading = outPortal.Heading;
                }
            }
        }
    }
}
