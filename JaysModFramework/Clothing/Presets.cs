using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;

namespace JaysModFramework.Clothing
{
    public class Presets
    {
        private static readonly string DIRECTORY = "./scripts/JMF/Presets/Clothing/";
        
        public JMFDatabase<int, Mask> MaleMasks { get; }
        public Presets()
        {
            MaleMasks = new JMFDatabase<int, Mask>(DIRECTORY, "MaleMasks.xml");
            MaleMasks.SaveToFile(DIRECTORY, "MaleMasks.xml");
        }
    }
}
