using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Utils
{
    public static class BindUtilities
    {
        public static void TryBind<T>(IInternalBindPoint<T> bindPoint,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IBinder<T> bindableT)
                bindPoint.Add(bindableT);
        }

        public static void TryUnbind<T>(IInternalBindPoint<T> bindPoint,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IBinder<T> bindableT)
                bindPoint.Remove(bindableT);
        }
        
        public static void TryBind<T>(IInternalBindAction<T> bindAction,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IEventBinder<T> bindableT)
                bindAction.AddListener(bindableT);
        }
        
        public static void TryUnbind<T>(IInternalBindAction<T> bindAction,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IEventBinder<T> bindableT)
                bindAction.RemoveListener(bindableT);
        }
    }
}