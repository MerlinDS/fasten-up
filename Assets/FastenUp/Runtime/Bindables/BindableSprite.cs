using FastenUp.Runtime.Adapters;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    public sealed partial class BindableSprite : BaseBindable, IBindable<Sprite>
    {
        private IComponentFieldAdapter<Sprite> _adapter;

        private void Awake()
        {
            if (ImageAdapter.TryCreate(gameObject, out _adapter))
                return;

            SpriteRendererAdapter.TryCreate(gameObject, out _adapter);
        }

        public void SetValue(Sprite value)
        {
            if (_adapter is not null) 
                _adapter.Value = value;
        }
    }
}