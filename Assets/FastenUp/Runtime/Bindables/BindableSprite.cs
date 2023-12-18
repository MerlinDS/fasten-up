using FastenUp.Runtime.Adapters;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
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