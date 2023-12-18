using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Adapters
{
    public sealed class ImageAdapter : ComponentFieldAdapter<Image>,
        IComponentFieldAdapter<Sprite>
    {
        public static bool TryCreate<T>(GameObject gameObject, out IComponentFieldAdapter<T> adapter) =>
            TryCreate<ImageAdapter, T>(gameObject, out adapter);

        /// <inheritdoc />
        public ImageAdapter(Image component) : base(component)
        {
        }

        /// <inheritdoc />
        public Sprite Value
        {
            get => Component.sprite;
            set
            {
                Component.sprite = value;
                Component.enabled = value != null;
            }
        }
    }
}