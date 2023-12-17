using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + nameof(BindableText) , 0)]
    public sealed partial class BindableText : BaseBindable, IBindable<string>
    {
        private TextMeshProUGUI _component;

        private void Awake() =>
            TryGetComponent(out _component);

        /// <inheritdoc />
        public void SetValue(string value) =>
            _component.text = value;
    }
}