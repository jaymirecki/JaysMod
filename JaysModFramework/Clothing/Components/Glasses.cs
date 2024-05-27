using JMF.Utilities;
using OOD.Collections;
using Rage;

namespace JMF.Clothing.Components
{
    public struct Glasses: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent GlassesComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Glasses(string id, string name, PedHash model, OutfitComponent glassesComponent)
        {
            Name = name;
            Model = model;
            GlassesComponent = glassesComponent;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetProp(ped, GlassesComponent, Props.Glasses);
        }
        public ValidationState Validate()
        {
            return new ValidationState();
        }
    }
}
