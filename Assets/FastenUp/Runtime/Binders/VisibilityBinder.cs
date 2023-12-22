﻿using System.Collections.Generic;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Utils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// The two-way <see cref="IBindable"/> that controls visibility of UI components. 
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Visibility Binder", 1)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#visibility")]
    public sealed class VisibilityBinder : BaseBindable, IGettableBindable<bool>, IHierarchyCache
    {
        private readonly Queue<Transform> _transformQueue = new();
        private readonly List<Component> _componentBuffer = new();
        private readonly HashSet<Behaviour> _behaviourCache = new();
        private readonly HashSet<VisibilityBinder> _childrenCache = new();

        private enum DefaultVisibility : byte
        {
            Visible,

            [UsedImplicitly(ImplicitUseTargetFlags.Itself)]
            Hidden
        }

        [SerializeField] private DefaultVisibility _defaultVisibility;
        private bool _value = true;
        private VisibilityBinder _parent;

        /// <summary>
        /// Checks if this component can be visible.
        /// If parent is null, then this component is root, so it can be visible.
        /// </summary>
        private bool CanBeVisible =>
            _value && (_parent == null || _parent.GetValue());

        private void Awake()
        {
            UpdateCache();
            SetValue(_defaultVisibility == DefaultVisibility.Visible);
        }

        /// <inheritdoc />
        public void RebuildCache()
        {
            _behaviourCache.Clear();
            _childrenCache.Clear();

            UpdateCache();
        }

        /// <inheritdoc />
        public void SetValue(bool value)
        {
            if (_value == value)
                return;

            _value = value;
            SetVisibility(value);
        }

        /// <inheritdoc />
        public bool GetValue()
        {
            return _value;
        }

        private void SetVisibility(bool value)
        {
            if (value && !CanBeVisible)
                return;

            SetBehaviourVisibility(value);
            SetChildrenVisibility(value);
        }

        private void SetBehaviourVisibility(bool value)
        {
            foreach (var behaviour in _behaviourCache)
            {
                if (!Validate(behaviour))
                    continue;

                behaviour.enabled = value;
            }
        }

        private void SetChildrenVisibility(bool value)
        {
            foreach (var child in _childrenCache)
            {
                if (!Validate(child))
                    continue;

                child.SetVisibility(value);
            }
        }

        private bool Validate(Object @object)
        {
            if (@object != null)
                return true;

            Debug.LogError("Hierarchy was changed. But RefreshCache was not called.", this);
            return false;
        }

        private void UpdateCache()
        {
            _transformQueue.Enqueue(transform);
            while (_transformQueue.TryDequeue(out var source))
            {
                var hasCanvas = source.TryGetComponent<Canvas>(out var canvas);
                //If source has Canvas, then ignore its children except visibilities
                EnqueueChildrenOf(source, hasCanvas);
                if (hasCanvas)
                {
                    /*
                     * Add Canvas to cache but ignore its children.
                     * Canvas is responsible for its children visibility.
                     */
                    _behaviourCache.Add(canvas);
                    continue;
                }

                CollectBehavioursFrom<ICanvasElement>(source); //Collect Graphic, InputField, Toggle, etc.
                CollectBehavioursFrom<ILayoutElement>(source); //Collect LayoutElement, LayoutGroup, etc.
            }

            if (transform.parent != null)
                _parent = transform.parent.GetComponentInParent<VisibilityBinder>();
        }

        private void EnqueueChildrenOf(Transform source, bool onlyVisibilities = false)
        {
            var length = source.childCount;
            for (var i = 0; i < length; i++)
            {
                var child = source.GetChild(i);
                if (!child.gameObject.activeInHierarchy) //Do not touch disabled objects
                    continue;

                if (child.TryGetComponent<VisibilityBinder>(out var childVisibility))
                {
                    _childrenCache.Add(childVisibility);
                    continue;
                }

                if (onlyVisibilities)
                    continue;

                _transformQueue.Enqueue(child);
            }
        }

        private void CollectBehavioursFrom<T>(Component source)
        {
            source.GetComponents(typeof(T), _componentBuffer);
            foreach (var component in _componentBuffer)
            {
                if (component is Behaviour behaviour)
                    _behaviourCache.Add(behaviour);
            }

            _componentBuffer.Clear();
        }
    }
}