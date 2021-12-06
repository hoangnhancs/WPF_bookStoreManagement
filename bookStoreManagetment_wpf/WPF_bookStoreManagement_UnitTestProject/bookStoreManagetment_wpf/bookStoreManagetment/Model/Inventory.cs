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
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public List<int> ListIntergers { get; set; }

        public Inventory()
        {
            int maxQuantity = DataProvider.Ins.DB.items.Max(i => i.quantity);
            ListIntergers = Enumerable.Range(1, maxQuantity).ToList();
        }
        public supplier Supplier { get; set; }
        public mail Mail { get; set; }
        public custommer Custommer { get; set; }
        public employee Employee { get; set; }
        public sentmail Sentmail { get; set; }
        public khachtrahang Khachtrahang { get; set; }
    }
}
