using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Mediators
{
    /// <summary>
    /// Internal mediator interface for internal use only.
    /// Source code generator will generate implementation for this interface.
    /// </summary>
    public interface IInternalMediator
    {
        void Bind(IBinder binder);
        void Unbind(IBinder binder);
    }
}