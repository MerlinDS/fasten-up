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
    [TestOf(typeof(BindableAction))]
    public class BindableActionTest
    {

        [Test]
        public void Invoke_When_has_valid_action_Should_invoke_action()
        {
            //Arrange
            var mockActionBinder = Substitute.For<IActionBinder<UnityEvent>>();
            var mockAction = Substitute.For<UnityEvent>();
            mockActionBinder.OnAction.Returns(mockAction);
            var sut = new BindableAction();
            sut.As<IBindableAction<UnityEvent>>().Bind(mockActionBinder);
            //Act
            sut.Invoke();
            //Assert
            mockAction.Received(1).Invoke();
        }
        
        [Test]
        public void Invoke_When_has_no_actions_Should_not_throw_exception()
        {
            //Arrange
            var sut = new BindableAction();
            Action act = ()=> sut.Invoke();
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }
    }
}