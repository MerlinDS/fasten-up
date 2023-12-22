using FastenUp.Runtime.Base;
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
        public void TryBind_When_point_and_bindable_are_suited_Should_Add_bindable_to_point()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IBinder<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.Received(1).Add(bindable.As<IBinder<bool>>());
        }

        [Test]
        public void TryBind_When_point_and_bindable_have_different_names_Should_not_Add_bindable_to_point()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IBinder<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test2", bindable);
            //Assert
            point.DidNotReceive().Add(bindable.As<IBinder<bool>>());
        }

        [Test]
        public void TryBind_When_point_and_bindable_have_different_types_Should_not_Add_bindable_to_point()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IBinder<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindable<int>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.DidNotReceive().Add(Arg.Any<IBinder<int>>());
        }

        [Test]
        public void TryBind_When_bindable_is_null_Should_not_Add_bindable_to_point()
        {
            //Arrange
            var point = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", null);
            //Assert
            point.DidNotReceive().Add(Arg.Any<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_point_and_bindable_are_suited_Should_Remove_bindable_from_point()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IBinder<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryUnbind(point,"Test", bindable);
            //Assert
            point.Received(1).Remove(bindable.As<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_point_and_bindable_have_different_names_Should_not_Remove_bindable_from_point()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IBinder<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test2", bindable);
            //Assert
            point.DidNotReceive().Remove(bindable.As<IBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_point_and_bindable_have_different_types_Should_not_Remove_bindable_from_point()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IBinder<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindable<int>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.DidNotReceive().Remove(Arg.Any<IBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_bindable_is_null_Should_not_Remove_bindable_from_point()
        {
            //Arrange
            var point = Substitute.For<IInternalBindable<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", null);
            //Assert
            point.DidNotReceive().Remove(Arg.Any<IBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_action_and_bindable_are_suited_Should_Add_bindable_to_action()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IEventBinder<bool>>();
            bindable.Name.Returns("Test");
            var action = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(action,"Test", bindable);
            //Assert
            action.Received(1).Add(bindable.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_action_and_bindable_have_different_names_Should_not_Add_bindable_to_action()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IEventBinder<bool>>();
            bindable.Name.Returns("Test");
            var action = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(action,"Test2", bindable);
            //Assert
            action.DidNotReceive().Add(bindable.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryBind_When_action_and_bindable_have_different_types_Should_not_Add_bindable_to_action()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IEventBinder<bool>>();
            bindable.Name.Returns("Test");
            var action = Substitute.For<IInternalBindableEvent<int>>();
            //Act
            BindUtilities.TryBind(action,"Test", bindable);
            //Assert
            action.DidNotReceive().Add(Arg.Any<IEventBinder<int>>());
        }
        
        [Test]
        public void TryBind_When_bindable_is_null_Should_not_Add_bindable_to_action()
        {
            //Arrange
            var action = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(action,"Test", null);
            //Assert
            action.DidNotReceive().Add(Arg.Any<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_action_and_bindable_are_suited_Should_Remove_bindable_from_action()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IEventBinder<bool>>();
            bindable.Name.Returns("Test");
            var action = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryUnbind(action,"Test", bindable);
            //Assert
            action.Received(1).Remove(bindable.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_action_and_bindable_have_different_names_Should_not_Remove_bindable_from_action()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IEventBinder<bool>>();
            bindable.Name.Returns("Test");
            var action = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(action,"Test2", bindable);
            //Assert
            action.DidNotReceive().Remove(bindable.As<IEventBinder<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_action_and_bindable_have_different_types_Should_not_Remove_bindable_from_action()
        {
            //Arrange
            var bindable = Substitute.For<IBinder, IEventBinder<bool>>();
            bindable.Name.Returns("Test");
            var action = Substitute.For<IInternalBindableEvent<int>>();
            //Act
            BindUtilities.TryBind(action,"Test", bindable);
            //Assert
            action.DidNotReceive().Remove(Arg.Any<IEventBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_bindable_is_null_Should_not_Remove_bindable_from_action()
        {
            //Arrange
            var action = Substitute.For<IInternalBindableEvent<bool>>();
            //Act
            BindUtilities.TryBind(action,"Test", null);
            //Assert
            action.DidNotReceive().Remove(Arg.Any<IEventBinder<bool>>());
        }
        
    }
}