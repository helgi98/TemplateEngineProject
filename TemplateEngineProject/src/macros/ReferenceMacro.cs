using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class ReferenceMacro : IMacro
    {
        private string _objectReference;

        public ReferenceMacro(string objectReference)
        {
            _objectReference = objectReference;
        }

        public string Execute(ContextTable context)
        {
            return utilities.ReflectionUtils.GetObjectFromContext(context, _objectReference).ToString();
        }
    }
}
