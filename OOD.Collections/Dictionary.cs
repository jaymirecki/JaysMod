namespace OOD.Collections
{
    public class Dictionary<TKey, TVal>: System.Collections.Generic.Dictionary<TKey, TVal>
    {
        public TVal[] ValueArray
        {
            get
            {
                TVal[] values = new TVal[Values.Count];
                Values.CopyTo(values, 0);
                return values;
            }
        }
        public bool TryAdd(TKey key, TVal value)
        {
            try
            {
                Add(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool TryRemove(TKey key)
        {
            try
            {
                Remove(key);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void AddOrUpdateValue(TKey key, TVal value)
        {
            TryRemove(key);
            Add(key, value);
        }
        public TVal GetValue(TKey key)
        {
            if (TryGetValue(key, out TVal value))
            {
                return value;
            }
            else
            {
                return default(TVal);
            }
        }
        public new TVal this[TKey key]
        {
            get { return GetValue(key); }
            set { AddOrUpdateValue(key, value); }
        }
    }
}
