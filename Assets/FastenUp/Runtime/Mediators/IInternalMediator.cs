using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Mediators
{
    /// <summary>
    /// Internal mediator interface for internal use only.
    /// Source code generator will generate implementation for this interface.
    /// </summary>
    public interface IInternalMediator
    {
        void Bind(IBindable bindable);
        void Unbind(IBindable bindable);
    }
}