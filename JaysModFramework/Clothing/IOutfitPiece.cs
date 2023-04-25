using GTA;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Persistence;
using System.Xml.Serialization;

namespace JaysModFramework.Clothing
{
    internal interface IOutfitPiece: IJMFDatabaseItem<string>
    {
        void SetToNPC(NPC npc);
        string Name { get; }
    }
}
