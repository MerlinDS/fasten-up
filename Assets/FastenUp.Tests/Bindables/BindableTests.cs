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
            var binder = Substitute.For<IValueReceiver<bool>>();
            var sut = new Bindable<bool>();
            //Act & Assert
            sut.As<IBindable<bool>>().Bind(binder);
            binder.Received(1).SetValue(false);
        }

        [Test]
        public void Bind_When_gettable_bindable_was_not_bind_Should_set_value_and_subscribe_to_bindable()
        {
            //Arrange
            var binder = Substitute.For<IValueProvider<bool>, IValueReceiver<bool>>();
            var sut = new Bindable<bool>();
            //Act & Assert
            sut.As<IBindable<bool>>().Bind(binder);
            binder.Received(1).As<IValueReceiver<bool>>().SetValue(false);
            binder.Received(1).OnBinderChanged += Arg.Any<OnBinderChanged>();
        }

        [Test]
        public void Bind_When_bindable_already_was_bind_Should_throw_exception()
        {
            //Arrange
            var binder = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder);
            Action act = () => sut.As<IBindable<bool>>().Bind(binder);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }

        [Test]
        public void Unbind_When_bindable_was_bind_Should_not_throw_exception()
        {
            //Arrange
            var binder = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder);
            Action act = () => sut.As<IBindable<bool>>().Unbind(binder);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Unbind_When_gettable_bindable_was_bind_Should_unsubscribe_from_bindable()
        {
            //Arrange
            var binder = Substitute.For<IValueProvider<bool>>();
            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder);
            //Act
            sut.As<IBindable<bool>>().Unbind(binder);
            //Assert
            binder.Received(1).OnBinderChanged -= Arg.Any<OnBinderChanged>();
        }

        [Test]
        public void Unbind_When_bindable_was_not_bind_Should_throw_exception()
        {
            //Arrange
            var binder = Substitute.For<IBinder<bool>>();
            var sut = new Bindable<bool>();
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

        [Test]
        public void OnValueChanged_When_gettable_bindable_value_changed_Should_update_value_and_invoke_event()
        {
            //Arrange
            var binder = Substitute.For<IValueProvider<bool>>();
            binder.GetValue().Returns(true);

            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder);
            //Act
            binder.OnBinderChanged += Raise.Event<OnBinderChanged>(binder);
            //Assert
            sut.Value.Should().BeTrue();
        }

        [Test]
        public void
            OnValueChanged_When_gettable_bindable_value_changed_but_sent_invalid_date_Should_not_update_value_and_invoke_event()
        {
            //Arrange
            var binder = Substitute.For<IValueProvider<bool>>();
            binder.GetValue().Returns(true);

            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder);
            //Act
            binder.OnBinderChanged += Raise.Event<OnBinderChanged>
                (Substitute.For<IBinder>());
            //Assert
            sut.Value.Should().BeFalse();
        }

        [Test]
        public void OnValueChanged_When_gettable_bindable_value_changed_Should_notify_other_binders()
        {
            //Arrange
            var other = Substitute.For<IValueReceiver<bool>>();
            var binder = Substitute.For<IValueProvider<bool>, IValueReceiver<bool>>();
            binder.GetValue().Returns(true);

            var sut = new Bindable<bool>();
            sut.As<IBindable<bool>>().Bind(binder);
            sut.As<IBindable<bool>>().Bind(other);
            //Act
            binder.OnBinderChanged += Raise.Event<OnBinderChanged>(binder);
            //Assert
            binder.As<IValueReceiver<bool>>().DidNotReceive().SetValue(true);
            other.Received(1).SetValue(true);
        }
    }
}