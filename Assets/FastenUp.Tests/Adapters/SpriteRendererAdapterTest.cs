using FastenUp.Runtime.Adapters;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace FastenUp.Tests.Adapters
{
    [TestFixture]
    [TestOf(typeof(SpriteRendererAdapter))]
    public class SpriteRendererAdapterTest
    {

        [Test]
        public void Create_for_sprite_When_gameObject_has_SpriteRenderer_Should_return_adapter()
        {
            //Arrange
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            gameObject.AddComponent<SpriteRenderer>();
            //Act
            var adapter = SpriteRendererAdapter.Create<Sprite>(gameObject);
            //Assert
            adapter.Should().NotBeNull();
        }
        
        [Test]
        public void Create_for_sprite_When_gameObject_has_no_SpriteRenderer_Should_return_null()
        {
            //Arrange
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            //Act
            var adapter = SpriteRendererAdapter.Create<Sprite>(gameObject);
            //Assert
            adapter.Should().BeNull();
        }
        
        [Test]
        public void Crate_for_color_When_gameObject_has_SpriteRenderer_Should_return_adapter()
        {
            //Arrange
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            gameObject.AddComponent<SpriteRenderer>();
            //Act
            var adapter = SpriteRendererAdapter.Create<Color>(gameObject);
            //Assert
            adapter.Should().NotBeNull();
        }
        
        [Test]
        public void Crate_for_color_When_gameObject_has_no_SpriteRenderer_Should_return_null()
        {
            //Arrange
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            //Act
            var adapter = SpriteRendererAdapter.Create<Color>(gameObject);
            //Assert
            adapter.Should().BeNull();
        }
        
        [Test]
        public void Value_get_for_sprite_When_has_suitable_component_Should_return_value()
        {
            //Arrange
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            var component = gameObject.AddComponent<SpriteRenderer>();
            var expected = Sprite.Create(new Texture2D(1,1), new Rect(0,0,1,1), Vector2.one);
            component.sprite = expected;
            var adapter = SpriteRendererAdapter.Create<Sprite>(gameObject);
            //Act
            var value = adapter!.Value;
            //Assert
            value.Should().Be(expected);
        }
        
        [Test]
        public void Value_set_for_sprite_When_has_suitable_component_Should_set_value()
        {
            //Arrange
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            var component = gameObject.AddComponent<SpriteRenderer>();
            var adapter = SpriteRendererAdapter.Create<Sprite>(gameObject);
            var expected = Sprite.Create(new Texture2D(1,1), new Rect(0,0,1,1), Vector2.one);
            //Act
            adapter!.Value = expected;
            //Assert
            component.sprite.Should().Be(expected);
        }
        
        [Test]
        public void Value_get_for_color_When_has_suitable_component_Should_return_value()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            gameObject.AddComponent<SpriteRenderer>().color = expected;
            var adapter = SpriteRendererAdapter.Create<Color>(gameObject);
            //Act
            var value = adapter!.Value;
            //Assert
            value.Should().Be(expected);
        }
        
        [Test]
        public void Value_set_for_color_When_has_suitable_component_Should_set_value()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = new GameObject(nameof(SpriteRendererAdapterTest));
            var component = gameObject.AddComponent<SpriteRenderer>();
            var adapter = SpriteRendererAdapter.Create<Color>(gameObject);
            //Act
            adapter!.Value = expected;
            //Assert
            component.color.Should().Be(expected);
        }
    }
}