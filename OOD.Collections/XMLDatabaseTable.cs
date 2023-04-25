﻿namespace OOD.Collections
{
    public interface IXMLDatabaseItem<TKey>
    {
        TKey ID { get; }
    }
    public abstract class XMLDatabaseTable<TKey, TValue> where TValue: IXMLDatabaseItem<TKey>
    {
        #region Properties
        public string TableName { get; internal set; }
        public string Directory { get; internal set; }
        public abstract string Filepath { get; }
        public abstract bool ReadOnly { get; }
        public abstract int Count { get; }
        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { AddOrUpdateValue(value); }
        }
        #endregion
        #region GetValue
        public abstract bool TryGetValue(TKey id, out TValue value);
        public abstract TValue GetValue(TKey id);
        public abstract bool Contains(TKey id);
        #endregion Get
        #region AddValue
        public abstract bool TryAddValue(TValue value);
        public void AddValue(TValue value)
        {
            TryAddValue(value);
        }
        public abstract bool TryUpdateValue(TValue value);
        public void UpdateValue(TValue value)
        {
            TryUpdateValue(value);
        }
        public void AddOrUpdateValue(TValue value)
        {
            if (!TryAddValue(value))
            {
                UpdateValue(value);
            }
        }
        #endregion AddValue
        #region RemoveValue
        public abstract bool TryRemoveValue(TKey id);
        public void RemoveValue(TKey id)
        {
            TryRemoveValue(id);
        }
        #endregion RemoveValue
        public abstract void ClearCache();
    }
}