﻿using GTA;
using OOD.Collections;

namespace JaysModFramework.Clothing
{
    internal interface IOutfitPiece: IXMLDatabaseItem<string>
    {
        void SetToPed(Ped ped);
        string Name { get; set; }
        PedHash Model { get; set; }
    }
}
