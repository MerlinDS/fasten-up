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
    [TestOf(typeof(BindableEvent<>))]
    public class GenericBindActionTest
    {
        [Test]
        public void Add_When_eventBinder_already_added_Should_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = () =>
            {
                sut.As<IInternalBindableEvent<UnityAction<int>>>().Add(eventBinder);
                sut.As<IInternalBindableEvent<UnityAction<int>>>().Add(eventBinder);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Add_When_eventBinder_not_added_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = ()=> sut.As<IInternalBindableEvent<UnityAction<int>>>().Add(eventBinder);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Remove_When_eventBinder_not_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = ()=> sut.As<IInternalBindableEvent<UnityAction<int>>>().Remove(bindableListener);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Remove_When_eventBinder_added_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = ()=>
            {
                sut.As<IInternalBindableEvent<UnityAction<int>>>().Add(eventBinder);
                sut.As<IInternalBindableEvent<UnityAction<int>>>().Remove(eventBinder);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
    }
}