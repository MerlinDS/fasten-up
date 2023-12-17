using UnityEngine;

namespace FastenUp.Runtime.Exceptions
{
    public sealed class FastenUpBindableException : FastenUpException
    {
        public GameObject GameObject { get; }

        public FastenUpBindableException(string message, GameObject gameObject) : base(message)
        {
            GameObject = gameObject;
        }
    }
}