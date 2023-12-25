using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Base class for all binders.
    /// </summary>
    /// <remarks>
    /// This class is provide basic functionality for all binders, like name validation and binding to mediators.
    /// </remarks>
    [Serializable]
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        private readonly List<IInternalMediator> _mediators = new(1); //One mediator is enough for most cases

        [field: SerializeField] public string Name { get; [ExcludeFromCodeCoverage] private set; }

        public event OnBinderChanged OnBinderChanged;
        
        /// <summary>
        /// Checks if game object of this binder should be included in search for <see cref="IMediator"/>.
        /// True by default.
        /// </summary>
        /// <remarks>
        /// To prevent search for <see cref="IMediator"/> in game object of this binder, override this property and return <see langword="false"/>.
        /// </remarks>
        protected virtual bool IncludeOwnGameObjectInFind => true;


        protected void InvokeOnBinderChanged() =>
            OnBinderChanged?.Invoke(this);

        protected virtual void OnEnable()
        {
            if (!ValidateName())
                return;

            if (!TryCacheMediator())
                return;

            foreach (var mediator in _mediators)
                mediator.Bind(this);
        }

        protected virtual void OnDisable()
        {
            foreach (var mediator in _mediators)
                mediator.Unbind(this);
        }

        private bool ValidateName()
        {
            if (!string.IsNullOrEmpty(Name))
                return !Name.StartsWith('#'); //Name that starts with '#' must be ignored

            Debug.LogError($"{name} will be ignored: name for binding was not set!", gameObject);
            return false;
        }

        private bool TryCacheMediator()
        {
            if (_mediators.Count > 0) //Already cached
                return true;

            _mediators.AddRange(FindMediators());
            if (_mediators.Count > 0)
                return true;

            Debug.LogError($"{name} will be ignored: {nameof(IMediator)} was not found!", gameObject);
            return false;
        }

        private IEnumerable<IInternalMediator> FindMediators()
        {
            if (IncludeOwnGameObjectInFind)
                return GetComponentsInParent<IInternalMediator>();

            var parent = transform.parent;
            return parent == null
                ? Array.Empty<IInternalMediator>()
                : parent.GetComponentsInParent<IInternalMediator>();
        }
    }
}