namespace JMF
{
    namespace Interiors
    {
        public struct RoomPortal
        {
            public string ID;
            public Vector3 Position;
            public float Heading;
            public RoomPortalType PortalType;

            public RoomPortal(string id, Vector3 position, float heading, RoomPortalType portalType)
            {
                ID = id;
                Position = position;
                Heading = heading;
                PortalType = portalType;
            }
        }
    }
}
