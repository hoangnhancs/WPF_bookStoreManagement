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
        private ObservableCollection<importBill> _ListImportBill;
        public ObservableCollection<importBill> ListImportBill { get => _ListImportBill; set { _ListImportBill = value; OnPropertyChanged(); } }
        
        // list commnad
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand LoadDataViewImportSheetCommand { get; set; }
        public ICommand DeleteImportSheetCommand { get; set; }

        public PhieuThuViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
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
            var listBill = DataProvider.Ins.DB.profitSummaries;
            foreach(var bill in listBill)
            {
                if(bill.billType == "import")
                {
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
        }
    }
}
