using FastenUp.Runtime.Bindables;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableColor))]
    public class BindableColorTest
    {
        [Test]
        public void SetValue_When_gameObject_has_Image_Should_set_color_to_Image()
        {
            //Arrange
            var expected = Color.red;
            var sut = CreateSut();
            //Act
            sut.SetValue(expected);
            //Assert
            sut.GetComponent<Image>().color.Should().Be(expected);
        }

        private static BindableColor CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableColorTest));
            gameObject.AddComponent<Image>();
            var sut = gameObject.AddComponent<BindableColor>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}