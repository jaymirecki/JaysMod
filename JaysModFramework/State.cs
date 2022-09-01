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
        private JMFDictionary<string, Vehicle> Vehicles { get { return Vehicle.SpawnedVehicles; } }
        private JMFDictionary<string, NPC> NPCs { get { return NPC.SpawnedNPCs; } }
        private readonly XmlSerializer StateSerializer = new XmlSerializer(typeof(State));
        private readonly XmlSerializer NPCSerializer = new XmlSerializer(typeof(NPC));
        private readonly XmlSerializer VehicleSerializer = new XmlSerializer(typeof(Vehicle));
        private const string SourceDirectory = "./scripts/JMF/Saves/";
        #endregion
        #region Constructors
        public State()
        {
        }
        #endregion
        #region Save
        public void Save(string saveId)
        {
            string saveDirectory = CreateSaveDirectory(saveId);
            string stateFile = saveDirectory + "state.xml";
            TextWriter writer = new StreamWriter(stateFile);
            StateSerializer.Serialize(writer, this);
            writer.Close();
            SaveNPCs(saveDirectory);
            SaveVehicles(saveDirectory);
        }
        private static string CreateSaveDirectory(string saveId)
        {
            EnsureDirectory("./scripts");
            EnsureDirectory("./scripts/JMF");
            EnsureDirectory("./scripts/JMF/Saves");

            string saveDirectory = SourceDirectory + saveId + "/";
            EnsureDirectory(saveDirectory);
            EnsureDirectory(saveDirectory + "NPCs");
            EnsureDirectory(saveDirectory + "Vehicles");

            return saveDirectory;
        }
        private static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private void SaveNPCs(string saveDirectory)
        {
            string npcDirectory = saveDirectory + "NPCs/";
            foreach (NPC npc in NPCs.Values)
            {
                string npcFile = npcDirectory + npc.ID + ".xml";
                TextWriter writer = new StreamWriter(npcFile);
                NPCSerializer.Serialize(writer, npc);
                writer.Close();
            }
        }
        private void SaveVehicles(string saveDirectory)
        {
            string vehicleDirectory = saveDirectory + "Vehicles/";
            Debug.Log(vehicleDirectory);
            foreach (Vehicle vehicle in Vehicles.Values)
            {
                Debug.Log(vehicle.ID);
                string vehicleFile = vehicleDirectory + vehicle.ID + ".xml";
                TextWriter writer = new StreamWriter(vehicleFile);
                VehicleSerializer.Serialize(writer, vehicle);
                writer.Close();
            }
        }
        #endregion
        #region Load
        public bool Load(string saveId)
        {
            string saveDirectory = SourceDirectory + saveId + "/";
            string stateFile = saveDirectory + "state.xml";
            if (!ValidateDirectories(saveDirectory))
            {
                return false;
            }
            TextReader reader = new StreamReader(stateFile);
            State other = (State)StateSerializer.Deserialize(reader);
            Date = other.Date;
            Weather = other.Weather;
            reader.Close();
            LoadNPCs(saveDirectory);
            //LoadVehicles(saveDirectory);
            return true;
        }
        public bool ValidateDirectories(string saveDirectory)
        {
            if (!Directory.Exists(saveDirectory))
            {
                return false;
            }
            if (!Directory.Exists(saveDirectory + "NPCs"))
            {
                return false;
            }
            if (!Directory.Exists(saveDirectory + "Vehicles"))
            {
                return false;
            }
            return true;
        }
        private void LoadNPCs(string saveDirectory)
        {
            NPC.DeleteAllNPCs();
            Debug.Log(NPCs.Count, true);
            string npcDirectory = saveDirectory + "NPCs/";
            foreach (string npcFile in Directory.GetFiles(npcDirectory))
            {
                TextReader reader = new StreamReader(npcFile);
                NPC npc = (NPC)NPCSerializer.Deserialize(reader);
                reader.Close();
            }
        }
        private void LoadVehicles(string saveDirectory)
        {
            string vehicleDirectory = saveDirectory + "Vehicles/";
            foreach (string vehicleFile in Directory.GetFiles(vehicleDirectory))
            {
                TextReader reader = new StreamReader(vehicleFile);
                Vehicle vehicle = (Vehicle)NPCSerializer.Deserialize(reader);
                reader.Close();
            }
        }
        public List<object> FindSaves()
        {
            List<string> stringList = new List<string>(Directory.EnumerateDirectories(SourceDirectory));
            List<object> objectList;
            objectList = stringList.ConvertAll(new Converter<string, object>(StringToObject));
            return objectList;
        }
        private static object StringToObject(string s)
        {
            return (object)s;
        }
        #endregion
    }
}
