using FastenUp.Runtime.Base;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Tests
{
    public class Temp
    {
        [Test]
        public void TempConcept()
        {
            //Arrange 
            //Act
            var mediator = new GameObject().AddComponent<TestMediator>(); 
            //Assert
        }
    }

    public partial class TestMediator : MonoBehaviour, IMediator
    {
    }
}