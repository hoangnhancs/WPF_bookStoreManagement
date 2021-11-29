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
    public class importBill
    {
        public profitSummary ProfitSummary { get; set; }
        public string Note { get; set; }
        public string Payment { get; set; }
        public string GroupSupplier { get; set; }
        public string supplier { get; set; }
    }
    public class PhieuThuViewModel:BaseViewModel
    {
        // ẩn hiện các nút khi chuyển grid
        private Visibility _isAddRule;
        public Visibility IsAddRule { get => _isAddRule; set { _isAddRule = value; OnPropertyChanged(); } }

        // danh sách tất cả nhân viên
        private List<string> _AllStaff;
        public List<string> AllStaff { get => _AllStaff; set { _AllStaff = value; OnPropertyChanged(); } }

        // nhân viên đang được chọn
        private string _displayStaff;
        public string DisplayStaff { get => _displayStaff; set { _displayStaff = value; OnPropertyChanged(); } }

        // xem phiếu thu
        private importBill _ViewImportSheet;
        public importBill ViewImportSheet { get => _ViewImportSheet; set { _ViewImportSheet = value; OnPropertyChanged(); } }

        // danh sách tất cả đơn hàng nhập
        private ObservableCollection<importBill> backupListImportBill;
        private ObservableCollection<importBill> _ListImportBill;
        public ObservableCollection<importBill> ListImportBill { get => _ListImportBill; set { _ListImportBill = value; OnPropertyChanged(); } }

        //filter
        // nhóm đối tượng chi trả
        private List<string> _ListDoiTuong;
        public List<string> ListDoiTuong { get => _ListDoiTuong; set { _ListDoiTuong = value; OnPropertyChanged(); } }
        // filter
        // ngày bắt đầu
        private string _displayBeginDay;
        public string displayBeginDay { get => _displayBeginDay; set { _displayBeginDay = value; OnPropertyChanged(); } }
        // ngày kết thúc
        private string _displayEndDay;
        public string displayEndDay { get => _displayEndDay; set { _displayEndDay = value; OnPropertyChanged(); } }
        // nhóm đối tượng filter
        private string _DisplayNameCustomer;
        public string DisplayNameCustomer { get => _DisplayNameCustomer; set { _DisplayNameCustomer = value; OnPropertyChanged(); } }
        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }
        // foreground
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }
        // query tìm kiếm
        private string _Query;
        public string Query { get => _Query; set { _Query = value; OnPropertyChanged(); } }
        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }

        // list commnad
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand LoadDataViewImportSheetCommand { get; set; }
        public ICommand DeleteImportSheetCommand { get; set; }
        //filter
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }

        public PhieuThuViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });

            // textchanged tìm kiếm 
            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Query != "" && Query != null)
                {
                    ObservableCollection<importBill> newListImportBill = new ObservableCollection<importBill>();
                    foreach (var bill in ListImportBill)
                    {
                        if (bill.ProfitSummary.billCode.ToLower().Contains(Query) || bill.ProfitSummary.nameBill.ToLower().Contains(Query))
                        {
                            newListImportBill.Add(bill);
                        }
                    }
                    ListImportBill = newListImportBill;
                }
                else
                {
                    Filter();
                }

            });

            // filter
            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (displayEndDay != null && displayBeginDay != null)
                    return true;
                if ( DisplayNameCustomer != null)
                    return true;
                return false;
            }, (p) =>
            {
                Filter();
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#FF008000");
                ForegroudFilter = (Brush)bc.ConvertFromString("#DDFFFFFF");
            });

            // đóng/mở filter grid
            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
            });

            // đóng filter grid
            CloseFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
            });

            // xoá filter
            DeleteFilterCommand = new RelayCommand<object>((p) => {
                if (DisplayNameCustomer != null  || displayEndDay != null || displayBeginDay != null)
                    return true;
                return false;
            }, (p) =>
            {
                displayBeginDay = null;
                displayEndDay = null;
                DisplayNameCustomer = "";
                DisplayNameCustomer = null;
                ListImportBill = backupListImportBill;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
            });
            // xoá dữ liệu xem phiếu thu
            DeleteImportSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá phiếu thu này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var delImportBill = p as importBill;

                    foreach (var sell in DataProvider.Ins.DB.sellBills.ToList())
                    {
                        if (sell.billCodeSell == delImportBill.ProfitSummary.billCode)
                        {
                            DataProvider.Ins.DB.sellBills.Remove(sell);
                        }
                    }

                    foreach (var bill in DataProvider.Ins.DB.bills.ToList())
                    {
                        if (bill.billCode == delImportBill.ProfitSummary.billCode)
                        {
                            DataProvider.Ins.DB.bills.Remove(bill);
                        }
                    }

                    foreach (var summary in DataProvider.Ins.DB.profitSummaries.ToList())
                    {
                        if (summary.billCode == delImportBill.ProfitSummary.billCode)
                        {
                            DataProvider.Ins.DB.profitSummaries.Remove(summary);
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();

                    foreach (var importSheet in ListImportBill)
                    {
                        if (importSheet.ProfitSummary.billCode == delImportBill.ProfitSummary.billCode)
                        {
                            ListImportBill.Remove(importSheet);
                            break;
                        }
                    }
                }     
            });


            // load dữ liệu xem phiếu thu
            LoadDataViewImportSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewImportSheet = (p as importBill);
            });


            // ẩn hiện grid
            ClickShowHideGridCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridListRule")
                    {
                        IsAddRule = Visibility.Visible;
                    }
                    else
                    {
                        IsAddRule = Visibility.Collapsed;

                        // reset filter search
                        Query = "";
                        displayBeginDay = null;
                        displayEndDay = null;
                        DisplayNameCustomer = "";
                        DisplayNameCustomer = null;
                        IsFilter = Visibility.Collapsed;
                        ListImportBill = backupListImportBill;

                        var bc = new BrushConverter();
                        BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                        ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
                    }
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            });
        }
        
        private void LoadData()
        {
            //// load tất cả nhân viên
            //AllStaff = new List<string>();
            //DisplayStaff = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault().lastName;
            //var staffs = DataProvider.Ins.DB.employees.ToList();
            //foreach (var staff in staffs)
            //{
            //    AllStaff.Add(staff.lastName);
            //}

            ListImportBill = new ObservableCollection<importBill>();
            ListDoiTuong = new List<string>();
            var listBill = DataProvider.Ins.DB.profitSummaries;
            foreach(var bill in listBill)
            {
                if(bill.billType == "import")
                {
                    if (bill.nameCustomer != null && bill.nameCustomer != "")
                    {
                        ListDoiTuong.Add(bill.nameCustomer);
                    }
                    
                    importBill newImport = new importBill();
                    newImport.ProfitSummary = bill;
                    var _note = DataProvider.Ins.DB.sellBills.Where(x => x.billCodeSell == bill.billCode).FirstOrDefault();
                    if (_note == null)
                    {
                        newImport.Note = "";
                    }
                    else
                    {
                       newImport.Note = _note.note;
                    }
                    newImport.Payment = bill.payment;
                    newImport.GroupSupplier = bill.typeGroup;
                    newImport.supplier = bill.nameCustomer;
                    ListImportBill.Add(newImport);
                }
            }
            backupListImportBill = ListImportBill;

            // ẩn grid filter
            IsFilter = Visibility.Collapsed;

            // set màu cho nút filter
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
            ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
        }

        //filter
        private void Filter()
        {
            List<importBill> newListImportBill = backupListImportBill.ToList();
            if (DisplayNameCustomer != null && DisplayNameCustomer != "")
            {
                newListImportBill = newListImportBill.Where(x => x.ProfitSummary.nameCustomer == DisplayNameCustomer).ToList();
            }

            if (displayBeginDay != null)
            {
                List<importBill> temp = new List<importBill>();
                foreach (var bill in newListImportBill)
                {
                    if (DateTime.Compare(bill.ProfitSummary.sellDay, DateTime.ParseExact(displayBeginDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) >= 0
                     && DateTime.Compare(bill.ProfitSummary.sellDay, DateTime.ParseExact(displayEndDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) <= 0)
                    {
                        temp.Add(bill);
                    }
                }
                newListImportBill = temp;
            }

            ListImportBill = new ObservableCollection<importBill>(newListImportBill);
        }
    }
}
