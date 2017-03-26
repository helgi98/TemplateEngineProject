﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngineProject.exceptions
{ 
    
    class ParserException: Exception
    {
        public ParserException()
        {
        }

        public ParserException(string message) :
            base(message)
        {

        }

        public ParserException(string message, Exception innerException) :
            base(message, innerException)
        {

        }
    }
}
