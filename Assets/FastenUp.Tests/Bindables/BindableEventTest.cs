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
    [TestOf(typeof(BindableEvent))]
    public class BindableEventTest
    {
        [Test]
        public void Bind_When_bindableListener_already_bind_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=>
            {
                sut.As<IBindableEvent<UnityAction>>().Bind(bindableListener);
                sut.As<IBindableEvent<UnityAction>>().Bind(bindableListener);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Bind_When_bindableListener_not_bind_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=> sut.As<IBindableEvent<UnityAction>>().Bind(bindableListener);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_bindableListener_not_bind_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=> sut.As<IBindableEvent<UnityAction>>().Unbind(bindableListener);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_bindableListener_was_bind_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IEventBinder<UnityAction>>();
            var sut = new BindableEvent();
            Action act = ()=>
            {
                sut.As<IBindableEvent<UnityAction>>().Bind(bindableListener);
                sut.As<IBindableEvent<UnityAction>>().Unbind(bindableListener);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
    }
}