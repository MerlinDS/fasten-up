﻿using UnityEngine.Events;

namespace FastenUp.Runtime.Binders.Actions
{
    /// <summary>
    /// Binders that provides the ability to invoke the <see cref="UnityEvent"/> in the bind Unity components through the
    /// <see cref="FastenUp.Runtime.Bindables.BindableAction"/>
    /// </summary>
    public sealed class ActionBinder : BaseActionBinder<UnityEvent>
    {
    }

    /// <summary>
    /// Binders that provides the ability to invoke the <see cref="UnityEvent{T}"/> in the bind Unity components through the
    /// <see cref="FastenUp.Runtime.Bindables.BindableAction{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the argument of the <see cref="UnityEvent{T}"/></typeparam>
    public abstract class ActionBinder<T> : BaseActionBinder<UnityEvent<T>>
    {
    }
}