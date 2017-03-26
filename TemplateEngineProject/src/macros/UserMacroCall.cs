using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class UserMacroCall : IMacro
    {
        private UserMacroInfo _macro;
        private string[] _argValues;

        public UserMacroCall(UserMacroInfo macro, string[] argValues)
        {
            _macro = macro;
            _argValues = argValues;
        }

        public string Execute(ContextTable context)
        {
            return _macro.Execute(_argValues, context);
        }
    }
}
