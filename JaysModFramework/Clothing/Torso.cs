using GTA;
using JaysModFramework.Clothing.Components;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public struct Torso: IOutfitPiece
    {
        #region Properties
        public string ID { get; }
        public string Name { get; }
        public PedHash Model { get; }
        #region Components
        private OutfitComponent _arms;
        private string _armsString
        {
            get { return _arms.ToJSON(); }
            set { _arms = new OutfitComponent(value); }
        }
        private OutfitComponent _undershirt;
        private string _undershirtString
        {
            get { return _undershirt.ToJSON(); }
            set { _undershirt = new OutfitComponent(value); }
        }
        private OutfitComponent _jacket;
        private string _jacketString
        {
            get { return _jacket.ToJSON(); }
            set { _jacket = new OutfitComponent(value); }
        }
        private OutfitComponent _badge;
        private string _badgeString
        {
            get { return _badge.ToJSON(); }
            set { _badge = new OutfitComponent(value); }
        }
        private int _parachuteIndex;
        private int _vest;
        #endregion Components
        #endregion Properties
        #region Constructors
        public Torso(string id, string name, PedHash model, OutfitComponent arms, OutfitComponent undershirt, OutfitComponent jacket, OutfitComponent badge, int parachuteIndex, int vest)
        {
            ID = id;
            Name = name;
            Model = model;
            _arms = arms;
            _undershirt = undershirt;
            _jacket = jacket;
            _badge = badge;
            _parachuteIndex = parachuteIndex;
            _vest = vest;
        }
        #endregion Constructors
        public void SetToNPC(NPC npc)
        {

        }
    }
}
