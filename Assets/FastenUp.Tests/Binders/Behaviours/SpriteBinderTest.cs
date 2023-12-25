using FastenUp.Runtime.Binders.Behaviours;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.Behaviours
{
    [TestFixture]
    [TestOf(typeof(SpriteBinder))]
    public class SpriteBinderTest
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

        private static SpriteBinder CreateSut()
        {
            var gameObject = new GameObject(nameof(SpriteBinderTest));
            var sut = gameObject.AddComponent<SpriteBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}