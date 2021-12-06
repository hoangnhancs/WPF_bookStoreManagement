using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public static class ListInterger
    {
        public static List<int>  CreateListInterger(int maxNumber)
        {
            List<int> ListIntergers = Enumerable.Range(1, maxNumber).ToList();
            return ListIntergers;
        }
    }
}
