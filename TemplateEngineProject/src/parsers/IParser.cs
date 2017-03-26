using System.IO;
using TemplateEngineProject.macros;

namespace TemplateEngineProject.parsers
{
    interface IParser
    {
        IMacro Parse(StreamReader template);
    }
}
