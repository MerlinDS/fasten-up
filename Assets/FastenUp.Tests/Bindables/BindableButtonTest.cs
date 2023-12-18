using System;
using FastenUp.Runtime.Bindables;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableButton))]
    public class BindableButtonTest
    {
        [Test]
        public void Set_Value_When_value_is_action_Should_add_listener_to_click()
        {
            //Arrange
            var expected = Substitute.For<UnityAction>();
            var sut = CreateSut();
            var button = sut.GetComponent<Button>();
            //Act
            sut.SetValue(expected);
            //Assert
            button.onClick.Invoke();
            expected.Received(1).Invoke();
        }

        [Test]
        public void Set_Value_When_value_is_null_Should_not_throw_exceptions()
        {
            //Arrange
            var sut = CreateSut();
            Action act = () => sut.SetValue(null);
            //Act & Assert
            act.Should().NotThrow();
        }

        [Test]
        public void OnDisable_Should_remove_all_listeners()
        {
            //Arrange
            var sut = CreateSut();
            var button = sut.GetComponent<Button>();
            var action = Substitute.For<UnityAction>();
            sut.SetValue(action);
            //Act
            sut.ExecuteOnDisable();
            //Assert
            button.onClick.Invoke();
            action.DidNotReceive().Invoke();
        }

        private static BindableButton CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableButtonTest));
            var sut = gameObject.AddComponent<BindableButton>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}