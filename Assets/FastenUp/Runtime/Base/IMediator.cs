using FastenUp.Runtime.Bindings;

namespace FastenUp.Runtime.Base
{
    public interface IMediator
    {
        
    }

    public interface IInternalMediator
    {
        public void UpdateProxies(IBindingPoint bindingPoint);
    }
}