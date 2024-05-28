using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace OOD.Collections
{
    public class MemoryXMLDatabaseTable<TKey, TValue>: XMLDatabaseTable<TKey, TValue> where TValue : IXMLDatabaseItem<TKey>
    {
        #region Properties
        private Dictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();
        public override int Count { get { return values.Count; } }
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
            return values.TryGetValue(ID, out value);
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
            if (values.TryAdd(value.ID, value))
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
            return values.TryRemove(id);
        }
        #endregion RemoveValue
        #region IEnumerable
        public override IEnumerator GetEnumerator()
        {
            return values.Values.GetEnumerator();
        }
        #endregion IEnumerable
        public override void ClearCache()
        {
            Load();
        }
        private void Load()
        {
            values.Clear();
            values = new Dictionary<TKey, TValue>();
            string directoryPath = Path.Combine(Directory, TableName);
            XmlSerializer serializer = new XmlSerializer(typeof(TValue));
            foreach (string filepath in System.IO.Directory.GetFiles(directoryPath))
            {
                using (TextReader reader = new StreamReader(filepath))
                {
                    TValue val = (TValue)serializer.Deserialize(reader);
                    values.TryAdd(val.ID, val);
                }
            }
        }
        private void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TValue));
            string directoryPath = Path.Combine(Directory, TableName);
            System.IO.Directory.CreateDirectory(directoryPath);
            foreach (TValue val in values.Values)
            {
                string filepath = Path.Combine(directoryPath, val.ID + ".xml");
                using (FileStream stream = File.Create(filepath)) { }

                using (TextWriter writer = new StreamWriter(filepath))
                {
                    serializer.Serialize(writer, val);
                }
            }
        }
        public override List<ValidationState> Validate(bool throwException = false)
        {
            List<ValidationState> states = new List<ValidationState>();
            foreach (TValue v in values.Values)
            {
                ValidationState s = v.Validate();
                if (!s.IsValid)
                {
                    if (throwException)
                    {
                        throw new InvalidDatabaseItemException(s.ErrorMessage);
                    }
                    states.Add(s);
                }
            }
            if (states.Count < 1)
            {
                states.Add(new ValidationState(true));
            }
            return states;
        }
    }
    public class InvalidDatabaseItemException : Exception
    {
        public InvalidDatabaseItemException() { }
        public InvalidDatabaseItemException(string message) : base(message) { }
    }
}
