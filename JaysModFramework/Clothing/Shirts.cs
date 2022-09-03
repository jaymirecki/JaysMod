using System.Collections.Generic;

namespace JaysModFramework.Clothing
{
    public struct Shirt
    {
        internal int Torso;
        internal int Undershirt;
        internal int ShirtOverlay;
        internal Dictionary<string, ShirtColor> Colors;
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                Shirt other = Shirts.GetShirt(value);
                Torso = other.Torso;
                Undershirt = other.Undershirt;
                ShirtOverlay = other.ShirtOverlay;
                Colors = other.Colors;
            }
        }
        private string _name;
        public Shirt(int torso, int undershirt, int shirt, Dictionary<string,ShirtColor> colors, string name)
        {
            Torso = torso;
            Undershirt = undershirt;
            ShirtOverlay = shirt;
            Colors = colors;
            _name = name;
        }
        private ShirtColor Color(string colorName)
        {
            if (Colors.TryGetValue(colorName, out ShirtColor color))
            {
                return color;
            }
            return new ShirtColor(0);
        }
        internal int TorsoColor(string colorName)
        {
            return Color(colorName).TorsoColor;
        }
        internal int UndershirtColor(string colorName)
        {
            return Color(colorName).Undershirtcolor;
        }
        internal int ShirtOverlayColor(string colorName)
        {
            return Color(colorName).ShirtOverlayColor;
        }
    }
    public struct ShirtColor
    {
        internal int TorsoColor;
        internal int Undershirtcolor;
        internal int ShirtOverlayColor;

        public ShirtColor(int torsoColor, int undershirtColor, int shirtOverlayColor)
        {
            TorsoColor = torsoColor;
            Undershirtcolor = undershirtColor;
            ShirtOverlayColor = shirtOverlayColor;
        }
        public ShirtColor(int color): this(color, color, color) { }
    }
    public static class Shirts
    {
        #region Colors
        private static Dictionary<string, ShirtColor> CombatColors = new Dictionary<string, ShirtColor>()
        {
            { "Blue Digital Camo", new ShirtColor(0) },
            { "Brown Digital Camo", new ShirtColor(1) },
            { "Green Digital Camo", new ShirtColor(2) },
            { "Tan Digital Camo", new ShirtColor(3) },
            { "Tan", new ShirtColor(4) },
            { "Tan Camo", new ShirtColor(5) },
            { "Dark Green Digital Camo", new ShirtColor(8) },
            { "Green", new ShirtColor(18) },
            { "Black", new ShirtColor(18, 20, 20) },
            { "Gray", new ShirtColor(18, 21, 21) },
            { "White", new ShirtColor(18, 22, 22) },
            { "Brown", new ShirtColor(18, 23, 23) },
            { "Forest Green", new ShirtColor(18, 24, 24) },
            { "Blue", new ShirtColor(18, 25, 25) },
        };
        #endregion Colors
        #region Shirts
        public static Shirt Combat =
            new Shirt(11, 15, 221, CombatColors, "Combat");
        public static Shirt CombatRolledSleeves =
            new Shirt(11, 15, 222, CombatColors, "Combat, Rolled Sleeves");
        public static Shirt CombatZipper =
            new Shirt(11, 15, 220, CombatColors, "Combat, Zipper");
        #endregion Shirts
        private static Dictionary<string, Shirt> ShirtCollection = new Dictionary<string, Shirt>()
        {
            { Combat.Name, Combat },
            { CombatRolledSleeves.Name, CombatRolledSleeves },
            { CombatZipper.Name, CombatZipper },
        };
        public static Shirt GetShirt(string name)
        {
            if (ShirtCollection.TryGetValue(name, out Shirt shirt))
            {
                return shirt;
            }
            return Combat;
        }
    }
}
