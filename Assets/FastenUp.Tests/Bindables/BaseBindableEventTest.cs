using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BaseBindableEvent<>))]
    public class BaseBindableEventTest
    {
        [Test]
        public void Bind_When_eventBinder_already_bind_Should_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=>
            {
                sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
                sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Bind_When_eventBinder_not_bind_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=> sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Bind_When_listener_was_added_Should_call_add_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            //Assert
            eventBinder.Received(1).AddListener(listener);
        }
        
        [Test]
        public void Unbind_When_eventBinder_not_bind_Should_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=> sut.As<IBindableEvent<UnityAction>>().Unbind(eventBinder);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_eventBinder_bind_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=>
            {
                sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
                sut.As<IBindableEvent<UnityAction>>().Unbind(eventBinder);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_listener_was_added_Should_call_remove_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            var listener = Substitute.For<UnityAction>();
            sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            sut.AddListener(listener);
            //Act
            sut.As<IBindableEvent<UnityAction>>().Unbind(eventBinder);
            //Assert
            eventBinder.Received(1).RemoveListener(listener);
        }

        [Test]
        public void AddListener_Should_call_add_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            var listener = Substitute.For<UnityAction>();
            //Act
            sut.AddListener(listener);
            //Assert
            eventBinder.Received(1).AddListener(listener);
        }
        
        [Test]
        public void RemoveListener_Should_call_remove_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            var listener = Substitute.For<UnityAction>();
            //Act
            sut.RemoveListener(listener);
            //Assert
            eventBinder.Received(1).RemoveListener(listener);
        }
        
        [Test]
        public void Dispose_Should_call_remove_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.Dispose();
            //Assert
            eventBinder.Received(1).RemoveListener(listener);
        }
        
        [Test]
        public void Dispose_Should_clear_listeners()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            sut.As<IBindableEvent<UnityAction>>().Bind(eventBinder);
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.Dispose();
            //Assert
            sut.HasListeners(listener).Should().BeFalse();
        }
        
        [Test]
        public void HasListeners_When_listener_not_added_Should_return_false()
        {
            //Arrange
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            var listener = Substitute.For<UnityAction>();
            //Act
            var result = sut.HasListeners(listener);
            //Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void HasListeners_When_listener_added_Should_return_true()
        {
            //Arrange
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            var result = sut.HasListeners(listener);
            //Assert
            result.Should().BeTrue();
        }
    }
}