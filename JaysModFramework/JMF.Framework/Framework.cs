using LemonUI;
using System.IO;
using System.Xml.Serialization;

namespace JMF
{
    public static class Framework
    {
        private const string ConfigFilepath = ".\\JMF\\config.xml";
        public static Config Config
        {
            get
            {
                if (!File.Exists(ConfigFilepath))
                {
                    Debug.Log(DebugSeverity.Warning, "Could not find config.xml.");
                    return new Config();
                }
                using (TextReader reader = new StreamReader(ConfigFilepath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Config));
                    return (Config)serializer.Deserialize(reader);
                }
            }
        }
        public static Database Database = new Database();
        public static State State = new State();
        public static bool IsDatabaseValid = Database.Validate();
        public static ObjectPool ObjectPool = new ObjectPool();
    }
}
