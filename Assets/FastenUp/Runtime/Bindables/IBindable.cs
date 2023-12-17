using FastenUp.Runtime.Delegates;

namespace FastenUp.Runtime.Bindables
{
    public interface IBindable
    {
        string Name { get; }
    }

    public interface IBindable<in T> : IBindable
    {
        void SetValue(T value);
    }

    public interface IGettableBindable<T> : IBindable<T>
    {
        public event OnBindableChanged OnBindableChanged;
        T GetValue();
    }
}