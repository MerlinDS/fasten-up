using System;
using FastenUp.Runtime.Binders.Events;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.Events
{
    [TestFixture]
    [TestOf(typeof(ButtonBinder))]
    public class ButtonBinderTest
    {
        [Test]
        public void AddListener_When_listener_is_null_Should_not_throw_exception()
        {
            // Arrange
            ButtonBinder sut = CreateSut();
            Action act = () => sut.AddListener(null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }


        [Test]
        public void OnClick_When_has_listener_button_clicked_Should_invoke_on_click()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            ButtonBinder sut = CreateSut();
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
            ButtonBinder sut = CreateSut();
            Action act = () => sut.RemoveListener(null);
            // Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void RemoveListener_When_listener_is_not_null_Should_remove_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            ButtonBinder sut = CreateSut();
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
            ButtonBinder sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.ExecuteOnDisable();
            sut.GetComponent<Button>().onClick.Invoke();
            // Assert
            actual.DidNotReceive().Invoke();
        }
        
        [Test]
        public void RemoveListener_When_has_no_listener_Should_not_remove_listener()
        {
            // Arrange
            var actual = Substitute.For<UnityAction>();
            ButtonBinder sut = CreateSut();
            // Act
            sut.RemoveListener(actual);
            // Assert
            actual.DidNotReceive().Invoke();
        }
        
        [Test]
        public void Awake_When_called_Should_set_component_interactable_to_false()
        {
            // Arrange
            ButtonBinder sut = CreateSut();
            // Act
            sut.ExecuteAwake();
            // Assert
            sut.GetComponent<Button>().interactable.Should().BeFalse();
        }
        
        [Test]
        public void AddListener_When_called_Should_set_component_interactable_to_true()
        {
            // Arrange
            ButtonBinder sut = CreateSut();
            // Act
            sut.AddListener(Substitute.For<UnityAction>());
            // Assert
            sut.GetComponent<Button>().interactable.Should().BeTrue();
        }
        
        [Test]
        public void RemoveListener_When_called_Should_set_component_interactable_to_false()
        {
            // Arrange
            ButtonBinder sut = CreateSut();
            var actual = Substitute.For<UnityAction>();
            sut.AddListener(actual);
            // Act
            sut.RemoveListener(actual);
            // Assert
            sut.GetComponent<Button>().interactable.Should().BeFalse();
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