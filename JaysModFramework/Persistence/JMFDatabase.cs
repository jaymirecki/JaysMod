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
    public class JMFDatabase<TKey, TValue> where TValue : IJMFDatabaseItem<TKey>, new()
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
        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { AddValue(key, value); }
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
            if (!CheckLoadFile(directory, filename, out string filepath)) return;
            Dictionary = new JMFDictionary<TKey, TValue>();
            TValue[] valueArray = ReadFromFile<TValue[]>(filepath);
            if (valueArray == null) return;
            foreach (TValue value in valueArray)
            {
                if (value != null)
                {
                    Dictionary.TryAdd(value.ID, value);
                }
            }
        }
        private static bool CheckLoadFile(string directory, string filename, out string filepath)
        {
            filepath = directory + filename;
            if (File.Exists(filepath))
            {
                return true;
            }

            return false;
        }
        private static T ReadFromFile<T>(string filepath)
        {
            TextReader reader = new StreamReader(filepath);
            XmlSerializer ItemSerializer = new XmlSerializer(typeof(T));
            T item;
            try
            {
                item = (T)ItemSerializer.Deserialize(reader);
            }
            catch
            {
                item = default(T);
            }
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
            if (TryGetValue(ID, out TValue value))
            {
                return value;
            }
            return value;
        }
        #endregion Get
        #region AddValue
        public bool TryAddValue(TKey ID, TValue value)
        {
            return Dictionary.TryAdd(ID, value);
        }
        public void AddValue(TKey ID, TValue value)
        {
            TryAddValue(ID, value);
        }
        #endregion AddValue
        public void Clear()
        {
            Dictionary.Clear();
        }
    }
}
