using FastenUp.Runtime.Binders.Behaviours;
using FastenUp.Runtime.Delegates;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.Behaviours
{
    [TestFixture]
    [TestOf(typeof(InputFieldBinder))]
    public class InputFieldBinderTest
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
        public void OnBinderChanged_When_input_field_value_changed_Should_invoke_OnBinderChanged()
        {
            // Arrange
            const string expected = "Test";
            var sut = CreateSut();
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.GetComponent<TMP_InputField>().text = expected;
            // Assert
            onValueChanged.Received(1).Invoke(sut);
        }
        
        [Test]
        public void OnBinderChanged_When_disabled_Should_not_invoke_OnBinderChanged()
        {
            // Arrange
            const string expected = "Test";
            var sut = CreateSut();
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            sut.ExecuteOnDisable();
            // Act
            sut.GetComponent<TMP_InputField>().text = expected;
            // Assert
            onValueChanged.DidNotReceive().Invoke(sut);
        }
        
        private static InputFieldBinder CreateSut()
        {
            var gameObject = new GameObject(nameof(InputFieldBinderTest));
            var sut = gameObject.AddComponent<InputFieldBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}