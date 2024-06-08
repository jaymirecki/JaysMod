using JMF.Clothing;
using JMF.Clothing.Components;
using JMF.Interiors;
using JMF.Universe;
using OOD.Collections;
using System.Collections.Generic;
using System.IO;

namespace JMF
{
    public class Database
    {
        private bool _initialized = false;
        private const string _baseDirectory = ".\\scripts\\JMF\\";
        private const string _presetsDirectory = _baseDirectory + "Presets\\";
        private const string _clothingDirectory = _presetsDirectory + "Clothing\\";
        private const string _iplsDirectory = "IPLs";
        private const string _mapsDirectory = "Maps";
        private const string _worldspacesDirectory = "Worldspaces";

        #region Outfits
        public XMLDatabaseTable<string, Outfit> Outfits;
        #region Components
        public XMLDatabaseTable<string, Mask> Masks;
        public XMLDatabaseTable<string, Torso> Torsos;
        public XMLDatabaseTable<string, Legs> Legs;
        public XMLDatabaseTable<string, Accessory> Accessory;
        public XMLDatabaseTable<string, Parachute> Parachute;
        #endregion Components
        #region Props
        public XMLDatabaseTable<string, Hat> Hats;
        public XMLDatabaseTable<string, Glasses> Glasses;
        public XMLDatabaseTable<string, Ears> Ears;
        public XMLDatabaseTable<string, Wrist> Wrists;
        #endregion Props
        #endregion Outfits
        //public MemoryXMLDatabaseTable<string, Torso> Hair;
        public XMLDatabaseTable<string, IPL> IPLs;
        public XMLDatabaseTable<string, Worldspace> Worldspaces;
        public XMLDatabaseTable<string, Map> Maps;
        public MemoryXMLDatabaseTable<string, VehicleModel> VehicleModels;
        public Database()
        {
            Load();
        }
        public void Load()
        {
            Outfits = new AlwaysEmptyXMLDatabaseTable<string, Outfit>();
            Masks = new AlwaysEmptyXMLDatabaseTable<string, Mask>();
            Torsos = new AlwaysEmptyXMLDatabaseTable<string, Torso>();
            Legs = new AlwaysEmptyXMLDatabaseTable<string, Legs>();
            Accessory = new AlwaysEmptyXMLDatabaseTable<string, Accessory>();
            Parachute = new AlwaysEmptyXMLDatabaseTable<string, Parachute>();
            Hats = new AlwaysEmptyXMLDatabaseTable<string, Hat>();
            Glasses = new AlwaysEmptyXMLDatabaseTable<string, Glasses>();
            Ears = new AlwaysEmptyXMLDatabaseTable<string, Ears>();
            Wrists = new AlwaysEmptyXMLDatabaseTable<string, Wrist>();

            Debug.Log(DebugSeverity.Info, "Loading IPLs data");
            IPLs = new MemoryXMLDatabaseTable<string, IPL>(Framework.Config.DataPath, _iplsDirectory);
            Debug.Log(DebugSeverity.Info, "Done");
            Debug.Log(DebugSeverity.Info, "Loading Maps data");
            Maps = new MemoryXMLDatabaseTable<string, Map>(Framework.Config.DataPath, _mapsDirectory);
            Debug.Log(DebugSeverity.Info, "Done");
            Debug.Log(DebugSeverity.Info, "Loading Worldspaces data");
            Worldspaces = new MemoryXMLDatabaseTable<string, Worldspace>(Framework.Config.DataPath, _worldspacesDirectory);
            Debug.Log(DebugSeverity.Info, "Done");
            VehicleModels = new MemoryXMLDatabaseTable<string, VehicleModel>(Framework.Config.DataPath, "VehicleModels");
            //VehicleModels.Save();
        }
        public bool Validate()
        {
            bool isValid = true;
            isValid = ValidateDatabase(IPLs) && isValid;
            isValid = ValidateDatabase(Maps) && isValid;
            isValid = ValidateDatabase(Worldspaces) && isValid;
            return isValid;
        }
        private bool ValidateDatabase(XMLDatabaseTable database)
        {
            List<ValidationState> states = database.Validate();
            foreach (ValidationState s in states)
            {
                if (!s.IsValid)
                {
                    Debug.Log(DebugSeverity.Error, s.ErrorMessage);
                    return false;
                }
            }
            return true;
        }
        public void ClearCache()
        {
            InitializeIfNot();
        }
        public void InitializeIfNot()
        {
            if (!_initialized)
            {
                Initialize();
            }
        }
        public void Initialize()
        {
            InitOutfits();
            //public MemoryXMLDatabaseTable<string, Torso> Hair = new MemoryXMLDatabaseTable<string, Torso>(_clothingDirectory, "hair", true);

            _initialized = true;
        }
        private void InitComponents()
        {
            Masks = new MemoryXMLDatabaseTable<string, Mask>(_clothingDirectory, "mask", true);
            Torsos = new MemoryXMLDatabaseTable<string, Torso>(_clothingDirectory, "torso", true);
            Legs = new MemoryXMLDatabaseTable<string, Legs>(_clothingDirectory, "legs", true);
            Accessory = new MemoryXMLDatabaseTable<string, Accessory>(_clothingDirectory, "accessory", true);
            Parachute = new MemoryXMLDatabaseTable<string, Parachute>(_clothingDirectory, "parachute", true);
        }
        private void InitProps()
        {
            Hats = new MemoryXMLDatabaseTable<string, Hat>(_clothingDirectory, "hat", true);
            Glasses = new MemoryXMLDatabaseTable<string, Glasses>(_clothingDirectory, "glasses", true);
            Ears = new MemoryXMLDatabaseTable<string, Ears>(_clothingDirectory, "ears", true);
            Wrists = new MemoryXMLDatabaseTable<string, Wrist>(_clothingDirectory, "wrist", true);
        }
        private void InitOutfits()
        {
            InitComponents();
            InitProps();

            // Outfit table needs to load after components, since it is dependent on their IDs
            Outfits = new MemoryXMLDatabaseTable<string, Outfit>(_clothingDirectory, "outfit", true);
            Debug.Log(DebugSeverity.Info, "Outfits database loaded with " + Outfits.Count + " items");
        }
    }
}
