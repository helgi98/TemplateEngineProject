using System.Collections;
using System.Text;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class ForeachMacro: IMacro
    {
        private readonly string _collectionName;
        private readonly string _variableName;
        private readonly IMacro _container;

        public ForeachMacro(IMacro container, string collectionName, string variableName)
        {
            _container = container;
            _collectionName = collectionName;
            _variableName = variableName;
        }

        public string Execute(ContextTable context)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable collection = utilities.ReflectionUtils.GetObjectFromContext(context, _collectionName) as IEnumerable;

            if (collection == null)
                throw new MacroExecutionException("[#ForeachMacro]No specified collection found");

            ContextTable newContext = (ContextTable)context.Clone();
            foreach (object elem in collection)
            {
                newContext.AddProperty(_variableName, elem);
                sb.Append(_container.Execute(newContext));
            }

            return sb.ToString();
        }
    }
}
