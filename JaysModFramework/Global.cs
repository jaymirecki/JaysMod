﻿using JaysModFramework.Clothing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JaysModFramework
{
    public static class Global
    {
        private const string ConfigFilepath = ".\\scripts\\JMF\\config.xml";
        public static readonly Database Database = new Database();
        public static Config Config
        {
            get
            {
                if (!File.Exists(ConfigFilepath)) return new Config();
                Debug.Log(DebugSeverity.Warning, "Could not find config.xml.");
                using (TextReader reader = new StreamReader(ConfigFilepath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Config));
                    return (Config)serializer.Deserialize(reader);
                }
            }
        }
    }
}
