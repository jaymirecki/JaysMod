using GTA;

namespace JaysModFramework.Clothing.Components
{
    public struct Accessory: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name; } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent AccessoryComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Accessory(string id, string name, PedHash model, OutfitComponent accessoryComponent)
        {
            Name = name;
            Model = model;
            AccessoryComponent = accessoryComponent;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetComponent(ped, AccessoryComponent, OutfitComponents.Mask);
        }
    }
}
