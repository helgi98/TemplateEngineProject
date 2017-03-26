using System.Text.RegularExpressions;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class UserMacroInfo
    {
        private IMacro _macro;
        private string[] _argNames;

        public UserMacroInfo(IMacro macro, string[] argNames)
        {
            _macro = macro;
            _argNames = argNames;
        }

        public string Execute(string[] argValues, ContextTable context)
        {
            object[] values = GetArgValues(argValues, context);

            ContextTable newContext = (ContextTable)context.Clone();

            for (int i = 0; i < _argNames.Length; ++i)
                newContext.AddProperty(_argNames[i], values[i]);

            return _macro.Execute(newContext);
        }

        private object[] GetArgValues(string[] argValues, ContextTable context)
        {
            object[] values = new object[argValues.Length];

            for (int i = 0; i < argValues.Length; ++i)
            {
                if (argValues[i].StartsWith("$"))
                    values[i] = utilities.ReflectionUtils.GetObjectFromContext(context, argValues[i].Substring(1));
                else values[i] = argValues[i];
            }

            return values;
        }
    }
}
