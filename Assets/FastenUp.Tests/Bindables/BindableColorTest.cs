using System;
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
            var sut = CreateSut(o=>o.AddComponent<Image>());
            //Act
            sut.SetValue(expected);
            //Assert
            sut.GetComponent<Image>().color.Should().Be(expected);
        }

        [Test]
        public void SetValue_When_gameObject_has_SpriteRenderer_Should_set_color_to_SpriteRenderer()
        {
            //Arrange
            var expected = Color.red;
            var sut = CreateSut(o=>o.AddComponent<SpriteRenderer>());
            //Act
            sut.SetValue(expected);
            //Assert
            sut.GetComponent<SpriteRenderer>().color.Should().Be(expected);
        }
        
        [Test]
        public void SetValue_When_gameObject_has_no_suitable_component_Should_not_throw()
        {
            //Arrange
            var sut = CreateSut();
            //Act
            Action action = () => sut.SetValue(Color.red);
            //Assert
            action.Should().NotThrow();
        }

        private static BindableColor CreateSut(Action<GameObject> factory = null)
        {
            var gameObject = new GameObject(nameof(BindableColorTest));
            factory?.Invoke(gameObject);
            var sut = gameObject.AddComponent<BindableColor>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}