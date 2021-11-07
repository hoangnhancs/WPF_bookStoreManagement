using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public class Inventory
    {
        public item Item { get; set; }
        public int Count { get; set; }
        public string Note { get; set; }
    }
}
