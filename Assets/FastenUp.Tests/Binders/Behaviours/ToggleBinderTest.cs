using FastenUp.Runtime.Binders.Behaviours;
using FastenUp.Runtime.Delegates;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.Behaviours
{
    [TestFixture]
    [TestOf(typeof(ToggleBinder))]
    public class ToggleBinderTest
    {
        [Test]
        public void Set_Value_When_called_Should_set_isOn_and_not_invoke_onValueChanged()
        {
            //Arrange
            ToggleBinder sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            //Act
            sut.SetValue(true);
            //Assert
            toggle.isOn.Should().BeTrue();
        }

        [Test]
        public void Get_Value_When_called_Should_return_isOn()
        {
            //Arrange
            ToggleBinder sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            //Act
            toggle.isOn = true;
            //Assert
            sut.GetValue().Should().BeTrue();
        }

        [Test]
        public void OnBinderChanged_When_toggle_change_isOn_Should_invoke_event()
        {
            //Arrange
            ToggleBinder sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            var onBinderChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onBinderChanged;
            //Act
            toggle.isOn = true;
            //Assert
            onBinderChanged.Received(1).Invoke(sut);
        }

        [Test]
        public void OnDisable_When_called_Should_remove_listeners()
        {
            //Arrange
            ToggleBinder sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            //Act
            sut.ExecuteOnDisable();
            //Assert
            toggle.isOn = true;
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        private static ToggleBinder CreateSut()
        {
            var gameObject = new GameObject(nameof(ToggleBinderTest));
            var sut = gameObject.AddComponent<ToggleBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}