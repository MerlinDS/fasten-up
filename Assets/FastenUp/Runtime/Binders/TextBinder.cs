using FastenUp.Runtime.Utils;
using TMPro;
using UnityEngine;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// The one-way <see cref="IBinder"/> binds a value to the text component.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Text Binder" , 1)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#text")]
    public sealed partial class TextBinder : BaseBinder, IValueReceiver<string>
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