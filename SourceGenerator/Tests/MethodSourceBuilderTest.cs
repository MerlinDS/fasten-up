using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.SourceGenerator.Tests
{
    [TestFixture]
    [TestOf(typeof(MethodSourceBuilder))]
    public class MethodSourceBuilderTest
    {
        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                var sut = new MethodSourceBuilder
                {
                    Name = "MethodName",
                    Body = "//Method body"
                };
                var expected = @"        public void MethodName()
        {
            //Method body
        }
";
                yield return new TestCaseData(sut, expected).SetName("Empty method");
                sut = new MethodSourceBuilder
                {
                    Name = "MethodName",
                    Body = "//Method body",
                    ReturnType = "int"
                };
                expected = @"        public int MethodName()
        {
            //Method body
        }
";
                yield return new TestCaseData(sut, expected).SetName("Method with return type");
                sut = new MethodSourceBuilder
                {
                    Name = "MethodName",
                    Body = "//Method body",
                    AccessModifier = AccessModifier.Internal
                };
                expected = @"        internal void MethodName()
        {
            //Method body
        }
";
                yield return new TestCaseData(sut, expected).SetName("Method with access modifier");
                var mockParameterA = Substitute.For<ISourceBuilder>();
                mockParameterA.Build().Returns("int a");
                sut = new MethodSourceBuilder
                {
                    Name = "MethodName",
                    Body = "//Method body",
                };
                sut.Parameters.Add(mockParameterA);
                expected = @"        public void MethodName(int a)
        {
            //Method body
        }
";
                yield return new TestCaseData(sut, expected).SetName("Method with parameter");
                var mockParameterB = Substitute.For<ISourceBuilder>();
                mockParameterB.Build().Returns("bool b");
                sut = new MethodSourceBuilder
                {
                    Name = "MethodName",
                    Body = "//Method body",
                };
                sut.Parameters.Add(mockParameterA);
                sut.Parameters.Add(mockParameterB);
                expected = @"        public void MethodName(int a, bool b)
        {
            //Method body
        }
";
                yield return new TestCaseData(sut, expected).SetName("Method with multiple parameters");
            }
        }

        [TestCaseSource(nameof(TestCases))]
        public void Build(MethodSourceBuilder sut, string expected)
        {
            // Act & Assert
            sut.Build().Should().Be(expected);
        }
    }
}