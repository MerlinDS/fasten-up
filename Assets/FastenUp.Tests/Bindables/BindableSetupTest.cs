using FastenUp.Runtime.Bindables;
using FluentAssertions;
using NUnit.Framework;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableSetup<>))]
    public class BindableSetupTest
    {

        [Test]
        public void Ctor_When_value_is_not_null_Should_set_value()
        {
            //Arrange & Act & Assert
            new BindableSetup<int>(1).Value.Should().Be(1);
        }
    }
}