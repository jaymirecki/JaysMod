using JaysModFramework.Clothing;
using JaysModFramework.Clothing.Components;
using OOD.Collections;
using System.IO;

namespace JaysModFramework
{
    public class Database
    {
        private bool _initialized = false;
        private const string _baseDirectory = ".\\scripts\\JMF\\";
        private const string _presetsDirectory = _baseDirectory + "Presets\\";
        private const string _clothingDirectory = _presetsDirectory + "Clothing\\";

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
        public Database() { }
        public void ClearCache()
        {
            InitIfNot();
            Torsos.ClearCache();
            Legs.ClearCache();
            //Hair.ClearCache();
        }
        public void InitIfNot()
        {
            if (!_initialized)
            {
                Init();
            }
        }
        public void Init()
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
        }
    }
}
