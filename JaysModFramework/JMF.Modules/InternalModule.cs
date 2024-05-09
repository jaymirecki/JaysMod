using JMF.Modules;

namespace JMF
{
    public abstract class InternalModule : Module
    {
        public InternalModule(): base()
        {
            ModuleManager.AddInternalModule(this);
        }
    }
    public abstract class InternalModule<T> : InternalModule where T : IModuleSettings
    {
        public virtual T Settings { get; private set; }
        public InternalModule(): base()
        {
            if (Settings.Enabled)
            {
                Activate();
            }
        }
    }
}
