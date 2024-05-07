using JaysModFramework.Utilities;
using Rage;

namespace JaysModFramework.Clothing.Components
{
    public struct Ears : IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent EarsComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Ears(string id, string name, PedHash model, OutfitComponent earsComponent)
        {
            Name = name;
            Model = model;
            EarsComponent = earsComponent;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetProp(ped, EarsComponent, Props.Ears);
        }
    }
}
