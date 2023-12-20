using System;
using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Base
{
    [TestFixture]
    [TestOf(typeof(BaseBindAction<>))]
    public class BaseBindActionTest
    {
        [Test]
        public void AddBindableListener_When_listener_already_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            Action act = ()=>
            {
                sut.AddBindableListener(bindableListener);
                sut.AddBindableListener(bindableListener);
            };
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void AddBindableListener_When_listener_not_added_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            Action act = ()=> sut.AddBindableListener(bindableListener);
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void AddBindableListener_When_action_was_added_Should_call_add_listener_on_bindable_listener()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.AddBindableListener(bindableListener);
            //Assert
            bindableListener.Received(1).AddListener(listener);
        }
        
        [Test]
        public void RemoveBindableListener_When_listener_not_added_Should_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            Action act = ()=> sut.RemoveBindableListener(bindableListener);
            //Act & Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void RemoveBindableListener_When_listener_added_Should_not_throw_exception()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            Action act = ()=>
            {
                sut.AddBindableListener(bindableListener);
                sut.RemoveBindableListener(bindableListener);
            };
            //Act & Assert
            act.Should().NotThrow<FastenUpException>();
        }
        
        [Test]
        public void RemoveBindableListener_When_action_was_added_Should_call_remove_listener_on_bindable_listener()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            var listener = Substitute.For<UnityAction>();
            sut.AddBindableListener(bindableListener);
            sut.AddListener(listener);
            //Act
            sut.RemoveBindableListener(bindableListener);
            //Assert
            bindableListener.Received(1).RemoveListener(listener);
        }

        [Test]
        public void AddListener_Should_call_add_listener_on_bindable_listener()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            sut.AddBindableListener(bindableListener);
            var listener = Substitute.For<UnityAction>();
            //Act
            sut.AddListener(listener);
            //Assert
            bindableListener.Received(1).AddListener(listener);
        }
        
        [Test]
        public void RemoveListener_Should_call_remove_listener_on_bindable_listener()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            sut.AddBindableListener(bindableListener);
            var listener = Substitute.For<UnityAction>();
            //Act
            sut.RemoveListener(listener);
            //Assert
            bindableListener.Received(1).RemoveListener(listener);
        }
        
        [Test]
        public void Dispose_Should_call_remove_listener_on_bindable_listener()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            sut.AddBindableListener(bindableListener);
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.Dispose();
            //Assert
            bindableListener.Received(1).RemoveListener(listener);
        }
        
        [Test]
        public void Dispose_Should_clear_listeners()
        {
            //Arrange
            var bindableListener = Substitute.For<IBindableListener<UnityAction>>();
            var sut = new TestBindAction();
            sut.AddBindableListener(bindableListener);
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            sut.Dispose();
            //Assert
            sut.HasListeners(listener).Should().BeFalse();
        }
        
        [Test]
        public void HasListeners_When_listener_not_added_Should_return_false()
        {
            //Arrange
            var sut = new TestBindAction();
            var listener = Substitute.For<UnityAction>();
            //Act
            var result = sut.HasListeners(listener);
            //Assert
            result.Should().BeFalse();
        }
        
        [Test]
        public void HasListeners_When_listener_added_Should_return_true()
        {
            //Arrange
            var sut = new TestBindAction();
            var listener = Substitute.For<UnityAction>();
            sut.AddListener(listener);
            //Act
            var result = sut.HasListeners(listener);
            //Assert
            result.Should().BeTrue();
        }

        private class TestBindAction : BaseBindAction<UnityAction>
        {
            public new void AddBindableListener(IBindableListener<UnityAction> listener) => 
                base.AddBindableListener(listener);
            
            public new void RemoveBindableListener(IBindableListener<UnityAction> listener) =>
                base.RemoveBindableListener(listener);
        }
    }
}