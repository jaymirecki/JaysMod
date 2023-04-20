using System.Collections.Generic;

namespace JaysModFramework.Clothing.Components
{
    internal enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    internal enum OutfitComponents { Mask = 1, Hair = 2, Hands = 3, Lower = 4, Parachute = 5, Shoes = 6, Neck = 7, Accessory = 8, Vest = 9, ShirtOverlay = 11 };
    internal enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    public enum SkinTones
    {
        Default,
    }
    public class OutfitColor
    {
        public readonly string Name;
        public readonly int ID;
        public OutfitColor(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
    public class BaseOutfitPiece
    {
        // The name of the outfit piece
        public readonly string Name = "Default Name";
        protected static readonly string defaultName = "Default Name";
        // The ID of the outfit piece (0 indexed for components)
        public readonly int ID = 0;
        protected static readonly int defaultID = 0;
        // The available colors (0 indexed for components)
        public readonly OutfitColor[] Colors = new OutfitColor[] { new OutfitColor("Default Color", 0) };
        protected static readonly OutfitColor[] defaultColors = new OutfitColor[] { new OutfitColor("Default Color", 0) };
        // The currently selected color (must be in the range of Colors)
        private int _currentColor = 0;
        protected static readonly int defaultCurrentColor = 0;
        public int CurrentColor
        {
            get
            {
                return _currentColor;
            }
            set
            {
                if (value < Colors.Length || value == 0)
                {
                    _currentColor = value;
                }
            }
        }
        public BaseOutfitPiece() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public BaseOutfitPiece(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public BaseOutfitPiece(string name, int id, OutfitColor[] colors, int currentColor)
        {
            Name = name;
            ID = id;
            Colors = colors;
            CurrentColor = currentColor;
        }
        public BaseOutfitPiece Copy()
        {
            return new BaseOutfitPiece(Name, ID, Colors, CurrentColor);
        }
    }
    public class BaseComponent : BaseOutfitPiece
    {
        internal OutfitComponents ComponentSlot = OutfitComponents.Mask;
        public BaseComponent() : base() { }
        public BaseComponent(string name, int id, OutfitColor[] colors) : base(name, id, colors) { }
        public BaseComponent(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor) { }
        public override string ToString()
        {
            return "{ " + Name + "; " + ID.ToString() + "; " + ComponentSlot + "; }";
        }
    }
    public class BaseProp : BaseOutfitPiece
    {
        internal Props ComponentSlot = Props.Hat;
        public BaseProp() : base() { }
        public BaseProp(string name, int id, OutfitColor[] colors) : base(name, id, colors) { }
        public BaseProp(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor) { }
        public override string ToString()
        {
            return "{ " + Name + "; " + ID.ToString() + "; " + ComponentSlot + "; }";
        }
    }
    //public class BaseSkintoneComponent : BaseComponent
    //{
    //    public readonly SkinTones SkinTone = SkinTones.Default;
    //    public new BaseSkintoneComponent Copy()
    //    {
    //        return (BaseSkintoneComponent)base.Copy();
    //    }
    //}
    // Corresponds with Beard slot
    public class Mask : BaseComponent
    {
        public Mask() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Mask(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Mask(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Mask;
        }
    }
    // Corresponds with the Upper slot
    public class Hands : BaseComponent
    {
        public Hands() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Hands(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Hands(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Hands;
        }
    }
    public class Lower : BaseComponent
    {
        public Lower() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Lower(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Lower(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Lower;
        }
    }
    // Corresponds with Hands slot
    public class Parachute : BaseComponent
    {
        public Parachute() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Parachute(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Parachute(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Parachute;
        }
    }
    public class Shoes : BaseComponent
    {
        public Shoes() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Shoes(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Shoes(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Shoes;
        }
    }
    // Corresponds with Teeth slot
    public class Neck : BaseComponent
    {
        public Neck() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Neck(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Neck(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Neck;
        }
    }
    public class Accessory : BaseComponent
    {
        public Accessory() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Accessory(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Accessory(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Accessory;
        }
    }
    // Corresponds with Accessory2 slot
    public class Vest : BaseComponent
    {
        public Vest() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Vest(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Vest(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Vest;
        }
    }
    public class ShirtOverlay : BaseComponent
    {
        public ShirtOverlay() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public ShirtOverlay(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public ShirtOverlay(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.ShirtOverlay;
        }
    }
    public class Hat : BaseProp
    {
        public Hat() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Hat(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Hat(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Hat;
        }
    }
    public class Glasses : BaseProp
    {
        public Glasses() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Glasses(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Glasses(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Glasses;
        }
    }
    // Corresponds with Misc slot
    public class Ears : BaseProp
    {
        public Ears() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Ears(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Ears(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Ears;
        }
    }
    public class Watch : BaseProp
    {
        public Watch() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Watch(string name, int id, OutfitColor[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Watch(string name, int id, OutfitColor[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Watch;
        }
    }
}
