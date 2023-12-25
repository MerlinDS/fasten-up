using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Binders.References;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.References
{
    [TestFixture]
    [TestOf(typeof(BaseRefBinder<>))]
    public class BaseRefBinderTest
    {

        [Test]
        public void TryGetReference_When_reference_is_not_set_Should_return_false()
        {
            //Arrange
            var sut = TestRefBinder.Create();
            //Act
            var result = sut.TryGetReference(out BaseRefBinderTest reference);
            //Assert
            result.Should().BeFalse();
            reference.Should().BeNull();
        }
        
        [Test]
        public void TryGetReference_When_reference_is_set_Should_return_true_and_reference()
        {
            //Arrange
            var sut = TestRefBinder.Create();
            var expected = sut.transform;
            //Act
            var result = sut.TryGetReference(out Transform reference);
            //Assert
            result.Should().BeTrue();
            reference.Should().Be(expected);
        }
        
        private class TestRefBinder : BaseRefBinder<Transform>, IMonoBehaviourTest
        {
            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;

            public static TestRefBinder Create()
            {
                var gameObject = new GameObject(nameof(TestRefBinder));
                var testRefBinder = gameObject.AddComponent<TestRefBinder>();
                testRefBinder.ExecuteAwake();
                return testRefBinder;
            }
        }
    }
}