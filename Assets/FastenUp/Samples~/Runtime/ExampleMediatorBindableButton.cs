using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableButton : MonoBehaviour, IMediator
    {
        private BindableEvent OnClick { get; } = new();
        private Bindable<int> ClickCount { get; } = new();

        private void Awake()
        {
            OnClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            ClickCount.Value++;
        }

        private void OnDestroy()
        {
            OnClick.Dispose();
        }
    }
}