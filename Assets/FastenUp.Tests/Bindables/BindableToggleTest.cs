using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Delegates;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableToggle))]
    public class BindableToggleTest
    {
        [Test]
        public void Set_Value_When_called_Should_set_isOn_and_not_invoke_onValueChanged()
        {
            //Arrange
            var sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            //Act
            sut.SetValue(true);
            //Assert
            toggle.isOn.Should().BeTrue();
        }

        [Test]
        public void Get_Value_When_called_Should_return_isOn()
        {
            //Arrange
            var sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            //Act
            toggle.isOn = true;
            //Assert
            sut.GetValue().Should().BeTrue();
        }

        [Test]
        public void OnBindableChanged_When_toggle_change_isOn_Should_invoke_event()
        {
            //Arrange
            var sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            //Act
            toggle.isOn = true;
            //Assert
            onValueChanged.Received(1).Invoke(sut);
        }

        [Test]
        public void OnDisable_When_called_Should_remove_listeners()
        {
            //Arrange
            var sut = CreateSut();
            var toggle = sut.GetComponent<Toggle>();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            //Act
            sut.ExecuteOnDisable();
            //Assert
            toggle.isOn = true;
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        private static BindableToggle CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableToggleTest));
            var sut = gameObject.AddComponent<BindableToggle>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}