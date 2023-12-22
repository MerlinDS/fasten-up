using UnityEngine;

namespace FastenUp.Runtime.Binders
{
    public sealed partial class SpriteBinder : IBinder<string>
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