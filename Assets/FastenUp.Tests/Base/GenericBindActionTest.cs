using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Base
{
    [TestFixture]
    [TestOf(typeof(BindAction<>))]
    public class GenericBindActionTest
    {
        [Test]
        public void AddBindableListener_When_listener_already_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction<int>>>();
            var sut = new BindAction<int>();
            Action act = () =>
            {
                sut.As<IInternalBindAction<UnityAction<int>>>().AddListener(bindableListener);
                sut.As<IInternalBindAction<UnityAction<int>>>().AddListener(bindableListener);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void AddBindableListener_When_listener_not_added_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction<int>>>();
            var sut = new BindAction<int>();
            Action act = ()=> sut.As<IInternalBindAction<UnityAction<int>>>().AddListener(bindableListener);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void RemoveBindableListener_When_listener_not_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction<int>>>();
            var sut = new BindAction<int>();
            Action act = ()=> sut.As<IInternalBindAction<UnityAction<int>>>().RemoveListener(bindableListener);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void RemoveBindableListener_When_listener_added_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction<int>>>();
            var sut = new BindAction<int>();
            Action act = ()=>
            {
                sut.As<IInternalBindAction<UnityAction<int>>>().AddListener(bindableListener);
                sut.As<IInternalBindAction<UnityAction<int>>>().RemoveListener(bindableListener);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
    }
}