using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace FastenUp.Tests.Exceptions
{
    [TestFixture]
    [TestOf(typeof(FastenUpBindableException))]
    public class FastenUpBindableExceptionTest
    {
        [Test]
        public void Ctor()
        {
            //Arrange
            var gameObject = new GameObject(nameof(FastenUpBindableExceptionTest));
            //Act
            var exception = new FastenUpBindableException("Test", gameObject);
            //Assert
            exception.GameObject.Should().Be(gameObject);
        }
    }
}