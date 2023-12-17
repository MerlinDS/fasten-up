using System.Globalization;

namespace FastenUp.Runtime.Bindables
{
    public partial class BindableText : IBindable<int>, IBindable<float>
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