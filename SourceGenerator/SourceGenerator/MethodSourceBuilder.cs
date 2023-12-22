using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace FastenUp.SourceGenerator
{
    public class MethodSourceBuilder : ISourceBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();
        public string Name { get; set; }
        public string ReturnType { get; set; } = Templates.Void;
        public Accessibility Accessibility { get; set; } = Accessibility.Public;

        public ISourceBuilder Body { get; set; }
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
            _builder.Append(Accessibility.ToString().ToLower()).Append(Templates.Space);
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
            var lines = Body?.Build().Split('\n') ?? Array.Empty<string>();
            foreach (var line in lines)
            {
                if (line.Length <= 0)
                    continue;

                _builder.Append(Templates.Tab).Append(Templates.Tab).Append(Templates.Tab).AppendLine(line.TrimEnd());
            }

            _builder.Append(Templates.Tab).Append(Templates.Tab).Append(Templates.CloseBracket);
        }
    }
}