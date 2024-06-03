using System.Xml.Serialization;

namespace JMF
{
    namespace Universe
    {
        public static class Truth
        {
            public static Worldspace CurrentWorldspace
            {
                get;
                private set;
            }
            public static bool ChangeWorldspace(string worldspaceId, string mapId)
            {
                if (Framework.Database.Worldspaces.TryGetValue(worldspaceId, out Worldspace worldspace))
                {
                    if (CurrentWorldspace != null)
                    {
                        CurrentWorldspace.Unload();
                    }
                    CurrentWorldspace = worldspace;
                    CurrentWorldspace.LoadWorldspace(mapId);
                    return true;
                }
                return false;
            }
        }
    }
}
