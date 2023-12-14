using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FastenUp.SourceGenerator
{
    [Generator]
    public class InternalMediatorSourceGenerator : ISourceGenerator
    {
        /// <inheritdoc />
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc />
        public void Execute(GeneratorExecutionContext context)
        {
            var iMediatorSymbol = context.Compilation.GetTypeByMetadataName("FastenUp.Runtime.Base.IMediator");
            var proxyTypeSymbol = context.Compilation.GetTypeByMetadataName("FastenUp.Runtime.Proxies.DataProxy`1");

            foreach (var mediatorSymbol in GetMediatorSymbols(context.Compilation, iMediatorSymbol))
            {
                GenerateInternalMediatorClass(context, mediatorSymbol, proxyTypeSymbol);
            }
        }

        private static IEnumerable<INamedTypeSymbol> GetMediatorSymbols(Compilation compilation,
            INamedTypeSymbol iMediatorSymbol)
        {
            return compilation.SyntaxTrees
                .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>())
                .Select(classDeclaration => compilation.GetSemanticModel(classDeclaration.SyntaxTree)
                    .GetDeclaredSymbol(classDeclaration))
                .OfType<INamedTypeSymbol>()
                .Where(classSymbol => classSymbol.Interfaces.Contains(iMediatorSymbol));
        }

        private static void GenerateInternalMediatorClass(GeneratorExecutionContext context,
            INamespaceOrTypeSymbol sourceSymbol,
            ISymbol proxyTypeSymbol)
        {
            var sourceName = sourceSymbol.Name;
            var sourceNamespace = sourceSymbol.ContainingNamespace.ToDisplayString();
            var outputFillName = $"{sourceNamespace}.Internal{sourceName}";
            var outputBuilder = new ClassSourceBuilder
            {
                Namespace = sourceNamespace,
                ClassName = sourceName,
                IsPartial = true,
                Accessibility = sourceSymbol.DeclaredAccessibility
            };

            outputBuilder.Imports.Add("FastenUp.Runtime.Base");
            outputBuilder.Imports.Add("FastenUp.Runtime.Bindings");
            outputBuilder.Imports.Add("FastenUp.Runtime.Proxies");
            outputBuilder.Imports.Add("FastenUp.Runtime.Extensions");
            outputBuilder.Inheritance.Add("IInternalMediator");

            var proxyNames = GetFieldSymbols(sourceSymbol, proxyTypeSymbol).Select(x => x.Name);
            outputBuilder.Methods.Add(new MethodSourceBuilder
            {
                Name = "UpdateProxies",
                Accessibility = Accessibility.Public,
                Body = new UpdateProxiesBodyBuilder
                {
                    InvocationName = "UpdateProxy",
                    ParameterName = "bindingPoint",
                    ProxyNames = proxyNames.ToArray()
                },
                Parameters =
                {
                    new ParameterBuilder
                    {
                        Name = "bindingPoint",
                        Type = "IBindingPoint"
                    }
                }
            });

            context.AddSource($"{outputFillName}.cs", outputBuilder.Build());
        }
        
        private static IEnumerable<IFieldSymbol> GetFieldSymbols(INamespaceOrTypeSymbol mediatorSymbol, ISymbol target)
        {
            return mediatorSymbol.GetMembers().OfType<IFieldSymbol>()
                .Where(symbol => symbol.Type.OriginalDefinition.Name == target.Name);
        }

        private class UpdateProxiesBodyBuilder : ISourceBuilder
        {
            public string[] ProxyNames { get; set; }

            public string InvocationName { get; set; }

            public string ParameterName { get; set; }

            /// <inheritdoc />
            public string Build()
            {
                var sourceBuilder = new StringBuilder();
                foreach (var proxyName in ProxyNames)
                    AppendLine(sourceBuilder, proxyName);

                return sourceBuilder.ToString();
            }

            private void AppendLine(StringBuilder sourceBuilder, string proxyName)
            {
                sourceBuilder.Append(proxyName).Append(Templates.Dot).Append(InvocationName)
                    .Append(Templates.OpenParenthesis);
                sourceBuilder.Append(Templates.Quote).Append(proxyName).Append(Templates.Quote);
                sourceBuilder.Append(Templates.Comma).Append(Templates.Space).Append(ParameterName);
                sourceBuilder.Append(Templates.CloseParenthesis).AppendLine(Templates.Semicolon);
            }
        }
    }
}