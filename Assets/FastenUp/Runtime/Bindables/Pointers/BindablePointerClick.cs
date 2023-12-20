using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Bindables.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerClickHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.PointerMenu + nameof(BindablePointerClick), 0)]
    public sealed class BindablePointerClick : BindablePointer, IPointerClickHandler
    {
        /// <inheritdoc />
        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}