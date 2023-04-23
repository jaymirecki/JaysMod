using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;

namespace JaysModFramework.Clothing
{
    public class Presets
    {
        private static readonly string DIRECTORY = "./scripts/JMF/Presets/Clothing/";
        private static readonly string MALEACCESSORIES = "MaleAccessories.xml";
        private static readonly string MALEHAIRS = "MaleHairs.xml";
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
        public JMFDatabase<int, Hair> MaleHairs { get; }
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
            MaleHairs = new JMFDatabase<int, Hair>(DIRECTORY, MALEHAIRS);
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
        public void Clear()
        {
            MaleAccessories.Clear();
            MaleHairs.Clear();
            MaleHands.Clear();
            MaleLowers.Clear();
            MaleMasks.Clear();
            MaleNecks.Clear();
            MaleParachutes.Clear();
            MaleShirtOverlays.Clear();
            MaleShoes.Clear();
            MaleVests.Clear();
            MaleHats.Clear();
            MaleGlasses.Clear();
            MaleEars.Clear();
            MaleWatches.Clear();
            MaleOutfits.Clear();
        }
        public void Load()
        {
            MaleAccessories.LoadFromFile(DIRECTORY, MALEACCESSORIES);
            MaleHairs.LoadFromFile(DIRECTORY, MALEHAIRS);
            MaleHands.LoadFromFile(DIRECTORY, MALEHANDS);
            MaleLowers.LoadFromFile(DIRECTORY, MALELOWERS);
            MaleMasks.LoadFromFile(DIRECTORY, MALEMASKS);
            MaleNecks.LoadFromFile(DIRECTORY, MALENECKS);
            MaleParachutes.LoadFromFile(DIRECTORY, MALEPARACHUTES);
            MaleShirtOverlays.LoadFromFile(DIRECTORY, MALESHIRTOVERLAYS);
            MaleShoes.LoadFromFile(DIRECTORY, MALESHOES);
            MaleVests.LoadFromFile(DIRECTORY, MALEVESTS);
            MaleHats.LoadFromFile(DIRECTORY, MALEHATS);
            MaleGlasses.LoadFromFile(DIRECTORY, MALEGLASSES);
            MaleEars.LoadFromFile(DIRECTORY, MALEEARS);
            MaleWatches.LoadFromFile(DIRECTORY, MALEWATCHES);
            MaleOutfits.LoadFromFile(DIRECTORY, MALEOUTFITS);
        }
        public void Reload()
        {
            Clear();
            Load();
        }
    }
}
