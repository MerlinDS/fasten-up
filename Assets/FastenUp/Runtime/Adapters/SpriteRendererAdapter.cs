using JetBrains.Annotations;
using UnityEngine;

namespace FastenUp.Runtime.Adapters
{
    public sealed class SpriteRendererAdapter : ComponentFieldAdapter<SpriteRenderer>,
        IComponentFieldAdapter<Sprite>,
        IComponentFieldAdapter<Color>
    {
        private Color _value;

        [CanBeNull]
        public static IComponentFieldAdapter<T> Create<T>(GameObject gameObject) => 
            Create<SpriteRendererAdapter, T>(gameObject);
        

        /// <inheritdoc />
        private SpriteRendererAdapter(SpriteRenderer component) : base(component)
        {
        }

        /// <inheritdoc />
        Sprite IComponentFieldAdapter<Sprite>.Value
        {
            get => Component.sprite;
            set
            {
                Component.sprite = value;
                Component.enabled = value != null;
            }
        }

        /// <inheritdoc />
        Color IComponentFieldAdapter<Color>.Value
        {
            get => _value;
            set => _value = value;
        }
    }
}