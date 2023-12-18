using UnityEngine;

namespace FastenUp.Runtime.Bindables
{
    public sealed partial class BindableSprite : IBindable<string>
    {
        /// <inheritdoc />
        public void SetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                SetValue(default(Sprite));
                return;
            }
            
            SetValue(Resources.Load<Sprite>(value));
        }
    }
}