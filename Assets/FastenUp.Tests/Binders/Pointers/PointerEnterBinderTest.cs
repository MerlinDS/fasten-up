using FastenUp.Runtime.Binders.Pointers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Tests.Binders.Pointers
{
    [TestFixture]
    [TestOf(typeof(PointerEnterBinder))]
    public class PointerEnterBinderTest
    {
        [Test]
        public void OnPointerEnter_When_has_listener_Should_invoke_event()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            PointerEnterBinder sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerEnter(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }
        
        private static PointerEnterBinder CreateSut() => 
            new GameObject().AddComponent<PointerEnterBinder>();
    }
}