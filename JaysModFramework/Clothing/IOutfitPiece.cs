using GTA;
using OOD.Collections;

namespace JaysModFramework.Clothing
{
    internal interface IOutfitPiece: IXMLDatabaseItem<string>
    {
        void SetToNPC(NPC npc);
        string Name { get; }
        PedHash Model { get; }
    }
}
