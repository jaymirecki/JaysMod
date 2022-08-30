using System.Collections.Generic;
using System.Xml;

namespace JaysModFramework
{
    internal class XmlDictionary<TKey, TVal>: Dictionary<TKey, TVal>, IXmlSerializable where TVal: IXmlSerializable, new()
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
        #region Write
        public void WriteXml(XmlWriter writer)
        {
            foreach(KeyValuePair<TKey, TVal> kvp in this)
            {
                writer.WriteStartElement("KVP");
                WritePair(writer, kvp);
                writer.WriteEndElement();
            }
        }
        private void WritePair(XmlWriter writer, KeyValuePair<TKey, TVal> kvp)
        {
            XmlSerialization.WriteElement(writer, "Key", kvp.Key);
            XmlSerialization.WriteComplexElement(writer, "Value", kvp.Value);
        }
        #endregion
        #region Read
        public void ReadXml(XmlReader reader)
        {
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("KVP");
                ReadPair(reader);
                reader.ReadEndElement();
            }
        }
        private void ReadPair(XmlReader reader)
        {
            TKey key = XmlSerialization.ReadElement<TKey>(reader, "Key");
            TVal val = XmlSerialization.ReadComplexElement<TVal>(reader, "Value");
            this.Add(key, val);
        }
        #endregion
    }
}
