using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Delegates;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableInputField))]
    public class BindableInputFieldTest
    {

        [Test]
        public void SetValue_When_type_string_Should_set_value_to_input_field()
        {
            // Arrange
            const string expected = "Test";
            var sut = CreateSut();
            // Act
            sut.SetValue(expected);
            // Assert
            sut.GetComponent<TMP_InputField>().text.Should().Be(expected);
        }
        
        [Test]
        public void GetValue_When_type_string_Should_return_value_from_input_field()
        {
            // Arrange
            const string expected = "Test";
            var sut = CreateSut();
            sut.GetComponent<TMP_InputField>().text = expected;
            // Act
            var result = sut.GetValue();
            // Assert
            result.Should().Be(expected);
        }
        
        [Test]
        public void OnBindableChanged_When_input_field_value_changed_Should_invoke_OnBindableChanged()
        {
            // Arrange
            const string expected = "Test";
            var sut = CreateSut();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            sut.GetComponent<TMP_InputField>().text = expected;
            // Assert
            onValueChanged.Received(1).Invoke(sut);
        }
        
        [Test]
        public void OnBindableChanged_When_disabled_Should_not_invoke_OnBindableChanged()
        {
            // Arrange
            const string expected = "Test";
            var sut = CreateSut();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            sut.ExecuteOnDisable();
            // Act
            sut.GetComponent<TMP_InputField>().text = expected;
            // Assert
            onValueChanged.DidNotReceive().Invoke(sut);
        }
        
        private static BindableInputField CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableInputFieldTest));
            var sut = gameObject.AddComponent<BindableInputField>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}