using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JaysModFramework.Persistence
{
    public static class Save
    {
        #region Properties
        private const string SourceDirectory = "./scripts/JMF/";
        #endregion
        #region Save
        public static void SaveItem<T>(string directory, string id, T item)
        {
            string saveDirectory = CreateSaveDirectory(directory, id);
            string saveFile = saveDirectory + id + ".xml";
            WriteToFile(saveFile, item);
        }
        private static string CreateSaveDirectory(string directory, string id)
        {
            EnsureDirectory(SourceDirectory);

            string saveDirectory = SourceDirectory + directory + "/";
            EnsureDirectory(saveDirectory);

            return saveDirectory;
        }
        private static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private static void WriteToFile<T>(string filepath, T item)
        {
            TextWriter writer = new StreamWriter(filepath);
            XmlSerializer ItemSerializer = new XmlSerializer(typeof(T));
            ItemSerializer.Serialize(writer, item);
            writer.Close();
        }
        #endregion
    }
}
