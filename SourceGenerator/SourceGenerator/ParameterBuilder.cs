using System.Text;

namespace FastenUp.SourceGenerator
{
    public class ParameterBuilder : ISourceBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }

        public ParameterModifier Modifier { get; set; }

        public string Build()
        {
            try
            {
                if (NotValid())
                    return string.Empty;
                AppendModifier();
                AppendParameter();
                AppendDefaultValue();

                return _builder.ToString();
            }
            finally
            {
                _builder.Clear();
            }
        }

        private bool NotValid() =>
            string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Type);

        private void AppendModifier()
        {
            if (Modifier == ParameterModifier.None)
                return;

            var modifier = Modifier.ToString().ToLower();
            _builder.Append(modifier).Append(Templates.Space);
        }

        private void AppendParameter()
        {
            _builder.Append(Type).Append(Templates.Space).Append(Name);
        }

        private void AppendDefaultValue()
        {
            if (string.IsNullOrEmpty(DefaultValue))
                return;

            _builder.Append(Templates.Space).Append(Templates.Equal).Append(Templates.Space).Append(DefaultValue);
        }
    }
}