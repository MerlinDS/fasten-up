using System;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Actions;

namespace FastenUp.Runtime.Utils
{
    public static class BindUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NameEquals(ReadOnlySpan<char> name, IBinder binder) => 
            binder is not null && name.SequenceEqual(binder.Name);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryBind<T>(IBindable<T> bindable, IBinder binder)
        {
            if (binder is IBinder<T> binderT)
                bindable.Bind(binderT);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindable<T> bindable, IBinder binder)
        {
            if (binder is IBinder<T> binderT)
                bindable.Unbind(binderT);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryBind<T>(IBindableEvent<T> bindableEvent, IBinder eventBinder)
        {
            if (eventBinder is IEventBinder<T> eventBinderT)
                bindableEvent.Bind(eventBinderT);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindableEvent<T> bindableEvent, IBinder eventBinder)
        {
            if (eventBinder is IEventBinder<T> eventBinderT)
                bindableEvent.Unbind(eventBinderT);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryBind<T>(IBindableAction<T> bindableAction, IBinder actionBinder)
            where T : UnityEngine.Events.UnityEventBase, new()
        {
            if (actionBinder is IActionBinder<T> actionBinderT)
                bindableAction.Bind(actionBinderT);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindableAction<T> bindableAction, IBinder actionBinder)
            where T : UnityEngine.Events.UnityEventBase, new()
        {
            if (actionBinder is IActionBinder<T> actionBinderT)
                bindableAction.Unbind(actionBinderT);
        }
    }
}