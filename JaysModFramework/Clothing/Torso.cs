using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    public struct Torso: IOutfitPiece
    {
        #region Properties
        public string ID { get; }
        public string Name { get; }
        private OutfitComponent _arms;
        #endregion Properties
        #region Constructors
        //public Torso() { }
        #endregion Constructors
        public void SetToNPC(NPC npc)
        {

        }
    }
}
