using System;
using System.IO;
using System.Linq;
using System.Text;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.macros;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.parsers
{
    class CreateMacroParser : IParser
    {
        private readonly MacrosTable _macrosTable;
        private string _macroName;
        private string[] _macroArgs;

        public CreateMacroParser(MacrosTable macrosTable)
        {
            _macrosTable = macrosTable;
        }

        public IMacro Parse(StreamReader template)
        {
            ReadMacroInfo(template);

            TemplateParser parser = new TemplateParser(_macrosTable, true);
            IMacro macro = parser.Parse(template);
            _macrosTable.AddMacro(_macroName, _macroArgs.Length, new UserMacroInfo(macro, _macroArgs));

            return null;
        }

        private void ReadMacroInfo(StreamReader template)
        {
            string[] args = ReadArgs(template);

            if (args.Length <= 0) throw new ParserException("[CreateMacroParser]No macro name specified");

            _macroName = args[0];
            _macroArgs = args.Skip(1).ToArray().Select(arg => arg.Substring(1)).ToArray();
        }

        private string[] ReadArgs(StreamReader template)
        {
            int symbol;

            while ((symbol = template.Read()) != '(')
                if (symbol == -1) throw new ParserException("[IfElementParser]Invalid syntax");

            StringBuilder sb = new StringBuilder();

            while ((symbol = template.Read()) != ')')
            {
                sb.Append((char) symbol);
                if (symbol == -1) throw new ParserException("[IfElementParser]Invalid syntax");
            }

            return sb.ToString().Split();
        }
    }
}
