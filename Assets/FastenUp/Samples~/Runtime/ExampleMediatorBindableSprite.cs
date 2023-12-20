using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableSprite : MonoBehaviour, IMediator
    {
        private BindPoint<Sprite> Sprite { get; } = new();
        private BindPoint<string> Path { get; } = new();
        
        private void Awake()
        {
            Path.Value = "FastenUp - Icon";
            Sprite.Value = Resources.Load<Sprite>(Path.Value);
        }
    }
}