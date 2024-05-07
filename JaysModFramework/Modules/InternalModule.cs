namespace JMF
{
    public abstract class InternalModule : Module
    {
        public InternalModule(): base()
        {
            ModuleManager.AddInternalModule(this);
        }
    }
}
