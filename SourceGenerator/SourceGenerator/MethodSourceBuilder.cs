using System.Collections.Generic;
using System.Text;

namespace FastenUp.SourceGenerator
{
    public class MethodSourceBuilder : ISourceBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public AccessModifier AccessModifier { get; set; }

        public string Body { get; set; }
        public List<ISourceBuilder> Parameters { get; } = new List<ISourceBuilder>();

        public string Build()
        {
            try
            {
                AppendHeader();
                AppendBody();
                return _builder.ToString();
            }
            finally
            {
                _builder.Clear();
            }
        }

        private void AppendHeader()
        {
            AppendAccessModifier();
            AppendReturnType();
            AppendParameters();
        }

        private void AppendAccessModifier()
        {
            _builder.Append(Templates.Tab).Append(Templates.Tab);
            _builder.Append(AccessModifier.ToString().ToLower()).Append(Templates.Space);
        }

        private void AppendReturnType()
        {
            _builder.Append(string.IsNullOrEmpty(ReturnType) ? Templates.Void : ReturnType).Append(Templates.Space);
        }
        
        private void AppendParameters()
        {
            _builder.Append(Name).Append(Templates.OpenParenthesis);
            
            foreach (var builder in Parameters)
                _builder.Append(builder.Build()).Append(Templates.Comma).Append(Templates.Space);
            if (Parameters.Count > 0)
                _builder.Remove(_builder.Length - 2, 2);
            
            _builder.Append(Templates.CloseParenthesis).AppendLine();
        }
        
        private void AppendBody()
        {
            _builder.Append(Templates.Tab).Append(Templates.Tab).AppendLine(Templates.OpenBracket);
            _builder.Append(Templates.Tab).Append(Templates.Tab).Append(Templates.Tab).AppendLine(Body);
            _builder.Append(Templates.Tab).Append(Templates.Tab).AppendLine(Templates.CloseBracket);
        }
    }
}