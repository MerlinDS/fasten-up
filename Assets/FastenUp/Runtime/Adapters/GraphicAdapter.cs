using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Adapters
{
    public sealed class GraphicAdapter : ComponentFieldAdapter<Graphic>,
        IComponentFieldAdapter<Color>
    {
        public static bool TryCreate<T>(GameObject gameObject, out IComponentFieldAdapter<T> adapter) => 
            TryCreate<GraphicAdapter, T>(gameObject, out adapter);
        
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