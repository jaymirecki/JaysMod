using System.IO;
using System.Xml.Serialization;

namespace JMF
{
    public static class Global
    {
        private const string ConfigFilepath = ".\\JMF\\config.xml";
        //public static readonly Database Database = new Database();
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
    }
}
