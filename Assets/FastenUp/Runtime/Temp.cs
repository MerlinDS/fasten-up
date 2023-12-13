using UnityEngine;

namespace FastenUp.Runtime
{
    public class Temp : MonoBehaviour
    {
        static string GetStringFromSourceGenerator()
        {
            return ExampleSourceGenerated.ExampleSourceGenerated.GetTestText();
        }
    }
}