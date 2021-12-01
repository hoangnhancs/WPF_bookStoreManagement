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
    public class exportBill
    {
        public profitSummary ProfitSummary { get; set; }
    }
    public class PhieuChiViewModel : BaseViewModel
    {
        // query tìm kiếm
        private string _Query;
        public string Query { get => _Query; set { _Query = value; OnPropertyChanged(); } }

        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }

        // title 
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        // chỉ xem
        private bool _IsEdit;
        public bool IsEdit { get => _IsEdit; set { _IsEdit = value; OnPropertyChanged(); } }

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

        // filter
        // ngày bắt đầu
        private string _displayBeginDay;
        public string displayBeginDay { get => _displayBeginDay; set { _displayBeginDay = value; OnPropertyChanged(); } }
        // ngày kết thúc
        private string _displayEndDay;
        public string displayEndDay { get => _displayEndDay; set { _displayEndDay = value; OnPropertyChanged(); } }
        // nhóm đối tượng filter
        private string _DisplayGroupType;
        public string DisplayGroupType { get => _DisplayGroupType; set { _DisplayGroupType = value; OnPropertyChanged(); } }
        // đối tượng
        private string _DisplayNameType;
        public string DisplayNameType { get => _DisplayNameType; set { _DisplayNameType = value; OnPropertyChanged(); } }
        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }
        // foreground
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }


        // danh sách tất cả đơn hàng xuất
        private ObservableCollection<exportBill> backupListExportBill;
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
        public ICommand SelectionChangedNhomNguoiNhanFilterCommand { get; set; }
        //filter
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }

        public PhieuChiViewModel()
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
                    ObservableCollection<exportBill> newListExportBill = new ObservableCollection<exportBill>();
                    foreach (var bill in ListExportBill)
                    {
                        if (bill.ProfitSummary.billCode.ToLower().Contains(Query) || bill.ProfitSummary.nameBill.ToLower().Contains(Query))
                        {
                            newListExportBill.Add(bill);
                        }
                    }
                    ListExportBill = newListExportBill;
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
                if (DisplayGroupType != null || DisplayNameType != null)
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
            if (DisplayGroupType != null || DisplayNameType != null || displayEndDay != null || displayBeginDay != null)
                    return true;
                return false;
            }, (p) =>
            {
                displayBeginDay = null;
                displayEndDay = null;
                DisplayNameType = "";
                DisplayNameType = null;
                DisplayGroupType = "";
                DisplayGroupType = null;
                ListExportBill = backupListExportBill;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
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
                if (IsEdit)
                {
                    var lastRowBackup = backupListExportBill[backupListExportBill.Count - 1];
                    ViewExportSheet.ProfitSummary.budget = lastRowBackup.ProfitSummary.budget - ViewExportSheet.ProfitSummary.rootPrice;
                    var EditExport = DataProvider.Ins.DB.profitSummaries.Where(x => x.billCode == ViewExportSheet.ProfitSummary.billCode).FirstOrDefault();
                    EditExport = ViewExportSheet.ProfitSummary;
                    DataProvider.Ins.DB.SaveChanges();
                    IsEdit = false;
                }
                else
                {
                    bill saveBill = new bill
                    {
                        billCode = ViewExportSheet.ProfitSummary.billCode,
                        billType = ViewExportSheet.ProfitSummary.billType
                    };
                    // thêm dữ liệu vào database
                    DataProvider.Ins.DB.bills.Add(saveBill);
                    DataProvider.Ins.DB.SaveChanges();

                    // cập nhật bill trong profit
                    var lastRowBackup = backupListExportBill[backupListExportBill.Count - 1];
                    ViewExportSheet.ProfitSummary.budget = lastRowBackup.ProfitSummary.budget - ViewExportSheet.ProfitSummary.rootPrice;
                    DataProvider.Ins.DB.profitSummaries.Add(ViewExportSheet.ProfitSummary);
                    DataProvider.Ins.DB.SaveChanges();

                    // thêm vào list 
                    backupListExportBill.Add(ViewExportSheet);
                }
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
                            backupListExportBill.Remove(importSheet);
                            break;
                        }
                    }
                }
            });

            // sự kiện thay đổi lựu chọn nhóm người nhận filter
            SelectionChangedNhomNguoiNhanFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var newListDoiTuong = new List<string>();
                if (DisplayGroupType == "Nhân Viên")
                {
                    newListDoiTuong = ListNhanVien;
                }
                else if (DisplayGroupType == "Nhà Cung Cấp")
                {
                    foreach (var supplier in DataProvider.Ins.DB.suppliers.ToList())
                    {
                        newListDoiTuong.Add(supplier.nameSupplier);
                    }
                }
                ListDoiTuong = newListDoiTuong;

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
                    foreach (var supplier in DataProvider.Ins.DB.suppliers.ToList())
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

                        // reset filter search
                        Query = "";
                        displayBeginDay = null;
                        displayEndDay = null;
                        DisplayNameType = "";
                        DisplayNameType = null;
                        DisplayGroupType = "";
                        DisplayGroupType = null;
                        IsFilter = Visibility.Collapsed;
                        ListExportBill = backupListExportBill;

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

            // load dữ liệu xem phiếu chi
            LoadDataViewExportSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewExportSheet = (p as exportBill);
                IsEdit = true;
                //IsSavePhieuChi = Visibility.Collapsed;
                //IsReadOnly = true;
                //IsEnable = false;
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
                    newExport.ProfitSummary.nameEmployee = currencAcc.firstName + " " + currencAcc.lastName;
                    ListExportBill.Add(newExport);
                }
            }
            backupListExportBill = ListExportBill;

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

            // tạo phiếu chi
            createDataThemPhieuChi();

            // ẩn grid filter
            IsFilter = Visibility.Collapsed;

            // set màu cho nút filter
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
            ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
        }

        // tạo dữ liệu cho thêm phiếu chi
        private void createDataThemPhieuChi()
        {
            exportBill newViewExportSheet = new exportBill();
            // load acc đang đăng nhập
            var currentAcc = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();

            // tìm id phù hợp
            int startID = backupListExportBill.Count;
            string code = "";
            do
            {
                code = "EP" + getNextCode.getCode(startID);
                var checkCode = DataProvider.Ins.DB.profitSummaries.Where(x => x.billCode == code).FirstOrDefault();
                if (checkCode == null)
                {
                    break;
                }
                startID++;
            } while (true);

            // tạo mới các trường 
            newViewExportSheet.ProfitSummary = new profitSummary
            {
                billCode = code,
                billType = "Export",
                idEmployee = currentAcc.idEmployee,
                nameEmployee = currentAcc.firstName + " " + currentAcc.lastName,
                day = DateTime.Now
            };

            ViewExportSheet = newViewExportSheet;
        }

        //filter
        private void Filter()
        {
            List<exportBill> newListExportBill = backupListExportBill.ToList();
            if (DisplayGroupType != null && DisplayGroupType != "")
            {
                newListExportBill = newListExportBill.Where(x => x.ProfitSummary.typeGroup == DisplayGroupType).ToList();
            }

            if (DisplayNameType != null && DisplayNameType != "")
            {
                newListExportBill = newListExportBill.Where(x => x.ProfitSummary.nameCustomer == DisplayNameType).ToList();
            }

            if (displayBeginDay != null)
            {
                List<exportBill> temp = new List<exportBill>();
                foreach (var bill in newListExportBill)
                {
                    if (DateTime.Compare(bill.ProfitSummary.day, DateTime.ParseExact(displayBeginDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) >= 0
                     && DateTime.Compare(bill.ProfitSummary.day, DateTime.ParseExact(displayEndDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) <= 0)
                    {
                        temp.Add(bill);
                    }
                }
                newListExportBill = temp;
            }

            ListExportBill = new ObservableCollection<exportBill>(newListExportBill);
        }
    }
}