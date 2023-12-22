using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableSprite : MonoBehaviour, IMediator
    {
        private Bindable<Sprite> Sprite { get; } = new();
        private Bindable<string> Path { get; } = new();
        
        private void Awake()
        {
            Path.Value = "FastenUp - Icon";
            Sprite.Value = Resources.Load<Sprite>(Path.Value);
        }
    }
}