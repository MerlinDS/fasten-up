using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BaseBindable<>))]
    public class BaseBindableTest
    {

        [Test]
        public void Bind_When_binder_was_not_bind_Should_set_value()
        {
            //Arrange
            var binder = Substitute.For<IValueReceiver<bool>>();
            var sut = Substitute.ForPartsOf<BaseBindable<bool>>(false);
            //Act & Assert
            sut.As<IBindable<bool>>().Bind(binder);
            binder.Received(1).SetValue(false);
        }
        
        [Test]
        public void Bind_When_binder_already_was_bind_Should_throw_exception()
        {
            //Arrange
            var binder = Substitute.For<IBinder<bool>>();
            var sut = Substitute.ForPartsOf<BaseBindable<bool>>(false);
            sut.As<IBindable<bool>>().Bind(binder);
            Action act = () => sut.As<IBindable<bool>>().Bind(binder);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_binder_was_bind_Should_not_throw_exception()
        {
            //Arrange
            var binder = Substitute.For<IBinder<bool>>();
            var sut = Substitute.ForPartsOf<BaseBindable<bool>>(false);
            sut.As<IBindable<bool>>().Bind(binder);
            Action act = () => sut.As<IBindable<bool>>().Unbind(binder);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }
        
        [Test]
        public void Unbind_When_binder_was_not_bind_Should_throw_exception()
        {
            //Arrange
            var binder = Substitute.For<IBinder<bool>>();
            var sut = Substitute.ForPartsOf<BaseBindable<bool>>(false);
            Action act = () => sut.As<IBindable<bool>>().Unbind(binder);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Value_When_set_Should_notify_all_bindables()
        {
            //Arrange
            var binder1 = Substitute.For<IValueReceiver<bool>>();
            var binder2 = Substitute.For<IValueReceiver<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder1);
            sut.As<IBindable<bool>>().Bind(binder2);
            //Act
            sut.Value = true;
            //Assert
            binder1.Received(1).SetValue(true);
            binder2.Received(1).SetValue(true);
        }
    }
}