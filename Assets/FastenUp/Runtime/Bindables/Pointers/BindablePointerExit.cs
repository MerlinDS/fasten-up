using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerExitHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.PointerMenu + nameof(BindablePointerExit), 0)]
    public sealed class BindablePointerExit : BindablePointer, IPointerExitHandler
    {
        /// <inheritdoc />
        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}