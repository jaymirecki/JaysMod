using GTA;
using GTA.Native;
using JaysModFramework;
using JaysModFramework.Clothing;
using JaysModFramework.Managers;
using LemonUI;
using System;
using System.Windows.Forms;

namespace JaysMod
{
    using Menu = JaysModFramework.Menus.Menu;
    //[ScriptAttributes(NoDefaultInstance = true)]
    public partial class JaysMod : Script
    {
        private ObjectPool _pool;
        //private NPC PlayerNPC;
        //private State State;
        private BigMapManager _mapManager = new BigMapManager();

        public JaysMod()
        {
            _pool = new ObjectPool();
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }
        void OnTick(object sender, EventArgs e)
        {
            _pool.Process();
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
            else if (e.KeyCode == Keys.F5)
            {
                Debug.Menu(_pool).Visible = true;
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
