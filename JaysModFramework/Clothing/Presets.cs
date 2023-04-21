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
            MaleOutfits = new JMFDatabase<string, Outfit>(DIRECTORY, MALEOUTFITS);
        }
    }
}
