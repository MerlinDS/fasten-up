using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Actions;
using FastenUp.Runtime.Binders.Collections;
using FastenUp.Runtime.Binders.Events;
using FastenUp.Runtime.Binders.References;
using FastenUp.Runtime.Utils;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

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
            bool result = BindUtilities.NameEquals("Test", binder);
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
            bool result = BindUtilities.NameEquals("Test2", binder);
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void NameEquals_When_binder_is_null_Should_return_false()
        {
            //Act
            bool result = BindUtilities.NameEquals("Test", null);
            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void TryBind_When_binder_has_valid_type_Should_bind()
        {
            //Arrange
            IBinder binder = Substitute.For<IBinder, IBinder<bool>>();
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
            IBinder binder = Substitute.For<IBinder, IBinder<bool>>();
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
            IBinder binder = Substitute.For<IBinder, IBinder<bool>>();
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
            IBinder binder = Substitute.For<IBinder, IBinder<bool>>();
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
            IBinder eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
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
            IBinder eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
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
            IBinder eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
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
            IBinder eventBinder = Substitute.For<IBinder, IEventBinder<bool>>();
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

        [Test]
        public void TryBind_When_actionBinder_has_valid_type_Should_bind()
        {
            //Arrange
            IBinder actionBinder = Substitute.For<IBinder, IActionBinder<UnityEvent>>();
            var bindableAction = Substitute.For<IBindableAction<UnityEvent>>();
            //Act
            BindUtilities.TryBind(bindableAction, actionBinder);
            //Assert
            bindableAction.Received(1).Bind(actionBinder.As<IActionBinder<UnityEvent>>());
        }

        [Test]
        public void TryBind_When_actionBinder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            IBinder actionBinder = Substitute.For<IBinder, IActionBinder<UnityEvent<int>>>();
            var bindableAction = Substitute.For<IBindableAction<UnityEvent>>();
            //Act
            BindUtilities.TryBind(bindableAction, actionBinder);
            //Assert
            bindableAction.DidNotReceive().Bind(actionBinder.As<IActionBinder<UnityEvent>>());
        }

        [Test]
        public void TryBind_When_actionBinder_is_null_Should_not_bind()
        {
            //Arrange
            var bindableAction = Substitute.For<IBindableAction<UnityEvent>>();
            //Act
            BindUtilities.TryBind(bindableAction, null);
            //Assert
            bindableAction.DidNotReceive().Bind(Arg.Any<IActionBinder<UnityEvent>>());
        }

        [Test]
        public void TryUnbind_When_actionBinder_has_valid_type_Should_unbind()
        {
            //Arrange
            IBinder actionBinder = Substitute.For<IBinder, IActionBinder<UnityEvent>>();
            var bindableAction = Substitute.For<IBindableAction<UnityEvent>>();
            //Act
            BindUtilities.TryUnbind(bindableAction, actionBinder);
            //Assert
            bindableAction.Received(1).Unbind(actionBinder.As<IActionBinder<UnityEvent>>());
        }

        [Test]
        public void TryUnbind_When_actionBinder_has_invalid_type_Should_not_unbind()
        {
            //Arrange
            IBinder actionBinder = Substitute.For<IBinder, IActionBinder<UnityEvent<int>>>();
            var bindableAction = Substitute.For<IBindableAction<UnityEvent>>();
            //Act
            BindUtilities.TryBind(bindableAction, actionBinder);
            //Assert
            bindableAction.DidNotReceive().Unbind(Arg.Any<IActionBinder<UnityEvent>>());
        }

        [Test]
        public void TryUnbind_When_actionBinder_is_null_Should_not_unbind()
        {
            //Arrange
            var bindableAction = Substitute.For<IBindableAction<UnityEvent>>();
            //Act
            BindUtilities.TryBind(bindableAction, null);
            //Assert
            bindableAction.DidNotReceive().Unbind(Arg.Any<IActionBinder<UnityEvent>>());
        }

        [Test]
        public void TryBind_When_actionBinder_has_valid_generic_type_Should_bind()
        {
            //Arrange
            IBinder actionBinder = Substitute.For<IBinder, IActionBinder<UnityEvent<bool>>>();
            var bindableAction = Substitute.For<IBindableAction<UnityEvent<bool>>>();
            //Act
            BindUtilities.TryBind(bindableAction, actionBinder);
            //Assert
            bindableAction.Received(1).Bind(actionBinder.As<IActionBinder<UnityEvent<bool>>>());
        }
        
        [Test]
        public void TryBind_When_actionBinder_has_invalid_generic_type_Should_not_bind()
        {
            //Arrange
            IBinder actionBinder = Substitute.For<IBinder, IActionBinder<UnityEvent<int>>>();
            var bindableAction = Substitute.For<IBindableAction<UnityEvent<bool>>>();
            //Act
            BindUtilities.TryBind(bindableAction, actionBinder);
            //Assert
            bindableAction.DidNotReceive().Bind(actionBinder.As<IActionBinder<UnityEvent<bool>>>());
        }
        
        [Test]
        public void TryBind_When_refBinder_has_valid_type_Should_bind()
        {
            //Arrange
            IBinder refBinder = Substitute.For<IBinder, IRefBinder>();
            var bindableRef = Substitute.For<IBindableRef<TestReference>>();
            refBinder.As<IRefBinder>().TryGetReference(out Arg.Any<TestReference>()).Returns(x =>
            {
                x[0] = Substitute.For<TestReference>();
                return true;
            });
            //Act
            BindUtilities.TryBind(bindableRef, refBinder);
            //Assert
            bindableRef.Received(1).Bind(Arg.Any<TestReference>());
        }
        
        [Test]
        public void TryBind_When_refBinder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            IBinder refBinder = Substitute.For<IBinder, IRefBinder>();
            var bindableRef = Substitute.For<IBindableRef<TestReference>>();
            refBinder.As<IRefBinder>().TryGetReference(out Arg.Any<TestReference>()).Returns(false);
            //Act
            BindUtilities.TryBind(bindableRef, refBinder);
            //Assert
            bindableRef.DidNotReceive().Bind(Arg.Any<TestReference>());
        }
        
        [Test]
        public void TryBind_When_refBinder_is_null_Should_not_bind()
        {
            //Arrange
            var bindableRef = Substitute.For<IBindableRef<TestReference>>();
            //Act
            BindUtilities.TryBind(bindableRef, null);
            //Assert
            bindableRef.DidNotReceive().Bind(Arg.Any<TestReference>());
        }
        
        [Test]
        public void TryUnbind_When_refBinder_has_valid_type_Should_unbind()
        {
            //Arrange
            IBinder refBinder = Substitute.For<IBinder, IRefBinder>();
            var bindableRef = Substitute.For<IBindableRef<TestReference>>();
            refBinder.As<IRefBinder>().TryGetReference(out Arg.Any<TestReference>()).Returns(x =>
            {
                x[0] = Substitute.For<TestReference>();
                return true;
            });
            //Act
            BindUtilities.TryUnbind(bindableRef, refBinder);
            //Assert
            bindableRef.Received(1).Unbind(Arg.Any<TestReference>());
        }
        
        [Test]
        public void TryBind_When_collectionBinder_has_valid_type_Should_bind()
        {
            //Arrange
            IBinder collectionBinder = Substitute.For<IBinder, ICollectionBinder<TestReference>>();
            var bindableCollection = Substitute.For<IBindableCollection<TestReference>>();
            //Act
            BindUtilities.TryBind(bindableCollection, collectionBinder);
            //Assert
            bindableCollection.Received(1).Bind(collectionBinder.As<ICollectionBinder<TestReference>>());
        }
        
        [Test]
        public void TryBind_When_collectionBinder_has_invalid_type_Should_not_bind()
        {
            //Arrange
            IBinder collectionBinder = Substitute.For<IBinder, ICollectionBinder<TestReference>>();
            var bindableCollection = Substitute.For<IBindableCollection<int>>();
            //Act
            BindUtilities.TryBind(bindableCollection, collectionBinder);
            //Assert
            bindableCollection.DidNotReceive().Bind(Arg.Any<ICollectionBinder<int>>());
        }
        
        [Test]
        public void TryBind_When_collectionBinder_is_null_Should_not_bind()
        {
            //Arrange
            var bindableCollection = Substitute.For<IBindableCollection<TestReference>>();
            //Act
            BindUtilities.TryBind(bindableCollection, null);
            //Assert
            bindableCollection.DidNotReceive().Bind(Arg.Any<ICollectionBinder<TestReference>>());
        }
        
        [Test]
        public void TryUnbind_When_collectionBinder_has_valid_type_Should_unbind()
        {
            //Arrange
            IBinder collectionBinder = Substitute.For<IBinder, ICollectionBinder<TestReference>>();
            var bindableCollection = Substitute.For<IBindableCollection<TestReference>>();
            //Act
            BindUtilities.TryUnbind(bindableCollection, collectionBinder);
            //Assert
            bindableCollection.Received(1).Unbind(collectionBinder.As<ICollectionBinder<TestReference>>());
        }
        
        [Test]
        public void TryUnbind_When_collectionBinder_has_invalid_type_Should_not_unbind()
        {
            //Arrange
            IBinder collectionBinder = Substitute.For<IBinder, ICollectionBinder<TestReference>>();
            var bindableCollection = Substitute.For<IBindableCollection<int>>();
            //Act
            BindUtilities.TryBind(bindableCollection, collectionBinder);
            //Assert
            bindableCollection.DidNotReceive().Unbind(Arg.Any<ICollectionBinder<int>>());
        }
        
        [Test]
        public void TryUnbind_When_collectionBinder_is_null_Should_not_unbind()
        {
            //Arrange
            var bindableCollection = Substitute.For<IBindableCollection<TestReference>>();
            //Act
            BindUtilities.TryBind(bindableCollection, null);
            //Assert
            bindableCollection.DidNotReceive().Unbind(Arg.Any<ICollectionBinder<TestReference>>());
        }
        
        internal class TestReference
        {
            
        }
    }
}