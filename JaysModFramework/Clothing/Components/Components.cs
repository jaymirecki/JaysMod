using JaysModFramework.Persistence;
using System.Collections.Generic;

namespace JaysModFramework.Clothing.Components
{
    public struct ComponentKey
    {
        public int ID;
        public int CurrentColor;
        public ComponentKey(int id, int currentColor)
        {
            ID = id;
            CurrentColor = currentColor;
        }
        public ComponentKey FromComponent<T>(T component) where T: BaseOutfitPiece
        {
            return new ComponentKey(component.ID, component.CurrentColor);
        }
        public T ToComponent<T>(ComponentKey key, JMFDatabase<int, T> presets) where T: BaseOutfitPiece, new()
        {
            T component = presets[key.ID];
            if (component == null)
            {
                component = new T();
                component.ID = key.ID;
                component.CurrentColor = key.CurrentColor;
            }
            component.CurrentColor = key.CurrentColor;
            return component;
        }
    }
    internal enum HeadOverlays { Blemishes = 0, FacialHair = 1, Eyebrows = 2 };
    internal enum OutfitComponents { Mask = 1, Hair = 2, Hands = 3, Lower = 4, Parachute = 5, Shoes = 6, Neck = 7, Accessory = 8, Vest = 9, ShirtOverlay = 11 };
    internal enum Props { Hat = 0, Glasses = 1, Ears = 2, Watch = 3 };
    public enum SkinTones
    {
        Default,
    }
    public interface IBaseOutfitPiece : IJMFDatabaseItem<int>
    {
        string Name { get; set; }
        int Index { get; set; }
        string[] Colors { get; set; }
        int CurrentColor { get; set; }
    }
    public class BaseOutfitPiece : IJMFDatabaseItem<int>
    {
        // The name of the outfit piece
        public string Name = "Default Name";
        protected static readonly string defaultName = "Default Name";
        public int ID
        {
            get
            {
                return Index;
            }
            set
            {
                Index = value;
            }
        }
        // The ID of the outfit piece (0 indexed for components)
        public int Index = 0;
        protected static readonly int defaultID = 0;
        // The available colors (0 indexed for components)
        public string[] Colors = new string[] { "Default Color", };
        protected static readonly string[] defaultColors = new string[] { "Default Color", };
        // The currently selected color (must be in the range of Colors)
        private int _currentColor = 0;
        protected static readonly int defaultCurrentColor = 0;
        public int CurrentColor = 0;
        public BaseOutfitPiece() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public BaseOutfitPiece(int index) : this(index, defaultCurrentColor) { }
        public BaseOutfitPiece(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public BaseOutfitPiece(string name, int index, string[] colors) : this(name, index, colors, defaultCurrentColor) { }
        public BaseOutfitPiece(string name, int index, string[] colors, int currentColor)
        {
            Name = name;
            Index = index;
            Colors = colors;
            CurrentColor = currentColor;
        }
        public BaseOutfitPiece Copy()
        {
            return new BaseOutfitPiece(Name, Index, Colors, CurrentColor);
        }
    }
    public class BaseComponent : BaseOutfitPiece
    {
        internal OutfitComponents ComponentSlot = OutfitComponents.Mask;
        public BaseComponent() : base() { }
        public BaseComponent(int index) : this(index, defaultCurrentColor) { }
        public BaseComponent(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public BaseComponent(string name, int id, string[] colors) : base(name, id, colors) { }
        public BaseComponent(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor) { }
        public override string ToString()
        {
            return "{ " + Name + "; " + Index.ToString() + "; " + ComponentSlot + "; " + CurrentColor.ToString() + "; }";
        }
    }
    public class BaseProp : BaseOutfitPiece
    {
        protected static new readonly int defaultID = -1;
        internal Props ComponentSlot = Props.Hat;
        public BaseProp() : base() { }
        public BaseProp(int index) : this(index, defaultCurrentColor) { }
        public BaseProp(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public BaseProp(string name, int id, string[] colors) : base(name, id, colors) { }
        public BaseProp(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor) { }
        public override string ToString()
        {
            return "{ " + Name + "; " + Index.ToString() + "; " + ComponentSlot + "; " + CurrentColor.ToString() + "; }";
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
        public bool HideHair = false;
        public Mask() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Mask(int index) : this(index, defaultCurrentColor) { }
        public Mask(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Mask(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Mask(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Mask;
        }
    }
    // Corresponds with the Upper slot
    public class Hands : BaseComponent
    {
        public Hands() : this(defaultID) { }
        public Hands(int index) : this(index, defaultCurrentColor) { }
        public Hands(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Hands(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Hands(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Hands;
        }
    }
    public class Lower : BaseComponent
    {
        public Lower() : this(defaultID) { }
        public Lower(int index) : this(index, defaultCurrentColor) { }
        public Lower(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Lower(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Lower(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Lower;
        }
    }
    // Corresponds with Hands slot
    public class Parachute : BaseComponent
    {
        public Parachute() : this(defaultID) { }
        public Parachute(int index) : this(index, defaultCurrentColor) { }
        public Parachute(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Parachute(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Parachute(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Parachute;
        }
    }
    public class Shoes : BaseComponent
    {
        public Shoes() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Shoes(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Shoes(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Shoes(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Shoes(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Shoes;
        }
    }
    // Corresponds with Teeth slot
    public class Neck : BaseComponent
    {
        public Neck() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Neck(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Neck(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Neck(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Neck(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Neck;
        }
    }
    public class Accessory : BaseComponent
    {
        public Accessory() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Accessory(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Accessory(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Accessory(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Accessory(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Accessory;
        }
    }
    // Corresponds with Accessory2 slot
    public class Vest : BaseComponent
    {
        public Vest() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Vest(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Vest(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Vest(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Vest(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.Vest;
        }
    }
    public class ShirtOverlay : BaseComponent
    {
        public ShirtOverlay() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public ShirtOverlay(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public ShirtOverlay(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public ShirtOverlay(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public ShirtOverlay(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = OutfitComponents.ShirtOverlay;
        }
    }
    public class Hat : BaseProp
    {
        public string VisorDownIndex = "";
        public string VisorUpIndex = "";
        public bool HasNightVision = false;
        public Hat() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Hat(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Hat(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Hat(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Hat(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Hat;
        }
    }
    public class Glasses : BaseProp
    {
        public Glasses() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Glasses(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Glasses(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Glasses(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Glasses(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Glasses;
        }
    }
    // Corresponds with Misc slot
    public class Ears : BaseProp
    {
        public Ears() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Ears(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Ears(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Ears(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Ears(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Ears;
        }
    }
    public class Watch : BaseProp
    {
        public Watch() : this(defaultName, defaultID, defaultColors, defaultCurrentColor) { }
        public Watch(int index) : this(defaultName, index, defaultColors, defaultCurrentColor) { }
        public Watch(int index, int currentColor) : this(defaultName, index, defaultColors, currentColor) { }
        public Watch(string name, int id, string[] colors) : this(name, id, colors, defaultCurrentColor) { }
        public Watch(string name, int id, string[] colors, int currentColor) : base(name, id, colors, currentColor)
        {
            ComponentSlot = Props.Watch;
        }
    }
}
