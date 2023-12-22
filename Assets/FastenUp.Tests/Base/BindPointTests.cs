using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Base
{
    [TestFixture]
    [TestOf(typeof(BindPoint<>))]
    public class BindPointTests
    {
        [Test]
        public void Add_When_bindable_was_not_added_Should_set_value()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new BindPoint<bool>();
            //Act & Assert
            sut.As<IInternalBindPoint<bool>>().Add(bindable);
            bindable.Received(1).SetValue(false);
        }

        [Test]
        public void Add_When_gettable_bindable_was_not_added_Should_set_value_and_subscribe_to_bindable()
        {
            //Arrange
            var bindable = Substitute.For<IGettableBinder<bool>>();
            var sut = new BindPoint<bool>();
            //Act & Assert
            sut.As<IInternalBindPoint<bool>>().Add(bindable);
            bindable.Received(1).SetValue(false);
            bindable.Received(1).OnBinderChanged += Arg.Any<OnBinderChanged>();
        }

        [Test]
        public void Add_When_bindable_already_was_added_Should_throw_exception()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new BindPoint<bool>();
            sut.As<IInternalBindPoint<bool>>().Add(bindable);
            Action act = () => sut.As<IInternalBindPoint<bool>>().Add(bindable);
            //Act & Assert
            act.Should().Throw<FastenUpException>()
                .WithMessage("Bindable already added to bind point.");
        }

        [Test]
        public void Remove_When_bindable_was_added_Should_not_throw_exception()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new BindPoint<bool>();
            sut.As<IInternalBindPoint<bool>>().Add(bindable);
            Action act = () => sut.As<IInternalBindPoint<bool>>().Remove(bindable);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void Remove_When_gettable_bindable_was_added_Should_unsubscribe_from_bindable()
        {
            //Arrange
            var bindable = Substitute.For<IGettableBinder<bool>>();
            var sut = new BindPoint<bool>();
            sut.As<IInternalBindPoint<bool>>().Add(bindable);
            //Act
            sut.As<IInternalBindPoint<bool>>().Remove(bindable);
            //Assert
            bindable.Received(1).OnBinderChanged -= Arg.Any<OnBinderChanged>();
        }

        [Test]
        public void Remove_When_bindable_was_not_added_Should_throw_exception()
        {
            //Arrange
            var bindable = Substitute.For<IBinder<bool>>();
            var sut = new BindPoint<bool>();
            Action act = () => sut.As<IInternalBindPoint<bool>>().Remove(bindable);
            //Act & Assert
            act.Should().Throw<FastenUpException>()
                .WithMessage("Bindable not found in bind point.");
        }

        [Test]
        public void Value_When_set_Should_notify_all_bindables()
        {
            //Arrange
            var bindable1 = Substitute.For<IBinder<bool>>();
            var bindable2 = Substitute.For<IBinder<bool>>();
            var sut = new BindPoint<bool>();
            sut.As<IInternalBindPoint<bool>>().Add(bindable1);
            sut.As<IInternalBindPoint<bool>>().Add(bindable2);
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

            var sut = new BindPoint<bool>();
            sut.As<IInternalBindPoint<bool>>().Add(bindable);
            //Act
            bindable.OnBinderChanged += Raise.Event<OnBinderChanged>(bindable);
            //Assert
            sut.Value.Should().BeTrue();
        }
    }
}