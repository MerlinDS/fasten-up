using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders.Behaviours;
using FastenUp.Runtime.Delegates;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityTestingAssist.Runtime;
using Object = UnityEngine.Object;

namespace FastenUp.Tests.Binders.Behaviours
{
    [TestFixture]
    [TestOf(typeof(VisibilityBinder))]
    public class VisibilityBinderTest
    {
        private static IEnumerable<TestCaseData> SetValueTestCases
        {
            get
            {
                // ReSharper disable once JoinDeclarationAndInitializer
                ITestCaseBuilder builder;
                builder = CreateBuilder((sut, container) =>
                {
                    var children = new[]
                    {
                        CreateGameObject(nameof(TestGraphic), container).AddComponent<TestGraphic>().Delegate,
                        CreateGameObject(nameof(TestLayoutElement), container).AddComponent<TestLayoutElement>()
                            .Delegate
                    };

                    sut.BehaviourChildren.Returns(children);
                });

                yield return new TestCaseData(builder, false, (Action<ITestCaseSut>)(sut =>
                {
                    foreach (var child in sut.BehaviourChildren)
                        child.Received().OnDisable();
                })).SetName(
                    "When_value_is_false_root_has_no_canvas_and_children_with_behaviours_Should_set_value_to_false_and_disable_children");

                yield return new TestCaseData(builder, true, (Action<ITestCaseSut>)(sut =>
                {
                    foreach (var child in sut.BehaviourChildren)
                        child.Received().OnEnable();
                })).SetName(
                    "When_value_is_true_root_has_no_canvas_and_children_with_behaviours_Should_set_value_to_true_and_enable_children");


                builder = CreateBuilder((sut, container) =>
                {
                    var canvas = container.AddComponent<Canvas>();
                    var children = new[]
                    {
                        CreateGameObject(nameof(TestGraphic), container).AddComponent<TestGraphic>().Delegate,
                        CreateGameObject(nameof(TestLayoutElement), container).AddComponent<TestLayoutElement>()
                            .Delegate
                    };

                    sut.Canvas.Returns(canvas);
                    sut.BehaviourChildren.Returns(children);
                });

                yield return new TestCaseData(builder, false, (Action<ITestCaseSut>)(sut =>
                {
                    sut.Canvas.enabled.Should().BeFalse();
                    foreach (var child in sut.BehaviourChildren)
                        child.DidNotReceive().OnDisable(); //Canvas is responsible for its children visibility
                })).SetName(
                    "When_value_is_false_root_has_canvas_and_children_with_behaviours_Should_set_value_to_false_disable_canvas_but_ignore_children");

                yield return new TestCaseData(builder, true,
                    (Action<ITestCaseSut>)(sut =>
                    {
                        sut.Canvas.enabled.Should().BeTrue();
                        foreach (var child in sut.BehaviourChildren)
                            child.DidNotReceive().OnDisable(); //Canvas is responsible for its children visibility
                    })).SetName(
                    "When_value_is_true_root_has_canvas_and_children_with_behaviours_Should_set_value_to_true_enable_canvas_but_ignore_children");

                builder = CreateBuilder((sut, container) =>
                {
                    var behaviours = new[]
                    {
                        CreateGameObject(nameof(TestGraphic), container).AddComponent<TestGraphic>().Delegate,
                        CreateGameObject(nameof(TestLayoutElement), container).AddComponent<TestLayoutElement>()
                            .Delegate
                    };

                    sut.BehaviourChildren.Returns(behaviours);

                    var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", container);
                    var childVisibilityBehaviours = new[]
                    {
                        CreateGameObject(nameof(TestGraphic), childGameObject)
                            .AddComponent<TestGraphic>().Delegate,
                        CreateGameObject(nameof(TestLayoutElement), childGameObject)
                            .AddComponent<TestLayoutElement>().Delegate
                    };
                    var childVisibility = CreateBinder(childGameObject);

                    var child = Substitute.For<ITestCaseSut>();
                    child.VisibilityBinder.Returns(childVisibility);
                    child.BehaviourChildren.Returns(childVisibilityBehaviours);

                    sut.BehaviourChildren.Returns(behaviours);
                    sut.Children.Returns(new[] { child });
                });
                yield return new TestCaseData(builder, false, (Action<ITestCaseSut>)(sut =>
                {
                    foreach (var behaviour in sut.BehaviourChildren)
                        behaviour.Received().OnDisable();

                    foreach (var child in sut.Children)
                    {
                        child.VisibilityBinder.GetValue().Should()
                            .BeTrue("child visibility value should not be changed by parent");
                        foreach (var childBehaviour in child.BehaviourChildren)
                            childBehaviour.Received().OnDisable();
                    }
                })).SetName(
                    "When_value_is_false_root_has_children_with_behaviours_and_visibility_Should_set_value_to_false_and_disable_all_children_but_not_change_visibility_value_of_children");

                yield return new TestCaseData(builder, true, (Action<ITestCaseSut>)(sut =>
                {
                    foreach (var behaviour in sut.BehaviourChildren)
                        behaviour.Received().OnEnable();

                    foreach (var child in sut.Children)
                    {
                        child.VisibilityBinder.GetValue().Should()
                            .BeTrue("child visibility value should not be changed by parent");
                        foreach (var childBehaviour in child.BehaviourChildren)
                            childBehaviour.Received().OnEnable();
                    }
                })).SetName(
                    "When_value_is_true_root_has_children_with_behaviours_and_visibility_Should_set_value_to_true_and_enable_all_children_but_not_change_visibility_value_of_children");

                builder = CreateBuilder((sut, container) =>
                {
                    var behaviours = new[]
                    {
                        CreateGameObject(nameof(TestGraphic), container).AddComponent<TestGraphic>().Delegate,
                        CreateGameObject(nameof(TestLayoutElement), container).AddComponent<TestLayoutElement>()
                            .Delegate
                    };

                    sut.BehaviourChildren.Returns(behaviours);

                    var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", container);
                    var childVisibilityBehaviours = new[]
                    {
                        CreateGameObject(nameof(TestGraphic), childGameObject)
                            .AddComponent<TestGraphic>().Delegate,
                        CreateGameObject(nameof(TestLayoutElement), childGameObject)
                            .AddComponent<TestLayoutElement>().Delegate
                    };
                    var childVisibility = CreateBinder(childGameObject);
                    childVisibility.SetValue(false);

                    foreach (var behaviour in childVisibilityBehaviours)
                        behaviour.ClearReceivedCalls();

                    var child = Substitute.For<ITestCaseSut>();
                    child.VisibilityBinder.Returns(childVisibility);
                    child.BehaviourChildren.Returns(childVisibilityBehaviours);

                    sut.BehaviourChildren.Returns(behaviours);
                    sut.Children.Returns(new[] { child });
                });

                yield return new TestCaseData(builder, false, (Action<ITestCaseSut>)(sut =>
                {
                    foreach (var behaviour in sut.BehaviourChildren)
                        behaviour.Received().OnDisable();

                    foreach (var child in sut.Children)
                    {
                        child.VisibilityBinder.GetValue().Should()
                            .BeFalse("child visibility value should not be changed by parent");
                        foreach (var childBehaviour in child.BehaviourChildren)
                            childBehaviour.DidNotReceive().OnDisable();
                    }
                })).SetName(
                    "When_value_is_false_root_has_children_with_behaviours_and_visibility_with_default_false_Should_set_value_to_false_and_disable_own_behaviours_and_ignore_children");

                yield return new TestCaseData(builder, true, (Action<ITestCaseSut>)(sut =>
                {
                    foreach (var behaviour in sut.BehaviourChildren)
                        behaviour.Received().OnEnable();

                    foreach (var child in sut.Children)
                    {
                        child.VisibilityBinder.GetValue().Should()
                            .BeFalse("child visibility value should not be changed by parent");
                        foreach (var childBehaviour in child.BehaviourChildren)
                            childBehaviour.DidNotReceive().OnEnable();
                    }
                })).SetName(
                    "When_value_is_true_root_has_children_with_behaviours_and_visibility_with_default_false_Should_set_value_to_true_and_enable_own_behaviours_and_ignore_children");
            }
        }

        [TestCaseSource(nameof(SetValueTestCases))]
        public void SetValue(ITestCaseBuilder builder, bool value, Action<ITestCaseSut> assert)
        {
            //Arrange
            var sut = builder.Build();
            if (value)
                sut.VisibilityBinder.SetValue(false);

            if (sut.BehaviourChildren != null)
            {
                foreach (var child in sut.BehaviourChildren)
                    child.ClearReceivedCalls();
            }

            //Act
            sut.VisibilityBinder.SetValue(value);
            //Assert
            sut.VisibilityBinder.GetValue().Should().Be(value);
            assert(sut);
        }

        [Test]
        public void SetValue_When_value_true_and_parent_not_visible_Should_not_enable_children()
        {
            //Arrange
            var parent = CreateBinder(CreateGameObject(nameof(VisibilityBinderTest)));
            parent.SetValue(false);
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", parent.gameObject);
            var childVisibilityBehaviours = new[]
            {
                CreateGameObject(nameof(TestGraphic), childGameObject)
                    .AddComponent<TestGraphic>().Delegate,
                CreateGameObject(nameof(TestLayoutElement), childGameObject)
                    .AddComponent<TestLayoutElement>().Delegate
            };
            var sut = CreateBinder(childGameObject);
            sut.SetValue(false);

            foreach (var behaviour in childVisibilityBehaviours)
                behaviour.ClearReceivedCalls();
            //Act
            sut.SetValue(true);
            //Assert
            sut.GetValue().Should().BeTrue();
            foreach (var behaviour in childVisibilityBehaviours)
                behaviour.DidNotReceive().OnEnable();
        }

        [Test]
        public void SetValue_When_value_false_and_children_gameObjects_disabled_Should_not_enable_children()
        {
            //Arrange
            var gameObject = CreateGameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);

            var behaviours = new[]
            {
                childGameObject.AddComponent<TestGraphic>().Delegate,
                childGameObject.AddComponent<TestLayoutElement>().Delegate
            };
            childGameObject.SetActive(false);
            var sut = CreateBinder(gameObject);

            foreach (var behaviour in behaviours)
                behaviour.ClearReceivedCalls();
            //Act
            sut.SetValue(false);
            //Assert
            sut.GetValue().Should().BeFalse();
            foreach (var behaviour in behaviours)
                behaviour.DidNotReceive().OnDisable();
        }

        [Test]
        public void SetValue_When_child_gameObject_with_behaviour_was_destroyed_and_cache_not_rebuilt_Should_log_error()
        {
            //Arrange
            var gameObject = CreateGameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);
            childGameObject.AddComponent<TestGraphic>();
            var sut = CreateBinder(gameObject);
            //Act
            Object.DestroyImmediate(childGameObject);
            sut.SetValue(false);
            //Assert
            LogAssert.Expect(LogType.Error, "Hierarchy was changed. But RefreshCache was not called.");
        }

        [Test]
        public void
            SetValue_When_child_gameObject_with_visibility_was_destroyed_and_cache_not_rebuilt_Should_log_error()
        {
            //Arrange
            var gameObject = CreateGameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);
            childGameObject.AddComponent<VisibilityBinder>();
            var sut = CreateBinder(gameObject);
            //Act
            Object.DestroyImmediate(childGameObject);
            sut.SetValue(false);
            //Assert
            LogAssert.Expect(LogType.Error, "Hierarchy was changed. But RefreshCache was not called.");
        }

        [Test]
        public void SetValue_When_child_gameObject_with_behaviour_was_destroyed_and_cache_rebuilt_Should_not_log_error()
        {
            //Arrange
            var gameObject = CreateGameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);
            childGameObject.AddComponent<TestGraphic>();
            var sut = CreateBinder(gameObject);
            //Act
            Object.DestroyImmediate(childGameObject);
            sut.RebuildCache();
            sut.SetValue(false);
            //Assert
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void
            SetValue_When_child_gameObject_with_visibility_was_destroyed_and_cache_rebuilt_Should_not_log_error()
        {
            //Arrange
            var gameObject = CreateGameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);
            childGameObject.AddComponent<VisibilityBinder>();
            var sut = CreateBinder(gameObject);
            //Act
            Object.DestroyImmediate(childGameObject);
            sut.RebuildCache();
            sut.SetValue(false);
            //Assert
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void OnBinderChanged_When_visibility_changed_Should_call_SetValue()
        {
            //Arrange
            var gameObject = new GameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);
            var sut = CreateBinder(childGameObject);
            var parent = CreateBinder(gameObject);
            var handler = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += handler;
            //Act
            parent.SetValue(false);
            //Assert
            handler.Received(1).Invoke(sut);
        }

        [Test]
        public void SetValue_When_value_the_same_Should_not_update_children()
        {
            //Arrange
            var gameObject = new GameObject(nameof(VisibilityBinderTest));
            var childGameObject = CreateGameObject(nameof(VisibilityBinderTest) + "Child", gameObject);
            var sut = CreateBinder(childGameObject);
            var parent = CreateBinder(gameObject);
            var handler = Substitute.For<OnBinderChanged>();
            sut.OnBinderChanged += handler;
            //Act
            parent.SetValue(true);
            //Assert
            handler.DidNotReceive().Invoke(sut);
        }

        private static ITestCaseBuilder CreateBuilder(Action<ITestCaseSut, GameObject> build)
        {
            var builder = Substitute.For<ITestCaseBuilder>();
            builder.Build().Returns(_ =>
            {
                var sut = Substitute.For<ITestCaseSut>();
                var container = CreateGameObject(nameof(VisibilityBinderTest));
                build(sut, container);
                var visibility = CreateBinder(container);
                sut.VisibilityBinder.Returns(visibility);
                return sut;
            });
            return builder;
        }

        private static VisibilityBinder CreateBinder(GameObject gameObject)
        {
            var visibility = gameObject.AddComponent<VisibilityBinder>();
            visibility.ExecuteAwake();
            return visibility;
        }

        private static GameObject CreateGameObject(string name, GameObject parent = null)
        {
            var gameObject = new GameObject(name);
            if (parent != null)
                gameObject.transform.SetParent(parent.transform);
            return gameObject;
        }


        public interface ITestCaseBuilder
        {
            ITestCaseSut Build();
        }

        public interface ITestCaseSut
        {
            Canvas Canvas { get; }
            VisibilityBinder VisibilityBinder { get; }
            IBehaviourDelegate[] BehaviourChildren { get; }
            ITestCaseSut[] Children { get; }
        }

        private class TestGraphic : Graphic
        {
            public IBehaviourDelegate Delegate { get; } = Substitute.For<IBehaviourDelegate>();

            /// <inheritdoc />
            protected override void OnEnable()
            {
                Delegate.OnEnable();
                base.OnEnable();
            }

            /// <inheritdoc />
            protected override void OnDisable()
            {
                Delegate.OnDisable();
                base.OnDisable();
            }
        }

        private class TestLayoutElement : LayoutElement
        {
            public IBehaviourDelegate Delegate { get; } = Substitute.For<IBehaviourDelegate>();

            /// <inheritdoc />
            protected override void OnEnable()
            {
                Delegate.OnEnable();
                base.OnEnable();
            }

            /// <inheritdoc />
            protected override void OnDisable()
            {
                Delegate.OnDisable();
                base.OnDisable();
            }
        }

        public interface IBehaviourDelegate
        {
            void OnEnable();
            void OnDisable();
        }
    }
}