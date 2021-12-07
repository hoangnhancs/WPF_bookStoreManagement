using bookStoreManagetment.Model;
using MaterialDesignThemes.Wpf;
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
    public class ShowKhachHang : BaseViewModel
    {
        public custommer Cus { get; set; }
        public List<string> ListBill { get; set; }
        public int counListBill { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        private bool _isNam;
        public bool isNam { get => _isNam; set { _isNam = value; OnPropertyChanged(); } }
        private bool _isNu;
        public bool isNu { get => _isNu; set { _isNu = value; OnPropertyChanged(); } }
    }
    public class DSKhachHangViewModel : BaseViewModel
    {
        //is has account
        private bool isHasAccount;

        //is has account
        private bool isErrorNumBill;

        //chỉnh sửa nhân viên
        private bool _IsEdit;
        public bool IsEdit { get => _IsEdit; set { _IsEdit = value; OnPropertyChanged(); } }

        //id account cũ
        private int _IdOldAcc;
        public int IdOldAcc { get => _IdOldAcc; set { _IdOldAcc = value; OnPropertyChanged(); } }

        //dislay nhan vien
        private ShowKhachHang _ViewKhachHang;
        public ShowKhachHang ViewKhachHang { get => _ViewKhachHang; set { _ViewKhachHang = value; OnPropertyChanged(); } }

        // title 
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        // filter
        // số bill ít nhất
        private int _DisplayMinBill;
        public int DisplayMinBill { get => _DisplayMinBill; set { _DisplayMinBill = value; OnPropertyChanged(); } }
        // số fill nhiều nhất
        private int _DisplayMaxBill;
        public int DisplayMaxBill { get => _DisplayMaxBill; set { _DisplayMaxBill = value; OnPropertyChanged(); } }
        // ngày kết thúc
        private string _DisplaySex;
        public string DisplaySex { get => _DisplaySex; set { _DisplaySex = value; OnPropertyChanged(); } }
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

        // danh sách tất cả vị trí
        private List<string> _ListDoiTuongKH;
        public List<string> ListDoiTuongKH { get => _ListDoiTuongKH; set { _ListDoiTuongKH = value; OnPropertyChanged(); } }
        // danh sách giới tính
        private List<string> _ListSex;
        public List<string> ListSex { get => _ListSex; set { _ListSex = value; OnPropertyChanged(); } }

        // danh sách tất cả đơn hàng xuất
        private ObservableCollection<ShowKhachHang> backupListCustomers;
        private ObservableCollection<ShowKhachHang> _ListCustomers;
        public ObservableCollection<ShowKhachHang> ListCustomers { get => _ListCustomers; set { _ListCustomers = value; OnPropertyChanged(); } }

        // command
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand DeleteKhachHangCommand { get; set; }
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand LoadDataViewKhachHangCommand { get; set; }
        public ICommand LoadAddNhanVienCommand { get; set; }
        public ICommand SaveNhanVienCommand { get; set; }
        public ICommand CheckedSexMaleCommand { get; set; }
        public ICommand CheckedSexFemaleCommand { get; set; }
        public ICommand TextChangedNameAccountCommand { get; set; }
        public ICommand TextChangedNumOfBillCommand { get; set; }

        //filter
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }
        public DSKhachHangViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });

            // load form
            TextChangedNumOfBillCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DisplayMaxBill < DisplayMinBill)
                {
                    (p as PackIcon).Visibility = Visibility.Visible;
                    isErrorNumBill = true;
                }
                else
                {
                    (p as PackIcon).Visibility = Visibility.Collapsed;
                    isErrorNumBill = false;
                }
            });

            // textchanged tìm kiếm 
            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Query != "" && Query != null)
                {
                    ObservableCollection<ShowKhachHang> newListNhanVien = new ObservableCollection<ShowKhachHang>();
                    foreach (var Custom in backupListCustomers)
                    {
                        if (Custom.Cus.idCustommer.ToLower().Contains(Query) || Custom.FullName.ToLower().Contains(Query))
                        {
                            newListNhanVien.Add(Custom);
                        }
                    }
                    ListCustomers = newListNhanVien;
                }
                else
                {
                    Filter(backupListCustomers);
                }

            });

            // filter
            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (isErrorNumBill)
                    return false;
                if (DisplaySex != null || DisplayMaxBill != 0 || DisplayMinBill != 0)
                    return true;
                return false;
            }, (p) =>
            {
                Filter(backupListCustomers);
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d75c1e");
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
                if (DisplaySex != null || DisplayMinBill != 0 || DisplayMaxBill != 0)
                    return true;
                return false;
            }, (p) =>
            {
                DisplaySex = "";
                DisplaySex = null;
                DisplayMaxBill = 0;
                DisplayMinBill = 0;
                Query = "";
                ListCustomers = backupListCustomers;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // xoá nhân viên
            DeleteKhachHangCommand = new RelayCommand<object>((p) => {
                if (p == null)
                    return false;
                if ((p as ShowKhachHang).Cus == null)
                    return false;
                return true;
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá khách hàng này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var delKhachHang = p as ShowKhachHang;

                    var currentKH = DataProvider.Ins.DB.custommers.Where(x => x.idCustommer == delKhachHang.Cus.idCustommer).FirstOrDefault();
                    DataProvider.Ins.DB.custommers.Remove(currentKH);
                    DataProvider.Ins.DB.SaveChanges();

                    backupListCustomers.Remove(delKhachHang);
                    ListCustomers = backupListCustomers;
                }
            });

            // ẩn hiện grid
            ClickShowHideGridCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridDSKhachHang")
                    {
                        Title = "Danh Sách Nhân Viên";
                    }
                    else
                    {
                        //IsAddPhieuChi = Visibility.Collapsed;

                        // reset filter search
                        Query = "";
                        DisplaySex = "";
                        DisplaySex = null;
                        DisplayMinBill = 0;
                        DisplayMinBill = 0;
                        IsFilter = Visibility.Collapsed;
                        ListCustomers = backupListCustomers;

                        var bc = new BrushConverter();
                        BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
                    }
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }

            });

            // load dữ liệu xem / edit
            LoadDataViewKhachHangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsEdit = true;
                ViewKhachHang = p as ShowKhachHang;
                Title = "Danh Sách Khách Hàng > " + ViewKhachHang.Cus.idCustommer;
            });

            // load dữ liệu thêm mới
            LoadAddNhanVienCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ShowKhachHang newViewKhachHang = new ShowKhachHang();
                newViewKhachHang.Cus = new custommer()
                {
                    nameAccount = "",
                    custommerEmail = "",
                    custommerAddress = "",
                    phoneNumber = "",
                };
                newViewKhachHang.ListBill = new List<string>();
                ViewKhachHang = newViewKhachHang;
                Title = "Danh Sách Khách Hàng > Thêm Khách Hàng";
            });

            // thêm mới hoặc lưu edit
            SaveNhanVienCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (ViewKhachHang == null)
                        return false;
                    if (ViewKhachHang.Cus == null)
                        return false;
                    if (ViewKhachHang.isNam == false && ViewKhachHang.isNu == false)
                        return false;
                    if (ViewKhachHang.Cus.firstName == null || ViewKhachHang.Cus.lastName == null || ViewKhachHang.Cus.nameAccount == null)
                        return false;
                    if (ViewKhachHang.Cus.firstName == "" || ViewKhachHang.Cus.lastName == "" || ViewKhachHang.Cus.nameAccount == "")
                        return false;
                    return true;
                },
                (p) =>
                {
                    if (IsEdit)
                    {
                        var check = ViewKhachHang;
                        if (ViewKhachHang.isNam)
                        {
                            ViewKhachHang.Cus.sex = "Nam";
                        }
                        else
                        {
                            ViewKhachHang.Cus.sex = "Nu";

                        }

                        var currentKH = DataProvider.Ins.DB.custommers.Where(x => x.idCustommer == ViewKhachHang.Cus.idCustommer).FirstOrDefault();
                        currentKH = ViewKhachHang.Cus;
                        DataProvider.Ins.DB.SaveChanges();
                        ListCustomers = backupListCustomers;

                        IsEdit = false;
                    }
                    else
                    {
                        // tìm id phù hợp
                        int startID = backupListCustomers.Count;
                        string code = "";
                        do
                        {
                            code = "CUS" + getNextCode.getCode(startID);
                            var checkCode = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == code).FirstOrDefault();
                            if (checkCode == null)
                            {
                                break;
                            }
                            startID++;
                        } while (true);

                        var sexCurrent = "";
                        if (ViewKhachHang.isNam)
                        {
                            sexCurrent = "Nam";
                        }
                        else
                        {
                            sexCurrent = "Nu";

                        }
                        DataProvider.Ins.DB.custommers.Add(new custommer
                        {
                            idCustommer = code,
                            accumulatedPoints = 0,
                            custommerAddress = ViewKhachHang.Cus.custommerAddress,
                            nameAccount = ViewKhachHang.Cus.nameAccount,
                            citizenIdentification = "",
                            custommerEmail = ViewKhachHang.Cus.custommerEmail,
                            custommerNote = "",
                            dateOfBirth = ViewKhachHang.Cus.dateOfBirth,
                            firstName = ViewKhachHang.Cus.firstName,
                            lastName = ViewKhachHang.Cus.lastName,
                            phoneNumber = ViewKhachHang.Cus.phoneNumber,
                            sex = sexCurrent,
                        });
                        DataProvider.Ins.DB.SaveChanges();
                        ViewKhachHang.Cus.idCustommer = code;
                        ViewKhachHang.Cus.sex = sexCurrent;
                        ViewKhachHang.Cus.accumulatedPoints = 0;
                        ViewKhachHang.counListBill = 0;
                        ViewKhachHang.FullName = ViewKhachHang.Cus.lastName + " " + ViewKhachHang.Cus.lastName;
                        ViewKhachHang.ListBill = new List<string>();
                        backupListCustomers.Add(ViewKhachHang);
                        ListCustomers = backupListCustomers;
                    }

                    (p as DataGrid).Items.Refresh();
                });

            // check sex
            CheckedSexMaleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewKhachHang.isNu = false;
            });

            // check sex
            CheckedSexFemaleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewKhachHang.isNam = false;
            });

            // kiểm tra tài khoản tồn tại
            TextChangedNameAccountCommand = new RelayCommand<PackIcon>((p) => { return true; }, (p) =>
            {
                var checkAcc = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == ViewKhachHang.Cus.nameAccount).FirstOrDefault();
                if (checkAcc != null && IsEdit == false)
                {
                    (p as PackIcon).Visibility = Visibility.Visible;
                    isHasAccount = true;
                }
                else
                {
                    (p as PackIcon).Visibility = Visibility.Collapsed;
                    isHasAccount = false;
                }
            });

        }

        // hàm thêm
        // hàm load dư liệu
        private void LoadData()
        {
            // load tất cả khách hàng
            //load list đối tượng khách hàng
            ListDoiTuongKH = new List<string>();
            ListCustomers = new ObservableCollection<ShowKhachHang>();
            var Customs = DataProvider.Ins.DB.custommers.ToList();
            foreach (var Cus in Customs)
            {
                // load list doi tuong kh muon đở name account :v 
                if (ListDoiTuongKH.Contains(Cus.nameAccount) == false)
                {
                    ListDoiTuongKH.Add(Cus.nameAccount);
                }

                ShowKhachHang newCus = new ShowKhachHang();
                newCus.Cus = Cus;
                newCus.FullName = Cus.lastName + " " + Cus.firstName;
                if (Cus.sex.ToLower() == "nam")
                {
                    newCus.isNam = true;
                    newCus.isNu = false;
                }
                else
                {
                    newCus.isNam = false;
                    newCus.isNu = true;
                }
                LoadBillOffCus(newCus);
                ListCustomers.Add(newCus);
            }
            backupListCustomers = ListCustomers;

            // load list giới tính
            ListSex = new List<string>()
            {
                "Nam",
                "Nu"
            };

            

            // title
            Title = "Danh Sách Khách Hàng";

            // ẩn grid filter
            IsFilter = Visibility.Collapsed;

            // set màu cho nút filter
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
        }
        
        private void LoadBillOffCus(ShowKhachHang KH) 
        {

            if (KH.ListBill == null)
            {
                KH.ListBill = new List<string>();
            }

            var listBill = DataProvider.Ins.DB.sellBills.Where(x => x.idCustomer == KH.Cus.idCustommer).ToList();
            KH.counListBill = listBill.Count;
            foreach(var bill in listBill)
            {
                if (KH.ListBill.Contains(bill.billCodeSell) == false)
                {
                    KH.ListBill.Add(bill.billCodeSell);
                }
            }
        }

        //filter
        private void Filter(ObservableCollection<ShowKhachHang> ListViewKH)
        {
            List<ShowKhachHang> newListNhanVien = ListViewKH.ToList();
            if (DisplaySex != null && DisplaySex != "")
            {
                newListNhanVien = newListNhanVien.Where(x => x.Cus.sex == DisplaySex).ToList();
            }

            if (DisplayMinBill != 0)
            {
                newListNhanVien = newListNhanVien.Where(x => x.counListBill >= DisplayMinBill).ToList();
            }

            if (DisplayMaxBill != 0)
            {
                newListNhanVien = newListNhanVien.Where(x => x.counListBill <= DisplayMaxBill).ToList();
            }

            ListCustomers = new ObservableCollection<ShowKhachHang>(newListNhanVien);
        }
    }
}
