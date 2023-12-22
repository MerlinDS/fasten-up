using System;
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
    [TestOf(typeof(BindableEvent))]
    public class BindableEventTest
    {
        [Test]
        public void AddBindableListener_When_listener_already_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=>
            {
                sut.As<IInternalBindableEvent<UnityAction>>().Add(bindableListener);
                sut.As<IInternalBindableEvent<UnityAction>>().Add(bindableListener);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void AddBindableListener_When_listener_not_added_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=> sut.As<IInternalBindableEvent<UnityAction>>().Add(bindableListener);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void RemoveBindableListener_When_listener_not_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=> sut.As<IInternalBindableEvent<UnityAction>>().Remove(bindableListener);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void RemoveBindableListener_When_listener_added_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=>
            {
                sut.As<IInternalBindableEvent<UnityAction>>().Add(bindableListener);
                sut.As<IInternalBindableEvent<UnityAction>>().Remove(bindableListener);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
    }
}