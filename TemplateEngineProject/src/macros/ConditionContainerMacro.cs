using System.Collections.Generic;
using System.Text;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class ConditionContainerMacro : IMacro
    {
        private readonly Queue<IMacro> _macros = new Queue<IMacro>();

        public void AddElement(IMacro condition) => _macros.Enqueue(condition);

        public string Execute(ContextTable context)
        {
            bool isChosen = false;

            StringBuilder result = new StringBuilder();
            foreach (IMacro macro in _macros)
            {
                if (macro is ConditionMacro && isChosen)
                    break;
                else if (macro is ConditionMacro)
                    isChosen = ((ConditionMacro) macro).EvaluateCondition(context);
                else if (isChosen)
                    result.Append(macro.Execute(context));
            }

            return result.ToString();
        }
    }
}