using System;

namespace JMF.Modules
{
    public interface IModuleSettings
    {
        bool Enabled { get; set; }
    }
    public class ModuleSettings: IModuleSettings
    {
        public bool Enabled { get; set; } = false;
        public ModuleSettings() { }
        public ModuleSettings(bool enabled)
        {
            Enabled = enabled;
        }
    }
}
