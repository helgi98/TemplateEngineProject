using System.IO;
using TemplateEngineProject.macros;

namespace TemplateEngineProject.parsers
{
    class ElseElementParser: IParser
    {
        public IMacro Parse(StreamReader template)
        {
            return new ElseMacro();
        }
    }
}
