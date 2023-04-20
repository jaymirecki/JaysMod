using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JaysModFramework.Persistence
{
    public interface IJMFDatabaseItem
    {
        string ID { get; }
    }
    public class JMFDatabase<TValue> where TValue : IJMFDatabaseItem
    {
        #region Properties
        private JMFDictionary<string, TValue> Dictionary;
        #endregion
        #region Constructors
        public JMFDatabase()
        {
            Dictionary = new JMFDictionary<string, TValue>();
        }
        public JMFDatabase(string directory):this()
        {
            LoadFromFile(directory);
        }
        #endregion Constructors
        #region Load
        public void LoadFromFile(string directory)
        {
            Dictionary = new JMFDictionary<string, TValue>();
            string[] files = Directory.GetFiles(directory);
            foreach (string file in files)
            {
                TValue item = LoadItem<TValue>(directory, file);
                Dictionary.Add(item.ID, item);
            }
        }
        private static T LoadItem<T>(string directory, string file)
        {
            string loadFile;
            T item = default(T);
            try
            {
                loadFile = CheckLoadFile(file);
                ReadFromFile(loadFile, out item);
            }
            catch { }
            return item;
        }
        private static string CheckLoadFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                return filepath;
            }

            throw new Exception("Invalid Directory");
        }
        private static T ReadFromFile<T>(string filepath, out T item)
        {
            TextReader reader = new StreamReader(filepath);
            XmlSerializer ItemSerializer = new XmlSerializer(typeof(T));
            item = (T)ItemSerializer.Deserialize(reader);
            reader.Close();
            return item;
        }
        #endregion Load
        #region Save
        public void SaveToFile(string directory)
        {
            foreach (TValue item in Dictionary.Values)
            {
                SaveItem(directory, item);
            }
        }
        private static void SaveItem<T>(string directory, T item) where T: IJMFDatabaseItem
        {
            string saveFile = item.ID + ".xml";
            string filepath = EnsureSaveFile(directory, saveFile);
            WriteToFile(filepath, item);
        }
        private static string EnsureSaveFile(string directory, string file)
        {
            string filepath = directory + file;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }

            return filepath;
        }
        private static void WriteToFile<T>(string filepath, T item)
        {
            TextWriter writer = new StreamWriter(filepath);
            XmlSerializer ItemSerializer = new XmlSerializer(typeof(T));
            ItemSerializer.Serialize(writer, item);
            writer.Close();
        }
        #endregion Save
        #region GetValue
        public bool TryGetValue(string ID, out TValue value)
        {
            return Dictionary.TryGetValue(ID, out value);
        }
        public TValue GetValue(string ID)
        {
            TValue value = default(TValue);
            TryGetValue(ID, out value);
            return value;
        }
        #endregion Get
    }
}
