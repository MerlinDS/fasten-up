using System;
using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Collections;
using FastenUp.Runtime.Mediators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityTestingAssist.Runtime;
using Object = UnityEngine.Object;

namespace FastenUp.Tests.Binders.Collections
{
    [TestFixture]
    [TestOf(typeof(MediatorCollectionBinder))]
    public class MediatorCollectionBinderTest
    {
        [Test]
        public void
            Add_When_prefab_has_no_mediator_assigner_Should_add_mediator_to_collection_and_attach_mediator_assigner()
        {
            // Arrange
            var prefab = CreatePrefab();
            var sut = TestBehavior.Create(prefab);
            var mediator = Substitute.For<IInternalMediator>();
            sut.ExecuteOnEnable();
            // Act
            sut.Add(mediator);
            // Assert
            sut.gameObject.transform.childCount.Should().Be(1);
            sut.GetComponentInChildren<MediatorAssigner>().Should().NotBeNull();
            sut.gameObject.transform.GetChild(0).GetSiblingIndex().Should().Be(0);
        }

        [Test]
        public void Add_When_prefab_has_mediator_assigner_Should_add_mediator_to_collection()
        {
            // Arrange
            var prefab = CreatePrefab(true);
            var sut = TestBehavior.Create(prefab);
            var mediator = Substitute.For<IInternalMediator>();
            sut.ExecuteOnEnable();
            // Act
            sut.Add(mediator);
            // Assert
            sut.gameObject.transform.childCount.Should().Be(1);
            sut.GetComponentInChildren<MediatorAssigner>().Should().NotBeNull();
            sut.gameObject.transform.GetChild(0).GetSiblingIndex().Should().Be(0);
        }

        [Test]
        public void Remove_When_mediator_is_not_in_collection_Should_not_throw_exception()
        {
            // Arrange
            var prefab = CreatePrefab();
            var sut = TestBehavior.Create(prefab);
            var mediator = Substitute.For<IInternalMediator>();
            sut.ExecuteOnEnable();
            // Act
            Action act = () => sut.Remove(mediator);
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Remove_When_mediator_is_in_collection_Should_release_mediator_assigner()
        {
            // Arrange
            var prefab = CreatePrefab();
            var sut = TestBehavior.Create(prefab);
            var mediator = Substitute.For<IInternalMediator>();
            sut.ExecuteOnEnable();
            sut.Add(mediator);
            // Act
            sut.Remove(mediator);
            // Assert
            sut.gameObject.transform.GetChild(0).gameObject.activeSelf.Should().BeFalse();
        }

        [Test] public void Remove_When_called_twice_on_same_mediator_Should_release_mediator_assigner_once()
        {
            // Arrange
            var prefab = CreatePrefab();
            var sut = TestBehavior.Create(prefab);
            var mediator = Substitute.For<IInternalMediator>();
            sut.ExecuteOnEnable();
            sut.Add(mediator);
            sut.Remove(mediator);
            // Act
            sut.Remove(mediator);
            // Assert
            sut.gameObject.transform.GetChild(0).gameObject.activeSelf.Should().BeFalse();
        }

        [Test]
        public void OnEnable_When_prefab_is_not_null_Should_disable_prefab()
        {
            // Arrange
            var prefab = CreatePrefab();
            var sut = TestBehavior.Create(prefab);
            // Act
            sut.ExecuteOnEnable();
            // Assert
            prefab.activeSelf.Should().BeFalse();
        }

        [Test]
        public void OnEnable_When_prefab_is_null_Should_throw_exception()
        {
            // Arrange
            var sut = TestBehavior.Create(null);
            // Act
            Action act = () => sut.ExecuteOnEnable();
            // Assert
            act.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void OnDisable_When_called_Should_clear_pool()
        {
            // Arrange
            var prefab = CreatePrefab();
            var sut = TestBehavior.Create(prefab);
            sut.ExecuteOnEnable();
            sut.Add(Substitute.For<IInternalMediator>());
            var actual = sut.gameObject.transform.GetChild(0).gameObject;
            // Act
            sut.ExecuteOnDisable();
            // Assert
            actual.activeSelf.Should().BeFalse();
        }

        private sealed class TestBehavior : MonoBehaviour, IMonoBehaviourTest
        {
            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;

            public static MediatorCollectionBinder Create(Object prefab)
            {
                var test = new MonoBehaviourTest<TestBehavior>(false);
                var binder = test.gameObject.AddComponent<MediatorCollectionBinder>();
                test.gameObject.AddComponent<TestMediator>();
                binder.EditSerializable().Field("_prefab", prefab).Apply();
                binder.EditSerializable().Field("Name", "Test").Apply();
                return binder;
            }
        }

        private static GameObject CreatePrefab(bool hasAssigner = false)
        {
            var prefab = new GameObject(nameof(TestBehavior) + "Prefab");
            if (hasAssigner)
                prefab.AddComponent<MediatorAssigner>();
            return prefab;
        }

        private sealed class TestMediator : MonoBehaviour, IInternalMediator
        {
            /// <inheritdoc />
            public void Bind(IBinder binder)
            {
            }

            /// <inheritdoc />
            public void Unbind(IBinder binder)
            {
            }
        }
    }
}