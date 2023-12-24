﻿using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Actions;
using FastenUp.Runtime.Binders.Events;
using FastenUp.Runtime.Mediators;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Mediators
{
    [TestFixture]
    [TestOf(typeof(IInternalMediator))]
    public class MediatorGenerationTests
    {
        [Test]
        public void Bind()
        {
            //Arrange
            var binders = new[]
            {
                CreateBinder<IValueReceiver<int>>("Property"),
                CreateBinder<IValueProvider<TestReference>>("Reference"),
                CreateBinder<IEventBinder<UnityAction>>("Event"),
                CreateBinder<IActionBinder<UnityEvent>>("Action")
            };

            var testReference = Substitute.For<TestReference>();
            var mockAction = Substitute.For<UnityAction>();
            var mockEvent = Substitute.For<UnityEvent>();
            binders[1].As<IValueProvider<TestReference>>().GetValue().Returns(testReference);
            binders[3].As<IActionBinder<UnityEvent>>().OnAction.Returns(mockEvent);
            var mediator = new IntegrationTestMediator();
            //Act
            foreach (var binder in binders)
                mediator.Bind(binder);

            mediator.Property.Value = 1;
            mediator.Event.AddListener(mockAction);
            mediator.Action.Invoke();
            //Assert
            mediator.Reference.Value.Should().Be(testReference, "Reference should provided by binder");
            binders[0].As<IValueReceiver<int>>().Received().SetValue(1);
            binders[2].As<IEventBinder<UnityAction>>().Received().AddListener(mockAction);
            mockEvent.Received().Invoke();
        }

        private static IBinder CreateBinder<T>(string name)
            where T : class, IBinder
        {
            var binder = Substitute.For<T>();
            binder.Name.Returns(name);
            return binder;
        }
    }

    internal sealed partial class IntegrationTestMediator : IMediator
    {
        public Bindable<int> Property { get; } = new();
        public BindableRef<TestReference> Reference { get; } = new();
        public BindableEvent Event { get; } = new();

        public BindableAction Action { get; } = new();
    }

    internal abstract class TestReference
    {
    }
}