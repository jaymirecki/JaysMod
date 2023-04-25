using OOD.Collections;

namespace JaysModFramework.Clothing.Components
{
    internal enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    internal enum OutfitComponents { Mask = 1, Hair = 2, Arms = 3, Lower = 4, Parachute = 5, Shoes = 6, Accessory = 7, Undershirt = 8, Vest = 9, Badge = 10, Jacket = 11 };
    internal enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    public enum SkinTones
    {
        Default,
    }
    public interface IBaseOutfitPiece : IXMLDatabaseItem<int>
    {
        string Name { get; set; }
        int Index { get; set; }
        string[] Colors { get; set; }
        int CurrentColor { get; set; }
    }
}
