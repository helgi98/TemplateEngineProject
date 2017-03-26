using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngineProject.exceptions
{
    class MacroExecutionException: Exception
    {
        public MacroExecutionException()
        {
        }

        public MacroExecutionException(string message) :
            base(message)
        {
            
        }

        public MacroExecutionException(string message, Exception innerException) :
            base(message, innerException)
        {
            
        }
    }
}
