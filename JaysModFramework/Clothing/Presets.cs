using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;

namespace JaysModFramework.Clothing
{
    public class Presets
    {
        private static readonly string DIRECTORY = "./scripts/JMF/Presets/Clothing/";
        private static readonly string MALEACCESSORIES = "MaleAccessories.xml";
        private static readonly string MALEHANDS = "MaleHands.xml";
        private static readonly string MALELOWERS = "MaleLowers.xml";
        private static readonly string MALEMASKS = "MaleMasks.xml";
        private static readonly string MALENECKS = "MaleNecks.xml";
        private static readonly string MALEPARACHUTES = "MaleParachutes.xml";
        private static readonly string MALESHIRTOVERLAYS = "MaleShirtOverlays.xml";
        private static readonly string MALESHOES = "MaleShoes.xml";
        private static readonly string MALEVESTS = "MaleVests.xml";
        private static readonly string MALEHATS = "MaleHats.xml";
        private static readonly string MALEGLASSES = "MaleGlasses.xml";
        private static readonly string MALEEARS = "MaleEars.xml";
        private static readonly string MALEWATCHES = "MaleWatches.xml";
        private static readonly string MALEOUTFITS = "MaleOutfits.xml";

        public JMFDatabase<int, Accessory> MaleAccessories { get; }
        public JMFDatabase<int, Hands> MaleHands { get; }
        public JMFDatabase<int, Lower> MaleLowers { get; }
        public JMFDatabase<int, Mask> MaleMasks { get; }
        public JMFDatabase<int, Neck> MaleNecks { get; }
        public JMFDatabase<int, Parachute> MaleParachutes { get; }
        public JMFDatabase<int, ShirtOverlay> MaleShirtOverlays { get; }
        public JMFDatabase<int, Shoes> MaleShoes { get; }
        public JMFDatabase<int, Vest> MaleVests { get; }
        public JMFDatabase<int, Hat> MaleHats { get; }
        public JMFDatabase<int, Glasses> MaleGlasses { get; }
        public JMFDatabase<int, Ears> MaleEars { get; }
        public JMFDatabase<int, Watch> MaleWatches { get; }
        public JMFDatabase<string, Outfit> MaleOutfits { get; }
        public Presets()
        {
            MaleAccessories = new JMFDatabase<int, Accessory>(DIRECTORY, MALEACCESSORIES);
            MaleHands = new JMFDatabase<int, Hands>(DIRECTORY, MALEHANDS);
            MaleLowers = new JMFDatabase<int, Lower>(DIRECTORY, MALELOWERS);
            MaleMasks = new JMFDatabase<int, Mask>(DIRECTORY, MALEMASKS);
            MaleNecks = new JMFDatabase<int, Neck>(DIRECTORY, MALENECKS);
            MaleParachutes = new JMFDatabase<int, Parachute>(DIRECTORY, MALEPARACHUTES);
            MaleShirtOverlays = new JMFDatabase<int, ShirtOverlay>(DIRECTORY, MALESHIRTOVERLAYS);
            MaleShoes = new JMFDatabase<int, Shoes>(DIRECTORY, MALESHOES);
            MaleVests = new JMFDatabase<int, Vest>(DIRECTORY, MALEVESTS);
            MaleHats = new JMFDatabase<int, Hat>(DIRECTORY, MALEHATS);
            MaleGlasses = new JMFDatabase<int, Glasses>(DIRECTORY, MALEGLASSES);
            MaleEars = new JMFDatabase<int, Ears>(DIRECTORY, MALEEARS);
            MaleWatches = new JMFDatabase<int, Watch>(DIRECTORY, MALEWATCHES);
            MaleOutfits = new JMFDatabase<string, Outfit>(DIRECTORY, MALEOUTFITS);
        }
        public void Reload()
        {
            MaleAccessories.Clear();
            MaleAccessories.LoadFromFile(DIRECTORY, MALEACCESSORIES);
            MaleHands.Clear();
            MaleHands.LoadFromFile(DIRECTORY, MALEHANDS);
            MaleLowers.Clear();
            MaleLowers.LoadFromFile(DIRECTORY, MALELOWERS);
            MaleMasks.Clear();
            MaleMasks.LoadFromFile(DIRECTORY, MALEMASKS);
            MaleNecks.Clear();
            MaleNecks.LoadFromFile(DIRECTORY, MALENECKS);
            MaleParachutes.Clear();
            MaleParachutes.LoadFromFile(DIRECTORY, MALEPARACHUTES);
            MaleShirtOverlays.Clear();
            MaleShirtOverlays.LoadFromFile(DIRECTORY, MALESHIRTOVERLAYS);
            MaleShoes.Clear();
            MaleShoes.LoadFromFile(DIRECTORY, MALESHOES);
            MaleVests.Clear();
            MaleVests.LoadFromFile(DIRECTORY, MALEVESTS);
            MaleHats.Clear();
            MaleHats.LoadFromFile(DIRECTORY, MALEHATS);
            MaleGlasses.Clear();
            MaleGlasses.LoadFromFile(DIRECTORY, MALEGLASSES);
            MaleEars.Clear();
            MaleEars.LoadFromFile(DIRECTORY, MALEEARS);
            MaleWatches.Clear();
            MaleWatches.LoadFromFile(DIRECTORY, MALEWATCHES);
            MaleOutfits.Clear();
            MaleOutfits.LoadFromFile(DIRECTORY, MALEOUTFITS);
        }
    }
}
