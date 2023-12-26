using System;
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
            var enumerator = sut.GetEnumerator();
            using var disposable = enumerator as IDisposable;
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
            var enumerator = sut.GetEnumerator();
            using var disposable = enumerator as IDisposable;
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
    }
}