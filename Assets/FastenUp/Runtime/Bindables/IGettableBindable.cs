using FastenUp.Runtime.Delegates;

namespace FastenUp.Runtime.Bindables
{
    /// <remarks>Provides two-way binding</remarks>
    /// <typeparam name="T">Type of the value that will be set or get by the bindable component</typeparam>
    /// <inheritdoc cref="IBindable"/>
    public interface IGettableBindable<T> : IBindable<T>
    {
        public event OnBindableChanged OnBindableChanged;
        T GetValue();
    }
}