using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Utils
{
    public static class BindUtilities
    {
        public static void TryBind<T>(IInternalBindable<T> bindable, IBinder binder)
        {
            if (binder is IBinder<T> bindableT)
                bindable.Add(bindableT);
        }
        
        public static void TryUnbind<T>(IInternalBindable<T> bindable, IBinder binder)
        {
            if (binder is IBinder<T> bindableT)
                bindable.Remove(bindableT);
        }
        
        public static void TryBind<T>(IInternalBindableEvent<T> bindableEvent, IBinder eventBinder)
        {
            if (eventBinder is IEventBinder<T> bindableT)
                bindableEvent.Add(bindableT);
        }
        
        public static void TryUnbind<T>(IInternalBindableEvent<T> bindableEvent, IBinder eventBinder)
        {
            if (eventBinder is IEventBinder<T> bindableT)
                bindableEvent.Remove(bindableT);
        }
    }
}