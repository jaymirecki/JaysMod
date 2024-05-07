using JaysModFramework.Menus;
using System;
using System.Windows.Forms;

[assembly: Rage.Attributes.Plugin("Interaction Menu for JMF", Description = "Creates an Interaction Menu")]
namespace JaysModFramework.Modules
{
    using Menu = Menus.Menu;
    public class InteractionMenu : InternalModule
    {
        internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
        public override string ModuleName { get; } = "Interaction Menu for JMF";
        public override string ModuleDescription { get; } = "Creates an Interaction Menu. WARNING: Disabling this module may prevent correct use of other modules.";
        public override bool DefaultActivationState { get { return Global.Config.InteractionMenuModuleEnabled; } }
        private ObjectPool _pool = new ObjectPool();
        private Menu _menu;
        public static void Main()
        {
            InteractionMenu instance = new InteractionMenu();
            instance.Tick();
        }
        public InteractionMenu() : base()
        {
            _menu = new Menu("Interactions", _pool);
            _menu.Add(ModuleManager.ModuleMenu(_pool));
            _menu.Add(Debug.Menu(_pool));
        }

        public override void OnControlReleased(Utilities.Control control) {
            if (control == Utilities.Control.SelectCharacterMultiplayer)
            {
                _menu.Open();
            }
        }
        public override void OnTick() 
        {
            _pool.Process();
        }
        public override void OnControlHeld(Utilities.Control control)
        {
            if (control == Utilities.Control.CharacterWheel)
            {
                _menu.Open();
            }
        }
    }
}
