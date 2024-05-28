using System.Collections.Generic;

namespace JMF
{
    namespace Interiors
    {
        public struct IPLSettings
        {
            public string ID;
            public List<string> EntitySets;
            public string Theme;

            public IPLSettings(string id, List<string> entitySets, string theme = "")
            {
                ID = id;
                EntitySets = entitySets == null ? new List<string>() : entitySets;
                Theme = theme;
            }
        }
    }
}
