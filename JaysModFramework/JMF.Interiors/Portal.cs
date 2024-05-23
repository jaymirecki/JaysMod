namespace JMF
{
    namespace Interiors
    {
        public struct Portal
        {
            public string ID;
            public string InPortalIPL;
            public string InPortalID;
            public string OutPortalIPL;
            public string OutPortalID;

            public Portal(string id, string inPortalIPL, string inPortalID, string outPortalIPL, string outPortalID)
            {
                ID = id;
                InPortalIPL = inPortalIPL;
                InPortalID = inPortalID;
                OutPortalIPL = outPortalIPL;
                OutPortalID = outPortalID;
            }
        }
    }
}
