using JaysModFramework.Menus;
using System;
using System.Windows.Forms;

namespace JaysModFramework.Modules
{
    using Menu = Menus.Menu;
    public class InteractionMenuModule : Module
    {
        internal override int MajorVersion => 1;
        internal override int MinorVersion => 0;
        internal override int PatchVersion => 0;
        public override string ModuleName { get; } = "Interaction Menu for JMF";
        public override string ModuleDescription { get; } = "Creates an Interaction Menu. WARNING: Disabling this module may prevent correct use of other modules.";
        public override bool DefaultActivationState { get { return Global.Config.InteractionMenuModuleEnabled; } }
        private ObjectPool _pool = new ObjectPool();
        private Menu _menu;
        public InteractionMenuModule() : base()
        {
            _menu = new Menu("Interactions", _pool);
            _menu.Add(ModuleManager.ModuleMenu(_pool));
            _menu.Add(Debug.Menu(_pool));
        }

        public void OnKeyDown(object sender, KeyEventArgs e) { }
        public override void OnTick() 
        {
            _pool.Process();
        }
        public override void OnControlHeld(GTA.Control control)
        {
            if (control == GTA.Control.CharacterWheel)
            {
                _menu.Open();
            }
        }
    }
}
