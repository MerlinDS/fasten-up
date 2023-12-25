using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace FastenUp.Runtime.Binders.Behaviours
{
    public sealed partial class DropdownBinder : IValueReceiver<string[]>, 
        IValueReceiver<List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc />
        public void SetValue(string[] value)
        {
            if (value is not { Length: > 0 })
            {
                ClearOptions();
                return;
            }

            SetValue(value.Select(option => new TMP_Dropdown.OptionData(option)).ToList());
        }

        /// <inheritdoc />
        public void SetValue(List<TMP_Dropdown.OptionData> value)
        {
            if (value is not { Count: > 0 })
            {
                ClearOptions();
                return;
            }

            _dropdown.options = value;
        }

        private void ClearOptions()
        {
            _dropdown.ClearOptions();
        }
    }
}