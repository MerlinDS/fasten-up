using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FastenUp.SourceGenerator.Tests
{
    [TestFixture]
    [TestOf(typeof(ClassSourceBuilder))]
    public class ClassSourceBuilderTests
    {
        private static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                var sourceBuilder = Substitute.For<ISourceBuilder>();
                var sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName"
                };
                var expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
namespace Namespace
{
    public class ClassName
    {
        
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Simple class");
                sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName",
                    IsPartial = true
                };
                expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
namespace Namespace
{
    public partial class ClassName
    {
        
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Partial class");
                sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName",
                    IsAbstract = true,
                    IsPartial = true
                };
                expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
namespace Namespace
{
    public abstract partial class ClassName
    {
        
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Abstract partial class");
                sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName",
                    IsStatic = true,
                    IsPartial = true,
                    IsAbstract = true,
                };
                expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
namespace Namespace
{
    public static partial class ClassName
    {
        
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Static partial class");
                sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName",
                };
                sut.Inheritance.Add("BaseClass");
                sut.Inheritance.Add("IInterface");
                expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
namespace Namespace
{
    public class ClassName : BaseClass, IInterface
    {
        
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Class with inheritance");
                sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName",
                };
                sut.Imports.Add("System");
                expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
using System;

namespace Namespace
{
    public class ClassName
    {
        
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Class with imports");
                sut = new ClassSourceBuilder
                {
                    Namespace = "Namespace",
                    ClassName = "ClassName",
                };
                sourceBuilder.Build().Returns(@"        public void Method()
        {
            // Method body
        }");
                sut.Methods.Add(sourceBuilder);
                expected = @"// --- GENERATED BY FASTEN UP SOURCE GENERATOR ---
namespace Namespace
{
    public class ClassName
    {
        public void Method()
        {
            // Method body
        }
    }
}
";
                yield return new TestCaseData(sut, expected).SetName("Class with methods");
            }
            
        }

        [TestCaseSource(nameof(TestCases))]
        public void Build(ClassSourceBuilder sut, string expected)
        {
            // Act
            var actual = sut.Build();
            // Assert
            actual.Should().Be(expected);
        }
    }
    // --- END GENERATED CODE ---
}