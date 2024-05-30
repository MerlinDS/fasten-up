using FastenUp.Runtime.Binders.Pointers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Tests.Binders.Pointers
{
    [TestFixture]
    [TestOf(typeof(PointerExitBinder))]
    public class PointerExitBinderTest
    {
        [Test]
        public void OnPointerExit_When_has_listener_Should_invoke_event()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            PointerExitBinder sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerExit(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }
        
        private static PointerExitBinder CreateSut() => 
            new GameObject().AddComponent<PointerExitBinder>();
    }
}