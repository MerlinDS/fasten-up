namespace FastenUp.Runtime.Base
{
    public interface IInternalMediator
    {
        // public void UpdateProxies(IBindingPoint bindingPoint);
        void Bind(IBindable bindable);
        void Unbind(IBindable bindable);
    }
}