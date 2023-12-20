using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableText : MonoBehaviour, IMediator
    {
        private BindPoint<string> Text { get; } = new();

        private void Awake()
        {
            Text.Value = $"This text is changed by a {nameof(ExampleMediatorBindableText)}!";
        }
    }
}