using FastenUp.Runtime.Bindables.Pointers;
using FastenUp.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastenUp.Runtime.Binders.Pointers
{
    /// <summary>
    /// This class is used to bind to the <see cref="IPointerEnterHandler"/> interface.
    /// </summary> 
    [AddComponentMenu(FastenUpComponentMenu.EventHandlersMenu + "Pointer Enter Binder", 1)]
    [HelpURL("https://github.com/MerlinDS/fasten-up/wiki/Binders#pointer-enter")]
    public sealed class PointerEnterBinder : BindablePointer, IPointerEnterHandler
    {
        /// <inheritdoc />
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEvent(eventData);
        }
    }
}