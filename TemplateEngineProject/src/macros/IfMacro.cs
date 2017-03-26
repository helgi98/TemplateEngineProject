using System;
using System.Collections.Generic;
using System.Linq;
using TemplateEngineProject.tables;
using TemplateEngineProject.utilities;

namespace TemplateEngineProject.macros
{
    class IfMacro : ConditionMacro
    {
        private readonly List<String> _condition;

        public IfMacro(List<string> condition)
        {
            _condition = condition;
        }

        public override bool EvaluateCondition(ContextTable context)
        {
            return PostfixSystem.EvaluatePostfix(_condition, el =>
                utilities.ReflectionUtils.GetObjectFromContext(context, el));
        }
    }
}