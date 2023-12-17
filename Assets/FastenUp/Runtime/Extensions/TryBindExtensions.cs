using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Extensions
{
    public static class TryBindExtensions
    {
        internal static void TryBind<T>(this IInternalBindPoint<T> bindPoint,
            ReadOnlySpan<char> name, IBindable bindable)
        {
            if (name.SequenceEqual(bindable?.Name) && bindable is IBindable<T> bindableT)
                bindPoint.Add(bindableT);
        }

        internal static void TryUnbind<T>(this IInternalBindPoint<T> bindPoint,
            ReadOnlySpan<char> name, IBindable bindable)
        {
            if (name.SequenceEqual(bindable?.Name) && bindable is IBindable<T> bindableT)
                bindPoint.Remove(bindableT);
        }
    }
}