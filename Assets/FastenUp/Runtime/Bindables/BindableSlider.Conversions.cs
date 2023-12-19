using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    public sealed partial class BindableSlider : IGettableBindable<int>, IBindable<Vector2Int>
    {
        /// <inheritdoc />
        public void SetValue(int value)
        {
            SetValue((float)value);
        }

        /// <inheritdoc />
        int IGettableBindable<int>.GetValue()
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