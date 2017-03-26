using System;
using System.IO;
using System.Text;
using TemplateEngineProject.macros;

namespace TemplateEngineProject.parsers
{
    class ReferenceMacroParser : IParser
    {
        public IMacro Parse(StreamReader template)
        {
            string reference = ReadReference(template);
            return new ReferenceMacro(reference);
        }


        private string ReadReference(StreamReader template)
        {
            StringBuilder sb = new StringBuilder();

            while (!IsStopSymbol(template.Peek()))
                sb.Append((char)template.Read());

            return sb.ToString();
        }


        protected bool IsStopSymbol(int symbol)
        {
            if (Char.IsLetter((char)symbol) || Char.IsDigit((char)symbol) ||
                symbol == '.' || symbol == -1) return false;

            return true;
        }
    }
}
