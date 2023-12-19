using FastenUp.Runtime.Adapters;
using FluentAssertions;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Tests.Adapters
{
    [TestFixture]
    [TestOf(typeof(GraphicAdapter))]
    public class GraphicAdapterTest
    {
        [Test]
        public void Create_When_gameObject_has_Image_Should_return_adapter()
        {
            //Arrange
            var gameObject = new GameObject(nameof(GraphicAdapterTest));
            gameObject.AddComponent<Image>();
            //Act
            var adapter = GraphicAdapter.Create<Color>(gameObject);
            //Assert
            adapter.Should().NotBeNull();
        }

        [Test]
        public void Create_When_gameObject_has_Text_Should_return_adapter()
        {
            //Arrange
            var gameObject = new GameObject(nameof(GraphicAdapterTest));
            gameObject.AddComponent<TextMeshProUGUI>();
            //Act
            var adapter = GraphicAdapter.Create<Color>(gameObject);
            //Assert
            adapter.Should().NotBeNull();
        }

        [Test]
        public void Value_get_When_has_suitable_component_Should_return_value()
        {
            //Arrange
            var gameObject = new GameObject(nameof(GraphicAdapterTest));
            var component = gameObject.AddComponent<Image>();
            component.color = Color.red;
            var adapter = GraphicAdapter.Create<Color>(gameObject);
            //Act
            var value = adapter!.Value;
            //Assert
            value.Should().Be(component.color);
        }

        [Test]
        public void Value_set_When_has_suitable_component_Should_set_value()
        {
            //Arrange
            var gameObject = new GameObject(nameof(GraphicAdapterTest));
            var component = gameObject.AddComponent<Image>();
            var adapter = GraphicAdapter.Create<Color>(gameObject);
            //Act
            adapter!.Value = Color.red;
            //Assert
            component.color.Should().Be(Color.red);
        }

        [Test]
        public void Create_When_gameObject_has_no_suitable_component_Should_return_null()
        {
            //Arrange
            var gameObject = new GameObject(nameof(GraphicAdapterTest));
            //Act
            var adapter = GraphicAdapter.Create<Color>(gameObject);
            //Assert
            adapter.Should().BeNull();
        }
    }
}