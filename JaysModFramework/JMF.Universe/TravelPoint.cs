using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public struct TravelPoint
        {
            public string ID;
            public Vector3 Position;
            public Vector3 ConnectedPosition;
            public string ParentWorldspaceID;
            private Worldspace _parentWorldspace;
            [XmlIgnore]
            public Worldspace ParentWorldspace
            {
                get
                {
                    if (_parentWorldspace == null)
                    {
                        _parentWorldspace = new Worldspace();
                    }
                    return _parentWorldspace;
                }
            }
            public string ConnectedWorldspaceID;
            private Worldspace _connectedWorldspace;
            [XmlIgnore]
            public Worldspace ConnectedWorldspace
            {
                get
                {
                    if (_connectedWorldspace == null)
                    {
                        _connectedWorldspace = new Worldspace();
                    }
                    return _connectedWorldspace;
                }
            }

            public TravelPoint(
                string id = "",
                Vector3 position = new Vector3(),
                Vector3 connectedPosition = new Vector3(),
                string parentWorldspaceID = "",
                string connectWorldspaceID = ""
                )
            {
                ID = id;
                Position = position;
                ConnectedPosition = connectedPosition;
                ParentWorldspaceID = parentWorldspaceID;
                ConnectedWorldspaceID = connectWorldspaceID;
                _parentWorldspace = null;
                _connectedWorldspace = null;
            }
        }
    }
}
