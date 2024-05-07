using JaysModFramework.Utilities;
using Rage;

namespace JaysModFramework.Clothing.Components
{
    public struct Torso: IOutfitPiece
    {
        #region Properties
        public string ID { get { return Name + Model.ToString(); } }
        public string Name { get; set; }
        public PedHash Model { get; set; }
        #region Components
        public OutfitComponent Arms;
        public OutfitComponent Undershirt;
        public OutfitComponent Jacket;
        public OutfitComponent Badge;
        public int ParachuteIndex;
        public int Vest;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Torso(string id, string name, PedHash model, OutfitComponent arms, OutfitComponent undershirt, OutfitComponent jacket, OutfitComponent badge, int parachuteIndex, int vest)
        {
            Name = name;
            Model = model;
            Arms = arms;
            Undershirt = undershirt;
            Jacket = jacket;
            Badge = badge;
            ParachuteIndex = parachuteIndex;
            Vest = vest;
        }
        #endregion Constructors
        public void SetToPed(Ped ped)
        {
            Components.SetComponent(ped, Arms, OutfitComponents.Arms);
            Components.SetComponent(ped, Undershirt, OutfitComponents.Undershirt);
            Components.SetComponent(ped, Jacket, OutfitComponents.Jacket);
            Components.SetComponent(ped, Badge, OutfitComponents.Badge);
        }
    }
}
