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
    [TestOf(typeof(BindableSlider))]
    public class BindableSliderTest
    {
        [Test]
        public void Set_Value_When_type_float_Should_set_value_to_slider_silently()
        {
            // Arrange
            const float expected = 0.5f;
            var sut = CreateSut();
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            sut.GetComponent<Slider>().value.Should().Be(expected);
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void Set_Value_When_type_int_Should_set_value_to_slider_silently()
        {
            // Arrange
            const int expected = 5;
            var sut = CreateSut();
            var slider = sut.GetComponent<Slider>();
            slider.maxValue = 10;
            slider.wholeNumbers = true;
            var onValueChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onValueChanged;
            // Act
            sut.SetValue(expected);
            // Assert
            slider.value.Should().Be(expected);
            onValueChanged.DidNotReceive().Invoke(sut);
        }

        [Test]
        public void Set_Value_When_type_Vector2_Should_set_min_and_max_value_to_slider()
        {
            // Arrange
            var expected = new Vector2(-0.5f, 0.5f);
            var sut = CreateSut();
            var slider = sut.GetComponent<Slider>();
            // Act
            sut.SetValue(expected);
            // Assert
            slider.minValue.Should().Be(expected.x);
            slider.maxValue.Should().Be(expected.y);
            slider.wholeNumbers.Should().BeFalse();
        }

        [Test]
        public void Set_Value_When_type_Vector2Int_Should_set_min_and_max_value_to_slider()
        {
            // Arrange
            var expected = new Vector2Int(-5, 5);
            var sut = CreateSut();
            var slider = sut.GetComponent<Slider>();
            slider.wholeNumbers = true;
            // Act
            sut.SetValue(expected);
            // Assert
            slider.minValue.Should().Be(expected.x);
            slider.maxValue.Should().Be(expected.y);
            slider.wholeNumbers.Should().BeTrue();
        }

        [Test]
        public void Get_Value_When_type_float_Should_return_value_from_slider()
        {
            // Arrange
            const float expected = 0.5f;
            var sut = CreateSut();
            sut.GetComponent<Slider>().SetValueWithoutNotify(expected);
            // Act
            var actual = sut.GetValue();
            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Get_Value_When_type_int_Should_return_value_from_slider()
        {
            // Arrange
            const int expected = 5;
            var sut = CreateSut();
            var slider = sut.GetComponent<Slider>();
            slider.maxValue = 10;
            slider.wholeNumbers = true;
            slider.SetValueWithoutNotify(expected);
            // Act
            var actual = sut.As<IGettableBindable<int>>().GetValue();
            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void OnValueChanged_When_slider_value_changed_Should_invoke_OnBindableChanged()
        {
            // Arrange
            var sut = CreateSut();
            var onBindableChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onBindableChanged;
            // Act
            sut.GetComponent<Slider>().value = 0.5f;
            // Assert
            onBindableChanged.Received(1).Invoke(sut);
        }

        [Test]
        public void OnValueChanged_When_slider_value_changed_Should_not_invoke_OnBindableChanged_if_disabled()
        {
            // Arrange
            var sut = CreateSut();
            var onBindableChanged = Substitute.For<OnBindableChanged>();
            sut.OnBindableChanged += onBindableChanged;
            sut.ExecuteOnDisable();
            // Act
            sut.GetComponent<Slider>().value = 0.5f;
            // Assert
            onBindableChanged.DidNotReceive().Invoke(sut);
        }


        private static BindableSlider CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableSliderTest));
            var sut = gameObject.AddComponent<BindableSlider>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}