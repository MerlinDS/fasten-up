using System.Diagnostics.CodeAnalysis;
using FastenUp.Runtime.Binders.References;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
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
            bool result = sut.TryGetReference(out BaseRefBinderTest reference);
            //Assert
            result.Should().BeFalse();
            reference.Should().BeNull();
        }

        [Test]
        public void TryGetReference_When_reference_is_set_Should_return_true_and_reference()
        {
            //Arrange
            var sut = TestRefBinder.Create();
            Transform expected = sut.transform;
            //Act
            bool result = sut.TryGetReference(out Transform reference);
            //Assert
            result.Should().BeTrue();
            reference.Should().Be(expected);
        }

        [Test]
        public void Awake_When_Reference_property_is_valid_Should_set_reference()
        {
            //Arrange
            var sut = TestImageRefBinder.Create();
            var expected = new GameObject(nameof(BaseBinderTests) + "Other").AddComponent<Image>();
            sut.EditSerializable()
                .Field("Reference", a => a.objectReferenceValue = expected)
                .Apply();
            sut.ExecuteAwake();
            //Act
            bool result = sut.TryGetReference(out Image reference);
            //Assert
            result.Should().BeTrue();
            reference.Should().Be(expected);
        }

        [Test]
        public void Awake_When_Reference_property_is_null_valid_Should_set_reference()
        {
            //Arrange
            var sut = TestImageRefBinder.Create();
            var expected = sut.gameObject.GetComponent<Image>();
            sut.ExecuteAwake();
            //Act
            bool result = sut.TryGetReference(out Image reference);
            //Assert
            result.Should().BeTrue();
            reference.Should().Be(expected);
        }

        [Test]
        public void Awake_When_Reference_property_is_not_valid_Should_set_reference()
        {
            //Arrange
            var sut = TestImageRefBinder.Create();
            var expected = sut.gameObject.GetComponent<Image>();
            sut.EditSerializable()
                .Field("Reference", a => a.objectReferenceValue = sut.gameObject)
                .Apply();
            sut.ExecuteAwake();
            //Act
            bool result = sut.TryGetReference(out Image reference);
            //Assert
            result.Should().BeTrue();
            reference.Should().Be(expected);
            LogAssert.Expect(LogType.Warning,
                $"Reference TestImageRefBinder (UnityEngine.RectTransform) is not of type {nameof(Image)}. Instead, will try to find it as a component on the same GameObject.");
        }

        [Test]
        public void Awake_When_Reference_property_is_not_valid_and_component_is_not_found_Should_not_set_reference()
        {
            //Arrange
            var sut = TestImageRefBinder.Create();
            Object.DestroyImmediate(sut.gameObject.GetComponent<Image>());
            sut.EditSerializable()
                .Field("Reference", a => a.objectReferenceValue =
                    sut.gameObject)
                .Apply();
            sut.ExecuteAwake();
            //Act
            bool result = sut.TryGetReference(out Image reference);
            //Assert
            result.Should().BeFalse();
            reference.Should().BeNull();
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

        private class TestImageRefBinder : BaseRefBinder<Image>, IMonoBehaviourTest
        {
            /// <inheritdoc />
            [ExcludeFromCodeCoverage]
            public bool IsTestFinished => true;

            public static TestImageRefBinder Create()
            {
                var gameObject = new GameObject(nameof(TestImageRefBinder));
                gameObject.AddComponent<Image>();
                var testRefBinder = gameObject.AddComponent<TestImageRefBinder>();
                return testRefBinder;
            }
        }
    }
}