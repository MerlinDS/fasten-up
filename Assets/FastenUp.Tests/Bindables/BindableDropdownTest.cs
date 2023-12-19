using System.Collections.Generic;
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
    [TestOf(typeof(BindableDropdown))]
    public class BindableDropdownTest
    {

        [Test]
        public void SetValue_When_type_is_int_Should_set_value_to_dropdown_silently()
        {
            // Arrange
            const int expected = 4;
            var sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> {"1", "2", "3", "4", "5"});
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            dropdown.value.Should().Be(expected);
            onValueChanged.DidNotReceive().Invoke(sut);
        }
        
        [Test]
        public void SetValue_When_type_is_list_of_strings_Should_set_options_to_dropdown_silently()
        {
            // Arrange
            var expected = new List<string> {"1", "2", "3", "4", "5"};
            var sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            dropdown.options.Should().HaveCount(expected.Count);
            foreach (var option in dropdown.options) 
                expected.Should().Contain(option.text);
            onValueChanged.DidNotReceive().Invoke(sut);
        }
        
        [Test]
        public void SetValue_When_type_is_list_of_option_data_Should_set_options_to_dropdown_silently()
        {
            // Arrange
            var expected = new List<TMP_Dropdown.OptionData>
            {
                new("1"),
                new("2"),
                new("3"),
                new("4"),
                new("5")
            };
            var sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            dropdown.options.Should().BeEquivalentTo(expected);
            onValueChanged.DidNotReceive().Invoke(sut);
        }
        
        [Test]
        public void GetValue_When_type_is_int_Should_return_value_from_dropdown()
        {
            // Arrange
            const int expected = 4;
            var sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> {"1", "2", "3", "4", "5"});
            dropdown.SetValueWithoutNotify(5);
            // Act
            var result = sut.GetValue();
            // Assert
            result.Should().Be(expected);
        }
        
        [Test]
        public void OnBindableChanged_When_dropdown_value_changed_Should_invoke_OnBindableChanged()
        {
            // Arrange
            var sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> {"1", "2", "3", "4", "5"});
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            dropdown.value = 4;
            // Assert
            onValueChanged.Received(1).Invoke(sut);
        }
        
        [Test]
        public void OnBindableChanged_When_disabled_Should_not_invoke_OnBindableChanged()
        {
            // Arrange
            var sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> {"1", "2", "3", "4", "5"});
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            sut.ExecuteOnDisable();
            // Act
            dropdown.value = 4;
            // Assert
            onValueChanged.DidNotReceive().Invoke(sut);
        }
        
        private static BindableDropdown CreateSut()
        {
            var gameObject = new GameObject();
            var sut = gameObject.AddComponent<BindableDropdown>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}