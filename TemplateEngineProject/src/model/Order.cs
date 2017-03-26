using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateEngineProject.model
{
    public class Order
    {
        public List<Item> OrderItemsList { get; set; }
        public DateTime Date { get; set; }
        public Double Total { get; set; }
        public bool AlreadyPaid { get; set; }
    }
}
