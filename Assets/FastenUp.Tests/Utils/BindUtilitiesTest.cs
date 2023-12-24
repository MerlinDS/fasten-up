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
        public void NameEquals_When_binder_has_same_name_Should_return_true()
        {
            //Arrange
            var binder = Substitute.For<IBinder>();
            binder.Name.Returns("Test");
            //Act
            var result = BindUtilities.NameEquals("Test", binder);
            //Assert
            result.Should().BeTrue();
        }
        
        [Test]
        public void NameEquals_When_binder_has_different_name_Should_return_false()
        {
            //Arrange
            var binder = Substitute.For<IBinder>();
            binder.Name.Returns("Test");
            //Act
            var result = BindUtilities.NameEquals("Test2", binder);
            //Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void NameEquals_When_binder_is_null_Should_return_false()
        {
            //Act
            var result = BindUtilities.NameEquals("Test", null);
            //Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void TryBind_When_binder_has_valid_type_Should_bind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IBindable<bool>>();
            //Act
            BindUtilities.TryBind(bindable, binder);
            //Assert
            bindable.Received(1).Bind(binder.As<IBinder<bool>>());
        }

        [Test]
        public void TryBind_When_binder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IBindable<int>>();
            //Act
            BindUtilities.TryBind(bindable, binder);
            //Assert
            bindable.DidNotReceive().Bind(Arg.Any<IBinder<int>>());
        }

        [Test]
        public void TryBind_When_binder_is_null_Should_not_bind()
        {
            //Arrange
            var bindable = Substitute.For<IBindable<bool>>();
            //Act
            BindUtilities.TryBind(bindable, null);
            //Assert
            bindable.DidNotReceive().Bind(Arg.Any<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_binder_has_valid_type_Should_unbind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IBindable<bool>>();
            //Act
            BindUtilities.TryUnbind(bindable, binder);
            //Assert
            bindable.Received(1).Unbind(binder.As<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_binder_has_invalid_type_Should_not_unbind()
        {
            //Arrange
            var binder = Substitute.For<IBinder, IBinder<bool>>();
            var bindable = Substitute.For<IBindable<int>>();
            //Act
            BindUtilities.TryBind(bindable, binder);
            //Assert
            bindable.DidNotReceive().Unbind(Arg.Any<IBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_binder_is_null_Should_not_unbind()
        {
            //Arrange
            var bindable = Substitute.For<IBindable<bool>>();
            //Act
            BindUtilities.TryBind(bindable, null);
            //Assert
            bindable.DidNotReceive().Unbind(Arg.Any<IBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_eventBinder_has_valid_type_Should_bind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.Received(1).Bind(eventBinder.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_eventBinder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IBindableEvent<int>>();
            //Act
            BindUtilities.TryBind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.DidNotReceive().Bind(eventBinder.As<IEventBinder<int>>());
        }
        
        [Test]
        public void TryBind_When_eventBinder_is_null_Should_not_bind()
        {
            //Arrange
            var bindableEvent = Substitute.For<IBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(bindableEvent, null);
            //Assert
            bindableEvent.DidNotReceive().Bind(Arg.Any<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_eventBinder_has_valid_type_Should_unbind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IBindableEvent<bool>>();
            //Act
            BindUtilities.TryUnbind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.Received(1).Unbind(eventBinder.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_eventBinder_has_invalid_type_Should_not_unbind()
        {
            //Arrange
            var eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
            var bindableEvent = Substitute.For<IBindableEvent<int>>();
            //Act
            BindUtilities.TryBind(bindableEvent, eventBinder);
            //Assert
            bindableEvent.DidNotReceive().Unbind(Arg.Any<IEventBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_eventBinder_is_null_Should_not_unbind()
        {
            //Arrange
            var bindableEvent = Substitute.For<IBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(bindableEvent, null);
            //Assert
            bindableEvent.DidNotReceive().Unbind(Arg.Any<IEventBinder<bool>>());
        }
        
    }
}