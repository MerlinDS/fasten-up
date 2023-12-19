using FastenUp.Runtime.Adapters;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Tests.Adapters
{
    [TestFixture]
    [TestOf(typeof(ImageAdapter))]
    public class ImageAdapterTest
    {

        [Test]
        public void Create_for_sprite_When_gameObject_has_Image_Should_return_adapter()
        {
            //Arrange
            var gameObject = new GameObject(nameof(ImageAdapterTest));
            gameObject.AddComponent<Image>();
            //Act
            var adapter = ImageAdapter.Create<Sprite>(gameObject);
            //Assert
            adapter.Should().NotBeNull();
        }
        
        [Test]
        public void Create_for_sprite_When_gameObject_has_no_Image_Should_return_null()
        {
            //Arrange
            var gameObject = new GameObject(nameof(ImageAdapterTest));
            //Act
            var adapter = ImageAdapter.Create<Sprite>(gameObject);
            //Assert
            adapter.Should().BeNull();
        }
        
        [Test]
        public void Value_get_for_sprite_When_has_suitable_component_Should_return_value()
        {
            //Arrange
            var gameObject = new GameObject(nameof(ImageAdapterTest));
            var component = gameObject.AddComponent<Image>();
            var expected = Sprite.Create(new Texture2D(1,1), new Rect(0,0,1,1), Vector2.one);
            component.sprite = expected;
            var adapter = ImageAdapter.Create<Sprite>(gameObject);
            //Act
            var value = adapter!.Value;
            //Assert
            value.Should().Be(expected);
        }
        
        [Test]
        public void Value_set_for_sprite_When_has_suitable_component_Should_set_value()
        {
            //Arrange
            var gameObject = new GameObject(nameof(ImageAdapterTest));
            var component = gameObject.AddComponent<Image>();
            var adapter = ImageAdapter.Create<Sprite>(gameObject);
            var expected = Sprite.Create(new Texture2D(1,1), new Rect(0,0,1,1), Vector2.one);
            //Act
            adapter!.Value = expected;
            //Assert
            component.sprite.Should().Be(expected);
        }
    }
}