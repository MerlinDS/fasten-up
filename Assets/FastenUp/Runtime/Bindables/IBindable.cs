using FastenUp.Runtime.Delegates;

namespace FastenUp.Runtime.Bindables
{
    public interface IBindable
    {
        public event OnBindableChanged OnBindableChanged;
        string Name { get; }
    }
    
    public interface IBindable<T> : IBindable
    {
        void SetValue(T value);
        T GetValue();
    }
}