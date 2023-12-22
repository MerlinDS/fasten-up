﻿using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Base
{
    [TestFixture]
    [TestOf(typeof(BaseBindableEvent<>))]
    public class BaseBindableEventTest
    {
        [Test]
        public void Add_When_eventBinder_already_added_Should_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=>
            {
                sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
                sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Add_When_eventBinder_not_added_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=> sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Add_When_listener_was_added_Should_call_add_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
            //Assert
            eventBinder.Received(1).AddListener(listener);
        }
        
        [Test]
        public void Remove_When_eventBinder_not_added_Should_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=> sut.As<IInternalBindableEvent<UnityAction>>().Remove(eventBinder);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Remove_When_eventBinder_added_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            Action act = ()=>
            {
                sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
                sut.As<IInternalBindableEvent<UnityAction>>().Remove(eventBinder);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Remove_When_listener_was_added_Should_call_remove_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            var listener = Substitute.For<UnityAction>();
            sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
            sut.AddListener(listener);
            //Act
            sut.As<IInternalBindableEvent<UnityAction>>().Remove(eventBinder);
            //Assert
            eventBinder.Received(1).RemoveListener(listener);
        }

        [Test]
        public void AddListener_Should_call_add_listener_on_eventBinder()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction>>();
            var sut = Substitute.ForPartsOf<BaseBindableEvent<UnityAction>>();
            sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
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
            sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
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
            sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
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
            sut.As<IInternalBindableEvent<UnityAction>>().Add(eventBinder);
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