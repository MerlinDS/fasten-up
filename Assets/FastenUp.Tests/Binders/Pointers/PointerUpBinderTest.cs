using FastenUp.Runtime.Binders.Pointers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Tests.Binders.Pointers
{
    [TestFixture]
    [TestOf(typeof(PointerUpBinder))]
    public class PointerUpBinderTest
    {
        [Test]
        public void OnPointerMove_When_has_listener_Should_invoke_event()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            PointerUpBinder sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerUp(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }
        
        private static PointerUpBinder CreateSut() => 
            new GameObject().AddComponent<PointerUpBinder>();
    }
}