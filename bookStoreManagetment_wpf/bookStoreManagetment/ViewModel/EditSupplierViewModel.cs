using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.ViewModel
{
    
    public class EditSupplierViewModel:BaseViewModel
    {
        public Inventory inventory { get; set; }
        public EditSupplierViewModel(object obj)
        {
            inventory = (obj as Inventory);

        }
        public EditSupplierViewModel()
        {

        }
    }
}
