using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    /// <summary>
    /// The one-way <see cref="IBindable"/> binds a value to the text component.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + nameof(BindableText) , 0)]
    public sealed partial class BindableText : BaseBindable, IBindable<string>
    {
        private TextMeshProUGUI _component;

        private void Awake()
        {
            _component = GetComponent<TextMeshProUGUI>();
        }

        /// <inheritdoc />
        public void SetValue(string value)
        {
            _component.text = value;
        }
    }
}