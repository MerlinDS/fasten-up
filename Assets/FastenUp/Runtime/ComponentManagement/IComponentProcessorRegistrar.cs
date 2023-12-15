using System;

namespace FastenUp.Runtime.ComponentManagement
{
    public interface IComponentProcessorRegistrar 
    {
        void Register<TValue>(Func<ComponentManager.Processor<TValue>, ComponentManager.Processor<TValue>> setup);
        void UnregisterAll();
    }
}