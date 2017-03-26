using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.macros;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.parsers
{
    class MacroCallParser : IParser
    {
        string _macroName;
        MacrosTable _macrosTable;

        public MacroCallParser(string macroName, MacrosTable macrosTable)
        {
            _macroName = macroName;
            _macrosTable = macrosTable;
        }


        public IMacro Parse(StreamReader template)
        {
            try
            {
                string[] args = ReadArgs(template);

                UserMacroInfo macro = _macrosTable.GetMacro(_macroName, args.Length);

                return new UserMacroCall(macro, args);
            }
            catch (ParserException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ParserException($"[MacroCallParser]No macro \"{_macroName}\" with such parameters");
            }
        }

        private string[] ReadArgs(StreamReader template)
        {
            int symbol;

            StringBuilder sb = new StringBuilder();

            while ((symbol = template.Read()) != '(')
                if (!Char.IsWhiteSpace((char) symbol)) throw new ParserException("[IfElementParser]Invalid syntax");


            bool insideString = false;
            while (template.Peek() != -1)
            {
                if (template.Peek() == '\"' && symbol != '\\')
                    insideString = !insideString;
                if (!insideString && template.Peek() == ')')
                {
                    template.Read();
                    break;
                }
                symbol = template.Read();
                sb.Append((char) symbol);
            }

            if (sb.ToString() != "")
            {
                string[] args = Split(sb.ToString());

                for (int i = 0; i < args.Length; ++i)
                    args[i] = args[i].Trim('\"', ' ');

                return args;
            }


            return new string[0];
        }

        private string[] Split(String template)
        {
            List<String> args = new List<string>();

            bool insideString = false;

            StringBuilder sb = new StringBuilder();
            template = " " + template + " ";
            for (int i = 1; i < template.Length; ++i)
            {
                if (template[i] == '\"' && template[i - 1] != '\\')
                {
                    insideString = !insideString;
                    sb.Append("\"");
                }
                else if (!insideString && Char.IsWhiteSpace(template[i]) && sb.ToString() != "")
                {
                    args.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {

                    sb.Append(template[i]);
                }
            }

            if (insideString) throw new ParserException("[MacroCallParser]Not appropriate arguments");
            return args.ToArray();
        }
    }
}