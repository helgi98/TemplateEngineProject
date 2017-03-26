using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngineProject.model
{
    public class User
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String RegistrationLink { get; set; }
        public bool VerifiedEmail { get; set; }
        public Order Order { get; set; }
    }
}
