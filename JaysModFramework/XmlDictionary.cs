using System.Collections.Generic;
using System.Xml;

namespace JaysModFramework
{
    internal class JMFDictionary<TKey, TVal>: Dictionary<TKey, TVal>
    {
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
    }
}
