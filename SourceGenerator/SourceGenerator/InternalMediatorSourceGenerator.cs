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
            foreach (var mediatorSymbol in GetMediatorSymbols(context.Compilation))
            {
                GenerateInternalMediatorClass(context, mediatorSymbol);
            }
        }

        private static IEnumerable<INamedTypeSymbol> GetMediatorSymbols(Compilation compilation)
        {
            var iMediatorSymbol = compilation.GetTypeByMetadataName("FastenUp.Runtime.Base.IMediator");
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                //Get the semantic model for the syntax tree
                var semanticModel = compilation.GetSemanticModel(syntaxTree);

                // Get all class declarations in the syntax tree
                var classDeclarations = syntaxTree.GetRoot().DescendantNodes()
                    .OfType<ClassDeclarationSyntax>();

                // Iterate over all class declarations
                foreach (var classDeclaration in classDeclarations)
                {
                    // Get the symbol for the class
                    var symbol = semanticModel.GetDeclaredSymbol(classDeclaration);
                    if (symbol is INamedTypeSymbol classSymbol && classSymbol.Interfaces.Contains(iMediatorSymbol))
                    {
                        yield return classSymbol;
                    }
                }
            }
        }

        private static void GenerateInternalMediatorClass(GeneratorExecutionContext context,
            INamespaceOrTypeSymbol sourceSymbol)
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

            var target = context.Compilation.GetTypeByMetadataName("FastenUp.Runtime.Proxies.DataProxy`1");
            var proxyNames = GetFieldSymbols(sourceSymbol, target).Select(x => x.Name);
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
                {
                    sourceBuilder.Append(proxyName).Append(Templates.Dot)
                        .Append(InvocationName)
                        .Append(Templates.OpenParenthesis)
                        .Append(Templates.Quote).Append(proxyName).Append(Templates.Quote)
                        .Append(Templates.Comma).Append(Templates.Space).Append(ParameterName)
                        .Append(Templates.CloseParenthesis).AppendLine(Templates.Semicolon);
                }

                return sourceBuilder.ToString();
            }
        }

        private static IEnumerable<IFieldSymbol> GetFieldSymbols(INamespaceOrTypeSymbol mediatorSymbol,
            ISymbol target = null)
        {
            foreach (var symbol in mediatorSymbol.GetMembers())
            {
                if (!(symbol is IFieldSymbol fieldSymbol))
                    continue;

                if (target != null && fieldSymbol.Type.OriginalDefinition.Name != target.Name)
                    continue;

                yield return fieldSymbol;
            }
        }
    }
}