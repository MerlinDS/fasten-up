using FastenUp.Runtime.Base;
using UnityEngine;
using UnityEngine.Events;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableButton : MonoBehaviour, IMediator
    {
        private BindPoint<UnityAction> OnClick { get; } = new();
        private BindPoint<int> ClickCount { get; } = new();

        private void Awake()
        {
            OnClick.Value = () => ClickCount.Value++;
        }

        private void OnDestroy()
        {
            OnClick.Value = null;
        }
    }
}