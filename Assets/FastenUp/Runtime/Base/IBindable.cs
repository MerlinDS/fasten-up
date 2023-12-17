using FastenUp.Runtime.Delegates;

namespace FastenUp.Runtime.Base
{
    public interface IBindable
    {
        public event OnValueChanged OnValueChanged;
    }
    
    public interface IBindable<T> : IBindable
    {
        void SetValue(T value);
        T GetValue();
    }
}