using FastenUp.Runtime.Adapters;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace FastenUp.Tests.Adapters
{
    [TestFixture]
    [TestOf(typeof(ComponentFieldAdapter<>))]
    public class ComponentFieldAdapterTest
    {
        [Test]
        public void Create_When_gameObject_has_component_Should_return_adapter()
        {
            //Arrange
            var gameObject = new GameObject(nameof(ComponentFieldAdapterTest));
            var component = gameObject.AddComponent<TestComponent>();
            component.Value = "test";
            //Act
            var adapter = TestAdapter.Create(gameObject);
            //Assert
            adapter.Should().NotBeNull();
            adapter.Value.Should().Be(component.Value);
        }
        
        [Test]
        public void Create_When_gameObject_has_no_component_Should_return_null()
        {
            //Arrange
            var gameObject = new GameObject(nameof(ComponentFieldAdapterTest));
            //Act
            var adapter = TestAdapter.Create(gameObject);
            //Assert
            adapter.Should().BeNull();
        }


        private sealed class TestAdapter : ComponentFieldAdapter<TestComponent>,
            IComponentFieldAdapter<string>
        {
            public static IComponentFieldAdapter<string> Create(GameObject gameObject) =>
                Create<TestAdapter, string>(gameObject);
            public TestAdapter(TestComponent component) : base(component)
            {
            }

            /// <inheritdoc />
            public string Value
            {
                get => Component.Value;
                set => Component.Value = value;
            }
        }

        private sealed class TestComponent : MonoBehaviour
        {
            public string Value { get; set; }
        }
    }
}