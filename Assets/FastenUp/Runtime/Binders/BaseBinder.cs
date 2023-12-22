using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// Base class for all bindables.
    /// </summary>
    /// <remarks>
    /// This class is provide basic functionality for all bindables, like name validation and binding to mediators.
    /// </remarks>
    [Serializable]
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        private readonly List<IInternalMediator> _mediators = new(1);//One mediator is enough for most cases

        [field: SerializeField] public string Name { get; [ExcludeFromCodeCoverage] private set; }

        public event OnBindableChanged OnBindableChanged;

        protected void InvokeOnBindableChanged() =>
            OnBindableChanged?.Invoke(this);

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
                return true;

            Debug.LogError($"{name} will be ignored: name for binding was not set!", gameObject);
            return false;
        }

        private bool TryCacheMediator()
        {
            if (_mediators.Count > 0)//Already cached
                return false;
            
            _mediators.AddRange(GetComponentsInParent<IInternalMediator>());
            if (_mediators.Count > 0)
                return true;
            
            Debug.LogError($"{name} will be ignored: {nameof(IMediator)} was not found!", gameObject);
            return false;

        }
    }
}