using JaysModFramework.Clothing.Components;
using OOD.Collections;
using System.IO;

namespace JaysModFramework
{
    public class Database
    {
        private const string _baseDirectory = ".\\scripts\\JMF\\";
        private const string _presetsDirectory = _baseDirectory + "Presets\\";
        private const string _clothingDirectory = _presetsDirectory + "Clothing\\";

        public XMLDatabaseTable<string, Torso> Torsos = new MemoryXMLDatabaseTable<string, Torso>(_clothingDirectory, "torso", true);
        public XMLDatabaseTable<string, Legs> Legs = new MemoryXMLDatabaseTable<string, Legs>(_clothingDirectory, "legs", true);
        //public MemoryXMLDatabaseTable<string, Torso> Hair = new MemoryXMLDatabaseTable<string, Torso>(_clothingDirectory, "hair", true);

        public Database() { }
        public void ClearCache()
        {
            Debug.Notify(_clothingDirectory, true);
            Debug.Notify(Torsos.Filepath, true);
            Debug.Notify(Directory.GetCurrentDirectory(), true);
            Torsos.ClearCache();
            Legs.ClearCache();
            //Hair.ClearCache();
        }
    }
}
