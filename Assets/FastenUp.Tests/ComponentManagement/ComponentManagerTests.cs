using System;
using FastenUp.Runtime.ComponentManagement;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace FastenUp.Tests.ComponentManagement
{
    [TestFixture]
    [TestOf(typeof(ComponentManager))]
    public class ComponentManagerTests
    {
        [Test]
        public void SetValue_When_source_has_image_Should_set_color_in_image()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(true, false);

            var sut = new ComponentManager(gameObject);
            var (imageSetter, spriteRendererSetter) = RegisterSetters(sut);
            //Act
            sut.SetValue(expected);
            //Assert
            imageSetter.Received(1).Invoke(Arg.Any<Image>(), expected);
            spriteRendererSetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>(), Arg.Any<Color>());
        }

        [Test]
        public void SetValue_When_source_has_sprite_renderer_Should_set_color_in_sprite_renderer()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(false, true);

            var sut = new ComponentManager(gameObject);
            var (imageSetter, spriteRendererSetter) = RegisterSetters(sut);
            //Act
            sut.SetValue(expected);
            //Assert
            spriteRendererSetter.Received(1).Invoke(Arg.Any<SpriteRenderer>(), expected);
            imageSetter.DidNotReceive().Invoke(Arg.Any<Image>(), Arg.Any<Color>());
        }

        [Test]
        public void SetValue_When_source_has_image_and_sprite_renderer_Should_set_color_in_both()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(true, true);

            var sut = new ComponentManager(gameObject);
            var (imageSetter, spriteRendererSetter) = RegisterSetters(sut);
            //Act
            sut.SetValue(expected);
            //Assert
            imageSetter.Received(1).Invoke(Arg.Any<Image>(), expected);
            spriteRendererSetter.Received(1).Invoke(Arg.Any<SpriteRenderer>(), expected);
        }

        [Test]
        public void SetValue_When_source_has_no_image_and_sprite_renderer_Should_not_set_color_in_any()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(false, false);

            var sut = new ComponentManager(gameObject);
            var (imageSetter, spriteRendererSetter) = RegisterSetters(sut);
            //Act
            sut.SetValue(expected);
            //Assert
            imageSetter.DidNotReceive().Invoke(Arg.Any<Image>(), Arg.Any<Color>());
            spriteRendererSetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>(), Arg.Any<Color>());
        }

        [Test]
        public void SetValue_When_manager_has_no_setters_Should_not_set_value()
        {
            //Arrange
            var unexpected = Color.red;
            var gameObject = CreateGameObject(true, true);

            var sut = new ComponentManager(gameObject);
            //Act
            sut.SetValue(unexpected);
            //Assert
            gameObject.GetComponent<Image>().color.Should().NotBe(unexpected);
            gameObject.GetComponent<SpriteRenderer>().color.Should().NotBe(unexpected);
        }

        [Test]
        public void GetValue_When_source_has_image_Should_return_color_from_image()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(true, false);

            var sut = new ComponentManager(gameObject);
            var (imageGetter, spriteRendererGetter) = RegisterGetters(sut);
            imageGetter.Invoke(Arg.Any<Image>()).Returns(expected);
            //Act
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
            imageGetter.Received(1).Invoke(Arg.Any<Image>());
            spriteRendererGetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>());
        }

        [Test]
        public void GetValue_When_source_has_sprite_renderer_Should_return_color_from_sprite_renderer()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(false, true);

            var sut = new ComponentManager(gameObject);
            var (imageGetter, spriteRendererGetter) = RegisterGetters(sut);
            spriteRendererGetter.Invoke(Arg.Any<SpriteRenderer>()).Returns(expected);
            //Act
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
            spriteRendererGetter.Received(1).Invoke(Arg.Any<SpriteRenderer>());
            imageGetter.DidNotReceive().Invoke(Arg.Any<Image>());
        }

        [Test]
        public void GetValue_When_source_has_image_and_sprite_renderer_Should_return_color_from_image()
        {
            //Arrange
            var expected = Color.red;
            var gameObject = CreateGameObject(true, true);

            var sut = new ComponentManager(gameObject);
            var (imageGetter, spriteRendererGetter) = RegisterGetters(sut);
            imageGetter.Invoke(Arg.Any<Image>()).Returns(expected);
            //Act
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
            imageGetter.Received(1).Invoke(Arg.Any<Image>());
            spriteRendererGetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>());
        }

        [Test]
        public void GetValue_When_source_has_no_image_and_sprite_renderer_Should_return_default()
        {
            //Arrange
            var expected = default(Color);
            var gameObject = CreateGameObject(false, false);

            var sut = new ComponentManager(gameObject);
            var (imageGetter, spriteRendererGetter) = RegisterGetters(sut);
            //Act
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
            spriteRendererGetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>());
            imageGetter.DidNotReceive().Invoke(Arg.Any<Image>());
        }

        [Test]
        public void GetValue_When_manager_has_no_getters_Should_return_default_value()
        {
            //Arrange
            var expected = default(Color);
            var gameObject = CreateGameObject(true, true);

            var sut = new ComponentManager(gameObject);
            //Act
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void GetValue_When_manager_has_getter_of_other_type_Should_return_default_value()
        {
            //Arrange
            var expected = default(Color);
            var gameObject = CreateGameObject(true, true);

            var sut = new ComponentManager(gameObject);
            var getter = Substitute.For<Func<Image, int>>();
            sut.Register<int>(p => p.AddGetter(getter));
            //Act
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
            getter.DidNotReceive().Invoke(Arg.Any<Image>());
        }

        [Test]
        public void UnregisterAll_When_manager_has_getter_and_setters_Should_remove_them()
        {
            //Arrange
            var expected = default(Color);
            var gameObject = CreateGameObject(true, true);

            var sut = new ComponentManager(gameObject);
            var setters = RegisterSetters(sut);
            var getters = RegisterGetters(sut);
            //Act
            sut.UnregisterAll();
            sut.SetValue(Color.red);
            var actual = sut.GetValue<Color>();
            //Assert
            actual.Should().Be(expected);
            setters.imageSetter.DidNotReceive().Invoke(Arg.Any<Image>(), Arg.Any<Color>());
            setters.spriteRendererSetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>(), Arg.Any<Color>());
            getters.imageGetter.DidNotReceive().Invoke(Arg.Any<Image>());
            getters.spriteRendererGetter.DidNotReceive().Invoke(Arg.Any<SpriteRenderer>());
        }

        private static GameObject CreateGameObject(bool hasImage, bool hasSpriteRenderer)
        {
            var gameObject = new GameObject(nameof(ComponentManagerTests));
            if (hasImage)
                gameObject.AddComponent<Image>();
            if (hasSpriteRenderer)
                gameObject.AddComponent<SpriteRenderer>();
            return gameObject;
        }

        private static (Action<Image, Color> imageSetter, Action<SpriteRenderer, Color> spriteRendererSetter)
            RegisterSetters(IComponentProcessorRegistrar registrar)
        {
            var imageSetter = Substitute.For<Action<Image, Color>>();
            var spriteRendererSetter = Substitute.For<Action<SpriteRenderer, Color>>();
            registrar.Register<Color>(p => p
                .AddSetter(imageSetter)
                .AddSetter(spriteRendererSetter)
            );
            return (imageSetter, spriteRendererSetter);
        }

        private static (Func<Image, Color> imageGetter, Func<SpriteRenderer, Color> spriteRendererGetter)
            RegisterGetters(IComponentProcessorRegistrar registrar)
        {
            var imageSetter = Substitute.For<Func<Image, Color>>();
            var spriteRendererSetter = Substitute.For<Func<SpriteRenderer, Color>>();
            registrar.Register<Color>(p => p
                .AddGetter(imageSetter)
                .AddGetter(spriteRendererSetter)
            );
            return (imageSetter, spriteRendererSetter);
        }
    }
}