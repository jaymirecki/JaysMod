using JaysModFramework.Utilities;
using Rage;

namespace JaysModFramework.Clothing.Components
{
    public struct Hat: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        public string VisorID { get; set; }
        public bool NightVision { get; set; }
        #region Components
        public OutfitComponent HatComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Hat(string id, string name, PedHash model, OutfitComponent hatComponent, string visorID = "", bool nightVision = false)
        {
            Name = name;
            Model = model;
            HatComponent = hatComponent;
            VisorID = visorID;
            NightVision = nightVision;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetProp(ped, HatComponent, Props.Hat);
        }
    }
}
