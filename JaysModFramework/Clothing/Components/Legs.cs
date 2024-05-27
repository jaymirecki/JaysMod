using JMF.Utilities;
using Rage;

namespace JMF.Clothing.Components
{
    public struct Legs: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent Lower;
        public OutfitComponent Shoes;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Legs(string id, string name, PedHash model, OutfitComponent lower, OutfitComponent shoes)
        {
            Name = name;
            Model = model;
            Lower = lower;
            Shoes = shoes;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetComponent(ped, Lower, OutfitComponents.Lower);
            Components.SetComponent(ped, Shoes, OutfitComponents.Shoes);
        }
        public bool Validate()
        {
            return true;
        }
    }
}
