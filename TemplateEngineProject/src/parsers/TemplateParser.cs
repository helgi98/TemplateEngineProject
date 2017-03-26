using System;
using System.IO;
using System.Text;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.macros;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.parsers
{
    class TemplateParser : IParser
    {
        protected MacrosTable MacroTable;
        protected bool Endable;

        public TemplateParser(MacrosTable macrosTable, bool endable)
        {
            MacroTable = macrosTable;
            Endable = endable;
        }

        public TemplateParser(): this(new MacrosTable(), false)
        {
        }

        public virtual IMacro Parse(StreamReader template)
        {
            ContainerMacro container = new ContainerMacro();

            while (template.Peek() != -1)
            {
                IParser parser = GetParser(template);
                if (parser == null && Endable) break;

                IMacro macro = parser.Parse(template);
                if (macro != null) container.AddMacro(macro);
            }
            

            return container;
        }

        protected virtual IParser GetMacroCommandParser(string macroCommand)
        {
            switch (macroCommand)
            {
                case "macro":
                    return new CreateMacroParser(MacroTable);
                case "foreach":
                    return new ForeachParser(MacroTable);
                case "if":
                    return new ConditionContainerParser(MacroTable, true);
                case "end":
                    if (Endable) return null;
                    else throw new ParserException("[TemplateMacro]Not endable macro");
                default:
                    return new MacroCallParser(macroCommand, MacroTable);
            }
        }

        protected virtual IParser GetParser(StreamReader template)
        {
            int symbol = template.Peek();

            switch (symbol)
            {
                case '#':
                {
                    template.Read();

                    string macroCommand = ReadMacroCommand(template);
                    return GetMacroCommandParser(macroCommand);
                }
                case '$':
                {
                    template.Read();

                    return new ReferenceMacroParser();
                }
                default:
                    return new PlainTextParser();
            }
        }

        protected string ReadMacroCommand(StreamReader template)
        {
            StringBuilder sb = new StringBuilder();
            int symbol;

            while (!IsStopSymbol((symbol = template.Peek())))
            {
                sb.Append((char) symbol);
                template.Read();
            }

            return sb.ToString();
        }

        protected bool IsStopSymbol(int symbol)
        {
            switch (symbol)
            {
                case '(':
                    return true;
                case '$':
                    return true;
                case '#':
                    return true;
                case -1:
                    return true;
                default:
                    return Char.IsWhiteSpace((char) symbol);
            }
        }
    }
}