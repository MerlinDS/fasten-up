using FastenUp.Runtime.Bindables.Pointers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Tests.Bindables.Pointers
{
    [TestFixture]
    [TestOf(typeof(BindablePointerExit))]
    public class BindablePointerExitTest
    {
        [Test]
        public void OnPointerExit_When_has_listener_Should_invoke_event()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerExit(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }
        
        private static BindablePointerExit CreateSut() => 
            new GameObject().AddComponent<BindablePointerExit>();
    }
}