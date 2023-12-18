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
        private const string MediatorMetadataName = "FastenUp.Runtime.Base.IMediator";
        private const string InternalMediatorMetadataName = "FastenUp.Runtime.Base.IInternalMediator";
        private const string BindPointMetadataName = "FastenUp.Runtime.Base.IInternalBindPoint`1";

        /// <inheritdoc />
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc />
        public void Execute(GeneratorExecutionContext context)
        {
            if (!Validate(context))
                return;

            var iMediatorSymbol = context.Compilation.GetTypeByMetadataName(MediatorMetadataName);
            var proxyTypeSymbol = context.Compilation.GetTypeByMetadataName(BindPointMetadataName);

            foreach (var mediatorSymbol in GetMediatorSymbols(context.Compilation, iMediatorSymbol))
            {
                GenerateInternalMediatorClass(context, mediatorSymbol, proxyTypeSymbol);
            }
        }

        private static bool Validate(GeneratorExecutionContext context)
        {
            var symbol = context.Compilation.GetTypeByMetadataName(MediatorMetadataName);
            if (symbol is null)
                return false;

            symbol = context.Compilation.GetTypeByMetadataName(InternalMediatorMetadataName);
            if (symbol is null)
                return false;

            symbol = context.Compilation.GetTypeByMetadataName(BindPointMetadataName);
            return !(symbol is null);
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
                Accessibility = sourceSymbol.DeclaredAccessibility,
            };

            outputBuilder.Inheritance.Add(InternalMediatorMetadataName);

            var names = GetFieldSymbols(sourceSymbol, proxyTypeSymbol)
                .Select(x => x.Name).ToArray();
            outputBuilder.Methods.Add(new MethodSourceBuilder
            {
                Name = "Bind",
                Accessibility = Accessibility.Public,
                Body = new BindBodyBuilder
                {
                    InvocationName = "FastenUp.Runtime.Utils.BindUtilities.TryBind",
                    ParameterName = "bindable",
                    BindPointNames = names
                },
                Parameters =
                {
                    new ParameterBuilder
                    {
                        Name = "bindable",
                        Type = "FastenUp.Runtime.Bindables.IBindable"
                    }
                }
            });
            outputBuilder.Methods.Add(new MethodSourceBuilder
            {
                Name = "Unbind",
                Accessibility = Accessibility.Public,
                Body = new BindBodyBuilder
                {
                    InvocationName = "FastenUp.Runtime.Utils.BindUtilities.TryUnbind",
                    ParameterName = "bindable",
                    BindPointNames = names
                },
                Parameters =
                {
                    new ParameterBuilder
                    {
                        Name = "bindable",
                        Type = "FastenUp.Runtime.Bindables.IBindable"
                    }
                }
            });

            context.AddSource($"{outputFillName}.cs", outputBuilder.Build());
        }

        private static IEnumerable<IPropertySymbol> GetFieldSymbols(INamespaceOrTypeSymbol mediatorSymbol, ISymbol target)
        {
            var fieldSymbols = mediatorSymbol.GetMembers().OfType<IPropertySymbol>();
            foreach (var symbol in fieldSymbols)
            {
                if (symbol.Type.Interfaces.Any(x => x.OriginalDefinition.Equals(target,
                        SymbolEqualityComparer.Default)))
                    yield return symbol;
            }
        }

        private class BindBodyBuilder : ISourceBuilder
        {
            public string[] BindPointNames { get; set; }

            public string InvocationName { get; set; }

            public string ParameterName { get; set; }

            /// <inheritdoc />
            public string Build()
            {
                var sourceBuilder = new StringBuilder();
                foreach (var bindPointName in BindPointNames)
                    AppendLine(sourceBuilder, bindPointName);

                return sourceBuilder.ToString();
            }

            private void AppendLine(StringBuilder sourceBuilder, string bindPointName)
            {
                sourceBuilder.Append(InvocationName).Append(Templates.OpenParenthesis);
                sourceBuilder.Append(bindPointName).Append(Templates.Comma).Append(Templates.Space);
                sourceBuilder.Append("nameof").Append(Templates.OpenParenthesis).Append(bindPointName)
                    .Append(Templates.CloseParenthesis).Append(Templates.Comma).Append(Templates.Space);
                sourceBuilder.Append(ParameterName).Append(Templates.CloseParenthesis).AppendLine(Templates.Semicolon);
            }
        }
    }
}