using System;
using System.Collections;
using System.Collections.Generic;
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
            var sut = new BindableCollection<int> { 1, 2 };
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
            var sut = new BindableCollection<int> { 1, 2 };
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
            // ReSharper disable once CollectionNeverQueried.Local
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
            sut.Should().Contain(expected);
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
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new BindableCollection<object>();
            // Act
            bool actual = sut.Remove(null);
            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Remove_When_item_is_not_in_collection_Should_return_false()
        {
            // Arrange
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new BindableCollection<object>();
            var expected = new object();
            // Act
            bool actual = sut.Remove(expected);
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
            bool actual = sut.Remove(expected);
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
            sut.Should().NotContain(expected);
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
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new BindableCollection<object>();
            // Act
            bool actual = sut.Contains(null);
            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void Contains_When_item_is_not_in_collection_Should_return_false()
        {
            // Arrange
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new BindableCollection<object>();
            var expected = new object();
            // Act
            bool actual = sut.Contains(expected);
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
            bool actual = sut.Contains(expected);
            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void Count_When_collection_is_empty_Should_return_zero()
        {
            // Arrange
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new BindableCollection<object>();
            // Act & Assert
            sut.Count.Should().Be(0);
        }

        [Test]
        public void Count_When_collection_has_items_Should_return_count()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            sut.Add(new object());
            sut.Add(new object());
            // Act
            int actual = sut.Count;
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
            var sut = new BindableCollection<object> { new(), new() };
            // Act
            sut.Clear();
            // Assert
            sut.Should().BeEmpty();
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

        [Test]
        public void Clear_When_collection_has_items_Should_Invoke_OnItemRemoved()
        {
            // Arrange
            var sut = new BindableCollection<object> { new() };
            var onItemRemoved = Substitute.For<Action<object>>();
            sut.OnItemRemoved += onItemRemoved;
            // Act
            sut.Clear();
            // Assert
            onItemRemoved.Received(1).Invoke(Arg.Any<object>());
        }

        [Test]
        public void GetEnumerator_When_collection_cast_to_IEnumerable_Should_return_enumerator()
        {
            // Arrange
            IEnumerable sut = new BindableCollection<object> { new(), new() };
            // Act
            IEnumerator actual = sut.GetEnumerator();
            using var unknown = actual as IDisposable;
            // Assert 
            actual.Should().NotBeNull();
            actual.MoveNext().Should().BeTrue();
        }

        [Test]
        public void CopyTo_When_collection_not_empty_Should_copy_items_to_array()
        {
            // Arrange
            var sut = new BindableCollection<object> { new(), new() };
            var array = new object[2];
            // Act
            sut.CopyTo(array, 0);
            // Assert
            array.Should().Contain(sut);
        }

        [Test]
        public void IsReadOnly_When_collection_is_empty_Should_return_false()
        {
            // Arrange
            var sut = new BindableCollection<object>();
            // Act
            bool actual = sut.As<ICollection<object>>().IsReadOnly;
            // Assert
            actual.Should().BeFalse();
        }
    }
}