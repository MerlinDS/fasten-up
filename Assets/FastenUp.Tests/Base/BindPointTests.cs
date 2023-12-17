using FastenUp.Runtime.Base;
using FluentAssertions;
using NUnit.Framework;

namespace FastenUp.Tests.Base
{
    [TestFixture]
    [TestOf(typeof(BindPoint<>))]
    public class BindPointTests
    {
        
        [Test]
        public void ValueProperty_Should_set_and_return_specified_value()
        {
            //Act
            var sut = new BindPoint<bool>
            {
                Value = true
            };
            //Assert
            sut.Value.Should().BeTrue();
        }
    }
}