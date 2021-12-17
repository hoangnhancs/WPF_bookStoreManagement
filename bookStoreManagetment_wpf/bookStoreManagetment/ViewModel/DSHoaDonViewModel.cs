using AForge.Video;
using AForge.Video.DirectShow;
using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using ZXing;
using ZXing.QrCode;
using ZXing.Client.Result;
using ZXing.Common;
using System.Drawing.Imaging;
using System.Windows.Threading;
using System.Threading;
using GridViewExport.Behaviors;

namespace bookStoreManagetment.ViewModel
{
    public class DSHoaDonViewModel : BaseViewModel
    {
        public DSHoaDonViewModel()
        {
            #region Command load data cho DSHoaDon UC
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) => LoadDSHoaDon());
            void LoadDSHoaDon()
            {
                IsDSHoaDon = Visibility.Visible;
                IsBillViewing = Visibility.Collapsed;
                IsBillCreating = Visibility.Collapsed;
                IsOrderComfirmation = Visibility.Collapsed;
                HoadonList = new ObservableCollection<BillDetail>();
                Employee = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
                string[] OrderStatus = { "Đã thanh toán", "Đã giao hàng", "Đã trả hàng" };
                OrderStatusList = new List<string>(OrderStatus);

                var BillList = DataProvider.Ins.DB.bills.Where(x => x.billType == "export");
                foreach (bill sellBill in BillList)
                {
                    var billDetails = DataProvider.Ins.DB.sellBills.Where(x => x.billCodeSell == sellBill.billCode);
                    var total = 0;
                    string customerFullName = "";
                    string customerPhoneNumber = "";
                    string employeeFullName = "";
                    string customerAddress = "";
                    if (billDetails != null)
                    {
                        var curentCustomer = DataProvider.Ins.DB.custommers.Where(x => x.idCustommer == billDetails.FirstOrDefault().idCustomer).FirstOrDefault();
                        customerFullName = curentCustomer.lastName + " " + curentCustomer.firstName;
                        customerAddress = curentCustomer.custommerAddress;
                        customerPhoneNumber = curentCustomer.phoneNumber;
                        employeeFullName = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == billDetails.FirstOrDefault().idEmployee).Select(x => x.lastName + " " + x.firstName).FirstOrDefault();
                        total = (int)billDetails.Select(x => x.unitPrice * x.number * (1 - x.discount / 100)).Sum();
                    }
                    ListOrderItems = new List<SellBillItem>();
                    foreach (var billdetail in billDetails)
                    {
                        ListOrderItems.Add(new SellBillItem { Item = DataProvider.Ins.DB.items.Where(x => x.idItem == billdetail.idItem).FirstOrDefault(), Amount = billdetail.number, Discount = billdetail.discount });
                    }
                    BillDetail hoadon = new BillDetail
                    {
                        Bill = sellBill,
                        BillCode = sellBill.billCode,
                        CustomerFullName = customerFullName,
                        EmployeeFullName = employeeFullName,
                        Total = total,
                        CustomerPhoneNumber = customerPhoneNumber,
                        BillStatus = billDetails.FirstOrDefault().billstatus,
                        SellBill = billDetails.FirstOrDefault(),
                        OrderItems = ListOrderItems,
                        CustomerAddress = customerAddress,
                    };
                    HoadonList.Add(hoadon);
                }
                DisplayBillList = HoadonList.ToList();

                //NumRowEachPageTextBox = "5";
                //NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
                //currentpage = 1;
                //pack_page = 1;
                //settingButtonNextPrev();
            }
            #endregion

            #region Command reset filter DSHoaDon
            ResetFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) => ResetFilter());
            void ResetFilter()
            {
                SelectedEmployee = null;
                SelectedOrderStatus = null;
                SearchString = null;
            }
            #endregion

            #region Command in đơn hàng
            PrintBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {
                        printDialog.PrintVisual(p as Grid, "Invoice");
                    }
                }
                catch
                {

                }
            });

            OpenPrintBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InhoaDon newInHoaDon = new InhoaDon();
                newInHoaDon.ShowDialog();
            });
            #endregion

            #region Command Load data hóa đơn để xem
            LoadBillDetailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewBillDetail = SelectedBill;//(p as BillDetail);
                ThongTinHoaDon = ViewBillDetail;
                TienKhachTra = DataProvider.Ins.DB.profitSummaries.Where(x => x.billCode == ThongTinHoaDon.BillCode).FirstOrDefault().payPrice;
                TienThua = TienKhachTra - ThongTinHoaDon.Total;
                IsBillViewing = Visibility.Visible;
                IsDSHoaDon = Visibility.Collapsed;
                IsBillCreating = Visibility.Collapsed;
            });
            #endregion

            #region Command Load data để tạo đơn hàng
            LoadCreateBillCommand = new RelayCommand<object>((p) => { return true; }, (p) => LoadCreateBill());
            void LoadCreateBill()
            {
                IsBillCreating = Visibility.Visible;
                IsDSHoaDon = Visibility.Collapsed;
                IsBillViewing = Visibility.Collapsed;

                SellBillInfomation = SellBillInfomation;
                // Load danh sác khách hàng
                CustomerList = new ObservableCollection<custommer>(DataProvider.Ins.DB.custommers);
                // Load danh sách nhân viên
                EmployeeList = new ObservableCollection<employee>(DataProvider.Ins.DB.employees);
                // Load danh sách item 
                ItemsListSearch = new ObservableCollection<item>(DataProvider.Ins.DB.items);
                // Tạo mới thông tin đơn hàng
                SellBillInfomation = new ObservableCollection<SellBillItem>();
                // Tự tạo mới bill code tăng dần
                SellBillCode = "EP" + getNextCode.getCode(DataProvider.Ins.DB.bills.Where(x => x.billType == "export").ToList().Count);
                // Đặt mặc định ngày giờ hiện tại cho ngày đặt hàng, ngày giao hàng, ngày chứng từ
                DateTime CurrentDate = DateTime.Now;
                OrderDate = CurrentDate;
                DeliveryDate = CurrentDate;
                LicenseDate = CurrentDate;
                // Tạo danh sách phương thức thanh toán và phương thức giao hàng
                string[] PaymentMethods = { "Thẻ", "Tiền mặt" };
                string[] DeliveryMethods = { "Tại cửa hàng", "Giao hàng" };
                PaymentMethodList = new List<string>(PaymentMethods);
                DeliveryMethodList = new List<string>(DeliveryMethods);

                SelectedItem = null;
                SelectedCustomer = DataProvider.Ins.DB.custommers.Where(x => x.firstName == "Khách vãng lai").FirstOrDefault();
                PhoneNumber = null;
                Address = null;
                SelectedPaymentMethod = null;
                SelectedDeliveryMethod = null;
                SelectedEmployee = null;
                Tag = null;
                Note = null;

                GetVideoDevices();
                BarcodeScanner newBarcodeScanner = new BarcodeScanner();
                Barcode = "";
                newBarcodeScanner.ShowDialog();
            }
            #endregion

            #region Command thêm vật phẩm vào đơn hàng
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

            #region Barcode Scanner
            StartBarcodeScannerCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                StartCamera();
            });
            StopBarcodeScannerCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                StopCamera();
            });

            #endregion

            #region Command thanh toán cho đơn hàng
            CheckoutClickCommand = new RelayCommand<object>((p) =>
            {
                if (SellBillInfomation == null)
                {
                    return false;
                }
                if (SellBillInfomation.Count == 0 || SelectedEmployeeCreateBill == null || SelectedDeliveryMethod == null || SelectedPaymentMethod == null || IsOrderComfirmation == Visibility.Visible)
                    return false;
                return true;
            }, (p) =>
            {
                IsOrderComfirmation = Visibility.Visible;
                PayPrice = (int)Total;
            });

            CancelClickCommand = new RelayCommand<object>((p) => { return true; },
            (p) =>
            {
                IsOrderComfirmation = Visibility.Collapsed;
            });

            ConfirmClickCommand = new RelayCommand<object>((p) =>
            {
                if (PayPrice < (int)Total)
                    return false;
                return true;
            }, (p) =>
            {
                bill NewBill = new bill { billCode = SellBillCode, billType = "export" };
                DataProvider.Ins.DB.bills.Add(NewBill);
                bool Saved = false;
                sellBill SavedBill = new sellBill();
                foreach (var bill_ in SellBillInfomation)
                {
                    sellBill SaveSellBill = new sellBill()
                    {
                        billCodeSell = SellBillCode,
                        billstatus = "Đã thanh toán",
                        idEmployee = SelectedEmployeeCreateBill.idEmployee,
                        idCustomer = SelectedCustomer.idCustommer,
                        number = bill_.Amount,
                        sellDate = (DateTime)OrderDate,
                        licenseDate = (DateTime)LicenseDate,
                        deliveryDate = (DateTime)DeliveryDate,
                        idItem = bill_.Item.idItem,
                        unitPrice = bill_.Item.sellPriceItem,
                        tag = Tag,
                        note = Note,
                        deliveryMethod = SelectedDeliveryMethod,
                        paymentMethod = SelectedPaymentMethod,
                    };
                    if (Saved == false)
                    {
                        SavedBill = SaveSellBill;
                        Saved = true;
                    }
                    DataProvider.Ins.DB.sellBills.Add(SaveSellBill);

                    item curentItem = DataProvider.Ins.DB.items.Where(x => x.idItem == bill_.Item.idItem).FirstOrDefault();
                    curentItem.quantity -= bill_.Amount;
                }

                List<profitSummary> ListProfit = DataProvider.Ins.DB.profitSummaries.ToList();

                profitSummary SaveprofitSummary = new profitSummary()
                {
                    billCode = SellBillCode,
                    billType = "export",
                    rootPrice = (int)Total,
                    payPrice = PayPrice,
                    exchangePrice = ExchangePrice,
                    idCustomer = SelectedCustomer.idCustommer,
                    idEmployee = SelectedEmployeeCreateBill.idEmployee,
                    day = (DateTime)OrderDate,
                    nameCustomer = SelectedCustomerFullName,
                    nameEmployee = SelectedEmployeeCreateBillFullName,
                    budget = ListProfit.Count <= 0 ? (int)Total : (ListProfit[ListProfit.Count - 1].budget + (int)Total),
                    payment = SelectedPaymentMethod,
                };
                DataProvider.Ins.DB.profitSummaries.Add(SaveprofitSummary);

                custommer CurrentCustomer = DataProvider.Ins.DB.custommers.Where(x => x.idCustommer == SelectedCustomer.idCustommer).FirstOrDefault();
                CurrentCustomer.accumulatedPoints = CurrentCustomer.accumulatedPoints == null ? (int)Total : CurrentCustomer.accumulatedPoints + (int)Total;

                BillDetail hoadon = new BillDetail
                {
                    Bill = NewBill,
                    BillCode = NewBill.billCode,
                    CustomerFullName = SelectedCustomerFullName,
                    EmployeeFullName = SelectedEmployeeCreateBillFullName,
                    Total = (int)Total,
                    CustomerPhoneNumber = PhoneNumber,
                    BillStatus = SavedBill.billstatus,
                    SellBill = SavedBill,
                    OrderItems = ListOrderItems,
                    CustomerAddress = Address,
                };

                DataProvider.Ins.DB.SaveChanges();
                ThongTinHoaDon = hoadon;
                TienThua = ExchangePrice;
                TienKhachTra = PayPrice;
                HoadonList.Add(hoadon);
                DisplayBillList = HoadonList.ToList();
                //DivInventoryList = HoadonList;
                IsOrderComfirmation = Visibility.Collapsed;

                if (MessageBox.Show("Bạn có muốn in hóa đơn không?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    InhoaDon newInHoaDon = new InhoaDon();
                    newInHoaDon.ShowDialog();
                }
            });
            #endregion

            #region Command cho chuyển page
            //tbNumRowEachPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    currentpage = 1;
            //    //LoadData();
            //    SearchEngineer();
            //    //MessageBox.Show(DisplayBillList.Count().ToString());
            //    settingButtonNextPrev();
            //    //MessageBox.Show(DivInventoryList.Count().ToString());
            //});
            //btnNextClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    if (currentpage < maxpage)
            //    {
            //        currentpage += 1;
            //        if (currentpage % 3 == 0)
            //            pack_page = currentpage / 3;
            //        else
            //            pack_page = Convert.ToInt32(currentpage / 3) + 1;
            //        //MessageBox.Show("Max page is" + maxpage.ToString()+"pack_page is"+pack_page.ToString());
            //    }
            //    settingButtonNextPrev();
            //});
            //btnendPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    currentpage = maxpage;
            //    pack_page = max_pack_page;
            //    settingButtonNextPrev();
            //});
            //btnfirstPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    currentpage = 1;
            //    pack_page = 1;
            //    settingButtonNextPrev();
            //});
            //btnPrevPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    if (currentpage > 1)
            //    {
            //        currentpage -= 1;
            //        if (currentpage % 3 == 0)
            //            pack_page = currentpage / 3;
            //        else
            //            pack_page = Convert.ToInt32(currentpage / 3) + 1;
            //        //MessageBox.Show("Max page is" + maxpage.ToString()+"pack_page is"+pack_page.ToString());
            //    }
            //    settingButtonNextPrev();
            //});
            //btnLoc2Command = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    SearchEngineer();
            //    settingButtonNextPrev();
            //});
            #endregion

            #region Command xóa vật phẩm đang chọn khỏi đơn hàng
            RemoveItemFromSellBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SellBillInfomation.Remove(SelectedSellBillItem);
                UpdateTotal();
            });
            #endregion

            #region Command tính tổng tiền của đơn hàng
            UpdateTotalCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UpdateTotal();
            });
            #endregion

            #region Command back to DSHoaDon
            BacktoDSHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsBillViewing = Visibility.Collapsed;
                IsBillCreating = Visibility.Collapsed;
                IsDSHoaDon = Visibility.Visible;
            });
            #endregion

            #region Command export to Excel
            ExportFileCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataGridExport.ExportDataGrid(p);
            });
            #endregion
        }

        #region Khai báo biến và command
        private ObservableCollection<BillDetail> _HoadonList;
        public ObservableCollection<BillDetail> HoadonList
        {
            get => _HoadonList;
            set
            {
                _HoadonList = value;
                OnPropertyChanged();
            }
        }

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
                SearchEngineer();
                //settingButtonNextPrev();
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
                SearchEngineer();
                //settingButtonNextPrev();
            }
        }

        public ICommand ResetFilterCommand { get; set; }

        public ICommand PrintBillCommand { get; set; }

        public ICommand OpenPrintBillCommand { get; set; }

        public ICommand ExportFileCommand { get; set; }

        public ICommand LoadedUserControlCommand { get; set; }

        public ICommand LoadBillDetailCommand { get; set; }

        public ICommand BacktoDSHoaDonCommand { get; set; }

        public ICommand LoadCreateBillCommand { get; set; }

        public ICommand StartBarcodeScannerCommand { get; set; }

        public ICommand StopBarcodeScannerCommand { get; set; }

        private string _SearchString;
        public string SearchString
        {
            get => _SearchString;
            set
            {
                _SearchString = value;
                OnPropertyChanged();
                SearchEngineer();
                //settingButtonNextPrev();
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

        private Visibility _IsOrderComfirmation;
        public Visibility IsOrderComfirmation { get => _IsOrderComfirmation; set { _IsOrderComfirmation = value; OnPropertyChanged(); } }

        private BillDetail _ViewBillDetail;
        public BillDetail ViewBillDetail { get => _ViewBillDetail; set { _ViewBillDetail = value; OnPropertyChanged(); } }

        private List<SellBillItem> _ListOrderItems;
        public List<SellBillItem> ListOrderItems { get => _ListOrderItems; set { _ListOrderItems = value; OnPropertyChanged(); } }

        private ObservableCollection<item> _ListItems;
        public ObservableCollection<item> ListItems { get => _ListItems; set { _ListItems = value; OnPropertyChanged(); } }


        //Biến của phần tạo hóa đơn
        public ICommand CheckoutClickCommand { get; set; }

        public ICommand CancelClickCommand { get; set; }

        public ICommand ConfirmClickCommand { get; set; }

        public ICommand AddItemIntoSellBillCommand { get; set; }

        public ICommand InputAmountCommand { get; set; }

        public ICommand RemoveItemFromSellBillCommand { get; set; }

        public ICommand UpdateTotalCommand { get; set; }

        private ObservableCollection<custommer> _CustomerList;
        public ObservableCollection<custommer> CustomerList { get => _CustomerList; set { _CustomerList = value; OnPropertyChanged(); } }

        private ObservableCollection<item> _ItemsListSearch;
        public ObservableCollection<item> ItemsListSearch { get => _ItemsListSearch; set { _ItemsListSearch = value; OnPropertyChanged(); } }

        private item _SelectedItem;
        public item SelectedItem { get => _SelectedItem; set { _SelectedItem = value; OnPropertyChanged(); } }

        private  ObservableCollection<SellBillItem> _SellBillInfomation;
        public ObservableCollection<SellBillItem> SellBillInfomation { get => _SellBillInfomation; set { _SellBillInfomation = value; OnPropertyChanged(); if (SellBillInfomation != null) { UpdateTotal(); } } }

        private ObservableCollection<employee> _EmployeeList;
        public ObservableCollection<employee> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }

        private employee _SelectedEmployeeCreateBill;
        public employee SelectedEmployeeCreateBill
        {
            get => _SelectedEmployeeCreateBill; set
            {
                _SelectedEmployeeCreateBill = value;
                OnPropertyChanged();
                if (SelectedEmployeeCreateBill != null)
                    SelectedEmployeeCreateBillFullName = SelectedEmployeeCreateBill.lastName + " " + SelectedEmployeeCreateBill.firstName;
            }
        }

        private string _SelectedEmployeeCreateBillFullName;
        public string SelectedEmployeeCreateBillFullName { get => _SelectedEmployeeCreateBillFullName; set { _SelectedEmployeeCreateBillFullName = value; OnPropertyChanged(); } }

        private custommer _SelectedCustomer;
        public custommer SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();
                if (SelectedCustomer != null)
                {
                    SelectedCustomerFullName = SelectedCustomer.lastName + " " + SelectedCustomer.firstName;
                    PhoneNumber = SelectedCustomer.phoneNumber;
                    Address = SelectedCustomer.custommerAddress;
                }
            }
        }

        private string _SelectedCustomerFullName;
        public string SelectedCustomerFullName { get => _SelectedCustomerFullName; set { _SelectedCustomerFullName = value; OnPropertyChanged(); } }

        private SellBillItem _SelectedSellBillItem;
        public SellBillItem SelectedSellBillItem { get => _SelectedSellBillItem; set { _SelectedSellBillItem = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private float _Total;
        public float Total { get => _Total; set { _Total = value; OnPropertyChanged(); } }

        private int _PayPrice;
        public int PayPrice
        {
            get => _PayPrice;
            set
            {
                _PayPrice = value;
                OnPropertyChanged();
                if (Total != 0 && Total > 0 && PayPrice >= (int)Total)
                {
                    ExchangePrice = PayPrice - (int)Total;
                }
            }
        }

        private int _ExchangePrice;
        public int ExchangePrice { get => _ExchangePrice; set { _ExchangePrice = value; OnPropertyChanged(); } }

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

        // Biến, command cho in hóa đơn
        private BillDetail _ThongTinHoaDon;
        public BillDetail ThongTinHoaDon { get => _ThongTinHoaDon; set { _ThongTinHoaDon = value; OnPropertyChanged(); } }

        private int _TienKhachTra;
        public int TienKhachTra { get => _TienKhachTra; set { _TienKhachTra = value; OnPropertyChanged(); } }

        private int _TienThua;
        public int TienThua { get => _TienThua; set { _TienThua = value; OnPropertyChanged(); } }

        public ICommand InHoaDon { get; set; }
        #endregion

        #region Hàm tính tổng tiền đơn hàng
        public void UpdateTotal()
        {
            Total = 0;
            foreach (var billinfo in SellBillInfomation)
            {
                Total += billinfo.Item.sellPriceItem * billinfo.Amount * (1 - billinfo.Discount / 100);
            }
        }
        #endregion

        #region Page select
        ////Page Property
        //private ObservableCollection<BillDetail> _DivInventoryList;
        //public ObservableCollection<BillDetail> DivInventoryList { get => _DivInventoryList; set { _DivInventoryList = value; OnPropertyChanged(); } }

        //private Visibility _3cham1Visible;
        //public Visibility Bacham1Visible
        //{
        //    get { return _3cham1Visible; }
        //    set
        //    {
        //        _3cham1Visible = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private Visibility _3cham2Visible;
        //public Visibility Bacham2Visible
        //{
        //    get { return _3cham2Visible; }
        //    set
        //    {
        //        _3cham2Visible = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public int maxpage { get; set; }
        //public int max_pack_page { get; set; }
        //public int pack_page { get; set; }
        //public int currentpage = 1;
        //private string _numRowEachPageTextBox;
        //public string NumRowEachPageTextBox
        //{
        //    get { return _numRowEachPageTextBox; }
        //    set
        //    {
        //        _numRowEachPageTextBox = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public int NumRowEachPage;
        //private page btnPage1;
        //public page BtnPage1
        //{
        //    get { return btnPage1; }
        //    set
        //    {
        //        btnPage1 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private page btnPage2;
        //public page BtnPage2
        //{
        //    get { return btnPage2; }
        //    set
        //    {
        //        btnPage2 = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private page btnPage3;
        //public page BtnPage3
        //{
        //    get { return btnPage3; }
        //    set
        //    {
        //        btnPage3 = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private bool _leftVisi;
        //public bool LeftVisi
        //{
        //    get { return _leftVisi; }
        //    set
        //    {
        //        _leftVisi = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private bool _rightVisi;
        //public bool RightVisi
        //{
        //    get { return _rightVisi; }
        //    set
        //    {
        //        _rightVisi = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ICommand tbNumRowEachPageCommand { get; set; }
        //public ICommand btnNextClickCommand { get; set; }
        //public ICommand btnendPageCommand { get; set; }
        //public ICommand btnfirstPageCommand { get; set; }
        //public ICommand btnPrevPageCommand { get; set; }
        //public ICommand btnLoc2Command { get; set; }

        //void settingButtonNextPrev()
        //{
        //    int ilc = DisplayBillList.Count();
        //    BtnPage1 = new page();
        //    BtnPage2 = new page();
        //    BtnPage3 = new page();

        //    //currentpage = 1;

        //    if (NumRowEachPageTextBox != "")
        //    {
        //        //init max page
        //        NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
        //        if (ilc % NumRowEachPage == 0)
        //            maxpage = ilc / NumRowEachPage;
        //        else
        //            maxpage = Convert.ToInt32((ilc / NumRowEachPage)) + 1;
        //        if (maxpage % 3 == 0)
        //            max_pack_page = maxpage / 3;
        //        else
        //            max_pack_page = Convert.ToInt32(maxpage / 3) + 1;

        //        //Init max page
        //        DivInventoryList = new ObservableCollection<BillDetail>();
        //        DivInventoryList.Clear();
        //        int startPos = (currentpage - 1) * NumRowEachPage;
        //        int endPos = currentpage * NumRowEachPage - 1;
        //        if (endPos >= ilc)
        //            endPos = ilc - 1;

        //        int flag = 0;
        //        foreach (var item in DisplayBillList)
        //        {
        //            if (flag >= startPos && flag <= endPos)
        //                DivInventoryList.Add(item);
        //            flag++;
        //        }
        //        //MessageBox.Show(DivInventoryList.Count.ToString());

        //        //Button "..." visible

        //        //MessageBox.Show("max page is" + maxpage.ToString()+"current page is"+currentpage.ToString());
        //        //MessageBox.Show("Max pack page is" + max_pack_page.ToString() + "pack_page is" + pack_page.ToString());
        //        if (max_pack_page == 1)
        //        {
        //            Bacham1Visible = Visibility.Collapsed;
        //            Bacham2Visible = Visibility.Collapsed;
        //        }
        //        else
        //        {
        //            if (pack_page == max_pack_page)
        //            {
        //                Bacham1Visible = Visibility.Visible;
        //                Bacham2Visible = Visibility.Collapsed;
        //            }
        //            else
        //            {
        //                if (pack_page == 1)
        //                {
        //                    Bacham1Visible = Visibility.Collapsed;
        //                    Bacham2Visible = Visibility.Visible;
        //                }
        //                else
        //                {
        //                    Bacham1Visible = Visibility.Visible;
        //                    Bacham2Visible = Visibility.Visible;
        //                }
        //            }
        //        }

        //        //Button "..." visible

        //        if (currentpage == 1 && maxpage == 1)
        //        {
        //            LeftVisi = false;
        //            RightVisi = true;
        //        }
        //        else
        //        {
        //            if (currentpage == maxpage)
        //            {
        //                LeftVisi = true;
        //                RightVisi = false;
        //            }
        //            else
        //            {
        //                if (currentpage == 1)
        //                {
        //                    LeftVisi = false;
        //                    RightVisi = true;
        //                }
        //                else
        //                {
        //                    LeftVisi = true;
        //                    RightVisi = true;
        //                }
        //            }
        //        }

        //        if (maxpage >= 3)
        //        {
        //            BtnPage1.PageVisi = Visibility.Visible;
        //            BtnPage2.PageVisi = Visibility.Visible;
        //            BtnPage3.PageVisi = Visibility.Visible;

        //            switch (currentpage % 3)
        //            {
        //                case 1:
        //                    BtnPage1.BackGround = Brushes.Blue;
        //                    BtnPage2.BackGround = Brushes.White;
        //                    BtnPage3.BackGround = Brushes.White;
        //                    BtnPage1.PageVal = currentpage;
        //                    BtnPage2.PageVal = currentpage + 1;
        //                    BtnPage3.PageVal = currentpage + 2;
        //                    break;
        //                case 2:
        //                    BtnPage1.BackGround = Brushes.White;
        //                    BtnPage2.BackGround = Brushes.Blue;
        //                    BtnPage3.BackGround = Brushes.White;
        //                    BtnPage1.PageVal = currentpage - 1;
        //                    BtnPage2.PageVal = currentpage;
        //                    BtnPage3.PageVal = currentpage + 1;
        //                    break;
        //                case 0:
        //                    BtnPage1.BackGround = Brushes.White;
        //                    BtnPage2.BackGround = Brushes.White;
        //                    BtnPage3.BackGround = Brushes.Blue;
        //                    BtnPage1.PageVal = currentpage - 2;
        //                    BtnPage2.PageVal = currentpage - 1;
        //                    BtnPage3.PageVal = currentpage;
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            if (maxpage == 2)
        //            {
        //                BtnPage1.PageVisi = Visibility.Visible;
        //                BtnPage2.PageVisi = Visibility.Visible;
        //                BtnPage3.PageVisi = Visibility.Collapsed;
        //                switch (currentpage)
        //                {
        //                    case 1:
        //                        BtnPage1.BackGround = Brushes.Blue;
        //                        BtnPage2.BackGround = Brushes.White;
        //                        BtnPage1.PageVal = currentpage;
        //                        BtnPage2.PageVal = currentpage + 1;
        //                        break;
        //                    case 2:
        //                        BtnPage1.BackGround = Brushes.White;
        //                        BtnPage2.BackGround = Brushes.Blue;
        //                        BtnPage1.PageVal = currentpage - 1;
        //                        BtnPage2.PageVal = currentpage;
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                BtnPage1.PageVisi = Visibility.Visible;
        //                BtnPage2.PageVisi = Visibility.Collapsed;
        //                BtnPage3.PageVisi = Visibility.Collapsed;
        //                BtnPage1.PageVal = (currentpage - 1) * NumRowEachPage + 1; ;
        //                BtnPage1.BackGround = Brushes.Blue;
        //                BtnPage1.PageVal = currentpage;
        //            }
        //        }
        //        if (pack_page == max_pack_page)
        //        {
        //            if ((pack_page * 3) > maxpage)
        //                BtnPage3.PageVisi = Visibility.Collapsed;
        //            if ((pack_page * 3 - 1) > maxpage)
        //                BtnPage2.PageVisi = Visibility.Collapsed;
        //        }

        //    }
        //}

        #endregion

        #region Search Engineer 
        private void SearchEngineer()
        {
            if (SelectedOrderStatus != null || SelectedEmployee != null || (SearchString != null && SearchString != ""))
            {
                DisplayBillList = HoadonList.ToList();
                if (SelectedOrderStatus != null)
                    DisplayBillList = DisplayBillList.Where(x => x != null && x.SellBill.billstatus == SelectedOrderStatus).ToList();
                if (SelectedEmployee != null)
                    DisplayBillList = DisplayBillList.Where(x => x != null && x.SellBill.idEmployee == SelectedEmployee.idEmployee).ToList();
                if (SearchString != null && SearchString != "")
                    DisplayBillList = DisplayBillList.Where(x => x.CustomerFullName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 || x.Bill.billCode.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0 || x.CustomerPhoneNumber.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else
                DisplayBillList = HoadonList.ToList();
        }
        #endregion

        #region Barcode Scanner biến và hàm
        private BitmapImage _qrcode;
        public BitmapImage QRCode { get { return _qrcode; } set { _qrcode = value; OnPropertyChanged(); } }

        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { _currentDevice = value; this.OnPropertyChanged("CurrentDevice"); }
        }
        private FilterInfo _currentDevice;

        private string _Barcode;
        public string Barcode { get => _Barcode; set { _Barcode = value; OnPropertyChanged(""); } }

        private IVideoSource _videoSource;

        private void video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                //BitmapImage bi;
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

                //var result = DecodeQrCode(bitmap);
                ZXing.BarcodeReader Reader = new ZXing.BarcodeReader();

                Result result = Reader.Decode(bitmap);

                //Barcode = result.ToString();

                if (result != null)
                {

                    //MessageBox.Show(result.ToString());
                    Barcode = result.ToString();
                    item newitem = DataProvider.Ins.DB.items.Where(x => x.barcode == Barcode).FirstOrDefault();
                    if (newitem != null)
                    {
                        bool exists = false;
                        if (SellBillInfomation != null)
                        {
                            foreach (var billinfo in SellBillInfomation)
                            {
                                if (billinfo.Item == newitem)
                                {
                                    exists = true;
                                }
                            }
                        }
                        else
                        {
                            SellBillInfomation = new ObservableCollection<SellBillItem>();
                        }
                        if (exists == false)
                        { 
                            var BillDetail = new SellBillItem() { Item = newitem, Amount = 1, Discount = 0 };

                            App.Current.Dispatcher.Invoke((Action)delegate
                            {
                                SellBillInfomation.Add(BillDetail);
                            });
                        }
                        UpdateTotal();
                    }

                }
                QRCode = ToBitmapImage(bitmap);
                QRCode.Freeze();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StopCamera();
            }
        }

        private void GetVideoDevices()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevices.Add(filterInfo);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartCamera()
        {
            if (CurrentDevice != null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
            }
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            bi.Freeze();
            return bi;
        }
        #endregion
    }
}
