using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Utils
{
    public static class BindUtilities
    {
        public static void TryBind<T>(IInternalBindable<T> bindable,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IBinder<T> bindableT)
                bindable.Add(bindableT);
        }

        public static void TryUnbind<T>(IInternalBindable<T> bindable,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IBinder<T> bindableT)
                bindable.Remove(bindableT);
        }
        
        public static void TryBind<T>(IInternalBindableEvent<T> bindableEvent,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IEventBinder<T> bindableT)
                bindableEvent.Add(bindableT);
        }
        
        public static void TryUnbind<T>(IInternalBindableEvent<T> bindableEvent,
            ReadOnlySpan<char> name, IBinder binder)
        {
            if (name.SequenceEqual(binder?.Name) && binder is IEventBinder<T> bindableT)
                bindableEvent.Remove(bindableT);
        }
    }
}