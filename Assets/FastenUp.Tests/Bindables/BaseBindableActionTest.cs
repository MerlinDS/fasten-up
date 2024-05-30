using System;
using System.Collections.Generic;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders.Actions;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BaseBindableAction<>))]
    public class BaseBindableActionTest
    {

        [Test]
        public void Bind_When_actionBinder_has_valid_action_Should_add_actionBinder_to_binders()
        {
            //Arrange
            var actionBinder = Substitute.For<IActionBinder<UnityEvent>>();
            actionBinder.OnAction.Returns(Substitute.For<UnityEvent>());
            var sut = Substitute.ForPartsOf<TestBindableAction>();
            //Act
            sut.As<IBindableAction<UnityEvent>>().Bind(actionBinder);
            //Assert
            HashSet<IActionBinder<UnityEvent>>.Enumerator enumerator = sut.Binders.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(actionBinder);
        }
        
        [Test]
        public void Bind_When_actionBinder_has_null_action_Should_not_add_actionBinder_to_binders()
        {
            //Arrange
            var actionBinder = Substitute.For<IActionBinder<UnityEvent>>();
            var sut = Substitute.ForPartsOf<TestBindableAction>();
            //Act
            sut.As<IBindableAction<UnityEvent>>().Bind(actionBinder);
            //Assert
            HashSet<IActionBinder<UnityEvent>>.Enumerator enumerator = sut.Binders.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator.MoveNext().Should().BeFalse();
        }
        
        [Test]
        public void Bind_When_actionBinder_already_bind_Should_throw_exception()
        {
            //Arrange
            var actionBinder = Substitute.For<IActionBinder<UnityEvent>>();
            actionBinder.OnAction.Returns(Substitute.For<UnityEvent>());
            var sut = Substitute.ForPartsOf<TestBindableAction>();
            sut.As<IBindableAction<UnityEvent>>().Bind(actionBinder);
            //Act
            Action act =() => sut.As<IBindableAction<UnityEvent>>().Bind(actionBinder);
            //Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_actionBinder_has_valid_action_Should_remove_actionBinder_from_binders()
        {
            //Arrange
            var actionBinder = Substitute.For<IActionBinder<UnityEvent>>();
            actionBinder.OnAction.Returns(Substitute.For<UnityEvent>());
            var sut = Substitute.ForPartsOf<TestBindableAction>();
            sut.As<IBindableAction<UnityEvent>>().Bind(actionBinder);
            //Act
            sut.As<IBindableAction<UnityEvent>>().Unbind(actionBinder);
            //Assert
            HashSet<IActionBinder<UnityEvent>>.Enumerator enumerator = sut.Binders.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator.MoveNext().Should().BeFalse();
        }
        
        [Test]
        public void Unbind_When_actionBinder_not_bind_Should_throw_exception()
        {
            //Arrange
            var actionBinder = Substitute.For<IActionBinder<UnityEvent>>();
            actionBinder.OnAction.Returns(Substitute.For<UnityEvent>());
            var sut = Substitute.ForPartsOf<TestBindableAction>();
            //Act
            Action act =() => sut.As<IBindableAction<UnityEvent>>().Unbind(actionBinder);
            //Assert
            act.Should().Throw<FastenUpException>();
        }
        

        internal abstract class TestBindableAction : BaseBindableAction<UnityEvent>
        {
            public new BinderSet<IActionBinder<UnityEvent>> Binders => base.Binders;
        }
    }
}