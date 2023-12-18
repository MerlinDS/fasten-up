using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Adapters
{
    public sealed class GraphicAdapter : ComponentFieldAdapter<Graphic>,
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