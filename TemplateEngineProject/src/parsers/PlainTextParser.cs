using System.IO;
using System.Text;
using TemplateEngineProject.macros;

namespace TemplateEngineProject.parsers
{
    class PlainTextParser : IParser
    {
        public IMacro Parse(StreamReader template)
        {
            string text = ReadUntilStopSymbol(template);

            return new PlainTextMacro(text);
        }

        private string ReadUntilStopSymbol(StreamReader template)
        {
            StringBuilder sb = new StringBuilder();

            while (!IsStopSymbol(template.Peek()))
            {
                int symbol = template.Read();
                sb.Append((char) symbol);

                if (symbol == '\\' && IsStopSymbol(template.Peek()))
                    sb.Append((char) template.Read());
            }

            return sb.ToString()
                .Replace("\\.", ".")
                .Replace("\\#", "#")
                .Replace("\\$", "$");
        }

        private bool IsStopSymbol(int symbol)
        {
            switch (symbol)
            {
                case '#':
                    return true;
                case '$':
                    return true;
                case -1:
                    return true;
                default:
                    return false;
            }
        }
    }
}