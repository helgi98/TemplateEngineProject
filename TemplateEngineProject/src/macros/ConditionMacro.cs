using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    abstract class ConditionMacro : IMacro
    {
        public abstract bool EvaluateCondition(ContextTable context);

        public virtual string Execute(ContextTable context)
        {
            return "";
        }
    }
}
