using UnityEngine;

namespace FastenUp.Examples.Runtime
{
    public sealed class ActionExample : MonoBehaviour
    {
        public void Execute()
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        private void Update()
        {
            if((transform.localScale - Vector3.one).sqrMagnitude < 0.01F)
                return;
            
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
        }
    }
}