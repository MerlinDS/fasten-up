using FastenUp.Runtime.Base;
using FastenUp.Runtime.Delegates;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// Implementations of this interface will be bound to an <see cref="IBindPoint{T}"/>.
    /// It provides communication between the unity components and bind points in a <see cref="IMediator"/>.
    /// </summary>
    public interface IBindable
    {
        string Name { get; }
    }

    /// <remarks>Provides one-way binding</remarks>
    /// <typeparam name="T">Type of the value that will be set by the bindable component</typeparam>
    /// <inheritdoc cref="IBindable"/>
    public interface IBindable<in T> : IBindable
    {
        void SetValue(T value);
    }

    /// <remarks>Provides two-way binding</remarks>
    /// <typeparam name="T">Type of the value that will be set or get by the bindable component</typeparam>
    /// <inheritdoc cref="IBindable"/>
    public interface IGettableBindable<T> : IBindable<T>
    {
        public event OnBindableChanged OnBindableChanged;
        T GetValue();
    }
}