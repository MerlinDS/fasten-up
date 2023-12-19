using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Adapters
{
    /// <summary>
    /// This adapter is used to adapt components that inherit from <see cref="Graphic"/>,
    /// such as <see cref="Image"/>, <see cref="RawImage"/>, <see cref="Text"/>, etc.
    /// </summary>
    internal sealed class GraphicAdapter : ComponentFieldAdapter<Graphic>,
        IComponentFieldAdapter<Color>
    {
        [CanBeNull]
        public static IComponentFieldAdapter<T> Create<T>(GameObject gameObject) =>
            Create<GraphicAdapter, T>(gameObject);

        /// <inheritdoc />
        public GraphicAdapter(Graphic component) : base(component)
        {
        }

        /// <inheritdoc />
        public Color Value
        {
            get => Component.color;
            set => Component.color = value;
        }
    }
}