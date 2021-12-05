using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    #region Khai báo class chi tiết hóa đơn
    public class BillDetail
    {
        public bill Bill { get; set; }
        public string BillCode { get; set; }
        public string EmployeeFullName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public int Total { get; set; }
        public string BillStatus { get; set; }
        public List<SellBillItem> OrderItems { get; set; }
        public sellBill SellBill { get; set; }
    }
    #endregion

    #region Khai báo class SellBillItem
    public class SellBillItem
    {
        public item Item { get; set; }
        public int Amount { get; set; }
        public int Discount { get; set; }
    }
    #endregion
}
