using GTA;
using JaysModFramework.Persistence;

namespace JaysModFramework.Clothing
{
    internal interface IOutfitPiece: IJMFDatabaseItem<string>
    {
        void SetToNPC(NPC npc);
        string Name { get; }
        Gender Sex { get; }
    }
}
