using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the component with <see cref="Color"/> field.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + nameof(BindableColor) , 1)]
    public class BindableColor : BaseBindable, IBindable<Color>
    {
        private IColorFieldAdapter _adapter;

        private void Awake()
        {
            if (TryGetComponent<Graphic>(out var graphic))
            {
                _adapter = new GraphicColorAdapter(graphic);
                return;
            }

            if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                _adapter = new SpriteRendererColorAdapter(spriteRenderer);
        }

        /// <inheritdoc />
        public void SetValue(Color value) =>
            _adapter?.SetColor(value);

        private sealed class GraphicColorAdapter : IColorFieldAdapter
        {
            private readonly Graphic _graphic;

            public GraphicColorAdapter(Graphic graphic) =>
                _graphic = graphic;

            /// <inheritdoc />
            public void SetColor(Color value) =>
                _graphic.color = value;
        }

        private sealed class SpriteRendererColorAdapter : IColorFieldAdapter
        {
            private readonly SpriteRenderer _spriteRenderer;

            public SpriteRendererColorAdapter(SpriteRenderer spriteRenderer) =>
                _spriteRenderer = spriteRenderer;

            /// <inheritdoc />
            public void SetColor(Color value) =>
                _spriteRenderer.color = value;
        }

        private interface IColorFieldAdapter
        {
            void SetColor(Color value);
        }
    }
}