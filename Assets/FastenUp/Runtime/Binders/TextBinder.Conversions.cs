using System.Globalization;

namespace FastenUp.Runtime.Binders
{
    public partial class TextBinder : IBinder<int>, IBinder<float>
    {
        /// <inheritdoc />
        public void SetValue(int value)
        {
            SetValue(value.ToString());
        }

        /// <inheritdoc />
        public void SetValue(float value)
        {
            SetValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}