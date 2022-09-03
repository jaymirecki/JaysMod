using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaysModFramework.Clothing
{
    public struct Lower
    {
        internal int Pants;
        internal int Shoes;
        internal Dictionary<string, LowerColor> Colors;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Lower other = Lowers.GetLower(value);
                Pants = other.Pants;
                Shoes = other.Shoes;
                Colors = other.Colors;
            }
        }
        private string _name;
        public Lower(int pants, int shoes, Dictionary<string, LowerColor> colors, string name)
        {
            Pants = pants;
            Shoes = shoes;
            Colors = colors;
            _name = name;
        }
        private LowerColor Color(string colorName)
        {
            if (Colors.TryGetValue(colorName, out LowerColor color))
            {
                return color;
            }
            return new LowerColor(0, 0);
        }
        internal int PantsColor(string colorName)
        {
            return Color(colorName).PantsColor;
        }
        internal int ShoesColor(string colorName)
        {
            return Color(colorName).ShoesColor;
        }
    }
    public struct LowerColor
    {
        internal int PantsColor;
        internal int ShoesColor;

        public LowerColor(int pantsColor, int shoesColor)
        {
            PantsColor = pantsColor;
            ShoesColor = shoesColor;
        }
    }
    public static class Lowers
    {
        #region Colors
        private static Dictionary<string, LowerColor> CombatColors = new Dictionary<string, LowerColor>()
        {
            { "Blue Digital Camo", new LowerColor(0, 0) },
            { "Brown Digital Camo", new LowerColor(1, 0) },
            { "Green Digital Camo", new LowerColor(2, 0) },
            { "Tan Digital Camo", new LowerColor(3, 0) },
            { "Tan", new LowerColor(4, 0) },
            { "Tan Camo", new LowerColor(5, 0) },
            { "Dark Green Digital Camo", new LowerColor(8, 0) },
            { "Green", new LowerColor(18, 0) },
            { "Black", new LowerColor(20, 0) },
            { "Gray", new LowerColor(21, 0) },
            { "White", new LowerColor(22, 0) },
            { "Brown", new LowerColor(23, 0) },
            { "Forest Green", new LowerColor(24, 0) },
            { "Blue", new LowerColor(25, 0) },
        };
        #endregion Colors
        public static Lower Combat =
            new Lower(87, 35, CombatColors, "Combat");
        private static Dictionary<string, Lower> LowerCollection = new Dictionary<string, Lower>()
        {
            { Combat.Name, Combat },
        };
        public static Lower GetLower(string name)
        {
            if (LowerCollection.TryGetValue(name, out Lower lower))
            {
                return lower;
            }
            return Combat;
        }
    }
}
