using System;
using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Base
{
    public interface IBindPoint<T>
    {
        T Value { get; set; }
        event Action<T> OnValueChanged;
    }

    internal interface IInternalBindPoint<T>
    {
        internal void Add(IBindable<T> bindable);
        internal void Remove(IBindable<T> bindable);
    }
}