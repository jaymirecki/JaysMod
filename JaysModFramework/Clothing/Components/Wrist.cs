using JaysModFramework.Utilities;
using Rage;

namespace JaysModFramework.Clothing.Components
{
    public struct Wrist : IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent WristComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Wrist(string id, string name, PedHash model, OutfitComponent wristComponent)
        {
            Name = name;
            Model = model;
            WristComponent = wristComponent;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetProp(ped, WristComponent, Props.Wrist);
        }
    }
}
