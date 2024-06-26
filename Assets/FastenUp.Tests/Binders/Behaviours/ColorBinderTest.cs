﻿using FastenUp.Runtime.Binders.Behaviours;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.Behaviours
{
    [TestFixture]
    [TestOf(typeof(ColorBinder))]
    public class ColorBinderTest
    {
        [Test]
        public void SetValue_When_gameObject_has_Image_Should_set_color_to_Image()
        {
            //Arrange
            Color expected = Color.red;
            ColorBinder sut = CreateSut();
            //Act
            sut.SetValue(expected);
            //Assert
            sut.GetComponent<Image>().color.Should().Be(expected);
        }

        private static ColorBinder CreateSut()
        {
            var gameObject = new GameObject(nameof(ColorBinderTest));
            gameObject.AddComponent<Image>();
            var sut = gameObject.AddComponent<ColorBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}