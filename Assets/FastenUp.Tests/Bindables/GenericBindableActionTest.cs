using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders.Actions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableAction<>))]
    public class GenericBindableActionTest
    {

        [Test]
        public void Invoke_When_has_valid_action_Should_invoke_action()
        {
            //Arrange
            const int expected = 1;
            var mockActionBinder = Substitute.For<IActionBinder<UnityEvent<int>>>();
            var mockAction = Substitute.For<UnityEvent<int>>();
            mockActionBinder.OnAction.Returns(mockAction);
            var sut = new BindableAction<int>();
            sut.As<IBindableAction<UnityEvent<int>>>().Bind(mockActionBinder);
            //Act
            sut.Invoke(expected);
            //Assert
            mockAction.Received(1).Invoke(expected);
        }
        
        [Test]
        public void Invoke_When_has_no_actions_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableAction<int>();
            Action act = ()=> sut.Invoke(1);
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }
    }
}