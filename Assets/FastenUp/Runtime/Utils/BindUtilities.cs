using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Utils
{
    public static class BindUtilities
    {
        public static void TryBind<T>(IInternalBindPoint<T> bindPoint,
            ReadOnlySpan<char> name, IBindable bindable)
        {
            if (name.SequenceEqual(bindable?.Name) && bindable is IBindable<T> bindableT)
                bindPoint.Add(bindableT);
        }

        public static void TryUnbind<T>(IInternalBindPoint<T> bindPoint,
            ReadOnlySpan<char> name, IBindable bindable)
        {
            if (name.SequenceEqual(bindable?.Name) && bindable is IBindable<T> bindableT)
                bindPoint.Remove(bindableT);
        }
    }
}