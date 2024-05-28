using System.Collections;
using System.Collections.Generic;

namespace OOD.Collections
{
    public struct ValidationState
    {
        public bool IsValid;
        public string ErrorMessage;
        public ValidationState(bool isValid = true, string errorMessage = "")
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }
    public interface IXMLDatabaseItem<TKey>
    {
        TKey ID { get; }
        ValidationState Validate();
    }
    public abstract class XMLDatabaseTable: IEnumerable
    {
        public string TableName { get; internal set; }
        public string Directory { get; internal set; }
        public abstract bool ReadOnly { get; }
        public abstract int Count { get; }
        public abstract IEnumerator GetEnumerator();
        public abstract void ClearCache();
        public abstract List<ValidationState> Validate(bool throwException = false);
    }
    public abstract class XMLDatabaseTable<TKey, TValue> : XMLDatabaseTable where TValue: IXMLDatabaseItem<TKey>
    {
        #region Properties
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
    }
}
