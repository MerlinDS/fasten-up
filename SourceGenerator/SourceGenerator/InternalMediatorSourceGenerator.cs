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
                GenerateInternalMediatorClass(mediatorSymbol, context);
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

        private static void GenerateInternalMediatorClass(INamedTypeSymbol mediatorSymbol,
            GeneratorExecutionContext context)
        {
            var mediatorName = mediatorSymbol.Name;
            var mediatorNamespace = mediatorSymbol.ContainingNamespace.ToDisplayString();
            var internalMediatorName = $"Internal{mediatorName}";
            var internalMediatorFullName = $"{mediatorNamespace}.{internalMediatorName}";
            var dataProxySymbol = context.Compilation.GetTypeByMetadataName("FastenUp.Runtime.Proxies.DataProxy`1");

            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine("using FastenUp.Runtime.Base;");
            sourceBuilder.AppendLine("using FastenUp.Runtime.Bindings;");
            sourceBuilder.AppendLine("using FastenUp.Runtime.Proxies;");
            sourceBuilder.AppendLine("using FastenUp.Runtime.Extensions;");
            sourceBuilder.AppendLine();
            sourceBuilder.AppendLine($"namespace {mediatorNamespace}");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine($"    public partial class {mediatorName} : IInternalMediator");
            sourceBuilder.AppendLine("    {");
            sourceBuilder.AppendLine("        /// <inheritdoc />");
            sourceBuilder.AppendLine("        public void UpdateProxies(IBindingPoint bindingPoint)");
            sourceBuilder.AppendLine("        {");

            foreach (var fieldSymbol in GetFieldSymbols(mediatorSymbol, dataProxySymbol))
            {
                sourceBuilder.AppendLine($"                {fieldSymbol.Name}.UpdateProxy(\"{fieldSymbol.Name}\", bindingPoint);");
            }


            sourceBuilder.AppendLine("        }");
            sourceBuilder.AppendLine("    }");
            sourceBuilder.AppendLine("}");

            context.AddSource($"{internalMediatorFullName}.cs", sourceBuilder.ToString());
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