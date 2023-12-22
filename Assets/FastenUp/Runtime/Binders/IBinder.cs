using FastenUp.Runtime.Base;
using FastenUp.Runtime.Mediators;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Implementations of this interface will be bound to an <see cref="IBindable{T}"/>.
    /// It provides communication between the unity components and bindable property in a <see cref="IMediator"/>.
    /// </summary>
    public interface IBinder
    {
        string Name { get; }
    }

    /// <remarks>Provides one-way binding</remarks>
    /// <typeparam name="T">Type of the value that will be set by the binder component</typeparam>
    /// <inheritdoc cref="IBinder"/>
    public interface IBinder<in T> : IBinder
    {
        void SetValue(T value);
    }
}