using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JaysModFramework
{
    internal interface IXmlSerializable
    {
        void WriteXml(XmlWriter writer);
        void ReadXml(XmlReader reader);
    }
    internal static class XmlSerialization
    {
        #region Write
        public static void WriteElement<T>(XmlWriter writer, string elementName, T element)
        {
            writer.WriteStartElement(elementName);
            try
            {
                writer.WriteValue(element);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }
        public static void WriteEnumElement<T>(XmlWriter writer, string elementName, T element)
        {
            WriteElement(writer, elementName, element.ToString());
        }
        public static void WriteComplexElement<T>(XmlWriter writer, string elementName, T element) where T: IXmlSerializable
        {
            writer.WriteStartElement(elementName);
            try
            {
                element.WriteXml(writer);
            }
            finally
            {
                writer.WriteEndElement();
            }
        }
        #endregion
        #region Read
        public static T ReadElement<T>(XmlReader reader, string elementName)
        {
            T content;
            reader.ReadStartElement(elementName);
            try
            {
                content = (T)reader.ReadContentAs(typeof(T), null);
            }
            catch
            {
                content = default(T);
            }
            finally
            {
                reader.ReadEndElement();
            }
            return content;
        }
        public static T ReadEnumElement<T>(XmlReader reader, string elementName)
        {
            return (T)Enum.Parse(typeof(T), ReadElement<string>(reader, elementName), true);
        }
        public static T ReadComplexElement<T>(XmlReader reader, string elementName) where T : IXmlSerializable, new()
        {
            T content = new T();
            reader.ReadStartElement(elementName);
            try
            {
                content.ReadXml(reader);
            }
            finally
            {
                reader.ReadEndElement();
            }
            return content;
        }
        #endregion
    }
}
