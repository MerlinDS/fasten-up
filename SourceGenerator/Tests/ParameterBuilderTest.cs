using FluentAssertions;
using NUnit.Framework;

namespace FastenUp.SourceGenerator.Tests
{
    [TestFixture]
    [TestOf(typeof(ParameterBuilder))]
    public class ParameterBuilderTest
    {
        [TestCase(ParameterModifier.None, "int", "a", "int a")]
        [TestCase(ParameterModifier.Ref, "int", "a", "ref int a")]
        [TestCase(ParameterModifier.Out, "int", "a", "out int a")]
        [TestCase(ParameterModifier.Params, "int", "a", "params int a")]
        [TestCase(ParameterModifier.This, "int", "a", "this int a")]
        [TestCase(ParameterModifier.In, "int", "a", "in int a")]
        [TestCase(ParameterModifier.None, "int", null, "", Reason = "Name is null")]
        [TestCase(ParameterModifier.None, null, "a", "", Reason = "Type is null")]
        [TestCase(ParameterModifier.None, null, null, "", Reason = "Type and name are null")]
        public void Build(ParameterModifier modifier, string type, string name, string expected)
        {
            // Arrange
            var sut = new ParameterBuilder
            {
                Modifier = modifier,
                Type = type,
                Name = name
            };
            // Act
            var actual = sut.Build();
            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Build_When_has_default_value_Should_return_parameter_with_default_value()
        {
            // Arrange
            var sut = new ParameterBuilder
            {
                Type = "int",
                Name = "a",
                DefaultValue = "1"
            };
            // Act
            var actual = sut.Build();
            // Assert
            actual.Should().Be("int a = 1");
        }
    }
}