using System.Collections.Generic;
using System.Text;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.macros
{
    class ContainerMacro: IMacro
    {
        private readonly List<IMacro> _elements = new List<IMacro>();
        public List<IMacro> Elements => _elements;
        
        public void AddMacro(IMacro macro) => _elements.Add(macro);

        public virtual string Execute(ContextTable context)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IMacro macro in _elements)
                sb.Append(macro.Execute(context));

            return sb.ToString();
        }
    }
}
