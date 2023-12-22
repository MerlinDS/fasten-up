using FastenUp.Runtime.Bindables;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableSprite))]
    public class BindableSpriteTest
    {

        [Test]
        public void Set_Value_When_type_is_sprite_gameObject_has_Image_Should_set_sprite_to_Image()
        {
            // Arrange
            var expected = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.one);
            var sut = CreateSut();
            // Act
            sut.SetValue(expected);
            // Assert
            sut.GetComponent<Image>().sprite.Should().Be(expected);
        }

        [Test]
        public void Set_Value_When_type_is_string_Should_load_sprite_from_resources()
        {
            // Arrange
            const string path = "FastenUp - Icon";
            var expected = Resources.Load<Sprite>(path);
            var sut = CreateSut();
            // Act
            sut.SetValue(path);
            // Assert
            sut.GetComponent<Image>().sprite.Should().Be(expected);
        }

        [Test]
        public void Set_Value_When_type_is_string_and_value_is_null_or_empty_Should_set_null_to_sprite()
        {
            // Arrange
            var sut = CreateSut();
            // Act
            sut.SetValue(string.Empty);
            // Assert
            sut.GetComponent<Image>().sprite.Should().BeNull();
        }

        private static BindableSprite CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableSpriteTest));
            var sut = gameObject.AddComponent<BindableSprite>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}