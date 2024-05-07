using JaysModFramework.Utilities;
using Rage;

namespace JaysModFramework.Clothing.Components
{
    public struct Mask: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent MaskComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Mask(string id, string name, PedHash model, OutfitComponent maskComponent)
        {
            Name = name;
            Model = model;
            MaskComponent = maskComponent;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetComponent(ped, MaskComponent, OutfitComponents.Mask);
        }
    }
}
