using System;
using System.Collections.Generic;
using FastenUp.Runtime.Binders;
using FluentAssertions;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityTestingAssist.Runtime;

namespace FastenUp.Tests.Binders
{
    [TestFixture]
    [TestOf(typeof(TextBinder))]
    public class TextBinderTest
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

                TestCaseData Create(Action<TextBinder> action, string expected)
                {
                    return new TestCaseData(action, expected);
                }
            }
        }

        [TestCaseSource(nameof(SeValueTestCases))]
        public void SetValue(Action<TextBinder> setAction, string expected)
        {
            //Arrange
            var sut = CreateSut();
            //Act
            setAction(sut);
            //Assert
            sut.GetComponent<TextMeshProUGUI>().text.Should().Be(expected);
        }

        private static TextBinder CreateSut()
        {
            var gameObject = new GameObject(nameof(TextBinderTest));
            var sut = gameObject.AddComponent<TextBinder>();
            sut.ExecuteAwake();
            return sut;
        }
    }
}