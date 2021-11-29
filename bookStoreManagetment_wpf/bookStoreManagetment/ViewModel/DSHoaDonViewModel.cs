﻿using bookStoreManagetment.Model;
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
            // Load data cho DSHoaDon UC
            #region LoadedUserControlCommand
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) => LoadDSHoaDon());
            #endregion

            // Command reset filter DSHoaDon 
            #region ResetFilterCommand
            ResetFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) => ResetFilter());
            #endregion

            // Command Load data hóa đơn để xem
            #region LoadBillDataCommand
            LoadBillDataCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewBillDetail = (p as BillDetail);
                IsBillViewing = Visibility.Visible;
                IsDSHoaDon = Visibility.Collapsed;
                IsBillCreating = Visibility.Collapsed;
            });
            #endregion
            
            // Command Load data để tạo đơn hàng
            #region LoadCreateBillCommand
            LoadCreateBillCommand = new RelayCommand<object>((p) => { return true; }, (p) => LoadCreateBill());
            #endregion

            // Command thêm vật phẩm vào đơn hàng
            #region AddItemIntoSellBillCommand
            AddItemIntoSellBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null && SelectedItem.quantity > 0)
                {
                    bool exists = false;
                    if (SellBillInfomation != null)
                    {
                        foreach (var billinfo in SellBillInfomation)
                        {
                            if (billinfo.Item == SelectedItem)
                            {
                                exists = true;
                            }
                        }
                    }
                    if (exists == false)
                    {
                        var BillDetail = new SellBillItem() { Item = SelectedItem, Amount = 1, Discount = 0 };
                        SellBillInfomation.Add(BillDetail);
                    }
                    UpdateTotal();
                }
            });
            #endregion 

            // Hàm reset filter tìm kiếm của grid DS hóa đơn
            #region ResetFilter
            void ResetFilter()
            {
                SelectedEmployee = null;
                SelectedOrderStatus = null;
                SearchString = null;
            }
            #endregion

            // Hàm Load data của danh sách hóa đơn
            #region LoadDSHoaDon
            void LoadDSHoaDon()
            {
                IsDSHoaDon = Visibility.Visible;
                IsBillViewing = Visibility.Collapsed;
                IsBillCreating = Visibility.Collapsed;
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
            #endregion

            //Hàm load data để tạo đơn mua hàng
            #region LoadCreateBill
            void LoadCreateBill()
            {
                IsBillCreating = Visibility.Visible;
                IsDSHoaDon = Visibility.Collapsed;
                IsBillViewing = Visibility.Collapsed;
                // Load danh sác khách hàng
                CustommerList = new ObservableCollection<custommer>(DataProvider.Ins.DB.custommers);
                // Load danh sách nhân viên
                EmployeeList = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
                // Load danh sách item 
                ItemsListSearch = new ObservableCollection<item>(DataProvider.Ins.DB.items);
                // Tạo mới thông tin đơn hàng
                SellBillInfomation = new ObservableCollection<SellBillItem>();
                // Tự tạo mới bill code tăng dần
                SellBillCode = "bill" + (DataProvider.Ins.DB.bills.Count() + 1).ToString();
                // Đặt mặc định ngày giờ hiện tại cho ngày đặt hàng, ngày giao hàng, ngày chứng từ
                DateTime CurrentDate = DateTime.Now;
                OrderDate = CurrentDate;
                DeliveryDate = CurrentDate;
                LicenseDate = CurrentDate;
                // Tạo danh sách phương thức thanh toán và phương thức giao hàng
                string[] PaymentMethods = { "Thanh toán bằng thẻ", "Thanh toán bằng tiền mặt" };
                string[] DeliveryMethods = { "Mua trực tiếp tại cửa hàng", "Giao hàng" };
                PaymentMethodList = new List<string>(PaymentMethods);
                DeliveryMethodList = new List<string>(DeliveryMethods);
            }
            #endregion

            // Command thanh toán cho đơn hàng
            #region CheckoutClickCommand
            CheckoutClickCommand = new RelayCommand<object>((p) =>
            {
                if (SellBillInfomation.Count == 0 || SelectedCustommer == null || SelectedEmployeeCreateBill == null || SelectedDeliveryMethod == null || SelectedPaymentMethod == null)
                    return false;
                return true;
            }, (p) =>
            {
                if (MessageBox.Show("Xác nhận thanh toán?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bill SaveBill = new bill() { billCode = SellBillCode, billType = "sell", setBillDay = (DateTime)OrderDate };
                    DataProvider.Ins.DB.bills.Add(SaveBill);
                    //DataProvider.Ins.DB.SaveChanges();
                    foreach (var bill_ in SellBillInfomation)
                    {
                        sellBill SaveSellBill = new sellBill()
                        {
                            billCodeSell = SellBillCode,
                            billStatus = "Đã thanh toán",
                            idEmployee = SelectedEmployeeCreateBill.idEmployee,
                            idCustomer = SelectedCustommer.idCustommer,
                            number = bill_.Amount,
                            sellDate = (DateTime)OrderDate,
                            licenseDate = (DateTime)LicenseDate,
                            deliveryDate = (DateTime)DeliveryDate,
                            idItem = bill_.Item.idItem,
                            unitPrice = bill_.Item.priceItem,
                            discount = bill_.Discount,
                            tag = Tag,
                            note = Note,
                        };
                        DataProvider.Ins.DB.sellBills.Add(SaveSellBill);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                }
            });
            #endregion 

            // Command xóa vật phẩm đang chọn khỏi đơn hàng
            #region RemoveItemFromSellBillCommand
            RemoveItemFromSellBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SellBillInfomation.Remove(SelectedSellBillItem);
                UpdateTotal();
            });
            #endregion

            // Command tính tổng tiền của đơn hàng
            #region UpdateTotalCommand
            UpdateTotalCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UpdateTotal();
            });
            #endregion

            // Command back to DSHoaDon
            BacktoDSHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsBillViewing = Visibility.Collapsed;
                IsBillCreating = Visibility.Collapsed;
                IsDSHoaDon = Visibility.Visible;
            });

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
        public ICommand ResetFilterCommand { get; set; }

        public ICommand PrintBillCommand { get; set; }

        public ICommand LoadedUserControlCommand { get; set; }

        public ICommand LoadBillDataCommand { get; set; }

        public ICommand BacktoDSHoaDonCommand { get; set; }

        public ICommand LoadCreateBillCommand { get; set; }

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

        private Visibility _IsBillCreating;
        public Visibility IsBillCreating { get => _IsBillCreating; set { _IsBillCreating = value; OnPropertyChanged(); } }

        private BillDetail _ViewBillDetail;
        public BillDetail ViewBillDetail { get => _ViewBillDetail; set { _ViewBillDetail = value; OnPropertyChanged(); } }

        private List<SellBillItem> _ListOrderItems;
        public List<SellBillItem> ListOrderItems { get => _ListOrderItems; set { _ListOrderItems = value; OnPropertyChanged(); } }

        private ObservableCollection<item> _ListItems;
        public ObservableCollection<item> ListItems { get => _ListItems; set { _ListItems = value; OnPropertyChanged(); } }


        //Biến của phần tạo hóa đơn
        public ICommand CheckoutClickCommand { get; set; }

        public ICommand AddItemIntoSellBillCommand { get; set; }

        public ICommand InputAmountCommand { get; set; }

        public ICommand RemoveItemFromSellBillCommand { get; set; }

        public ICommand UpdateTotalCommand { get; set; }

        private ObservableCollection<custommer> _CustommerList;
        public ObservableCollection<custommer> CustommerList { get => _CustommerList; set { _CustommerList = value; OnPropertyChanged(); } }

        private custommer _SelectedCustommer;
        public custommer SelectedCustommer
        {
            get => _SelectedCustommer;
            set
            {
                _SelectedCustommer = value;
                OnPropertyChanged();
                if (SelectedCustommer != null)
                {
                    Address = SelectedCustommer.custommerAddress;
                    PhoneNumber = SelectedCustommer.phoneNumber;
                }
            }
        }

        private ObservableCollection<item> _ItemsListSearch;
        public ObservableCollection<item> ItemsListSearch { get => _ItemsListSearch; set { _ItemsListSearch = value; OnPropertyChanged(); } }

        private item _SelectedItem;
        public item SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private ObservableCollection<SellBillItem> _SellBillInfomation;
        public ObservableCollection<SellBillItem> SellBillInfomation { get => _SellBillInfomation; set { _SellBillInfomation = value; OnPropertyChanged(); if (SellBillInfomation != null) { UpdateTotal(); } } }

        private ObservableCollection<employee> _EmployeeList;
        public ObservableCollection<employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private employee _SelectedEmployeeCreateBill;
        public employee SelectedEmployeeCreateBill { get => _SelectedEmployeeCreateBill; set { _SelectedEmployeeCreateBill = value; OnPropertyChanged(); } }

        private SellBillItem _SelectedSellBillItem;
        public SellBillItem SelectedSellBillItem { get => _SelectedSellBillItem; set { _SelectedSellBillItem = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private float _Total;
        public float Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { _PhoneNumber = value; OnPropertyChanged(); } }

        private List<string> _PaymentMethodList;
        public List<string> PaymentMethodList { get => _PaymentMethodList; set { _PaymentMethodList = value; OnPropertyChanged(); } }

        private string _SelectedPaymentMethod;
        public string SelectedPaymentMethod { get => _SelectedPaymentMethod; set { _SelectedPaymentMethod = value; OnPropertyChanged(); } }

        private List<string> _DeliveryMethodList;
        public List<string> DeliveryMethodList { get => _DeliveryMethodList; set { _DeliveryMethodList = value; OnPropertyChanged(); } }

        private string _SelectedDeliveryMethod;
        public string SelectedDeliveryMethod { get => _SelectedDeliveryMethod; set { _SelectedDeliveryMethod = value; OnPropertyChanged(); } }

        private string _SellBillCode;
        public string SellBillCode { get => _SellBillCode; set { _SellBillCode = value; OnPropertyChanged(); } }

        private DateTime? _OrderDate;
        public DateTime? OrderDate { get => _OrderDate; set { _OrderDate = value; OnPropertyChanged(); } }

        private DateTime? _DeliveryDate;
        public DateTime? DeliveryDate { get => _DeliveryDate; set { _DeliveryDate = value; OnPropertyChanged(); } }

        private DateTime? _LicenseDate;
        public DateTime? LicenseDate { get => _LicenseDate; set { _LicenseDate = value; OnPropertyChanged(); } }

        private string _Tag;
        public string Tag { get => _Tag; set { _Tag = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        //Hàm tính tổng tiền đơn hàng
        public void UpdateTotal()
        {
            Total = 0;
            foreach (var billinfo in SellBillInfomation)
            {
                Total += billinfo.Item.priceItem * billinfo.Amount * (1 - billinfo.Discount / 100);
            }
        }
    }
}
