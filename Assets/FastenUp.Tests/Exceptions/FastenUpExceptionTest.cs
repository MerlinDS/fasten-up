using System;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace FastenUp.Tests.Exceptions
{
    [TestFixture]
    [TestOf(typeof(FastenUpException))]
    public class FastenUpExceptionTest
    {
        [Test]
        public void Constructor_When_called_with_message_Should_sets_message_property()
        {
            // Arrange & Act
            var exception = new FastenUpException("Test message");
            // Assert
            exception.Message.Should().Be("Test message");
        }

        [Test]
        public void Constructor_When_called_with_message_and_inner_exception_Should_sets_message_and_inner_exception_properties()
        {
            // Arrange
            var innerException = new Exception("Inner exception");
            // Act
            var exception = new FastenUpException("Test message", innerException);
            // Assert
            exception.Message.Should().Be("Test message");
            exception.InnerException.Should().Be(innerException);
        }
    }
}