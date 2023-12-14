﻿using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace FastenUp.SourceGenerator.Tests
{
    [TestFixture]
    [TestOf(typeof(InternalMediatorSourceGenerator))]
    public class InternalMediatorSourceGeneratorTest
    {
        private const string Source = @"
namespace FastenUp.Runtime.Extensions{}
namespace FastenUp.Runtime.Proxies
{
    public struct DataProxy<T>
    {
    }
}

namespace FastenUp.Runtime.Bindings
{
    public interface IBindingPoint<T> : IBindingPoint
    {
    }

    public interface IBindingPoint
    {
    }
}

namespace FastenUp.Runtime.Base
{
    public interface IMediator{}

    public interface IInternalMediator
    {
        void UpdateProxies(FastenUp.Runtime.Bindings.IBindingPoint bindingPoint);
    }
}
";

        private const string Imports = @"using FastenUp.Runtime.Base;
using FastenUp.Runtime.Bindings;
using FastenUp.Runtime.Proxies;
using FastenUp.Runtime.Extensions;
";

        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData(@"
namespace Test
{
    public partial class TestMediator : FastenUp.Runtime.Base.IMediator
    {
    }
}",
                        @"
namespace Test
{
    public partial class TestMediator : IInternalMediator
    {
        /// <inheritdoc />
        public void UpdateProxies(IBindingPoint bindingPoint)
        {
        }
    }
}
")
                    .SetName("One mediator without proxies");
                yield return new TestCaseData(@"
namespace Test
{
    public partial class TestMediator : FastenUp.Runtime.Base.IMediator
    {
        public DataProxy<bool> Visibility;
    }
}",
                        @"
namespace Test
{
    public partial class TestMediator : IInternalMediator
    {
        /// <inheritdoc />
        public void UpdateProxies(IBindingPoint bindingPoint)
        {
                Visibility.UpdateProxy(""Visibility"", bindingPoint);
        }
    }
}
")
                    .SetName("One mediator with proxy");
            }
        }

        [TestCaseSource(nameof(TestCases))]
        public void Generate(string source, string expected)
        {
            //Arrange
            expected = Imports + expected;
            var expectedCount = string.IsNullOrEmpty(expected) ? 0 : 1;
            var inputCompilation = CreateCompilation(Source + source);
            var sut = CSharpGeneratorDriver.Create(new InternalMediatorSourceGenerator());
            //Act
            var generators = sut.RunGenerators(inputCompilation);
            var actual = generators.GetRunResult();
            //Assert
            actual.GeneratedTrees.Should().HaveCount(expectedCount, "because we have one mediator declaration");
            if (expectedCount > 0)
                actual.GeneratedTrees[0].GetRoot().ToFullString().Should().Be(expected);
        }

        private static Compilation CreateCompilation(string source)
            => CSharpCompilation.Create("compilation",
                new[] { CSharpSyntaxTree.ParseText(source) },
                new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));
    }
}