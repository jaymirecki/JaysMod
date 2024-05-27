using JMF.Interiors;
using JMF.Menus;
using JMF.Native;
using JMF.Universe;
using OOD.Collections;

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
            public XMLDatabaseTable<string, Worldspace> Worldspaces { get; set; }
            public override void OnActivate()
            {
                Worldspaces = new MemoryXMLDatabaseTable<string, Worldspace>(Global.Config.DataPath, "Worldspaces");
                Worldspaces.GetValue("SanAndreas").Load();
            }
            public override void OnDeactivate()
            {
                Worldspaces.GetValue("SanAndreas").Unload();
            }
        }
    }
}
