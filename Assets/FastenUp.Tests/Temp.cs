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
            //Arrange 
            var bindingPoint = Substitute.For<IBindingPoint<bool>>();
            bindingPoint.Name.Returns("Visibility");
            bindingPoint.CanBind<bool>().Returns(true);
            bindingPoint.As<bool>().Returns(bindingPoint);
            bindingPoint.Value.Returns(true);
            //Act
            var mediator = new GameObject().AddComponent<TestMediator>(); 
            //Assert
            mediator.Visibility.Value.Should().BeFalse();
            mediator.UpdateProxies(bindingPoint);
            mediator.Visibility.Value.Should().BeTrue();
            mediator.Visibility.Value = false;
            mediator.Visibility.Value.Should().BeFalse();
            bindingPoint.Received(1).Value = false;
        }
    }

    public partial class TestMediator : MonoBehaviour, IMediator
    {
        public DataProxy<bool> Visibility;
        public DataProxy<int> IntValue;
    }
}