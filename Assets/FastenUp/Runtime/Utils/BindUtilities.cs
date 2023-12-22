using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;

namespace FastenUp.Runtime.Utils
{
    public static class BindUtilities
    {
        public static void TryBind<T>(IInternalBindable<T> bindable, IBinder binder)
        {
            if (binder is IBinder<T> bindableT)
                bindable.Bind(bindableT);
        }
        
        public static void TryUnbind<T>(IInternalBindable<T> bindable, IBinder binder)
        {
            if (binder is IBinder<T> bindableT)
                bindable.Unbind(bindableT);
        }
        
        public static void TryBind<T>(IInternalBindableEvent<T> bindableEvent, IBinder eventBinder)
        {
            if (eventBinder is IEventBinder<T> bindableT)
                bindableEvent.Bind(bindableT);
        }
        
        public static void TryUnbind<T>(IInternalBindableEvent<T> bindableEvent, IBinder eventBinder)
        {
            if (eventBinder is IEventBinder<T> bindableT)
                bindableEvent.Unbind(bindableT);
        }
    }
}