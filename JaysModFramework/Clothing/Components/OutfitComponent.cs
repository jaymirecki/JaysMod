using System;

namespace JMF.Clothing
{

    public struct OutfitComponent: IEquatable<OutfitComponent>
    {
        #region Properties
        public int Index { get; set; }
        private string[] _colors;
        public string[] Colors { 
            get 
            { 
                if (_colors != null) return _colors; 
                else return new string[] { "Default" };
            }
            set { _colors = value; }
        }
        public int CurrentColor { get; set; }
        public bool HasColor { get; set; }
        #endregion Properties
        #region Constructors
        public OutfitComponent(int index, string[] colors, int currentColor, bool hasColor)
        {
            Index = index;
            _colors = colors;
            CurrentColor = currentColor;
            HasColor = hasColor;
        }
        //public OutfitComponent(string json)
        //{
        //    OutfitComponent temp = JsonSerializer.Deserialize<OutfitComponent>(json);
        //    Index = temp.Index;
        //    _colors = temp.Colors;
        //    CurrentColor = temp.CurrentColor;
        //    HasColor = temp.HasColor;
        //}
        #endregion Constructors
        //public override string ToString()
        //{
        //    return ToJSON();
        //}
        //public string ToJSON()
        //{
        //    return JsonSerializer.Serialize(this);
        //}
        #region IEquatable
        public override bool Equals(object obj)
        {
            if ((obj == null) || (!this.GetType().Equals(obj.GetType()))) 
            { 
                return false;
            }
            else
            {
                OutfitComponent component = (OutfitComponent)obj;
                return Equals(component);
            }
        }
        public bool Equals(OutfitComponent component)
        {
            return
                Index == component.Index &&
                CurrentColor == component.CurrentColor &&
                HasColor == component.HasColor;
        }
        public static bool operator==(OutfitComponent componentA, OutfitComponent componentB)
        {
            return componentA.Equals(componentB);
        }
        public static bool operator!=(OutfitComponent componentA, OutfitComponent componentB)
        {
            return !componentA.Equals(componentB);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion IEquatable
    }
}
