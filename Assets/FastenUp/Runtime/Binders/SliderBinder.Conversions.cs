using UnityEngine;

namespace FastenUp.Runtime.Binders
{
    public sealed partial class SliderBinder : 
        IValueReceiver<int>, IValueProvider<int>, IValueReceiver<Vector2Int>
    {
        /// <inheritdoc />
        public void SetValue(int value)
        {
            SetValue((float)value);
        }

        /// <inheritdoc />
        int IValueProvider<int>.GetValue()
        {
            return (int)GetValue();
        }

        /// <inheritdoc />
        public void SetValue(Vector2Int value)
        {
            _slider.minValue = value.x;
            _slider.maxValue = value.y;
            _slider.wholeNumbers = true;
        }
    }
}