using JaysModFramework.Clothing.Components;
using OOD.Collections;

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

        public XMLDatabaseTable<int, Accessory> MaleAccessories { get; }
        public XMLDatabaseTable<int, Hair> MaleHairs { get; }
        public XMLDatabaseTable<int, Hands> MaleHands { get; }
        public XMLDatabaseTable<int, Lower> MaleLowers { get; }
        public XMLDatabaseTable<int, Mask> MaleMasks { get; }
        public XMLDatabaseTable<int, Neck> MaleNecks { get; }
        public XMLDatabaseTable<int, Parachute> MaleParachutes { get; }
        public XMLDatabaseTable<int, ShirtOverlay> MaleShirtOverlays { get; }
        public XMLDatabaseTable<int, Shoes> MaleShoes { get; }
        public XMLDatabaseTable<int, Vest> MaleVests { get; }
        public XMLDatabaseTable<int, Hat> MaleHats { get; }
        public XMLDatabaseTable<int, Glasses> MaleGlasses { get; }
        public XMLDatabaseTable<int, Ears> MaleEars { get; }
        public XMLDatabaseTable<int, Watch> MaleWatches { get; }
        public XMLDatabaseTable<string, Outfit> MaleOutfits { get; }
        public Presets()
        {
            MaleAccessories = new MemoryXMLDatabaseTable<int, Accessory>(DIRECTORY, MALEACCESSORIES);
            MaleHairs = new MemoryXMLDatabaseTable<int, Hair>(DIRECTORY, MALEHAIRS);
            MaleHands = new MemoryXMLDatabaseTable<int, Hands>(DIRECTORY, MALEHANDS);
            MaleLowers = new MemoryXMLDatabaseTable<int, Lower>(DIRECTORY, MALELOWERS);
            MaleMasks = new MemoryXMLDatabaseTable<int, Mask>(DIRECTORY, MALEMASKS);
            MaleNecks = new MemoryXMLDatabaseTable<int, Neck>(DIRECTORY, MALENECKS);
            MaleParachutes = new MemoryXMLDatabaseTable<int, Parachute>(DIRECTORY, MALEPARACHUTES);
            MaleShirtOverlays = new MemoryXMLDatabaseTable<int, ShirtOverlay>(DIRECTORY, MALESHIRTOVERLAYS);
            MaleShoes = new MemoryXMLDatabaseTable<int, Shoes>(DIRECTORY, MALESHOES);
            MaleVests = new MemoryXMLDatabaseTable<int, Vest>(DIRECTORY, MALEVESTS);
            MaleHats = new MemoryXMLDatabaseTable<int, Hat>(DIRECTORY, MALEHATS);
            MaleGlasses = new MemoryXMLDatabaseTable<int, Glasses>(DIRECTORY, MALEGLASSES);
            MaleEars = new MemoryXMLDatabaseTable<int, Ears>(DIRECTORY, MALEEARS);
            MaleWatches = new MemoryXMLDatabaseTable<int, Watch>(DIRECTORY, MALEWATCHES);
            MaleOutfits = new MemoryXMLDatabaseTable<string, Outfit>(DIRECTORY, MALEOUTFITS);
        }
        public void ClearCache()
        {
            MaleAccessories.ClearCache();
            MaleHairs.ClearCache();
            MaleHands.ClearCache();
            MaleLowers.ClearCache();
            MaleMasks.ClearCache();
            MaleNecks.ClearCache();
            MaleParachutes.ClearCache();
            MaleShirtOverlays.ClearCache();
            MaleShoes.ClearCache();
            MaleVests.ClearCache();
            MaleHats.ClearCache();
            MaleGlasses.ClearCache();
            MaleEars.ClearCache();
            MaleWatches.ClearCache();
            MaleOutfits.ClearCache();
        }
        public void Reload()
        {
            ClearCache();
        }
    }
}
