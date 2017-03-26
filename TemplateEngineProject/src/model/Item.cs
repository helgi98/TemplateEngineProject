using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngineProject.model
{
    public class Item
    {
        public String Name { get; set; }
        public String Category { get; set; }
        public String ImagePath { get; set; }
        public UInt32 Count { get; set; }
        public Double Price { get; set; }
    }
}
