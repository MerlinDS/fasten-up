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
    [TestOf(typeof(BaseActionBinder<>))]
    public class BaseActionBinderTest
    {

        [Test]
        public void OnAction_Should_return_action()
        {
            //Arrange
            var sut = TestBehaviour.Create();
            //Act & Assert
            sut.OnAction.Should().NotBeNull();
            sut.OnAction.Should().BeOfType<UnityEvent>();
        }
        
        private class TestBehaviour : BaseActionBinder<UnityEvent>, IMonoBehaviourTest
        {
            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;
            
            public static TestBehaviour Create()
            {
                var go = new GameObject(nameof(TestBehaviour));
                return go.AddComponent<TestBehaviour>();
            }
        }
    }
}