using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    public partial class ActionsOverviewMediator : MonoBehaviour, IMediator
    {
        public Bindable<bool> Visibility { get; } = new();
        
        public BindableAction ParameterlessAction { get; } = new();
        public BindableAction<bool> BooleanAction { get; } = new();
        
        public BindableAction<int> IntAction { get; } = new();
        
        public BindableAction<float> FloatAction { get; } = new();

        private void Awake()
        {
            InvokeIntAction(40);
        }
        
        public void InvokeParameterlessAction()
        {
            ParameterlessAction.Invoke();
        }

        public void InvokeBoolAction(bool value)
        {
            BooleanAction.Invoke(value);
        }
        
        public void InvokeIntAction(float value)
        {
            IntAction.Invoke((int)value);
        }
        
        public void InvokeFloatAction(float value)
        {
            FloatAction.Invoke(value);
        }
    }
}