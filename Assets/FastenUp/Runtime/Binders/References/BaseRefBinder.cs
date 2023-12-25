namespace FastenUp.Runtime.Binders.References
{
    public abstract class BaseRefBinder<TRef> : BaseBinder, IRefBinder
    {
        private TRef _reference;//TODO: Make this settable from the inspector.
        
        private void Awake()
        {
            _reference = GetComponent<TRef>();
        }

        /// <inheritdoc />
        public bool TryGetReference<T>(out T reference)
        {
            reference = default;
            if (_reference is not T @ref) 
                return false;
            
            reference = @ref;
            return true;

        }
    }
}