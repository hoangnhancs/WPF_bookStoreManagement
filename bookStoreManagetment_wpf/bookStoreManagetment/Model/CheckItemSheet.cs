using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public class inforItem
    {
        public string idItem { get; set; }
        public int OldQuantityItem { get; set; }
        public int NewQuantityItem { get; set; }
    }
    public class CheckItemSheet
    {
        //public string note { get; set; }
        public string codeCheckItem { get; set; }
        public string idEmployee { get; set; }
        public string nameEmployee { get; set; }
        public System.DateTime dateCheckItems { get; set; }
        public List<inforItem> InforItems { get; set; }

        public CheckItemSheet()
        {
            InforItems = new List<inforItem>();
        }
    }
}
