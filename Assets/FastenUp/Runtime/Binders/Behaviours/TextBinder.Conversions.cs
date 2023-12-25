using System.Globalization;

namespace FastenUp.Runtime.Binders.Behaviours
{
    public partial class TextBinder : IValueReceiver<int>, IValueReceiver<float>
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