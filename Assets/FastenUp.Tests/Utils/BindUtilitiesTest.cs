using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindables;
using FastenUp.Runtime.Utils;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.Tests.Utils
{
    [TestFixture]
    [TestOf(typeof(BindUtilities))]
    public class BindUtilitiesTest
    {
        [Test]
        public void TryBind_When_point_and_bindable_are_suited_Should_Add_bindable_to_point()
        {
            //Arrange
            var bindable = Substitute.For<IBindable, IBindable<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindPoint<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.Received(1).Add(bindable.As<IBindable<bool>>());
        }

        [Test]
        public void TryBind_When_point_and_bindable_have_different_names_Should_not_Add_bindable_to_point()
        {
            //Arrange
            var bindable = Substitute.For<IBindable, IBindable<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindPoint<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test2", bindable);
            //Assert
            point.DidNotReceive().Add(bindable.As<IBindable<bool>>());
        }

        [Test]
        public void TryBind_When_point_and_bindable_have_different_types_Should_not_Add_bindable_to_point()
        {
            //Arrange
            var bindable = Substitute.For<IBindable, IBindable<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindPoint<int>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.DidNotReceive().Add(Arg.Any<IBindable<int>>());
        }

        [Test]
        public void TryBind_When_bindable_is_null_Should_not_Add_bindable_to_point()
        {
            //Arrange
            var point = Substitute.For<IInternalBindPoint<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", null);
            //Assert
            point.DidNotReceive().Add(Arg.Any<IBindable<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_point_and_bindable_are_suited_Should_Remove_bindable_from_point()
        {
            //Arrange
            var bindable = Substitute.For<IBindable, IBindable<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindPoint<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.Received(1).Remove(bindable.As<IBindable<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_point_and_bindable_have_different_names_Should_not_Remove_bindable_from_point()
        {
            //Arrange
            var bindable = Substitute.For<IBindable, IBindable<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindPoint<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test2", bindable);
            //Assert
            point.DidNotReceive().Remove(bindable.As<IBindable<bool>>());
        }
        
        [Test]
        public void TryUnbind_When_point_and_bindable_have_different_types_Should_not_Remove_bindable_from_point()
        {
            //Arrange
            var bindable = Substitute.For<IBindable, IBindable<bool>>();
            bindable.Name.Returns("Test");
            var point = Substitute.For<IInternalBindPoint<int>>();
            //Act
            BindUtilities.TryBind(point,"Test", bindable);
            //Assert
            point.DidNotReceive().Remove(Arg.Any<IBindable<int>>());
        }
        
        [Test]
        public void TryUnbind_When_bindable_is_null_Should_not_Remove_bindable_from_point()
        {
            //Arrange
            var point = Substitute.For<IInternalBindPoint<bool>>();
            //Act
            BindUtilities.TryBind(point,"Test", null);
            //Assert
            point.DidNotReceive().Remove(Arg.Any<IBindable<bool>>());
        }
    }
}