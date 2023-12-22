using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableDropdown : MonoBehaviour, IMediator
    {
        private Bindable<string[]> Options { get; } = new();
        private Bindable<int> SelectedIndex { get; } = new();

        private Bindable<string> SelectedOption { get; } = new();

        private void Awake()
        {
            Options.Value = new[] { "Option 1", "Option 2", "Option 3" };
            SelectedIndex.OnValueChanged += OnSelectedIndexChanged;
            SelectedIndex.Value = 0;
        }

        private void OnDestroy()
        {
            SelectedIndex.OnValueChanged -= OnSelectedIndexChanged;
        }

        private void OnSelectedIndexChanged(int index)
        {
            SelectedOption.Value = Options.Value[index];
        }
    }
}