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
    public class exportBill
    {
        public profitSummary ProfitSummary { get; set; }
        public string FullNameEmployee { get; set; }
        public string Note { get; set; }
        public string Payment { get; set; }
        public string GroupPay { get; set; }
    }
    public class PhieuChiViewModel:BaseViewModel
    {
        // chỉ xem
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        // chỉ xem
        private bool _IsReadOnly;
        public bool IsReadOnly { get => _IsReadOnly; set { _IsReadOnly = value; OnPropertyChanged(); } }

        // chỉ xem
        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

        // ẩn hiện các nút khi chuyển grid
        private Visibility _isSavePhieuChi;
        public Visibility IsSavePhieuChi { get => _isSavePhieuChi; set { _isSavePhieuChi = value; OnPropertyChanged(); } }
        // ẩn hiện các nút khi chuyển grid
        private Visibility _isAddPhieuChi;

        public Visibility IsAddPhieuChi { get => _isAddPhieuChi; set { _isAddPhieuChi = value; OnPropertyChanged(); } }

        // xem phiếu chi
        private exportBill _ViewExportSheet;
        public exportBill ViewExportSheet { get => _ViewExportSheet; set { _ViewExportSheet = value; OnPropertyChanged(); } }

        // tạo dữ liệu cho grid thêm phiếu chi
        // nhóm đối tượng chi trả
        private List<string> _NhomDoiTuong;
        public List<string> NhomDoiTuong { get => _NhomDoiTuong; set { _NhomDoiTuong = value; OnPropertyChanged(); } }
        // nhóm đối tượng chi trả
        private List<string> _ListDoiTuong;
        public List<string> ListDoiTuong { get => _ListDoiTuong; set { _ListDoiTuong = value; OnPropertyChanged(); } }
        // Hình thức thanh toán
        private List<string> _ListHinhThucThanhToan;
        public List<string> ListHinhThucThanhToan { get => _ListHinhThucThanhToan; set { _ListHinhThucThanhToan = value; OnPropertyChanged(); } }
        // Tất cả nhân viên
        private List<string> _ListNhanVien;
        public List<string> ListNhanVien { get => _ListNhanVien; set { _ListNhanVien = value; OnPropertyChanged(); } }

        // danh sách tất cả đơn hàng xuất
        private ObservableCollection<exportBill> _ListExportBill;
        public ObservableCollection<exportBill> ListExportBill { get => _ListExportBill; set { _ListExportBill = value; OnPropertyChanged(); } }



        // command
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand LoadDataViewExportSheetCommand { get; set; }
        public ICommand AddPhieuChiSheetCommand { get; set; }
        public ICommand DeletePhieuChiSheetCommand { get; set; }
        public ICommand SavePhieuChiCommand { get; set; }
        public ICommand TextChangedTienNhanCommand { get; set; }
        public ICommand SelectionChangedNhomNguoiNhanCommand { get; set; }

        public PhieuChiViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });

            // sự kiện ô nhận tiền thay đổi
            TextChangedTienNhanCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                exportBill newExport = new exportBill();
                newExport = ViewExportSheet;
                int exchangPrice = ViewExportSheet.ProfitSummary.payPrice - ViewExportSheet.ProfitSummary.rootPrice;
                if (exchangPrice > 0)
                {
                    newExport.ProfitSummary.exchangePrice = exchangPrice;
                    OnPropertyChanged();
                }
                else
                {
                    newExport.ProfitSummary.exchangePrice = 0;
                }
                ViewExportSheet = newExport;
            });

            // Lưu phiếu chi
            SavePhieuChiCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                bill saveBill = new bill
                {
                    billCode = ViewExportSheet.ProfitSummary.billCode,
                    billType = ViewExportSheet.ProfitSummary.billType,
                    setBillDay = ViewExportSheet.ProfitSummary.sellDay
                };
                // thêm dữ liệu vào database
                DataProvider.Ins.DB.bills.Add(saveBill);
                DataProvider.Ins.DB.SaveChanges();

                // cập nhật bill trong profit
                ViewExportSheet.ProfitSummary.bill = saveBill;
                DataProvider.Ins.DB.profitSummaries.Add(ViewExportSheet.ProfitSummary);
                DataProvider.Ins.DB.SaveChanges();

                // thêm vào list 
                ListExportBill.Add(ViewExportSheet);
            });


            // xoá phiếu chi
            DeletePhieuChiSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá phiếu thu này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var delExportBill = p as exportBill;

                    foreach (var import in DataProvider.Ins.DB.importBills.ToList())
                    {
                        if (import.billCodeImport == delExportBill.ProfitSummary.billCode)
                        {
                            DataProvider.Ins.DB.importBills.Remove(import);
                        }
                    }

                    foreach (var bill in DataProvider.Ins.DB.bills.ToList())
                    {
                        if (bill.billCode == delExportBill.ProfitSummary.billCode)
                        {
                            DataProvider.Ins.DB.bills.Remove(bill);
                        }
                    }

                    foreach (var summary in DataProvider.Ins.DB.profitSummaries.ToList())
                    {
                        if (summary.billCode == delExportBill.ProfitSummary.billCode)
                        {
                            DataProvider.Ins.DB.profitSummaries.Remove(summary);
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();

                    foreach (var importSheet in ListExportBill)
                    {
                        if (importSheet.ProfitSummary.billCode == delExportBill.ProfitSummary.billCode)
                        {
                            ListExportBill.Remove(importSheet);
                            break;
                        }
                    }
                }
            });

            // sự kiện thay đổi lựu chọn nhóm người nhận
            SelectionChangedNhomNguoiNhanCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var newListDoiTuong = new List<string>();
                if (ViewExportSheet.ProfitSummary.typeGroup == "Nhân Viên")
                {
                    newListDoiTuong = ListNhanVien;
                }
                else if (ViewExportSheet.ProfitSummary.typeGroup == "Nhà Cung Cấp")
                {
                    foreach(var supplier in DataProvider.Ins.DB.suppliers.ToList())
                    {
                        newListDoiTuong.Add(supplier.nameSupplier);
                    }
                }
                ListDoiTuong = newListDoiTuong;
            
            });

            // Thêm phiếu chi
            AddPhieuChiSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // tạo mới 
                createDataThemPhieuChi();
                IsSavePhieuChi = Visibility.Visible;
                IsReadOnly = false;
                IsEnable = true;
                Title = "Danh Sách Phiếu Chi > Thêm Phiếu Chi";
            });

            // ẩn hiện grid
            ClickShowHideGridCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridListPhieuChi")
                    {
                        IsAddPhieuChi = Visibility.Visible;
                        Title = "Danh Sách Phiếu Chi";
                    }
                    else
                    {
                        IsAddPhieuChi = Visibility.Collapsed;
                    }
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }

            });

            // load dữ liệu xem phiếu chi
            LoadDataViewExportSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewExportSheet = (p as exportBill);
                IsSavePhieuChi = Visibility.Collapsed;
                IsReadOnly = true;
                IsEnable = false;
                Title = "Danh Sách Phiếu Chi > " + ViewExportSheet.ProfitSummary.billCode;
            });
        }


        // load dữ liệu đầu vào
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

            // load list phiếu chi
            ListExportBill = new ObservableCollection<exportBill>();
            var listBill = DataProvider.Ins.DB.profitSummaries;
            foreach (var bill in listBill)
            {
                if (bill.billType.ToLower() == "export")
                {
                    var currentExport = DataProvider.Ins.DB.importBills.Where(x => x.billCodeImport == bill.billCode).FirstOrDefault();
                    exportBill newExport = new exportBill();
                    newExport.ProfitSummary = bill;
                    if (bill.note == "" || bill.note == null)
                    {
                        var _note = DataProvider.Ins.DB.importBills.Where(x => x.billCodeImport == bill.billCode).FirstOrDefault();
                        if (_note == null)
                        {
                            newExport.ProfitSummary.note = "";
                        }
                        else
                        {
                            newExport.ProfitSummary.note = _note.note;
                        }
                    }
                    
                    var currencAcc = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == bill.idEmployee).FirstOrDefault();
                    newExport.FullNameEmployee = currencAcc.firstName + " " + currencAcc.lastName;
                    ListExportBill.Add(newExport);
                }
            }

            // list tất cả đối tượng
            NhomDoiTuong = new List<string>()
            {
                "Nhân Viên",
                "Nhà Cung Cấp"
            };

            // list hình thức thanh toán
            ListHinhThucThanhToan = new List<string>()
            {
                "Tiền Mặt",
                "Thẻ"
            };

            // load list tất cả nhân viên
            ListNhanVien = new List<string>();
            foreach (var employee in DataProvider.Ins.DB.employees.ToList())
            {
                ListNhanVien.Add(employee.firstName + " " + employee.lastName);
            }
        }

        // tạo dữ liệu cho thêm phiếu chi
        private void createDataThemPhieuChi()
        {
            exportBill newViewExportSheet = new exportBill();
            // load acc đang đăng nhập
            var currentAcc = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();

            // tạo mới các trường 
            newViewExportSheet.ProfitSummary = new profitSummary
            {
                billCode = "EP" + getNextCode.getCode(ListExportBill.Count),
                billType = "Export",
                employee = currentAcc,
                idEmployee = currentAcc.idEmployee,
                nameEmployee = currentAcc.firstName + " " + currentAcc.lastName,
                sellDay = DateTime.Now,
            };

            ViewExportSheet = newViewExportSheet;
        }
    }
}
