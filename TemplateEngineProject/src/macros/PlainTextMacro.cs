using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class PlainTextMacro : IMacro
    {
        private string _text;

        public PlainTextMacro(string text)
        {
            _text = text;
        }

        public string Execute(ContextTable context)
        {
            return _text;
        }
    }
}
