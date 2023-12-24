using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Events;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableEvent<>))]
    public class GenericBindActionTest
    {
        [Test]
        public void Bind_When_eventBinder_already_bind_Should_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = () =>
            {
                sut.As<IBindableEvent<UnityAction<int>>>().Bind(eventBinder);
                sut.As<IBindableEvent<UnityAction<int>>>().Bind(eventBinder);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Bind_When_eventBinder_not_bind_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = ()=> sut.As<IBindableEvent<UnityAction<int>>>().Bind(eventBinder);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_eventBinder_not_bind_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = ()=> sut.As<IBindableEvent<UnityAction<int>>>().Unbind(bindableListener);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_eventBinder_bind_Should_not_throw_exception()
        {
            //Arrange
            var eventBinder = Substitute.For<IEventBinder<UnityAction<int>>>();
            var sut = new BindableEvent<int>();
            Action act = ()=>
            {
                sut.As<IBindableEvent<UnityAction<int>>>().Bind(eventBinder);
                sut.As<IBindableEvent<UnityAction<int>>>().Unbind(eventBinder);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
    }
}