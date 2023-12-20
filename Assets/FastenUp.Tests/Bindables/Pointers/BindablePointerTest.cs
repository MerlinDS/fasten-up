using System;
using FastenUp.Runtime.Bindables.Pointers;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables.Pointers
{
    [TestFixture]
    [TestOf(typeof(BindablePointer))]
    public class BindablePointerTest
    {
        [Test]
        public void AddListener_When_type_not_generic_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            var sut = CreateSut();
            Action act = () => sut.AddListener((UnityAction)null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void AddListener_When_type_not_generic_listener_is_not_null_Should_add_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerEvent(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke();
        }

        [Test]
        public void AddListener_When_type_is_generic_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            var sut = CreateSut();
            Action act = () => sut.AddListener((UnityAction<PointerEventData>)null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void AddListener_When_type_is_generic_listener_is_not_null_Should_add_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerEvent(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }

        [Test]
        public void RemoveListener_When_type_not_generic_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            var sut = CreateSut();
            Action act = () => sut.RemoveListener((UnityAction)null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void RemoveListener_When_type_not_generic_listener_is_not_null_Should_remove_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.RemoveListener(actual);
            sut.OnPointerEvent(new PointerEventData(EventSystem.current));
            // Assert
            actual.DidNotReceive().Invoke();
        }

        [Test]
        public void RemoveListener_When_type_is_generic_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            var sut = CreateSut();
            Action act = () => sut.RemoveListener((UnityAction<PointerEventData>)null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void RemoveListener_When_type_is_generic_listener_is_not_null_Should_remove_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.RemoveListener(actual);
            sut.OnPointerEvent(new PointerEventData(EventSystem.current));
            // Assert
            actual.DidNotReceive().Invoke(Arg.Any<PointerEventData>());
        }

        [Test]
        public void OnDisable_When_has_listener_Should_remove_all_listeners()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.ExecuteOnDisable();
            sut.OnPointerEvent(new PointerEventData(EventSystem.current));
            // Assert
            actual.DidNotReceive().Invoke(Arg.Any<PointerEventData>());
        }

        private static TestBindablePointer CreateSut()
        {
            var gameObject = new GameObject();
            return gameObject.AddComponent<TestBindablePointer>();
        }

        private sealed class TestBindablePointer : BindablePointer
        {
            public new void OnPointerEvent(PointerEventData eventData)
            {
                base.OnPointerEvent(eventData);
            }
        }
    }
}