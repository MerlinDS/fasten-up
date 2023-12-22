using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Utils;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Utils
{
    [TestFixture]
    [TestOf(typeof(BindUtilities))]
    public class BindUtilitiesTest
    {
        [Test]
        public void TryBind_When_binder_has_valid_type_Should_bind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(bindable, binder);
            //Assert
            bindable.Received(1).Add(binder.As<IBinder<bool>>());
        }

        [Test]
        public void TryBind_When_binder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IInternalBindable<int>>();
            //Act
            BindUtilities.TryBind(bindable, binder);
            //Assert
            bindable.DidNotReceive().Add(Arg.Any<IBinder<int>>());
        }

        [Test]
        public void TryBind_When_binder_is_null_Should_not_bind()
        {
            //Arrange
            var bindable = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(bindable, null);
            //Assert
            bindable.DidNotReceive().Add(Arg.Any<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_binder_has_valid_type_Should_unbind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryUnbind(bindable, binder);
            //Assert
            bindable.Received(1).Remove(binder.As<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_binder_has_invalid_type_Should_not_unbind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IInternalBindable<int>>();
            //Act
            BindUtilities.TryBind(bindable, binder);
            //Assert
            bindable.DidNotReceive().Remove(Arg.Any<IBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_binder_is_null_Should_not_unbind()
        {
            //Arrange
            var bindable = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(bindable, null);
            //Assert
            bindable.DidNotReceive().Remove(Arg.Any<IBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_eventBinder_has_valid_type_Should_bind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.Received(1).Add(eventBinder.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_eventBinder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IInternalBindableEvent<int>>();
            //Act
            BindUtilities.TryBind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.DidNotReceive().Add(eventBinder.As<IEventBinder<int>>());
        }
        
        [Test]
        public void TryBind_When_eventBinder_is_null_Should_not_bind()
        {
            //Arrange
            var bindableEvent = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(bindableEvent, null);
            //Assert
            bindableEvent.DidNotReceive().Add(Arg.Any<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_eventBinder_has_valid_type_Should_unbind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryUnbind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.Received(1).Remove(eventBinder.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_eventBinder_has_invalid_type_Should_not_unbind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IInternalBindableEvent<int>>();
            //Act
            BindUtilities.TryBind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.DidNotReceive().Remove(Arg.Any<IEventBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_eventBinder_is_null_Should_not_unbind()
        {
            //Arrange
            var bindableEvent = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(bindableEvent, null);
            //Assert
            bindableEvent.DidNotReceive().Remove(Arg.Any<IEventBinder<bool>>());
        }
        
    }
}