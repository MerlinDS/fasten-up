using System;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Binders.Collections;
using FastenUp.Runtime.Exceptions;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableCollection<>))]
    public class BindableCollectionTest
    {
        [Test]
        public void Bind_When_binder_bound_Should_throw_exception()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<int>>();
            var sut = new BindableCollection<int>();
            sut.As<IBindableCollection<int>>().Bind(binder);
            // Act
            Action act = () => sut.As<IBindableCollection<int>>().Bind(binder);
            // Assert
            act.Should().Throw<FastenUpException>();
            binder.DidNotReceive().Add(Arg.Any<int>());
        }

        [Test]
        public void Bind_When_binder_not_bound_Should_not_throw_exception()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<int>>();
            var sut = new BindableCollection<int>();
            // Act
            Action act = () => sut.As<IBindableCollection<int>>().Bind(binder);
            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Bind_When_bindable_has_items_Should_add_items_to_binder()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<int>>();
            var sut = new BindableCollection<int>();
            sut.Add(1);
            sut.Add(2);
            // Act
            sut.As<IBindableCollection<int>>().Bind(binder);
            // Assert
            binder.Received(1).Add(1);
            binder.Received(1).Add(2);
        }
        
        [Test]
        public void Unbind_When_binder_not_bound_Should_throw_exception()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<int>>();
            var sut = new BindableCollection<int>();
            // Act
            Action act = () => sut.As<IBindableCollection<int>>().Unbind(binder);
            // Assert
            act.Should().Throw<FastenUpException>();
        }
        
        [Test]
        public void Unbind_When_binder_bound_Should_not_throw_exception()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<int>>();
            var sut = new BindableCollection<int>();
            sut.As<IBindableCollection<int>>().Bind(binder);
            // Act
            Action act = () => sut.As<IBindableCollection<int>>().Unbind(binder);
            // Assert
            act.Should().NotThrow();
        }
        
        [Test]
        public void Unbind_When_bindable_has_items_Should_remove_items_from_binder()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<int>>();
            var sut = new BindableCollection<int>();
            sut.Add(1);
            sut.Add(2);
            sut.As<IBindableCollection<int>>().Bind(binder);
            // Act
            sut.As<IBindableCollection<int>>().Unbind(binder);
            // Assert
            binder.Received(1).Remove(1);
            binder.Received(1).Remove(2);
        }
        
        [Test]
        public void Add_When_item_is_null_Should_throw_exception()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            // Act
            Action act = () => sut.Add(null);
            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Add_When_item_is_not_null_Should_add_item()
        {
            // Arrange
            var expected = new object();
            var sut = new BindableCollection<object>();
            // Act
            sut.Add(expected);
            // Assert
            var enumerator = sut.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator.MoveNext().Should().BeTrue();
            enumerator.Current.Should().Be(expected);
        }
        
        [Test]
        public void Add_When_has_binder_Should_notify_binder()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<object>>();
            var sut = new BindableCollection<object>();
            sut.As<IBindableCollection<object>>().Bind(binder);
            var expected = new object();
            // Act
            sut.Add(expected);
            // Assert
            binder.Received(1).Add(expected);
        }
        
        [Test]
        public void Remove_When_item_is_null_Should_return_false()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            // Act
            var actual = sut.Remove(null);
            // Assert
            actual.Should().BeFalse();
        }
        
        [Test]
        public void Remove_When_item_is_not_in_collection_Should_return_false()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            var expected = new object();
            // Act
            var actual = sut.Remove(expected);
            // Assert
            actual.Should().BeFalse();
        }
        
        [Test]
        public void Remove_When_item_is_in_collection_Should_return_true()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            var expected = new object();
            sut.Add(expected);
            // Act
            var actual = sut.Remove(expected);
            // Assert
            actual.Should().BeTrue();
        }   
        
        [Test]
        public void Remove_When_item_is_in_collection_Should_remove_item()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            var expected = new object();
            sut.Add(expected);
            // Act
            sut.Remove(expected);
            // Assert
            var enumerator = sut.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator.MoveNext().Should().BeFalse();
        }
        
        [Test]
        public void Remove_When_has_binder_Should_notify_binder()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<object>>();
            var sut = new BindableCollection<object>();
            sut.As<IBindableCollection<object>>().Bind(binder);
            var expected = new object();
            sut.Add(expected);
            // Act
            sut.Remove(expected);
            // Assert
            binder.Received(1).Remove(expected);
        }
        
        [Test]
        public void Contains_When_item_is_null_Should_return_false()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            // Act
            var actual = sut.Contains(null);
            // Assert
            actual.Should().BeFalse();
        }
        
        [Test]
        public void Contains_When_item_is_not_in_collection_Should_return_false()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            var expected = new object();
            // Act
            var actual = sut.Contains(expected);
            // Assert
            actual.Should().BeFalse();
        }
        
        [Test]
        public void Contains_When_item_is_in_collection_Should_return_true()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            var expected = new object();
            sut.Add(expected);
            // Act
            var actual = sut.Contains(expected);
            // Assert
            actual.Should().BeTrue();
        }
        
        [Test]
        public void Count_When_collection_is_empty_Should_return_zero()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            // Act
            var actual = sut.Count;
            // Assert
            actual.Should().Be(0);
        }
        
        [Test]
        public void Count_When_collection_has_items_Should_return_count()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            sut.Add(new object());
            sut.Add(new object());
            // Act
            var actual = sut.Count;
            // Assert
            actual.Should().Be(2);
        }
        
        [Test]
        public void Clear_When_collection_is_empty_Should_not_notify_binder()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<object>>();
            var sut = new BindableCollection<object>();
            sut.As<IBindableCollection<object>>().Bind(binder);
            // Act
            sut.Clear();
            // Assert
            binder.DidNotReceive().Remove(Arg.Any<object>());
        }
        
        [Test]
        public void Clear_When_collection_has_items_Should_clear_collection()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            sut.Add(new object());
            sut.Add(new object());
            // Act
            sut.Clear();
            // Assert
            var enumerator = sut.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator.MoveNext().Should().BeFalse();
        }
        
        [Test]
        public void Clear_When_collection_has_items_Should_notify_binder()
        {
            // Arrange
            var binder = Substitute.For<ICollectionBinder<object>>();
            var sut = new BindableCollection<object>();
            sut.As<IBindableCollection<object>>().Bind(binder);
            sut.Add(new object());
            // Act
            sut.Clear();
            // Assert
            binder.Received(1).Remove(Arg.Any<object>());
        }
    }
}