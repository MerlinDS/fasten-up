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
    [TestOf(typeof(BindableSprite))]
    public class BindableSpriteTest
    {

        [Test]
        public void Set_Value_When_no_suitable_component_found_Should_not_throw()
        {
            // Arrange
            var sut = CreateSut();
            // Act
            Action action = () => sut.SetValue(default(Sprite));
            // Assert
            action.Should().NotThrow();
        }
        
        [Test]
        public void Set_Value_When_type_is_sprite_gameObject_has_Image_Should_set_sprite_to_Image()
        {
            // Arrange
            var expected = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.one);
            var sut = CreateSut(o=>o.AddComponent<Image>());
            // Act
            sut.SetValue(expected);
            // Assert
            sut.GetComponent<Image>().sprite.Should().Be(expected);
        }
        
        [Test]
        public void Set_Value_When_type_is_sprite_gameObject_has_SpriteRenderer_Should_set_sprite_to_SpriteRenderer()
        {
            // Arrange
            var expected = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.one);
            var sut = CreateSut(o=>o.AddComponent<SpriteRenderer>());
            // Act
            sut.SetValue(expected);
            // Assert
            sut.GetComponent<SpriteRenderer>().sprite.Should().Be(expected);
        }
        
        [Test]
        public void Set_Value_When_type_is_string_Should_load_sprite_from_resources()
        {
            // Arrange
            const string path = "FastenUp - Icon";
            var expected = Resources.Load<Sprite>(path);
            var sut = CreateSut(o=>o.AddComponent<Image>());
            // Act
            sut.SetValue(path);
            // Assert
            sut.GetComponent<Image>().sprite.Should().Be(expected);
        }
        
        [Test]
        public void Set_Value_When_type_is_string_and_value_is_null_or_empty_Should_set_null_to_sprite()
        {
            // Arrange
            var sut = CreateSut(o=>o.AddComponent<Image>());
            // Act
            sut.SetValue(string.Empty);
            // Assert
            sut.GetComponent<Image>().sprite.Should().BeNull();
        }

        private static BindableSprite CreateSut(Action<GameObject> factory = null)
        {
            var gameObject = new GameObject(nameof(BindableSpriteTest));
            factory?.Invoke(gameObject);
            var sut = gameObject.AddComponent<BindableSprite>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}