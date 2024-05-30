using UnityEngine;

namespace FastenUp.Runtime.Binders.References
{
    public abstract class BaseRefBinder<TRef> : BaseBinder, IRefBinder
    {
        private TRef _reference;

        /// <summary>
        /// Property that is used to set the reference from the inspector.
        /// </summary>
        [field: SerializeReference]
        private Component Reference { get; set; }

        private void Awake()
        {
            if (Reference is not TRef reference)
            {
                ValidateReferenceType();
                _reference = GetComponent<TRef>();
                Reference = _reference as Component;
                return;
            }

            _reference = reference;
        }

        private void ValidateReferenceType()
        {
            if (Reference == null)
            {
                return;
            }

            Debug.LogWarningFormat(
                "Reference {0} is not of type {1}. Instead, will try to find it as a component on the same GameObject.",
                Reference, typeof(TRef).Name);
        }

        /// <inheritdoc />
        public bool TryGetReference<T>(out T reference)
        {
            reference = default;
            if (_reference is not T @ref)
            {
                return false;
            }

            reference = @ref;
            return true;
        }
    }
}