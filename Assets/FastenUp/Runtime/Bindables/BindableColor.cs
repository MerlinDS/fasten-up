﻿using FastenUp.Runtime.Adapters;
using FastenUp.Runtime.Utils;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the component with <see cref="Color"/> field.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + nameof(BindableColor), 1)]
    public class BindableColor : BaseBindable, IBindable<Color>
    {
        private IComponentFieldAdapter<Color> _adapter;

        private void Awake()
        {
            _adapter = GraphicAdapter.Create<Color>(gameObject) ??
                       SpriteRendererAdapter.Create<Color>(gameObject);
        }

        /// <inheritdoc />
        public void SetValue(Color value)
        {
            if (_adapter is not null)
                _adapter.Value = value;
        }
    }
}