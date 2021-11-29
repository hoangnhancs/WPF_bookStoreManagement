using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace bookStoreManagetment.ViewModel
{
    class CreateBillViewModel:BaseViewModel
    {
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

        public class SellBillItem : BaseViewModel
        {
            private item _Item;
            public item Item { get => _Item; set { _Item = value; OnPropertyChanged(); } }

            private int _Amount;
            public int Amount
            {
                get => _Amount; set
                {
                    _Amount = value; OnPropertyChanged();
                    if (Amount == null || Amount <= 0 )
                    {
                        Amount = 1;
                    }
                    else if (Amount > Item.quantity)
                    {
                        Amount = Item.quantity;
                    }
                } 
            }

            private int _Discount;
            public int Discount { get => _Discount; set { _Discount = value; OnPropertyChanged(); } }

        }

        private ObservableCollection<SellBillItem> _SellBillInfomation;
        public ObservableCollection<SellBillItem> SellBillInfomation { get => _SellBillInfomation; set { _SellBillInfomation = value; OnPropertyChanged(); if (SellBillInfomation != null) { UpdateTotal(); } } }

        private ObservableCollection<employee> _EmployeeList;
        public ObservableCollection<employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private employee _SelectedEmployee;
        public employee SelectedEmployee { get => _SelectedEmployee; set { _SelectedEmployee = value; OnPropertyChanged(); } }

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

        public CreateBillViewModel()
        {
            CustommerList = new ObservableCollection<custommer>(DataProvider.Ins.DB.custommers);
            EmployeeList = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
            ItemsListSearch = new ObservableCollection<item>(DataProvider.Ins.DB.items);
            SellBillInfomation = new ObservableCollection<SellBillItem>();
            SellBillCode = "bill" + (DataProvider.Ins.DB.bills.Count() + 1).ToString();
            //(DataProvider.Ins.DB.bills.Where(x => x.billType == "sell").Count() + 1).ToString();
            DateTime CurrentDate = DateTime.Now;
            OrderDate = CurrentDate;
            DeliveryDate = CurrentDate;
            LicenseDate = CurrentDate;
            
            string[] PaymentMethods = { "Thanh toán bằng thẻ", "Thanh toán bằng tiền mặt" };
            string[] DeliveryMethods = { "Mua trực tiếp tại cửa hàng", "Giao hàng" };
            PaymentMethodList = new List<string>(PaymentMethods);
            DeliveryMethodList = new List<string>(DeliveryMethods);

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

            CheckoutClickCommand = new RelayCommand<object>((p) =>
            {
                if (SellBillInfomation.Count == 0 || SelectedCustommer == null || SelectedEmployee == null || SelectedDeliveryMethod == null || SelectedPaymentMethod == null)
                    return false;
                return true;
            }, (p) =>
            {
                if (System.Windows.MessageBox.Show("Xác nhận thanh toán?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                            idEmployee = SelectedEmployee.idEmployee,
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

            RemoveItemFromSellBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SellBillInfomation.Remove(SelectedSellBillItem);
                UpdateTotal();
            });

            UpdateTotalCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UpdateTotal();
            });

        }
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
