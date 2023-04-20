using System.Collections.Generic;

namespace JaysModFramework.Clothing.Components
{
    public enum SkinTones
    {

    }
    public struct OutfitColor
    {
        public readonly string Name;
        public readonly int ID;
        public OutfitColor(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
    // Corresponds with Beard slot
    public struct Mask
    {
        public readonly string Name;
        public readonly int ID;
        public readonly OutfitColor[] Colors;
        public Mask(string name, int id, OutfitColor[] colors)
        {
            Name = name;
            ID = id;
            Colors = colors;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public struct Upper
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
        public readonly SkinTones SkinTone;
    }
    public struct Lower
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    // Corresponds with Hands slot
    public struct Parachute
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    public struct Shoes
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
        public readonly SkinTones SkinTone;
    }
    // Corresponds with Teeth slot
    public struct Neck
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    public struct Accessory
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    // 
    // Corresponds with Accessory2 slot
    public struct Vest
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    public struct ShirtOverlay
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    public struct Hat
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    public struct Glasses
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    // Corresponds with Misc slot
    public struct Ears
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
    public struct Watch
    {
        public readonly string Name;
        public readonly int ID;
        public readonly List<OutfitColor> Colors;
    }
}
