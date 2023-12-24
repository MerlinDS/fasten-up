using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Runtime.Binders
{
    /// <summary>
    /// The two-way <see cref="IBinder"/> binds a value to the component with <see cref="Slider"/> component.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu(FastenUpComponentMenu.BaseMenu + "Slider Binder" , 5)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#slider")]
    public sealed partial class SliderBinder : BaseBinder, IValueProvider<float>, IValueReceiver<Vector2>
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <inheritdoc />
        public void SetValue(float value)
        {
            _slider.SetValueWithoutNotify(value);
        }

        /// <inheritdoc />
        public float GetValue() =>
            _slider.value;

        /// <inheritdoc />
        public void SetValue(Vector2 value)
        {
            _slider.minValue = value.x;
            _slider.maxValue = value.y;
            _slider.wholeNumbers = false;
        }

        private void OnValueChanged(float _) =>
            InvokeOnBinderChanged();
    }
}