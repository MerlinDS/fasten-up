using System;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Delegates;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BaseBindable))]
    public class BaseBindableTests
    {
        [Test]
        public void OnEnable_When_has_mediator_Should_bind_to_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediator);
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
            var parent = new GameObject(nameof(BaseBindableTests));
            parent.AddComponent<MockMediator>().Set(mockMediator);
            test.component.SetParent(parent);
            //Act
            test.component.ExecuteOnEnable();
            //Assert
            mockMediator.Received(1).Bind(test.component);
        }

        [Test]
        public void OnEnable_When_has_no_mediators_Should_throw_exception()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            Action act = () => test.component.ExecuteOnEnable();
            //Act & Assert
            act.Should().Throw<Exception>();
        }

        [Test]
        public void OnDisable_When_has_mediator_Should_unbind_from_mediator()
        {
            //Arrange
            var mockMediator = Substitute.For<IInternalMediator>();
            var test = TestingBehaviour.Create();
            test.component.gameObject.AddComponent<MockMediator>().Set(mockMediator);
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
            var parent = new GameObject(nameof(BaseBindableTests));
            parent.AddComponent<MockMediator>().Set(mockMediator);
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
            Action act = () => test.component.InvokeValueChanged<string>();
            //Act & Assert
            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void InvokeValueChanged_When_has_subscribers_Should_invoke_subscribers()
        {
            //Arrange
            var test = TestingBehaviour.Create();
            var subscriber = Substitute.For<OnValueChanged>();
            test.component.OnValueChanged += subscriber;
            //Act
            test.component.InvokeValueChanged<string>();
            //Assert
            subscriber.Received(1).Invoke(test.component, typeof(string));
        }

        [Test]
        public void Name_getter_When_has_name_Should_return_name()
        {
            //Arrange
            const string expected = nameof(TestingBehaviour);
            var test = TestingBehaviour.Create();
            test.component.SetName(expected);
            //Act
            //Assert
            test.component.Name.Should().Be(expected);
        }

        private class TestingBehaviour : BaseBindable, IMonoBehaviourTest
        {
            public static MonoBehaviourTest<TestingBehaviour> Create() =>
                new(false);

            public void SetName(string bindingName)
            {
                //TODO: Move to UnityTestingAssist package
                var serializedObject = new SerializedObject(this);
                var property = serializedObject.FindProperty("<Name>k__BackingField");
                property.stringValue = bindingName;
                serializedObject.ApplyModifiedProperties();
                serializedObject.Dispose();
            }

            public void SetParent(GameObject parent) =>
                transform.SetParent(parent.transform);

            public new void InvokeValueChanged<TValue>() =>
                base.InvokeValueChanged<TValue>();

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
            public void Bind(IBindable bindable) =>
                _mediator.Bind(bindable);

            /// <inheritdoc />
            public void Unbind(IBindable bindable) =>
                _mediator.Unbind(bindable);
        }
    }
}