using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableButton : MonoBehaviour, IMediator
    {
        private BindAction OnClick { get; } = new();
        private BindPoint<int> ClickCount { get; } = new();

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