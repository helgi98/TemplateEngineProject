using System.IO;
using TemplateEngineProject.macros;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.parsers
{
    class ConditionContainerParser: TemplateParser
    {
        public ConditionContainerParser(MacrosTable macrosTable, bool endable) : base(macrosTable, endable)
        {
        }

        public override IMacro Parse(StreamReader template)
        {
            ConditionContainerMacro container = new ConditionContainerMacro();

            IParser parser = new IfElementParser();
            container.AddElement(parser.Parse(template));

            while (template.Peek() != -1)
            {
                parser = GetParser(template);
                if (parser == null && Endable) break;

                IMacro macro = parser?.Parse(template);
                if (macro != null) container.AddElement(macro);
            }

            return container;
        }

        protected override IParser GetMacroCommandParser(string macroCommand)
        {
            switch (macroCommand)
            {
                case "else":
                    return new ElseElementParser();
                case "elseif":
                    return new IfElementParser();
                default:
                    return base.GetMacroCommandParser(macroCommand);
            }
        }
    }
}
