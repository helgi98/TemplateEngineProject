using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    interface IMacro
    {
        string Execute(ContextTable context);
    }
}
