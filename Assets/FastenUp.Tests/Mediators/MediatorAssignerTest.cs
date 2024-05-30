using System;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Mediators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Mediators
{
    [TestFixture]
    [TestOf(typeof(MediatorAssigner))]
    public class MediatorAssignerTest
    {
        [Test]
        public void Assign_When_mediator_is_null_Should_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            Action act = () => sut.Assign<TestMediator>(null);
            // Act & Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Assign_When_mediator_is_not_null_Should_not_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            Action act = () => sut.Assign(Substitute.For<TestMediator>());
            // Act & Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Assign_When_has_binders_Should_bind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            sut.Bind(binder);
            var actual = Substitute.For<TestMediator>();
            // Act
            sut.Assign(actual);
            // Assert
            actual.Received(1).Bind(binder);
        }

        [Test]
        public void Assign_When_has_no_binders_Should_not_bind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var actual = Substitute.For<TestMediator>();
            // Act
            sut.Assign(actual);
            // Assert
            actual.DidNotReceive().Bind(Arg.Any<IBinder>());
        }

        [Test]
        public void Assign_When_has_binders_and_mediator_is_already_assigned_Should_unbind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            sut.Bind(binder);
            var actual = Substitute.For<TestMediator>();
            sut.Assign(actual);
            // Act
            sut.Assign(Substitute.For<TestMediator>());
            // Assert
            actual.Received(1).Unbind(binder);
        }

        [Test]
        public void Assign_When_has_no_binders_and_mediator_is_already_assigned_Should_unbind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var actual = Substitute.For<TestMediator>();
            sut.Assign(actual);
            // Act
            sut.Assign(Substitute.For<TestMediator>());
            // Assert
            actual.DidNotReceive().Unbind(Arg.Any<IBinder>());
        }

        [Test]
        public void Release_When_has_binders_and_mediator_is_assigned_Should_unbind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            sut.Bind(binder);
            var actual = Substitute.For<TestMediator>();
            sut.Assign(actual);
            // Act
            sut.Release();
            // Assert
            actual.Received(1).Unbind(binder);
        }

        [Test]
        public void Release_When_has_no_binders_and_mediator_is_assigned_Should_unbind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var actual = Substitute.For<TestMediator>();
            sut.Assign(actual);
            // Act
            sut.Release();
            // Assert
            actual.DidNotReceive().Unbind(Arg.Any<IBinder>());
        }

        [Test]
        public void Release_When_has_binders_and_mediator_is_not_assigned_Should_not_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            sut.Bind(binder);
            // Act
            Action act = () => sut.Release();
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Release_When_has_no_binders_and_mediator_is_not_assigned_Should_not_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            // Act
            Action act = () => sut.Release();
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Bind_When_has_assigned_mediator_Should_bind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            var actual = Substitute.For<TestMediator>();
            sut.Assign(actual);
            // Act
            sut.Bind(binder);
            // Assert
            actual.Received(1).Bind(binder);
        }

        [Test]
        public void Bind_When_has_no_assigned_mediator_Should_not_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            // Act
            Action act = () => sut.Bind(binder);
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Unbind_When_has_assigned_mediator_Should_unbind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            var actual = Substitute.For<TestMediator>();
            sut.Assign(actual);
            // Act
            sut.Unbind(binder);
            // Assert
            actual.Received(1).Unbind(binder);
        }

        [Test]
        public void Unbind_When_has_no_assigned_mediator_Should_not_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            // Act
            Action act = () => sut.Unbind(binder);
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void OnDestroy_When_has_assigned_mediator_Should_unbind_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            var binder = Substitute.For<IBinder>();
            var actual = Substitute.For<TestMediator>();
            sut.Bind(binder);
            sut.Assign(actual);
            // Act
            sut.ExecuteOnDestroy();
            // Assert
            actual.Received(1).Unbind(binder);
        }

        [Test]
        public void OnDestroy_When_has_no_assigned_mediator_Should_not_throw_exception()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            // Act
            Action act = () => sut.ExecuteOnDestroy();
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void OnDestroy_When_mediator_is_disposable_Should_dispose_mediator()
        {
            // Arrange
            MediatorAssigner sut = TestBehavior.CreateSut();
            TestMediator actual = Substitute.For<TestMediator, IDisposable>();
            sut.Assign(actual);
            // Act
            sut.ExecuteOnDestroy();
            // Assert
            actual.As<IDisposable>().Received(1).Dispose();
        }

        private sealed class TestBehavior : MonoBehaviour, IMonoBehaviourTest
        {
            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;

            public static MediatorAssigner CreateSut()
            {
                var test = new MonoBehaviourTest<TestBehavior>(false);
                return test.gameObject.AddComponent<MediatorAssigner>();
            }
        }

        internal abstract class TestMediator : IMediator, IInternalMediator
        {
            /// <inheritdoc />
            public abstract void Bind(IBinder binder);

            /// <inheritdoc />
            public abstract void Unbind(IBinder binder);
        }
    }
}