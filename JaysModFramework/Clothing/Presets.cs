using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;

namespace JaysModFramework.Clothing
{
    public class Presets
    {
        private static readonly string DIRECTORY = "./scripts/JMF/Presets/Clothing";
        
        public JMFDatabase<Mask> MaleMasks { get; }
        public Presets()
        {
            MaleMasks = new JMFDatabase<Mask>(DIRECTORY + "/MaleMasks/");
        }
    }
}
