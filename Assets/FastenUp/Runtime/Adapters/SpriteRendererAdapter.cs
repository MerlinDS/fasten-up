using JetBrains.Annotations;
using UnityEngine;

namespace FastenUp.Runtime.Adapters
{
    internal sealed class SpriteRendererAdapter : ComponentFieldAdapter<SpriteRenderer>,
        IComponentFieldAdapter<Sprite>,
        IComponentFieldAdapter<Color>
    {

        [CanBeNull]
        public static IComponentFieldAdapter<T> Create<T>(GameObject gameObject) => 
            Create<SpriteRendererAdapter, T>(gameObject);
        

        /// <inheritdoc />
        public SpriteRendererAdapter(SpriteRenderer component) : base(component)
        {
        }

        /// <inheritdoc />
        Sprite IComponentFieldAdapter<Sprite>.Value
        {
            get => Component.sprite;
            set
            {
                Component.sprite = value;
                Component.enabled = value is not null;
            }
        }

        /// <inheritdoc />
        Color IComponentFieldAdapter<Color>.Value
        {
            get => Component.color;
            set => Component.color = value;
        }
    }
}