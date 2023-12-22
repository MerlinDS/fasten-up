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
        private const string MediatorMetadataName = "FastenUp.Runtime.Mediators.IMediator";
        private const string InternalMediatorMetadataName = "FastenUp.Runtime.Mediators.IInternalMediator";
        private const string BindPointMetadataName = "FastenUp.Runtime.Bindables.IInternalBindable";

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

            var names = GetBindSymbols(sourceSymbol, proxyTypeSymbol)
                .Select(x => x.Name).ToArray();
            var parameterBuilder = new ParameterBuilder
            {
                Name = "binder",
                Type = "FastenUp.Runtime.Binders.IBinder"
            };
            outputBuilder.Methods.Add(new MethodSourceBuilder
            {
                Name = "Bind",
                Accessibility = Accessibility.Public,
                Body = new BindBodyBuilder
                {
                    NameCheckMethod = "FastenUp.Runtime.Utils.BindUtilities.NameEquals",
                    BindMethod = "FastenUp.Runtime.Utils.BindUtilities.TryBind",
                    ParameterName = "binder",
                    BindPointNames = names
                },
                Parameters =
                {
                    parameterBuilder
                }
            });
            outputBuilder.Methods.Add(new MethodSourceBuilder
            {
                Name = "Unbind",
                Accessibility = Accessibility.Public,
                Body = new BindBodyBuilder
                {
                    NameCheckMethod = "FastenUp.Runtime.Utils.BindUtilities.NameEquals",
                    BindMethod = "FastenUp.Runtime.Utils.BindUtilities.TryUnbind",
                    ParameterName = "binder",
                    BindPointNames = names
                },
                Parameters =
                {
                    parameterBuilder
                }
            });

            context.AddSource($"{outputFillName}.cs", outputBuilder.Build());
        }

        private static IEnumerable<IPropertySymbol> GetBindSymbols(INamespaceOrTypeSymbol mediatorSymbol,
            ISymbol target)
        {
            var fieldSymbols = mediatorSymbol.GetMembers().OfType<IPropertySymbol>();
            foreach (var symbol in fieldSymbols)
            {
                if (IsInheritedFrom(symbol.Type, target))
                    yield return symbol;
            }
        }

        private static bool IsInheritedFrom(ITypeSymbol typeSymbol, ISymbol target)
        {
            var symbols = new Queue<ITypeSymbol>();
            symbols.Enqueue(typeSymbol);

            while (symbols.Count > 0)
            {
                var symbol = symbols.Dequeue();
                if (symbol.OriginalDefinition.Equals(target, SymbolEqualityComparer.Default))
                    return true;

                if (symbol.BaseType != null)
                    symbols.Enqueue(symbol.BaseType);

                foreach (var s in symbol.Interfaces)
                    symbols.Enqueue(s);
            }

            return false;
        }

        private class BindBodyBuilder : ISourceBuilder
        {
            public string[] BindPointNames { get; set; }

            public string NameCheckMethod { get; set; }
            public string BindMethod { get; set; }

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
                //Append if(NameEquals(nameof(bindPointName), binder))
                sourceBuilder.Append("if").Append(Templates.Space)
                    .Append(Templates.OpenParenthesis).Append(NameCheckMethod)
                    .Append(Templates.OpenParenthesis).Append("nameof").Append(Templates.OpenParenthesis)
                    .Append(bindPointName).Append(Templates.CloseParenthesis).Append(Templates.Comma)
                    .Append(Templates.Space).Append(ParameterName).Append(Templates.CloseParenthesis)
                    .AppendLine(Templates.CloseParenthesis);
                
                //Append TryBind(bindPointName, nameof(bindPointName), binder);
                sourceBuilder.Append(Templates.Tab).Append(BindMethod).Append(Templates.OpenParenthesis);
                sourceBuilder.Append(bindPointName).Append(Templates.Comma).Append(Templates.Space);
                sourceBuilder.Append(ParameterName).Append(Templates.CloseParenthesis).AppendLine(Templates.Semicolon);
            }
        }
    }
}