using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.macros;

namespace TemplateEngineProject.parsers
{
    class IfElementParser : IParser
    {
        public virtual IMacro Parse(StreamReader template)
        {
            string condition = ReadCondition(template);
            List<string> postfixCondition = utilities.PostfixSystem.ToPostfixSystem(condition);


            return new IfMacro(postfixCondition);
        }

        public string ReadCondition(StreamReader template)
        {
            StringBuilder condition = new StringBuilder();

            int symbol;

            while ((symbol = template.Read()) != '(')
                if (!Char.IsWhiteSpace((char)symbol)) throw new ParserException("[IfElementParser]Invalid syntax");

            int counter = 1;

            while (counter != 0 && symbol != -1)
            {
                symbol = template.Read();
                switch (symbol)
                {
                    case '(':
                        counter++;
                        break;
                    case ')':
                        counter--;
                        break;
                    default:
                        condition.Append((char) symbol);
                        break;
                }
            }

            return condition.ToString();
        }
    }
}