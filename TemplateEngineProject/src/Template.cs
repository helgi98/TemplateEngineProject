using System;
using System.IO;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.macros;
using TemplateEngineProject.parsers;
using TemplateEngineProject.tables;

namespace TemplateEngineProject
{
    class Template
    {
        private StreamReader _template;
        private IMacro _macro;

        public Template(StreamReader template)
        {
            _template = template;
        }

        public void Parse()
        {
            try
            {
                _macro = new TemplateParser().Parse(_template);
            }
            catch (ParserException pe)
            {
                Console.WriteLine($"Exception during parsing: {pe.Message}\n{pe.StackTrace}");
            }
        }

        public string Execute(ContextTable context)
        {
            try
            {
                string result = _macro.Execute(context);

                return result;
            }
            catch (MacroExecutionException me)
            {
                Console.WriteLine($"Exception during macro execution: {me.Message}\n{me.StackTrace}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}\n{ex.StackTrace}");
            }

            return "";
        }
    }
}
