using FastenUp.Runtime.Base;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class ExampleMediatorBindableDropdown : MonoBehaviour, IMediator
    {
        private BindPoint<string[]> Options { get; } = new();
        private BindPoint<int> SelectedIndex { get; } = new();

        private BindPoint<string> SelectedOption { get; } = new();

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