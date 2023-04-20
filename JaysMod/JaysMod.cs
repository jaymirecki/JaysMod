using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Windows.Forms;
using JaysModFramework.Clothing;
using JaysModFramework.Clothing.Components;
using JaysModFramework;

namespace JaysMod
{

    //[ScriptAttributes(NoDefaultInstance = true)]
    public partial class JaysMod : Script
    {
        private MenuPool ModMenuPool;
        private UIMenu planeMenu;
        private UIMenu ClosetMenu;
        private string SaveId;

        //private NPC PlayerNPC;
        //private State State;

        public JaysMod()
        {
            ModMenuPool = new MenuPool();
            ModMenuPool.ResetCursorOnOpen = true;

            Tick += OnTick;
            KeyDown += OnKeyDown;
        }
        void OnTick(object sender, EventArgs e)
        {

        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G)
            {
                ReplacePlayerPed(PedHash.FreemodeMale01);
                Outfit outfit = new Outfit(MaleMasks.Combat, MaleMasks.Combat.Colors[0]);
                Debug.Log("Add outfit", true);
                Debug.Log(outfit.Mask, true);
                outfit.ToPed(Game.Player.Character);
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
