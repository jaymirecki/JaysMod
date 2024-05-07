using Rage;
using JaysModFramework.Menus;
using System;

namespace JaysModFramework
{
    public abstract class InternalModule : Module
    {
        public InternalModule(): base()
        {
            ModuleManager.AddInternalModule(this);
        }
    }
}
