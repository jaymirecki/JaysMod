using JMF.Interiors;
using JMF.Menus;
using JMF.Native;
using JMF.Universe;
using OOD.Collections;
using System;
using System.Collections.Generic;

namespace JMF
{
    namespace Modules
    {
        public class WorldspaceLoader: InternalModule<ModuleSettings>
        {
            internal override SemanticVersion Version { get; } = new SemanticVersion(1, 0, 0);
            public override string ModuleName { get; } = "Worldspace Loader";
            public override string ModuleDescription { get; } = "Loads worldspaces";
            public override ModuleSettings Settings { get { return new ModuleSettings(); } }
            public override void OnActivate()
            {
                Framework.Database.Worldspaces.GetValue("MCUDC").LoadWorldspace("MCUDC");
                Game.Player.Character.Position = new Vector3(2133, 4791, 41);
                Vehicle vehicle = new Vehicle();
                vehicle.Position = new Vector3(2133, 4820, 42);
                vehicle.Model = new Model("nimbus");
                vehicle.Heading = 90;
                vehicle.Spawn();
            }
            public override void OnDeactivate()
            {
                Framework.Database.Worldspaces.GetValue("MCUDC").Unload();
            }
            public override void OnTick()
            {
                Framework.Database.Worldspaces.GetValue("MCUDC").ManagePortals();
            }
        }
    }
}
