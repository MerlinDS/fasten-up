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
                AccessModifier = AccessModifier.Public // TODO: Get access modifier from sourceSymbol,
            };
            outputBuilder.Imports.Add("FastenUp.Runtime.Base");
            outputBuilder.Imports.Add("FastenUp.Runtime.Bindings");
            outputBuilder.Imports.Add("FastenUp.Runtime.Proxies");
            outputBuilder.Imports.Add("FastenUp.Runtime.Extensions");
            outputBuilder.Inheritance.Add("IInternalMediator");
            
            outputBuilder.Methods.Add(new MethodSourceBuilder
            {
                Name = "UpdateProxies",
                AccessModifier = AccessModifier.Public,
                // BodyOld = BuildUpdateProxiesBody(context, sourceSymbol),
            });
            
            context.AddSource($"{outputFillName}.cs", outputBuilder.Build());
        }

        private static string BuildUpdateProxiesBody(GeneratorExecutionContext context, 
            INamespaceOrTypeSymbol sourceSymbol)
        {
            var sourceBuilder = new StringBuilder();
            var dataProxySymbol = context.Compilation.GetTypeByMetadataName("FastenUp.Runtime.Proxies.DataProxy`1");
            foreach (var fieldSymbol in GetFieldSymbols(sourceSymbol, dataProxySymbol))
            {
                sourceBuilder.Append(fieldSymbol.Name).Append(Templates.Dot)
                    .Append("UpdateProxy")
                    .Append(Templates.OpenParenthesis)
                    .Append(Templates.Quote).Append(fieldSymbol.Name).Append(Templates.Quote)
                    .Append(Templates.Comma).Append(Templates.Space).Append("bindingPoint")
                    .Append(Templates.CloseParenthesis).AppendLine(Templates.Semicolon);
            }

            return sourceBuilder.ToString();
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