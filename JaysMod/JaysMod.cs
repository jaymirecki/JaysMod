﻿using GTA;
using GTA.Native;
using JaysModFramework;
using JaysModFramework.Clothing;
using JaysModFramework.Clothing.Components;
using JaysModFramework.Managers;
using NativeUI;
using System;
using System.Windows.Forms;

namespace JaysMod
{

    //[ScriptAttributes(NoDefaultInstance = true)]
    public partial class JaysMod : Script
    {
        private MenuPool ModMenuPool;

        //private NPC PlayerNPC;
        //private State State;
        private BigMapManager _mapManager = new BigMapManager();

        public JaysMod()
        {
            ModMenuPool = new MenuPool();
            ModMenuPool.ResetCursorOnOpen = true;

            Tick += OnTick;
            KeyDown += OnKeyDown;
        }
        void OnTick(object sender, EventArgs e)
        {
            _mapManager.OnTick(sender, e);
        }
        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G)
            {
                ReplacePlayerPed(PedHash.FreemodeMale01);
                Global.Database.ClearCache();
                Outfit outfit = Global.Database.Outfits["CombatFreemodeMale01"];
                outfit.SetToPed(Game.Player.Character);
            }
            else if (e.KeyCode == Keys.H)
            {
                ReplacePlayerPed(PedHash.Franklin);
            }

        }

        private void ReplacePlayerPed(PedHash hash)
        {
            var characterModel = new Model(hash);
            characterModel.Request(500);
            if (characterModel.IsInCdImage && characterModel.IsValid)
            {
                // If the model isn't loaded, wait until it is
                while (!characterModel.IsLoaded) Yield();

                // Set the player's model
                Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);
                Function.Call(Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, Game.Player.Character.Handle);
                characterModel.MarkAsNoLongerNeeded();
            }
        }
    }
}
