using System;
using System.Runtime.CompilerServices;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Actions;
using FastenUp.Runtime.Binders.Collections;
using FastenUp.Runtime.Binders.Events;
using FastenUp.Runtime.Binders.References;

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
        public static void TryBind<T>(IBindableEvent<T> bindableEvent, IBinder binder)
        {
            if (binder is IEventBinder<T> eventBinder)
                bindableEvent.Bind(eventBinder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindableEvent<T> bindableEvent, IBinder binder)
        {
            if (binder is IEventBinder<T> eventBinder)
                bindableEvent.Unbind(eventBinder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryBind<T>(IBindableAction<T> bindableAction, IBinder binder)
            where T : UnityEngine.Events.UnityEventBase, new()
        {
            if (binder is IActionBinder<T> actionBinderT)
                bindableAction.Bind(actionBinderT);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindableAction<T> bindableAction, IBinder binder)
            where T : UnityEngine.Events.UnityEventBase, new()
        {
            if (binder is IActionBinder<T> actionBinder)
                bindableAction.Unbind(actionBinder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryBind<T>(IBindableRef<T> bindableRef, IBinder binder)
        {
            if (binder is IRefBinder refBinder && refBinder.TryGetReference(out T value))
                bindableRef.Bind(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindableRef<T> bindableRef, IBinder binder)
        {
            if (binder is IRefBinder refBinder && refBinder.TryGetReference(out T value))
                bindableRef.Unbind(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryBind<T>(IBindableCollection<T> bindableCollection, IBinder binder)
        {
            if (binder is ICollectionBinder<T> collectionBinder)
                bindableCollection.Bind(collectionBinder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryUnbind<T>(IBindableCollection<T> bindableCollection, IBinder binder)
        {
            if (binder is ICollectionBinder<T> collectionBinder)
                bindableCollection.Unbind(collectionBinder);
        }
    }
}