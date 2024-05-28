namespace JMF
{
    namespace Universe
    {
        public struct Portal
        {
            public string ID;
            public string InPortalIPL;
            public string InPortalID;
            public string OutPortalMap;
            public string OutPortalIPL;
            public string OutPortalID;

            public Portal(
                string id = "",
                string inPortalIPL = "",
                string inPortalID = "",
                string outPortalMap = "",
                string outPortalIPL = "",
                string outPortalID = ""
                )
            {
                ID = id;
                InPortalIPL = inPortalIPL;
                InPortalID = inPortalID;
                OutPortalMap = outPortalMap;
                OutPortalIPL = outPortalIPL;
                OutPortalID = outPortalID;
            }
        }
    }
}
