using System.Collections.Generic;
using System.Text;

namespace FastenUp.SourceGenerator
{
    public class ClassSourceBuilder : ISourceBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public string Namespace { get; set; }
        public string ClassName { get; set; }

        public bool IsPartial { get; set; }

        public bool IsStatic { get; set; }

        public bool IsAbstract { get; set; }
        
        public AccessModifier AccessModifier { get; set; }

        public List<string> Imports { get; } = new List<string>();

        public List<string> Inheritance { get; } = new List<string>();

        public List<ISourceBuilder> Methods { get; } = new List<ISourceBuilder>();

        public string Build()
        {
            try
            {
                _builder.Append(Templates.Disclaimer);
                AppendImports();
                AppendNamespace();
                AppendClassHeader();
                AppendClassBody();
                AppendClosing();
                return _builder.ToString();
            }
            finally
            {
                _builder.Clear();
            }
        }

        private void AppendImports()
        {
            if (Imports.Count > 0)
                _builder.AppendLine();

            foreach (var import in Imports)
                _builder.Append(Templates.Using).Append(Templates.Space).Append(import).AppendLine(Templates.Semicolon);
            _builder.AppendLine();
        }

        private void AppendNamespace()
        {
            _builder.Append(Templates.Namespace).Append(Templates.Space).AppendLine(Namespace);
        }

        private void AppendClassHeader()
        {
            _builder.AppendLine(Templates.OpenBracket).Append(Templates.Tab);
            AppendAccessModifier();
            AppendModifiers();
            _builder.Append(Templates.Class).Append(Templates.Space).Append(ClassName);
            AppendInheritance();
            _builder.AppendLine();
        }
        
        private void AppendAccessModifier()
        {
            _builder.Append(AccessModifier.ToString().ToLower()).Append(Templates.Space);
        }

        private void AppendModifiers()
        {
            if (IsStatic)
                _builder.Append(Templates.Static).Append(Templates.Space);
            else if (IsAbstract)
                _builder.Append(Templates.Abstract).Append(Templates.Space);

            if (IsPartial)
                _builder.Append(Templates.Partial).Append(Templates.Space);
        }

        private void AppendInheritance()
        {
            if (Inheritance.Count <= 0)
                return;

            _builder.Append(Templates.Space).Append(Templates.Colon).Append(Templates.Space);
            _builder.Append(string.Join(", ", Inheritance));
        }

        private void AppendClassBody()
        {
            _builder.Append(Templates.Tab).AppendLine(Templates.OpenBracket);
            AppendMethods();
            _builder.Append(Templates.Tab).AppendLine(Templates.CloseBracket);
        }

        private void AppendMethods()
        {
            if (Methods.Count <= 0)
            {
                _builder.Append(Templates.Tab).Append(Templates.Tab);
                _builder.AppendLine();
                return;
            }

            foreach (var method in Methods)
                _builder.AppendLine(method.Build());
        }

        private void AppendClosing()
        {
            _builder.AppendLine(Templates.CloseBracket);
        }
    }
}