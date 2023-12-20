﻿using FastenUp.Runtime.Bindables.Pointers;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace FastenUp.Tests.Bindables.Pointers
{
    [TestFixture]
    [TestOf(typeof(BindablePointerClick))]
    public class BindablePointerClickTest
    {
        [Test]
        public void OnPointerClick_When_has_listener_Should_invoke_on_click()
        {
            // Arrange
            var actual = Substitute.For<UnityAction<PointerEventData>>();
            var sut = CreateSut();
            sut.AddListener(actual);
            // Act
            sut.OnPointerClick(new PointerEventData(EventSystem.current));
            // Assert
            actual.Received(1).Invoke(Arg.Any<PointerEventData>());
        }

        private static BindablePointerClick CreateSut()
        {
            var go = new GameObject();
            return go.AddComponent<BindablePointerClick>();
        }
    }
}