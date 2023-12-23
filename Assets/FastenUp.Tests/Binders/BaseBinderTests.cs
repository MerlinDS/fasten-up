using System;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Delegates;
using FastenUp.Runtime.Mediators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders
{
    [TestFixture]
    [TestOf(typeof(BaseBinder))]
    public class BaseBinderTests
    {
        [Test]
        public void OnEnable_When_has_mediator_Should_bind_to_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediator);
            test.component.SetName("Test");
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            mockMediator.Received(1).Bind(test.component);
        }
        
        [Test]
        public void OnEnable_When_has_two_mediators_Should_bind_to_both_mediators()
        {
            //Arrange
            var mockMediatorA = Substitute.For<IInternalMediator>();
            var mockMediatorB = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediatorA);
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediatorB);
            test.component.SetName("Test");
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            mockMediatorA.Received(1).Bind(test.component);
            mockMediatorB.Received(1).Bind(test.component);
        }
        
        [Test]
        public void OnEnable_When_mediator_was_cached_Should_not_bind_to_mediator_twice()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediator);
            test.component.SetName("Test");
            test.component.ExecuteOnEnable();
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            mockMediator.Received(1).Bind(test.component);
        }

        [Test]
        public void OnEnable_When_has_mediator_in_parent_object_Should_bind_to_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            var parent = new GameObject(nameof(BaseBinderTests));
            parent.AddComponent<MockMediator>().Set(mockMediator);
            test.component.SetParent(parent);
            test.component.SetName("Test");
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            mockMediator.Received(1).Bind(test.component);
        }

        [Test]
        public void OnEnable_When_has_no_mediators_Should_log_error()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            test.component.SetName("Test");
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            LogAssert.Expect(LogType.Error,
                $"{test.component.name} will be ignored: {nameof(IMediator)} was not found!");
        }

        [Test]
        public void OnEnable_When_name_was_not_set_Should_log_error()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            LogAssert.Expect(LogType.Error,
                $"{test.component.name} will be ignored: name for binding was not set!");
        }
        
        [Test]
        public void OnEnable_When_name_starts_with_hashtag_Should_not_log_error_and_bind_to_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.SetName("#Test");
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediator);
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            LogAssert.NoUnexpectedReceived();
            mockMediator.DidNotReceive().Bind(test.component);
        }

        [Test]
        public void OnDisable_When_has_mediator_Should_unbind_from_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediator);
            test.component.SetName("Test");
            test.component.ExecuteOnEnable();
            //Act
            test.component.ExecuteOnDisable();
            //Assert
            mockMediator.Received(1).Unbind(test.component);
        }

        [Test]
        public void OnDisable_When_has_mediator_in_parent_object_Should_unbind_from_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            var parent = new GameObject(nameof(BaseBinderTests));
            parent.AddComponent<MockMediator>().Set(mockMediator);
            test.component.SetName("Test");
            test.component.SetParent(parent);
            test.component.ExecuteOnEnable();
            //Act
            test.component.ExecuteOnDisable();
            //Assert
            mockMediator.Received(1).Unbind(test.component);
        }

        [Test]
        public void OnDisable_When_has_no_mediators_Should_not_throw_exception()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            Action act = () => test.component.ExecuteOnDisable();
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void InvokeValueChanged_When_has_no_subscribers_Should_not_throw_exception()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            Action act = () => test.component.InvokeOnBinderChanged();
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void InvokeValueChanged_When_has_subscribers_Should_invoke_subscribers()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            var subscriber = Substitute.For<OnBinderChanged>();
            test.component.OnBinderChanged += subscriber;
            //Act
            test.component.InvokeOnBinderChanged();
            //Assert
            subscriber.Received(1).Invoke(test.component);
        }

        [Test]
        public void Name_getter_When_has_name_Should_return_name()
        {
            //Arrange
            const string expected = nameof(TestingBehaviour);
            var test = TestingBehaviour.Create();
            test.component.EditSerializable()
                .Field(nameof(test.component.Name), expected)
                .Apply();
            //Act
            //Assert
            test.component.Name.Should().Be(expected);
        }

        private class TestingBehaviour : BaseBinder, IMonoBehaviourTest
        {
            public static MonoBehaviourTest<TestingBehaviour> Create() =>
                new(false);

            public void SetName(string binderName) =>
                this.EditSerializable().Field(nameof(Name), binderName).Apply();

            public void SetParent(GameObject parent) =>
                transform.SetParent(parent.transform);

            public new void InvokeOnBinderChanged() =>
                base.InvokeOnBinderChanged();

            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;
        }

        private class MockMediator : MonoBehaviour, IInternalMediator
        {
            private IInternalMediator _mediator;

            public void Set(IInternalMediator mediator) =>
                _mediator = mediator;

            /// <inheritdoc />
            public void Bind(IBinder binder) =>
                _mediator.Bind(binder);

            /// <inheritdoc />
            public void Unbind(IBinder binder) =>
                _mediator.Unbind(binder);
        }
    }
}