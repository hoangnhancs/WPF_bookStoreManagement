using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public static class getNextCode
    {
        public static string getCode(int count)
        {
            var nextCode = count + 1;
            if (nextCode < 10)
            {
                return "00" + nextCode.ToString();
            }
            else if(nextCode < 100)
            {
                return "0" + nextCode.ToString();
            }
            else
            {
                return nextCode.ToString();
            }
        }
    }
}
