using FastenUp.Runtime.Base;
using UnityEngine;
using UnityEngine.Events;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindablePointer : MonoBehaviour, IMediator
    {
        private BindPoint<UnityAction> OnClick { get; } = new();
        private BindPoint<UnityAction> OnEnter { get; } = new();
        private BindPoint<UnityAction> OnExit { get; } = new();
        
        private BindPoint<string> PointerAction { get; } = new();
        
        private void Awake()
        {
            OnClick.Value = () => PointerAction.Value = "Clicked";
            OnEnter.Value = () => PointerAction.Value = "Entered";
            OnExit.Value = () => PointerAction.Value = "Exited";
        }
        
        private void OnDestroy()
        {
            OnClick.Value = null;
            OnEnter.Value = null;
            OnExit.Value = null;
        }
    }
}