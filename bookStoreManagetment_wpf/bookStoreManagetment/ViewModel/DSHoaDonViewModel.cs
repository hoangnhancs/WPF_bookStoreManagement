using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace bookStoreManagetment.ViewModel
{
    class DSHoaDonViewModel : BaseViewModel
    {
        public class BillDetail
        {
            public bill Bill { get; set; }
            public string EmployeeFullName { get; set; }
            public string CustomerFullName { get; set; }
            public string CustommerPhoneNumber { get; set; }
            public string CustommerAddress { get; set; }
            public int Total { get; set; }
            public List<SellBillItem> OrderItems { get; set; }
            public sellBill SellBill { get; set; }
        }
        
        public class SellBillItem
        {
            public item Item;
            public int Amount;
            public int Discount;
        }

        public DSHoaDonViewModel()
        {
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) => LoadDSHoaDon());

            ResetFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) => ResetFilter());

            LoadBillDataCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewBillDetail = (p as BillDetail);
                IsBillViewing = Visibility.Visible;
            });

            // Copy của bên DS Thu Chi
            ClickShowHideGridCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridDSHoaDon")
                    {
                        IsBillViewing = Visibility.Visible;
                    }
                    else
                    {
                        IsBillViewing = Visibility.Collapsed;
                    }
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }

            });
            void ResetFilter()
            {
                SelectedEmployee = null;
                SelectedOrderStatus = null;
                SearchString = null;
            }

            void LoadDSHoaDon()
            {
                IsDSHoaDon = Visibility.Visible;
                IsBillViewing = Visibility.Collapsed;
                HoadonList = new ObservableCollection<BillDetail>();
                Employee = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
                ListItems = new ObservableCollection<item>(DataProvider.Ins.DB.items);
                string[] OrderStatus = { "Tất cả", "Đã thanh toán", "Đã giao hàng", "Đã trả hàng" };
                OrderStatusList = new List<string>(OrderStatus);

                Employee.Add(new employee { firstName = "Tất cả" });
                var BillList = DataProvider.Ins.DB.bills.Where(x => x.billType == "sell");
                foreach (bill sellBill in BillList)
                {
                    var billDetails = DataProvider.Ins.DB.sellBills.Where(x => x.billCodeSell == sellBill.billCode);
                    var total = 0;
                    string custommerFullName = "";
                    string custommerPhoneNumber = "";
                    string employeeFullName = "";
                    string custommerAddress = "";
                    if (billDetails != null)
                    {
                        var curentCustommer = DataProvider.Ins.DB.custommers.Where(x => x.idCustommer == billDetails.FirstOrDefault().idCustomer).FirstOrDefault();
                        custommerFullName = curentCustommer.lastName + " " + curentCustommer.firstName;
                        custommerAddress = curentCustommer.custommerAddress;
                        employeeFullName = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == billDetails.FirstOrDefault().idEmployee).Select(x => x.lastName + " " + x.firstName).SingleOrDefault();
                        total = (int)billDetails.Select(x => x.unitPrice * x.number * (1 - x.discount / 100)).Sum();
                        custommerPhoneNumber = curentCustommer.phoneNumber;
                    }
                    ListOrderItems = new List<SellBillItem>();
                    foreach (var billdetail in billDetails)
                    {
                        ListOrderItems.Add(new SellBillItem { Item = ListItems.Where(x => x.idItem == billdetail.idItem).SingleOrDefault(), Amount = billdetail.number, Discount = billdetail.discount });
                    }
                    BillDetail hoadon = new BillDetail
                    {
                        Bill = sellBill,
                        CustomerFullName = custommerFullName,
                        EmployeeFullName = employeeFullName,
                        Total = total,
                        CustommerPhoneNumber = custommerPhoneNumber,
                        SellBill = billDetails.FirstOrDefault(),
                        OrderItems = ListOrderItems,
                        CustommerAddress = custommerAddress,
                    };
                    HoadonList.Add(hoadon);
                }
                DisplayBillList = HoadonList.ToList();
            }
        }

        private ObservableCollection<BillDetail> _HoadonList;
        public ObservableCollection<BillDetail> HoadonList { get => _HoadonList; set { _HoadonList = value; OnPropertyChanged(); } }

        private List<BillDetail> _DisplayBillList;
        public List<BillDetail> DisplayBillList { get => _DisplayBillList; set { _DisplayBillList = value; OnPropertyChanged(); } }

        private ObservableCollection<employee> _Employee;
        public ObservableCollection<employee> Employee { get => _Employee; set { _Employee = value; OnPropertyChanged(); } }

        private List<string> _OrderStatusList;
        public List<string> OrderStatusList { get => _OrderStatusList; set { _OrderStatusList = value; OnPropertyChanged(); } }

        private string _SelectedOrderStatus;
        public string SelectedOrderStatus
        {
            get => _SelectedOrderStatus;
            set
            {
                _SelectedOrderStatus = value;
                OnPropertyChanged();
                if (SelectedOrderStatus != null)
                    if (SelectedOrderStatus == "Tất cả")
                        DisplayBillList = HoadonList.ToList();
                    else
                        DisplayBillList = HoadonList.Where(x => x != null && x.SellBill.billStatus == SelectedOrderStatus).ToList();
                else
                    DisplayBillList = HoadonList.ToList();
            }
        }

        private employee _SelectedEmployee;
        public employee SelectedEmployee
        {
            get => _SelectedEmployee;
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();
                if (SelectedEmployee != null)
                    if (SelectedEmployee.firstName == "Tất cả")
                        DisplayBillList = HoadonList.ToList();
                    else
                        DisplayBillList = HoadonList.Where(x => x != null && x.SellBill.idEmployee == SelectedEmployee.idEmployee).ToList();
                else
                    DisplayBillList = HoadonList.ToList();
            }
        }
        //public ICommand LoadDanhsachhoadonCommand { get; set; }

        public ICommand ResetFilterCommand { get; set; }

        public ICommand PrintBillCommand { get; set; }

        public ICommand LoadedUserControlCommand { get; set; }

        public ICommand LoadBillDataCommand { get; set; }

        public ICommand ClickShowHideGridCommand { get; set; }

        private string _SearchString;
        public string SearchString
        {
            get => _SearchString;
            set
            {
                _SearchString = value;
                OnPropertyChanged();
                if (SearchString != null && SearchString != "")
                {
                    DisplayBillList = HoadonList.Where(x => x.CustomerFullName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 || x.Bill.billCode.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 || x.CustommerPhoneNumber.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                else
                    DisplayBillList = HoadonList.ToList();
            }
        }

        private BillDetail _SelectedBill;
        public BillDetail SelectedBill { get => _SelectedBill; set { _SelectedBill = value; OnPropertyChanged(); } }

        private Visibility _IsDSHoaDon;
        public Visibility IsDSHoaDon { get => _IsDSHoaDon; set { _IsDSHoaDon = value; OnPropertyChanged(); } }

        private Visibility _IsBillViewing;
        public Visibility IsBillViewing { get => _IsBillViewing; set { _IsBillViewing = value; OnPropertyChanged(); } }

        private BillDetail _ViewBillDetail;
        public BillDetail ViewBillDetail { get => _ViewBillDetail; set { _ViewBillDetail = value; OnPropertyChanged(); } }

        private List<SellBillItem> _ListOrderItems;
        public List<SellBillItem> ListOrderItems { get => _ListOrderItems; set { _ListOrderItems = value; OnPropertyChanged(); } }

        private ObservableCollection<item> _ListItems;
        public ObservableCollection<item> ListItems { get => _ListItems; set { _ListItems = value; OnPropertyChanged(); } }

    }
}
