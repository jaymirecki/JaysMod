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

        public XMLDatabaseTable<string, Outfit> Outfits;
        public XMLDatabaseTable<string, Mask> Masks;
        public XMLDatabaseTable<string, Torso> Torsos;
        public XMLDatabaseTable<string, Legs> Legs;
        public XMLDatabaseTable<string, Accessory> Accessory;
        public XMLDatabaseTable<string, Parachute> Parachute;
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
            Masks = new MemoryXMLDatabaseTable<string, Mask>(_clothingDirectory, "mask", true);
            Torsos = new MemoryXMLDatabaseTable<string, Torso>(_clothingDirectory, "torso", true);
            Legs = new MemoryXMLDatabaseTable<string, Legs>(_clothingDirectory, "legs", true);
            Accessory = new MemoryXMLDatabaseTable<string, Accessory>(_clothingDirectory, "accessory", true);
            Parachute = new MemoryXMLDatabaseTable<string, Parachute>(_clothingDirectory, "parachute", true);
            //public MemoryXMLDatabaseTable<string, Torso> Hair = new MemoryXMLDatabaseTable<string, Torso>(_clothingDirectory, "hair", true);

            // Outfit table needs to load after components, since it is dependent on their IDs
            Outfits = new MemoryXMLDatabaseTable<string, Outfit>(_clothingDirectory, "outfit", true);
            _initialized = true;
        }
    }
}
