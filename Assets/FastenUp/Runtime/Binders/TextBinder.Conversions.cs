using System.Globalization;
using FastenUp.Runtime.Bindables;

namespace FastenUp.Runtime.Binders
{
    public partial class TextBinder : IBindable<int>, IBindable<float>
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