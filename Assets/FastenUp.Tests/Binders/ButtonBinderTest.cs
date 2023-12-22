using System;
using FastenUp.Runtime.Binders;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders
{
    [TestFixture]
    [TestOf(typeof(ButtonBinder))]
    public class ButtonBinderTest
    {
        [Test]
        public void AddListener_When_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            var sut = CreateSut();
            Action act = () => sut.AddListener(null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }


        [Test]
        public void OnClick_When_has_listener_button_clicked_Should_invoke_on_click()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.GetComponent<Button>().onClick.Invoke();
            // Assert
            actual.Received(1).Invoke();
        }

        [Test]
        public void RemoveListener_When_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            var sut = CreateSut();
            Action act = () => sut.RemoveListener(null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void RemoveListener_When_listener_is_not_null_Should_remove_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.RemoveListener(actual);
            sut.GetComponent<Button>().onClick.Invoke();
            // Assert
            actual.DidNotReceive().Invoke();
        }

        [Test]
        public void OnDisable_When_has_listener_Should_remove_all_listeners()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.ExecuteOnDisable();
            sut.GetComponent<Button>().onClick.Invoke();
            // Assert
            actual.DidNotReceive().Invoke();
        }

        private static ButtonBinder CreateSut()
        {
            var gameObject = new GameObject(nameof(ButtonBinderTest));
            var sut = gameObject.AddComponent<ButtonBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}