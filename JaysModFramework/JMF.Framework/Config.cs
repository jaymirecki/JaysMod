using JMF.Modules;

namespace JMF
{
    public class Config

    {
        public ModuleSettings InteractionMenuSettings { get; set; } = new ModuleSettings();
        public ModuleSettings BigMapSettings { get; set; } = new ModuleSettings();
        public ModuleSettings SilentSirensSettings { get; set; } = new ModuleSettings();
        public ModuleSettings RespawnerSettings { get; set; } = new ModuleSettings();
        public ModuleSettings FreemodePlayerSettings { get; set; } = new ModuleSettings();
        public ModuleSettings MatterOfTimeSettings { get; set; } = new ModuleSettings();
        public IPLLoaderSettings IPLLoaderSettings { get; set; } = new IPLLoaderSettings();
        public string DataPath { get; set; } = ".\\JMF\\Data";
    }
}
