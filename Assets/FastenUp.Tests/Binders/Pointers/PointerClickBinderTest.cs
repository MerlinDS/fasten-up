using FastenUp.Runtime.Binders.Pointers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Tests.Binders.Pointers
{
    [TestFixture]
    [TestOf(typeof(PointerClickBinder))]
    public class PointerClickBinderTest
    {
        [Test]
        public void OnPointerClick_When_has_listener_Should_invoke_on_click()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerClick(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }

        private static PointerClickBinder CreateSut()
        {
            var go = new GameObject();
            return go.AddComponent<PointerClickBinder>();
        }
    }
}