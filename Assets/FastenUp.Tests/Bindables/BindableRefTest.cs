using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
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
        public void Bind_When_binder_is_null_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableRef<TestReference>();
            Action act = () => sut.As<IBindable<TestReference>>().Bind(null);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Unbind_When_binder_is_null_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableRef<TestReference>();
            Action act = () => sut.As<IBindable<TestReference>>().Unbind(null);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Bind_When_binder_is_not_IValueProvider_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableRef<TestReference>();
            Action act = () => sut.As<IBindable<TestReference>>().Bind(Substitute.For<IBinder<TestReference>>());
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Unbind_When_binder_is_not_IValueProvider_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableRef<TestReference>();
            Action act = () => sut.As<IBindable<TestReference>>().Unbind(Substitute.For<IBinder<TestReference>>());
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }
        
        [Test]
        public void Bind_When_binder_is_IValueProvider_Should_set_value_and_invoke_OnRefChanged()
        {
            //Arrange
            var expected = Substitute.For<TestReference>();
            var binder = Substitute.For<IValueProvider<TestReference>>();
            binder.GetValue().Returns(expected);
            
            var sut = new BindableRef<TestReference>();
            var onRefChanged = Substitute.For<Action>();
            sut.OnRefChanged += onRefChanged;
            //Act
            sut.As<IBindable<TestReference>>().Bind(binder);
            //Assert
            sut.Value.Should().Be(expected);
            onRefChanged.Received(1).Invoke();
        }
        
        [Test]
        public void Unbind_When_binder_is_IValueProvider_and_value_equals_provided_value_Should_set_value_to_null_and_invoke_OnRefChanged()
        {
            //Arrange
            var expected = Substitute.For<TestReference>();
            var binder = Substitute.For<IValueProvider<TestReference>>();
            binder.GetValue().Returns(expected);
            
            var sut = new BindableRef<TestReference>();
            var onRefChanged = Substitute.For<Action>();
            sut.OnRefChanged += onRefChanged;
            
            sut.As<IBindable<TestReference>>().Bind(binder);
            onRefChanged.ClearReceivedCalls();
            //Act
            sut.As<IBindable<TestReference>>().Unbind(binder);
            //Assert
            sut.Value.Should().BeNull();
            onRefChanged.Received(1).Invoke();
        }
        
        [Test]
        public void Unbind_When_binder_is_IValueProvider_and_value_not_equals_provided_value_Should_not_set_value_to_null_and_not_invoke_OnRefChanged()
        {
            //Arrange
            var expected = Substitute.For<TestReference>();
            var binder = Substitute.For<IValueProvider<TestReference>>();
            binder.GetValue().Returns(expected);
            
            var sut = new BindableRef<TestReference>();
            var onRefChanged = Substitute.For<Action>();
            sut.OnRefChanged += onRefChanged;
            
            sut.As<IBindable<TestReference>>().Bind(binder);
            onRefChanged.ClearReceivedCalls();
            binder.GetValue().Returns(Substitute.For<TestReference>());
            //Act
            sut.As<IBindable<TestReference>>().Unbind(binder);
            //Assert
            sut.Value.Should().NotBeNull();
        }
        
        [Test]
        public void Unbind_When_binder_is_IValueProvider_and_value_is_null_Should_not_set_value_to_null_and_not_invoke_OnRefChanged()
        {
            //Arrange
            var binder = Substitute.For<IValueProvider<TestReference>>();
            binder.GetValue().Returns(Substitute.For<TestReference>());
            
            var sut = new BindableRef<TestReference>();
            var onRefChanged = Substitute.For<Action>();
            sut.OnRefChanged += onRefChanged;
            //Act
            sut.As<IBindable<TestReference>>().Unbind(binder);
            //Assert
            sut.Value.Should().BeNull();
        }


        internal abstract class TestReference
        {
        }
    }
}