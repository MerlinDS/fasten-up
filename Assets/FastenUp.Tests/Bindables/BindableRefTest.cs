using System;
using FastenUp.Runtime.Bindables;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableRef<>))]
    public class BindableRefTest
    {
        [Test]
        public void Bind_When_reference_is_null_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableRef<TestReference>();
            Action act = () => sut.As<IBindableRef<TestReference>>().Bind(null);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Unbind_When_reference_is_null_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableRef<TestReference>();
            Action act = () => sut.As<IBindableRef<TestReference>>().Unbind(null);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }
        
        [Test]
        public void Bind_When_reference_is_not_null_Should_set_value_and_invoke_OnRefChanged()
        {
            //Arrange
            var expected = Substitute.For<TestReference>();
            var sut = new BindableRef<TestReference>();
            var onRefChanged = Substitute.For<Action>();
            sut.OnRefChanged += onRefChanged;
            //Act
            sut.As<IBindableRef<TestReference>>().Bind(expected);
            //Assert
            sut.Value.Should().Be(expected);
            onRefChanged.Received(1).Invoke();
        }
        
        [Test]
        public void Unbind_When_reference_is_not_null_Should_set_value_to_null_and_invoke_OnRefChanged()
        {
            //Arrange
            var expected = Substitute.For<TestReference>();
            var sut = new BindableRef<TestReference>();
            var onRefChanged = Substitute.For<Action>();
            sut.As<IBindableRef<TestReference>>().Bind(expected);
            sut.OnRefChanged += onRefChanged;
            //Act
            sut.As<IBindableRef<TestReference>>().Unbind(expected);
            //Assert
            sut.Value.Should().BeNull();
            onRefChanged.Received(1).Invoke();
        }


        internal abstract class TestReference
        {
        }
    }
}