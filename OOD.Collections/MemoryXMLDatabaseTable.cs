using System.IO;
using System.Xml.Serialization;


namespace OOD.Collections
{
    public class MemoryXMLDatabaseTable<TKey, TValue>: XMLDatabaseTable<TKey, TValue> where TValue : IXMLDatabaseItem<TKey>
    {
        #region Properties
        private Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();
        public override int Count { get { return _values.Count; } }
        private string _filepath { get { return Path.Combine(Directory, TableName + ".xml"); } }
        public override bool ReadOnly { get; }
        #endregion
        #region Constructors
        public MemoryXMLDatabaseTable(string directory, string tableName, bool readOnly = false)
        {
            Directory = directory;
            TableName = tableName;
            ReadOnly = readOnly;
            Load();
        }
        #endregion Constructors
        #region GetValue
        public override bool TryGetValue(TKey ID, out TValue value)
        {
            return _values.TryGetValue(ID, out value);
        }
        public override TValue GetValue(TKey ID)
        {
            if (TryGetValue(ID, out TValue value))
            {
                return value;
            }
            return value;
        }
        public override bool Contains(TKey ID)
        {
            return TryGetValue(ID, out TValue value);
        }
        #endregion Get
        #region AddValue
        public override bool TryAddValue(TValue value)
        {
            if (ReadOnly) return false;
            if (_values.TryAdd(value.ID, value))
            {
                Save();
                return true;
            }
            return false;
        }
        public override bool TryUpdateValue(TValue value)
        {
            if (ReadOnly) return false;
            if (Contains(value.ID)) return false;
            RemoveValue(value.ID);
            AddValue(value);
            return true;
        }
        #endregion AddValue
        #region RemoveValue
        public override bool TryRemoveValue(TKey id)
        {
            if (ReadOnly) return false;
            return _values.TryRemove(id);
        }
        #endregion RemoveValue
        public override void ClearCache()
        {
            Load();
        }
        private void Load()
        {
            _values.Clear();
            if (!File.Exists(_filepath)) return;
            _values = new Dictionary<TKey, TValue>();
            using (TextReader reader = new StreamReader(_filepath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TValue[]));
                TValue[] valueArray;
                try
                {
                    valueArray = (TValue[])serializer.Deserialize(reader);
                }
                catch
                {
                    valueArray = null;
                }
                if (valueArray == null) return;
                foreach (TValue value in valueArray)
                {
                    if (value != null)
                    {
                        _values.TryAdd(value.ID, value);
                    }
                }
            }
        }
        private void Save()
        {
            System.IO.Directory.CreateDirectory(Directory);
            using (FileStream stream = File.Create(_filepath)) { }

            using (TextWriter writer = new StreamWriter(_filepath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TValue[]));
                serializer.Serialize(writer, _values.ValueArray);
            }
        }
    }
}
