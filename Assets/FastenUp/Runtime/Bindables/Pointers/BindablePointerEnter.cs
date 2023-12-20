using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerEnterHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.PointerMenu + nameof(BindablePointerEnter), 0)]
    public sealed class BindablePointerEnter : BindablePointer, IPointerEnterHandler
    {
        /// <inheritdoc />
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}