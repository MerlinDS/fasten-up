using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Mediators;
using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    internal partial class TextOverviewMediator : MonoBehaviour, IMediator
    {
        private Bindable<string> Text { get; } = new() { Value = $"This text is set by {nameof(TextOverviewMediator)}" };
        private Bindable<int> IntValue { get; } = new() { Value = 42 };
        private Bindable<float> FloatValue { get; } = new() { Value = 3.14f };

        private Bindable<string> InputText { get; } = new();

        private Bindable<string> OutputText { get; } = new();

        private Bindable<string[]> DropDownOptions { get; } =
            new() { Value = new[] { "Cat", "Dog", "Parrot", "Fish" } };

        private Bindable<int> DropDownSelectedIndex { get; } = new();

        private Bindable<string> DropDownSelectedOption { get; } = new();

        private void Awake()
        {
            DropDownSelectedIndex.OnValueChanged += OnDropDownSelectedIndexChanged;
            InputText.OnValueChanged += OnInputTextChanged;
            OnInputTextChanged(string.Empty);
            DropDownSelectedIndex.Value = 1;
        }


        private void OnDestroy()
        {
            InputText.OnValueChanged -= OnInputTextChanged;
            DropDownSelectedIndex.OnValueChanged -= OnDropDownSelectedIndexChanged;
        }


        public void ChangeIntValue(int value)
        {
            IntValue.Value = value;
        }

        public void ChangeFloatValue(float value)
        {
            FloatValue.Value = value;
        }

        private void OnInputTextChanged(string value)
        {
            OutputText.Value = $"Text from input: {value}";
        }

        private void OnDropDownSelectedIndexChanged(int index)
        {
            DropDownSelectedOption.Value = DropDownOptions.Value[index];
        }
    }
}