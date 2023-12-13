using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindings;
using FastenUp.Runtime.Proxies;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace FastenUp.Tests
{
    public class Temp
    {
        [Test]
        public void TempConcept()
        {
            var bindingPoint = Substitute.For<IBindingPoint<bool>>();
            bindingPoint.As<bool>().Returns(bindingPoint);
            bindingPoint.Value.Returns(true);
            
            var mediator = new GameObject().AddComponent<TestMediator>();
            mediator.Visibility.Value.Should().BeFalse();
            mediator.UpdateProxies(bindingPoint);
            mediator.Visibility.Value.Should().BeTrue();
            mediator.Visibility.Value = false;
            mediator.Visibility.Value.Should().BeFalse();
            bindingPoint.Received(1).Value = false;
        }

        public sealed partial class TestMediator : MonoBehaviour, IMediator
        {
            public DataProxy<bool> Visibility;
        }

        //Will be generated
        public sealed partial class TestMediator : IInternalMediator
        {
            /// <inheritdoc />
            public void UpdateProxies(IBindingPoint bindingPoint)
            {
                Visibility.Bind(bindingPoint.As<bool>());
            }
        }
    }
}