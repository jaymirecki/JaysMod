using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace OOD.Collections
{
    public class AlwaysEmptyXMLDatabaseTable<TKey, TValue>: XMLDatabaseTable<TKey, TValue> where TValue : IXMLDatabaseItem<TKey>
    {
        #region Properties
        private Dictionary<TKey, TValue> _values;
        public override int Count { get { return _values.Count; } }
        public override bool ReadOnly { get; }
        #endregion
        #region Constructors
        public AlwaysEmptyXMLDatabaseTable() {
            _values = new Dictionary<TKey, TValue>();
        }
        #endregion Constructors
        #region GetValue
        public override bool TryGetValue(TKey ID, out TValue value)
        {
            value = default(TValue);
            return false;
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
            return false;
        }
        public override bool TryUpdateValue(TValue value)
        {
            return false;
        }
        #endregion AddValue
        #region RemoveValue
        public override bool TryRemoveValue(TKey id)
        {
            return false;
        }
        #endregion RemoveValue
        #region IEnumerable
        public override IEnumerator GetEnumerator()
        {
            return _values.Values.GetEnumerator();
        }
        #endregion IEnumerable
        public override void ClearCache() { }
        public override List<ValidationState> Validate(bool throwException = false)
        {
            return new List<ValidationState>() { new ValidationState() };
        }
    }
}
