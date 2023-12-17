using System;
using System.Collections.Generic;
using FastenUp.Runtime.Bindables;
using FluentAssertions;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Bindables
{
    [TestFixture]
    [TestOf(typeof(BindableText))]
    public class BindableTextTest
    {
        private static IEnumerable<TestCaseData> SeValueTestCases
        {
            get
            {
                yield return Create(sut => sut.SetValue("Test"), "Test").SetName("Set value to 'Test'");
                yield return Create(sut => sut.SetValue(null), null).SetName("Set value to null");
                yield return Create(sut => sut.SetValue(string.Empty), string.Empty).SetName("Set value to empty");
                yield return Create(sut => sut.SetValue(" "), " ").SetName("Set value to space");
                yield return Create(sut => sut.SetValue(1), "1").SetName("Set value to 1");
                yield return Create(sut => sut.SetValue(1.1f), "1.1").SetName("Set value to 1.1");
                yield break;

                TestCaseData Create(Action<BindableText> action, string expected)
                {
                    return new TestCaseData(action, expected);
                }
            }
        }

        [TestCaseSource(nameof(SeValueTestCases))]
        public void SetValue(Action<BindableText> setAction, string expected)
        {
            //Arrange
            var sut = CreateSut();
            //Act
            setAction(sut);
            //Assert
            sut.GetComponent<TextMeshProUGUI>().text.Should().Be(expected);
        }

        private static BindableText CreateSut()
        {
            var gameObject = new GameObject(nameof(BindableTextTest));
            var sut = gameObject.AddComponent<BindableText>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}