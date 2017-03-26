using System;
using TemplateEngineProject.macros;
using System.Collections.Generic;

namespace TemplateEngineProject.tables
{
    class MacrosTable
    {
        private readonly Dictionary<KeyValuePair<string, int>, UserMacroInfo> _macros =
            new Dictionary<KeyValuePair<string, int>, UserMacroInfo>();

        public UserMacroInfo GetMacro(string macroName, int argsLength)
        {
            _macros.TryGetValue(new KeyValuePair<string, int>(macroName, argsLength), out UserMacroInfo macro);
            if (macro == null)
                throw new Exception("No such macro");
            return macro;
        }

        public void AddMacro(String macroName, int argsLength, UserMacroInfo macro)
            => _macros.Add(new KeyValuePair<string, int>(macroName, argsLength), macro);
    }
}