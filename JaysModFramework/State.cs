using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace JaysModFramework
{
    public class State
    {
        #region Properties
        public DateTime Date
        {
            get { return World.CurrentDate; }
            set { World.CurrentDate = value; }
        }
        public Weather Weather
        {
            get { return World.Weather; }
            set { World.Weather = value; }
        }
        private XmlDictionary<string, Vehicle> Vehicles { get { return Vehicle.SpawnedVehicles; } }
        private XmlDictionary<string, NPC> NPCs { get { return NPC.SpawnedNPCs; } }
        #endregion
        #region Constructors
        public State()
        {
        }
        #endregion
        #region XMLSerialization
        public void SerializeToXml(TextWriter stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineHandling = NewLineHandling.Entitize;
            XmlWriter writer = XmlWriter.Create(stream, settings);
            writer.WriteStartElement("State");
            WriteXml(writer);
            writer.WriteEndElement();
            writer.Close();
        }
        public void WriteXml(XmlWriter writer)
        {
            XmlSerialization.WriteElement(writer, "Date", Date);
            XmlSerialization.WriteElement(writer, "Weather", Weather.ToString());
            XmlSerialization.WriteComplexElement(writer, "Vehicles", Vehicles);
            XmlSerialization.WriteComplexElement(writer, "NPCs", NPCs);
        }
        #endregion
        #region XMLDeserialization
        public void DeserializeFromXML(TextReader stream)
        {
            NPC.ClearPlayerNPC();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader reader = XmlReader.Create(stream, settings);
            reader.ReadStartElement("State");
            ReadXml(reader);
            reader.ReadEndElement();
            reader.Close();
        }
        public void ReadXml(XmlReader reader)
        {
            Date = XmlSerialization.ReadElement<DateTime>(reader, "Date");
            Weather = XmlSerialization.ReadEnumElement<Weather>(reader, "Weather");
            XmlSerialization.ReadComplexElement<XmlDictionary<string, Vehicle>>(reader, "Vehicles");
            XmlSerialization.ReadComplexElement<XmlDictionary<string, NPC>>(reader, "NPCs");
        }
        #endregion
    }
}
