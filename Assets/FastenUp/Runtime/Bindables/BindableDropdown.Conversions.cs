using System.Collections.Generic;
using TMPro;

namespace FastenUp.Runtime.Bindables
{
    public sealed partial class BindableDropdown : IBindable<List<string>>, 
        IBindable<List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc />
        public void SetValue(List<string> value)
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(value);
        }
         
        /// <inheritdoc />
        public void SetValue(List<TMP_Dropdown.OptionData> value)
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(value);
        }
    }
}