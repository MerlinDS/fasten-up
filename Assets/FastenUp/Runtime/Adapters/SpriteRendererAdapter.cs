using UnityEngine;

namespace FastenUp.Runtime.Adapters
{
    public sealed class SpriteRendererAdapter : ComponentFieldAdapter<SpriteRenderer>,
        IComponentFieldAdapter<Sprite>,
        IComponentFieldAdapter<Color>
    {
        private Color _value;

        public static bool TryCreate<T>(GameObject gameObject, out IComponentFieldAdapter<T> adapter) => 
            TryCreate<SpriteRendererAdapter, T>(gameObject, out adapter);
        

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