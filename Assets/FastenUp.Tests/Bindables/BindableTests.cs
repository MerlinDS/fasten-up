using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(Bindable<>))]
    public class BindableTests
    {
        [Test]
        public void Bind_When_bindable_was_not_bind_Should_set_value()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            //Act & Assert
            sut.As<IInternalBindable<bool>>().Bind(bindable);
            bindable.Received(1).SetValue(false);
        }

        [Test]
        public void Bind_When_gettable_bindable_was_not_bind_Should_set_value_and_subscribe_to_bindable()
        {
            //Arrange
            var bindable = Substitute.For<IGettableBinder<bool>>();
            var sut = new Bindable<bool>();
            //Act & Assert
            sut.As<IInternalBindable<bool>>().Bind(bindable);
            bindable.Received(1).SetValue(false);
            bindable.Received(1).OnBinderChanged += Arg.Any<OnBinderChanged>();
        }

        [Test]
        public void Bind_When_bindable_already_was_bind_Should_throw_exception()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IInternalBindable<bool>>().Bind(bindable);
            Action act = () => sut.As<IInternalBindable<bool>>().Bind(bindable);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }

        [Test]
        public void Unbind_When_bindable_was_bind_Should_not_throw_exception()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IInternalBindable<bool>>().Bind(bindable);
            Action act = () => sut.As<IInternalBindable<bool>>().Unbind(bindable);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Unbind_When_gettable_bindable_was_bind_Should_unsubscribe_from_bindable()
        {
            //Arrange
            var bindable = Substitute.For<IGettableBinder<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IInternalBindable<bool>>().Bind(bindable);
            //Act
            sut.As<IInternalBindable<bool>>().Unbind(bindable);
            //Assert
            bindable.Received(1).OnBinderChanged -= Arg.Any<OnBinderChanged>();
        }

        [Test]
        public void Unbind_When_bindable_was_not_bind_Should_throw_exception()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            Action act = () => sut.As<IInternalBindable<bool>>().Unbind(bindable);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }

        [Test]
        public void Value_When_set_Should_notify_all_bindables()
        {
            //Arrange
            var bindable1 = Substitute.For<IBinder<bool>>();
            var bindable2 = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IInternalBindable<bool>>().Bind(bindable1);
            sut.As<IInternalBindable<bool>>().Bind(bindable2);
            //Act
            sut.Value = true;
            //Assert
            bindable1.Received(1).SetValue(true);
            bindable2.Received(1).SetValue(true);
        }

        [Test]
        public void OnValueChanged_When_gettable_bindable_value_changed_Should_update_value_and_invoke_event()
        {
            //Arrange
            var bindable = Substitute.For<IGettableBinder<bool>>();
            bindable.GetValue().Returns(true);

            var sut = new Bindable<bool>();
            sut.As<IInternalBindable<bool>>().Bind(bindable);
            //Act
            bindable.OnBinderChanged += Raise.Event<OnBinderChanged>(bindable);
            //Assert
            sut.Value.Should().BeTrue();
        }
    }
}