using JMF.Math;

namespace JMF
{
    namespace Interiors
    {
        public struct RoomPortal
        {
            public string ID;
            public Vector3 InPosition;
            public Vector3 OutPosition;
            public float OutHeading;
            public RoomPortalType PortalType;

            public RoomPortal(string id, Vector3 inPosition, Vector3 outPosition, float outHeading, RoomPortalType portalType)
            {
                ID = id;
                InPosition = inPosition;
                OutPosition = outPosition;
                OutHeading = outHeading;
                PortalType = portalType;
            }
        }
    }
}
