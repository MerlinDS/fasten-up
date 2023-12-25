using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.References;
using FastenUp.Runtime.Mediators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders.References
{
    [TestFixture]
    [TestOf(typeof(MediatorRefBinder))]
    public class MediatorRefBinderTest
    {
        [Test]
        public void TryGetReference_When_has_no_mediator_Should_return_false_and_null()
        {
            //Arrange
            var gameObject = new GameObject(nameof(MediatorRefBinderTest));
            var sut = gameObject.AddComponent<MediatorRefBinder>();
            sut.ExecuteAwake();
            //Act
            var result = sut.TryGetReference(out IMediator reference);
            //Assert
            result.Should().BeFalse();
            reference.Should().BeNull();
        }

        [Test]
        public void TryGetReference_When_has_mediator_Should_return_true_and_mediator()
        {
            //Arrange
            var gameObject = new GameObject(nameof(MediatorRefBinderTest));
            var mediator = gameObject.AddComponent<TestMediator>();
            var sut = gameObject.AddComponent<MediatorRefBinder>();
            sut.ExecuteAwake();
            //Act
            var result = sut.TryGetReference(out IMediator reference);
            //Assert
            result.Should().BeTrue();
            reference.Should().Be(mediator);
        }

        [Test]
        public void OnEnable_When_has_mediator_in_parent_and_in_self_Should_bind_mediator_to_parent_mediator()
        {
            //Arrange
            var gameObject = new GameObject(nameof(MediatorRefBinderTest));
            var mediator = gameObject.AddComponent<TestMediator>();
            var sut = gameObject.AddComponent<MediatorRefBinder>();
            sut.EditSerializable().Field("Name", "Test").Apply();
            var parentObject = new GameObject(nameof(MediatorRefBinderTest) + "Parent");
            gameObject.transform.SetParent(parentObject.transform);
            var parentMediator = parentObject.AddComponent<TestMediator>();
            //Act
            sut.ExecuteOnEnable();
            //Assert
            parentMediator.Mock.Received().Bind(sut);
            mediator.Mock.DidNotReceive().Bind(sut);
        }

        public class TestMediator : MonoBehaviour, IMediator, IInternalMediator
        {
            public readonly IInternalMediator Mock = Substitute.For<IInternalMediator>();

            /// <inheritdoc />
            public void Bind(IBinder binder)
            {
                Mock.Bind(binder);
            }

            /// <inheritdoc />
            public void Unbind(IBinder binder)
            {
                Mock.Unbind(binder);
            }
        }
    }
}