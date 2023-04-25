using GTA;

namespace JaysModFramework.Clothing.Components
{
    public struct Parachute: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name; } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent ParachuteComponent;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Parachute(string id, string name, PedHash model, OutfitComponent parachuteComponent)
        {
            Name = name;
            Model = model;
            ParachuteComponent = parachuteComponent;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            SetToPed(ped, 0);
        }
        public void SetToPed(Ped ped, Torso torso)
        {
            SetToPed(ped, torso.ParachuteIndex);
        }
        public void SetToPed(Ped ped, int torsoParachuteIndex)
        {
            OutfitComponent temp = ParachuteComponent;
            temp.Index = ParachuteComponent.Index + torsoParachuteIndex;
            Components.SetComponent(ped, temp, OutfitComponents.Parachute);
        }
    }
}
