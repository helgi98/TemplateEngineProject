using System;
using System.IO;
using System.Text;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.macros;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.parsers
{
    class ForeachParser : IParser
    {
        private readonly MacrosTable _macrosTable;

        public ForeachParser(MacrosTable macrosTable)
        {
            _macrosTable = macrosTable;
        }

        public IMacro Parse(StreamReader template)
        {
            string[] args = ReadArgs(template);
            if (args[1] != "in"
                || !args[0].StartsWith("$")
                || !args[2].StartsWith("$"))
                throw new ParserException("[ForeachParser]Invalid foreach-syntax");

            TemplateParser parser = new TemplateParser(_macrosTable, true);
            IMacro macro = parser.Parse(template);

            return new ForeachMacro(macro, args[2].Substring(1), args[0].Substring(1));
        }

        private string[] ReadArgs(StreamReader template)
        {
            int symbol;

            while ((symbol = template.Read()) != '(')
                if (Char.IsWhiteSpace((char)symbol)) throw new ParserException("[ForeachParser]Invalid syntax");

            StringBuilder sb = new StringBuilder();

            while ((symbol = template.Read()) != ')')
            {
                sb.Append((char) symbol);
                if (symbol == -1) throw new ParserException("[ForeachParser]No ()");
            }

            return sb.ToString().Split();
        }
    }
}