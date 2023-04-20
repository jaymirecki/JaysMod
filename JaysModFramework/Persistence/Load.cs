using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JaysModFramework.Persistence
{
    public static class Load
    {
        #region Properties
        private const string SourceDirectory = "./scripts/JMF/";
        #endregion
        #region Load
        public static T LoadItem<T>(string directory, string id)
        {
            string loadFile;
            T item = default(T);
            try
            {
                loadFile = CheckLoadFile(directory, id);
                ReadFromFile(loadFile, out item);
            }
            catch { }
            return item;
        }
        private static string CheckLoadFile(string directory, string id)
        {
            EnsureDirectory(SourceDirectory);

            string loadDirectory = SourceDirectory + directory + "/";
            string loadFile = loadDirectory + id + ".xml";
            if (File.Exists(loadFile))
            {
                return loadFile;
            }

            throw new Exception("Invalid Directory");
        }
        private static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private static T ReadFromFile<T>(string filepath, out T item)
        {
            TextReader reader = new StreamReader(filepath);
            XmlSerializer ItemSerializer = new XmlSerializer(typeof(T));
            item = (T)ItemSerializer.Deserialize(reader);
            reader.Close();
            return item;
        }
        #endregion
    }
}
