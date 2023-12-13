using FastenUp.Runtime.Bindings;
using FastenUp.Runtime.Proxies;

namespace FastenUp.Runtime.Extensions
{
    public static class InternalMediatorExtensions
    {
        public static void UpdateProxy<T>(this ref DataProxy<T> proxy, string name, IBindingPoint bindingPoint)
        {
            if(bindingPoint.Name != name)//TODO: add proper check
                return;
            
            if(!bindingPoint.CanBind<T>())
                return;
            
            proxy.Bind(bindingPoint.As<T>());
        }
    }
}