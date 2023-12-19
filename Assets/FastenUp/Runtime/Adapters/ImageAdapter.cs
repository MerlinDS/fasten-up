using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Adapters
{
    internal sealed class ImageAdapter : ComponentFieldAdapter<Image>,
        IComponentFieldAdapter<Sprite>
    {
        [CanBeNull]
        public static IComponentFieldAdapter<T> Create<T>(GameObject gameObject) =>
            Create<ImageAdapter, T>(gameObject);

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