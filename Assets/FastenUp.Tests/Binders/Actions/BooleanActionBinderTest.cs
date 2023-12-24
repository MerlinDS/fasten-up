using FastenUp.Runtime.Binders.Actions;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace FastenUp.Tests.Binders.Actions
{
    [TestFixture]
    [TestOf(typeof(BooleanActionBinder))]
    public class BooleanActionBinderTest
    {

        [Test]
        public void OnAction_Should_return_UnityEvent()
        {
            //Arrange
            var gameObject = new GameObject(nameof(BooleanActionBinderTest));
            var sut = gameObject.AddComponent<BooleanActionBinder>();
            //Act & Assert
            sut.OnAction.Should().NotBeNull();
            sut.OnAction.Should().BeOfType<UnityEvent<bool>>();
        }
    }
}