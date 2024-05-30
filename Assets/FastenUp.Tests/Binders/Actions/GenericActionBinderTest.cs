using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Binders.Actions;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FastenUp.Tests.Binders.Actions
{
    [TestFixture]
    [TestOf(typeof(ActionBinder<>))]
    public class GenericActionBinderTest
    {
        [Test]
        public void OnAction_Should_return_UnityEvent()
        {
            //Arrange
            ActionBinder<int> sut = TestBehaviour.Create();
            //Act & Assert
            sut.OnAction.Should().NotBeNull();
            sut.OnAction.Should().BeOfType<UnityEvent<int>>();
        }
        
        private class TestBehaviour : ActionBinder<int>, IMonoBehaviourTest
        {
            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;
            
            public static ActionBinder<int> Create()
            {
                var go = new GameObject(nameof(GenericActionBinderTest));
                return go.AddComponent<TestBehaviour>();
            }
        }
    }
}