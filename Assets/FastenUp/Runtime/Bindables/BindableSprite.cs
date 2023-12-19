using FastenUp.Runtime.Adapters;
using FastenUp.Runtime.Utils;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the component with <see cref="Sprite"/> field.
    /// </summary>
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + nameof(BindableSprite) , 2)]
    public sealed partial class BindableSprite : BaseBindable, IBindable<Sprite>
    {
        private IComponentFieldAdapter<Sprite> _adapter;

        private void Awake()
        {
            _adapter = ImageAdapter.Create<Sprite>(gameObject) ??
                       SpriteRendererAdapter.Create<Sprite>(gameObject);
        }

        public void SetValue(Sprite value)
        {
            if (_adapter is not null)
                _adapter.Value = value;
        }
    }
}