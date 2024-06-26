﻿using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Binders.Actions;
using FastenUp.Runtime.Binders.Collections;
using FastenUp.Runtime.Binders.Events;
using FastenUp.Runtime.Binders.References;
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
            IBinder[] binders = {
                CreateBinder<IValueReceiver<int>>("Property"),
                CreateBinder<IRefBinder>("Reference"),
                CreateBinder<IEventBinder<UnityAction>>("Event"),
                CreateBinder<IActionBinder<UnityEvent>>("Action"),
                CreateBinder<IValueReceiver<float>>("Setup"),
                CreateBinder<ICollectionBinder<IInternalMediator>>("Collection")
            };

            var testReference = Substitute.For<TestReference>();
            var mockAction = Substitute.For<UnityAction>();
            var mockEvent = Substitute.For<UnityEvent>();
            binders[1].As<IRefBinder>().TryGetReference(out Arg.Any<TestReference>()).Returns(x =>
            {
                x[0] = testReference;
                return true;
            });
            binders[3].As<IActionBinder<UnityEvent>>().OnAction.Returns(mockEvent);
            var mediator = new IntegrationTestMediator();
            //Act
            foreach (IBinder binder in binders)
            {
                mediator.Bind(binder);
            }

            mediator.Property.Value = 1;
            mediator.Setup.Value = 1F;
            mediator.Event.AddListener(mockAction);
            mediator.Action.Invoke();
            mediator.Collection.Add(Substitute.For<TestReference>());
            //Assert
            mediator.Reference.Value.Should().Be(testReference, "Reference should provided by binder");
            binders[0].As<IValueReceiver<int>>().Received().SetValue(1);
            binders[4].As<IValueReceiver<float>>().Received().SetValue(1F);
            binders[2].As<IEventBinder<UnityAction>>().Received().AddListener(mockAction);
            binders[5].As<ICollectionBinder<IInternalMediator>>().Received().Add(Arg.Any<TestReference>());
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
        
        public BindableSetup<float> Setup { get; } = new();
        public BindableRef<TestReference> Reference { get; } = new();
        public BindableEvent Event { get; } = new();

        public BindableAction Action { get; } = new();
        
        public BindableCollection<TestReference> Collection { get; } = new(); 
    }

    internal abstract partial class TestReference : IMediator
    {
    }
}