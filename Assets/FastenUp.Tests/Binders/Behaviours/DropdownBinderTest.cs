using System;
using System.Collections.Generic;
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
    [TestOf(typeof(DropdownBinder))]
    public class DropdownBinderTest
    {
        [Test]
        public void SetValue_When_type_is_int_Should_set_value_to_dropdown_silently()
        {
            // Arrange
            const int expected = 4;
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            dropdown.value.Should().Be(expected);
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void SetValue_When_type_is_array_of_strings_Should_set_options_to_dropdown_silently()
        {
            // Arrange
            var expected = new[] { "1", "2", "3", "4", "5" };
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            dropdown.options.Should().HaveCount(expected.Length);
            foreach (TMP_Dropdown.OptionData option in dropdown.options)
            {
                expected.Should().Contain(option.text);
            }

            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void SetValue_When_type_is_array_of_strings_but_value_is_null_Should_clear_options()
        {
            // Arrange
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue((string[])null);
            // Assert
            dropdown.options.Should().BeEmpty();
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void SetValue_When_type_is_array_of_strings_but_value_is_empty_Should_clean_options()
        {
            // Arrange
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue(Array.Empty<string>());
            // Assert
            dropdown.options.Should().BeEmpty();
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
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            dropdown.options.Should().BeEquivalentTo(expected);
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void SetValue_When_type_is_list_of_option_data_but_value_is_null_Should_clear_options()
        {
            // Arrange
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue((List<TMP_Dropdown.OptionData>)null);
            // Assert
            dropdown.options.Should().BeEmpty();
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void SetValue_When_type_is_list_of_option_data_but_value_is_empty_Should_clear_options()
        {
            // Arrange
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            sut.SetValue(new List<TMP_Dropdown.OptionData>());
            // Assert
            dropdown.options.Should().BeEmpty();
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void GetValue_When_type_is_int_Should_return_value_from_dropdown()
        {
            // Arrange
            const int expected = 4;
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            dropdown.SetValueWithoutNotify(5);
            // Act
            int result = sut.GetValue();
            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public void OnBinderChanged_When_dropdown_value_changed_Should_invoke_OnBinderChanged()
        {
            // Arrange
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            // Act
            dropdown.value = 4;
            // Assert
            onValueChanged.Received(1).Invoke(sut);
        }

        [Test]
        public void OnBinderChanged_When_disabled_Should_not_invoke_OnBinderChanged()
        {
            // Arrange
            DropdownBinder sut = CreateSut();
            var dropdown = sut.GetComponent<TMP_Dropdown>();
            dropdown.AddOptions(new List<string> { "1", "2", "3", "4", "5" });
            var onValueChanged = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += onValueChanged;
            sut.ExecuteOnDisable();
            // Act
            dropdown.value = 4;
            // Assert
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        private static DropdownBinder CreateSut()
        {
            var gameObject = new GameObject();
            var sut = gameObject.AddComponent<DropdownBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}