﻿using Rage;
using OOD.Collections;
using JMF.Utilities;

namespace JMF.Clothing
{
    internal interface IOutfitPiece: IXMLDatabaseItem<string>
    {
        void SetToPed(Ped ped);
        string Name { get; set; }
        PedHash Model { get; set; }
    }
}
