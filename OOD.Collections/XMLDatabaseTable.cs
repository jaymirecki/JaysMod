using System.Collections;

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
    public abstract class XMLDatabaseTable<TKey, TValue> : IEnumerable where TValue: IXMLDatabaseItem<TKey>
    {
        #region Properties
        public string TableName { get; internal set; }
        public string Directory { get; internal set; }
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
        public abstract IEnumerator GetEnumerator();
        public abstract void ClearCache();
    }
}
