using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JaysModFramework.Persistence
{
    public interface IJMFDatabaseItem<TKey>
    {
        TKey ID { get; }
    }
    public class JMFDatabase<TKey, TValue> where TValue : IJMFDatabaseItem<TKey>
    {
        #region Properties
        private JMFDictionary<TKey, TValue> Dictionary;
        public int Count
        {
            get
            {
                return Dictionary.Count;
            }
        }
        #endregion
        #region Constructors
        public JMFDatabase()
        {
            Dictionary = new JMFDictionary<TKey, TValue>();
        }
        public JMFDatabase(string directory, string filename):this()
        {
            LoadFromFile(directory, filename);
        }
        #endregion Constructors
        #region Load
        public void LoadFromFile(string directory, string filename)
        {
            try
            {
                string filepath = CheckLoadFile(directory, filename);
                Dictionary = new JMFDictionary<TKey, TValue>();
                TValue[] valueArray = ReadFromFile<TValue[]>(filepath);
                foreach (TValue value in valueArray)
                {
                    if (value != null)
                    {
                        Dictionary.TryAdd(value.ID, value);
                    }
                }
            }
            catch { }
        }
        private static string CheckLoadFile(string directory, string filename)
        {
            string filepath = directory + filename;
            if (File.Exists(filepath))
            {
                return filepath;
            }

            throw new Exception("Invalid Directory");
        }
        private static T ReadFromFile<T>(string filepath)
        {
            TextReader reader = new StreamReader(filepath);
            XmlSerializer ItemSerializer = new XmlSerializer(typeof(T));
            T item = (T)ItemSerializer.Deserialize(reader);
            reader.Close();
            return item;
        }
        #endregion Load
        #region Save
        public void SaveToFile(string directory, string filename)
        {
            string filepath = EnsureSaveFile(directory, filename);
            WriteToFile(filepath, Dictionary.ValueArray);
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
                FileStream stream = File.Create(filepath);
                stream.Close();
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
        public bool TryGetValue(TKey ID, out TValue value)
        {
            return Dictionary.TryGetValue(ID, out value);
        }
        public TValue GetValue(TKey ID)
        {
            TValue value = default(TValue);
            TryGetValue(ID, out value);
            return value;
        }
        #endregion Get
    }
}
