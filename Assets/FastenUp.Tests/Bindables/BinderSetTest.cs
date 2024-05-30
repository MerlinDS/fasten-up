using System;
using System.Collections.Generic;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BinderSet<>))]
    public class BinderSetTest
    {
        [Test]
        public void AddBinder_When_binder_not_In_set_Should_adds_binder()
        {
            // Arrange
            var binder = Substitute.For<IBinder>();
            var sut = new BinderSet<IBinder>();
            // Act
            sut.Add(binder);
            // Assert
            using HashSet<IBinder>.Enumerator enumerator = sut.GetEnumerator();
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(binder);
        }

        [Test]
        public void AddBinder_When_binder_In_set_Should_throw_exception()
        {
            // Arrange
            var binder = Substitute.For<IBinder>();
            var sut = new BinderSet<IBinder>();
            sut.Add(binder);
            // Act
            Action act = () => sut.Add(binder);
            // Assert
            act.Should().Throw<FastenUpException>();
        }

        [Test]
        public void RemoveBinder_When_binder_In_set_Should_removes_binder()
        {
            // Arrange
            var binder = Substitute.For<IBinder>();
            var sut = new BinderSet<IBinder>();
            sut.Add(binder);
            // Act
            sut.Remove(binder);
            // Assert
            using HashSet<IBinder>.Enumerator enumerator = sut.GetEnumerator();
            enumerator.MoveNext().Should().BeFalse();
        }

        [Test]
        public void RemoveBinder_When_binder_not_In_set_Should_throw_exception()
        {
            // Arrange
            var binder = Substitute.For<IBinder>();
            var sut = new BinderSet<IBinder>();
            // Act
            Action act = () => sut.Remove(binder);
            // Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Clear_When_called_Should_clears_set()
        {
            // Arrange
            var binder = Substitute.For<IBinder>();
            var sut = new BinderSet<IBinder>();
            sut.Add(binder);
            // Act
            sut.Clear();
            // Assert
            using HashSet<IBinder>.Enumerator enumerator = sut.GetEnumerator();
            enumerator.MoveNext().Should().BeFalse();
        }
    }
}