using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class ElseMacro: ConditionMacro
    {
        public override bool EvaluateCondition(ContextTable context) => true;
    }
}
