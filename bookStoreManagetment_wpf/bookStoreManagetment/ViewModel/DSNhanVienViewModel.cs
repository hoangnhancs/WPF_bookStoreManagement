using bookStoreManagetment.Model;
using bookStoreManagetment.Properties;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace bookStoreManagetment.ViewModel
{
    public class ShowNhanVien:BaseViewModel
    {
        public employee Staff { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        private bool _isNam;
        public bool isNam { get => _isNam; set { _isNam = value; OnPropertyChanged(); } }
        private bool _isNu;
        public bool isNu { get => _isNu; set { _isNu = value; OnPropertyChanged(); } }
        public string Password { get; set; }
        private ImageSource _SourceImage;
        public ImageSource SourceImage { get => _SourceImage; set { _SourceImage = value; OnPropertyChanged(); } }
    }
    public class DSNhanVienViewModel:BaseViewModel
    {
        // lỗi không có quyền
        private Visibility _ErrorPhanQuyen;
        public Visibility ErrorPhanQuyen { get => _ErrorPhanQuyen; set { _ErrorPhanQuyen = value; OnPropertyChanged(); } }

        // lỗi không có quyền
        private Visibility _ShowUploadButton;
        public Visibility ShowUploadButton { get => _ShowUploadButton; set { _ShowUploadButton = value; OnPropertyChanged(); } }


        //is has account
        private bool isHasAccount;

        //chỉnh sửa nhân viên
        private bool _IsEdit;
        public bool IsEdit { get => _IsEdit; set { _IsEdit = value; OnPropertyChanged(); } }

        //id account cũ
        private int _IdOldAcc;
        public int IdOldAcc { get => _IdOldAcc; set { _IdOldAcc = value; OnPropertyChanged(); } }

        //dislay nhan vien
        private ShowNhanVien _ViewNhanVien;
        public ShowNhanVien ViewNhanVien { get => _ViewNhanVien; set { _ViewNhanVien = value; OnPropertyChanged(); } }

        // title 
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        // filter
        // ngày bắt đầu
        private string _DisplayPosition;
        public string DisplayPosition { get => _DisplayPosition; set { _DisplayPosition = value; OnPropertyChanged(); } }
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
        private List<string> _ListPosition;
        public List<string> ListPosition { get => _ListPosition; set { _ListPosition = value; OnPropertyChanged(); } }
        // danh sách giới tính
        private List<string> _ListSex;
        public List<string> ListSex { get => _ListSex; set { _ListSex = value; OnPropertyChanged(); } }

        // danh sách tất cả đơn hàng xuất
        private ObservableCollection<ShowNhanVien> backupListEmployees;
        private ObservableCollection<ShowNhanVien> _ListEmployees;
        public ObservableCollection<ShowNhanVien> ListEmployees { get => _ListEmployees; set { _ListEmployees = value; OnPropertyChanged(); } }

        // command
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand DeleteNhanVienCommand { get; set; }
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand LoadDataViewNhanVienCommand { get; set; }
        public ICommand LoadAddNhanVienCommand { get; set; }
        public ICommand SaveNhanVienCommand { get; set; }
        public ICommand CheckedSexMaleCommand { get; set; }
        public ICommand CheckedSexFemaleCommand { get; set; }
        public ICommand TextChangedNameAccountCommand { get; set; }
        public ICommand UploadImageNVCommand { get; set; }

        //filter
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }
        public DSNhanVienViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });

            // load form
            UploadImageNVCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    var bit = new BitmapImage(new Uri(op.FileName));
                    ViewNhanVien.SourceImage = bit;
                    ShowUploadButton = Visibility.Collapsed;
                    ViewNhanVien.Staff.employeeImagePath = op.FileName;
                }
            });

            // textchanged tìm kiếm 
            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Query != "" && Query != null)
                {
                    ObservableCollection<ShowNhanVien> newListNhanVien = new ObservableCollection<ShowNhanVien>();
                    foreach (var staff in backupListEmployees)
                    {
                        if (staff.Staff.idEmployee.ToLower().Contains(Query) || staff.FullName.ToLower().Contains(Query))
                        {
                            newListNhanVien.Add(staff);
                        }
                    }
                    ListEmployees = newListNhanVien;
                }
                else
                {
                    Filter();
                }

            });

            // filter
            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (DisplaySex != null || DisplayPosition != null)
                    return true;
                return false;
            }, (p) =>
            {
                Filter();
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
                if (DisplaySex != null || DisplayPosition != null)
                    return true;
                return false;
            }, (p) =>
            {
                DisplaySex = "";
                DisplaySex = null;
                DisplayPosition = "";
                DisplayPosition = null;
                Query = "";
                ListEmployees = backupListEmployees;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // xoá nhân viên
            DeleteNhanVienCommand = new RelayCommand<object>((p) => {
                if (Permission.ChinhSuaNhanVien)
                {
                    ErrorPhanQuyen = Visibility.Collapsed;
                    return true;
                }
                ErrorPhanQuyen = Visibility.Visible;
                return false;

            }, (p) =>
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Bạn có muốn nhân viên này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var delNhanVien = p as ShowNhanVien;

                    var currentNV = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == delNhanVien.Staff.idEmployee).FirstOrDefault();
                    var currentAcc = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == delNhanVien.Staff.nameAccount).FirstOrDefault();
                    DataProvider.Ins.DB.accounts.Remove(currentAcc);
                    DataProvider.Ins.DB.employees.Remove(currentNV);
                    DataProvider.Ins.DB.SaveChanges();

                    backupListEmployees.Remove(delNhanVien);
                    ListEmployees = backupListEmployees;
                }
            });

            // ẩn hiện grid
            ClickShowHideGridCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridDSNhanVien")
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
                        DisplayPosition = "";
                        DisplayPosition = null;
                        IsFilter = Visibility.Collapsed;
                        ListEmployees = backupListEmployees;

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
            LoadDataViewNhanVienCommand = new RelayCommand<object>((p) => {
                if (Permission.ChinhSuaNhanVien)
                {
                    ErrorPhanQuyen = Visibility.Collapsed;
                    return true;
                }
                ErrorPhanQuyen = Visibility.Visible;
                return false;

            }, (p) =>
            { 

                IsEdit = true;
                ViewNhanVien = p as ShowNhanVien;
                Title = "Danh Sách Nhân Viên > " + ViewNhanVien.Staff.idEmployee;
                IdOldAcc = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == ViewNhanVien.Staff.nameAccount).FirstOrDefault().idAccount;

                if (ViewNhanVien.SourceImage == null)
                    ShowUploadButton = Visibility.Visible;
            });

            // load dữ liệu thêm mới
            LoadAddNhanVienCommand = new RelayCommand<object>((p) => {
                if (Permission.ChinhSuaNhanVien)
                {
                    ErrorPhanQuyen = Visibility.Collapsed;
                    return true;
                }
                ErrorPhanQuyen = Visibility.Visible;
                return false;

            }, (p) =>
            {
                ShowNhanVien newViewNhanVien = new ShowNhanVien();
                newViewNhanVien.Staff = new employee() { 
                    employeeAddress = "",
                    employeeEmail = "",
                    phoneNumber = ""
                };
                ViewNhanVien = newViewNhanVien;
                Title = "Danh Sách Nhân Viên > Thêm Nhân Viên";
                ShowUploadButton = Visibility.Visible;
            });

            // thêm mới hoặc lưu edit
            SaveNhanVienCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (isHasAccount && IsEdit == false)
                        return false;
                    if (ViewNhanVien == null)
                        return false;
                    if (ViewNhanVien.Staff == null)
                        return false;
                    if (ViewNhanVien.isNam == false && ViewNhanVien.isNu == false)
                        return false;
                    if (ViewNhanVien.Staff.employeeSalary == null)
                        return false;
                    if (ViewNhanVien.Staff.firstName == null || ViewNhanVien.Staff.lastName == null || ViewNhanVien.Password == null || ViewNhanVien.Staff.nameAccount == null)
                        return false;
                    if (ViewNhanVien.Staff.firstName == "" || ViewNhanVien.Staff.lastName == "" || ViewNhanVien.Password == "" || ViewNhanVien.Staff.nameAccount == "")

                        return false;
                    return true;
                    }, 
                (p) =>
                {
                    if (IsEdit)
                    {
                        var check = ViewNhanVien;
                        if (ViewNhanVien.isNam)
                        {
                            ViewNhanVien.Staff.sex = "Nam";
                        }
                        else
                        {
                            ViewNhanVien.Staff.sex = "Nu";

                        }

                        var currentNV = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == ViewNhanVien.Staff.idEmployee).FirstOrDefault();
                        currentNV = ViewNhanVien.Staff;

                        var currentAcc = DataProvider.Ins.DB.accounts.Where(x => x.idAccount == IdOldAcc).FirstOrDefault();
                        currentAcc.nameAccount = ViewNhanVien.Staff.nameAccount;
                        currentAcc.passwordAccount = ViewNhanVien.Password;

                        DataProvider.Ins.DB.SaveChanges();

                        var find = backupListEmployees.Where(x => x.Staff.idEmployee == ViewNhanVien.Staff.idEmployee).FirstOrDefault();
                        find = ViewNhanVien;
                        ListEmployees = backupListEmployees;

                        IsEdit = false;
                    }
                    else
                    {
                        // tìm id phù hợp
                        int startID = backupListEmployees.Count;
                        string code = "";
                        do
                        {
                            code = "EMP" + getNextCode.getCode(startID);
                            var checkCode = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == code).FirstOrDefault();
                            if (checkCode == null)
                            {
                                break;
                            }
                            startID++;
                        } while (true);

                        var check = ViewNhanVien;
                        var sexCurrent = "";
                        if (ViewNhanVien.isNam)
                        {
                            sexCurrent = "Nam";
                        }
                        else
                        {
                            sexCurrent = "Nu";

                        }
                        DataProvider.Ins.DB.employees.Add(new employee
                        {
                            idEmployee = code,
                            citizenIdentification = "",
                            dateOfBirth = DateTime.Now,
                            employeeAddress = ViewNhanVien.Staff.employeeAddress,
                            employeeEmail = ViewNhanVien.Staff.employeeEmail,
                            employeeNote = "",
                            employeeSalary = (int)ViewNhanVien.Staff.employeeSalary,
                            nameAccount = ViewNhanVien.Staff.nameAccount,
                            employeeType = "staff",
                            firstName = ViewNhanVien.Staff.firstName,
                            lastName = ViewNhanVien.Staff.lastName,
                            phoneNumber = ViewNhanVien.Staff.phoneNumber,
                            sex = sexCurrent,
                            employeeImagePath = ViewNhanVien.Staff.employeeImagePath
                        });

                        DataProvider.Ins.DB.accounts.Add(new account
                        {
                            nameAccount = ViewNhanVien.Staff.nameAccount,
                            passwordAccount = ViewNhanVien.Password,
                            typeAccount = "staff"
                        });

                        DataProvider.Ins.DB.SaveChanges();
                        ViewNhanVien.Staff.idEmployee = code;
                        ViewNhanVien.Staff.sex = sexCurrent;
                        ViewNhanVien.FullName = ViewNhanVien.Staff.lastName + " " + ViewNhanVien.Staff.lastName;
                        ViewNhanVien.Position = "Nhân Viên";
                        if (string.IsNullOrEmpty(ViewNhanVien.Staff.employeeImagePath))
                        {
                            ViewNhanVien.Staff.employeeImagePath = @"~\..\pictures\noImage.png";
                        }
                        backupListEmployees.Add(ViewNhanVien);
                    }

                    (p as System.Windows.Controls.DataGrid).Items.Refresh();
            });

            // check sex
            CheckedSexMaleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewNhanVien.isNu = false;
            });

            // check sex
            CheckedSexFemaleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewNhanVien.isNam = false;
            });

            // kiểm tra tài khoản tồn tại
            TextChangedNameAccountCommand = new RelayCommand<PackIcon>((p) => { return true; }, (p) =>
            {
                var checkAcc = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == ViewNhanVien.Staff.nameAccount).FirstOrDefault();
                if (checkAcc != null && IsEdit==false)
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
            // load tất cả nhân viên
            ListEmployees = new ObservableCollection<ShowNhanVien>();
            var staffs = DataProvider.Ins.DB.employees.ToList();
            foreach (var staff in staffs)
            {
                ShowNhanVien newStaff = new ShowNhanVien();
                newStaff.Staff = staff;
                newStaff.FullName = staff.lastName + " " + staff.firstName;
                if (staff.employeeType == "admin")
                {
                    newStaff.Position = "Chủ Cửa Hàng";
                }
                else
                {
                    newStaff.Position = "Nhân Viên";
                }
                if (staff.sex.ToLower() == "nam")
                {
                    newStaff.isNam = true;
                    newStaff.isNu = false;
                }
                else
                {
                    newStaff.isNam = false;
                    newStaff.isNu = true;
                }
                newStaff.Password = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == staff.nameAccount).FirstOrDefault().passwordAccount;
                try
                {
                    var uri = new Uri(staff.employeeImagePath);
                    if (uri != null)
                        newStaff.SourceImage = new BitmapImage(uri);
                    ShowUploadButton = Visibility.Collapsed;
                }
                catch
                {
                    ShowUploadButton = Visibility.Visible;
                }
                ListEmployees.Add(newStaff);
            }
            backupListEmployees = ListEmployees;

            // load list nhân viên 
            ListPosition = new List<string>()
            {
                "Chủ Cửa Hàng",
                "Nhân Viên"
            };

            // load list giới tính
            ListSex = new List<string>()
            {
                "Nam",
                "Nu"
            };


            // title
            Title = "Danh Sách Nhân Viên";

            // ẩn grid filter
            IsFilter = Visibility.Collapsed;

            // set màu cho nút filter
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
        }

        //filter
        private void Filter()
        {
            List<ShowNhanVien> newListNhanVien = backupListEmployees.ToList();
            if (DisplaySex != null && DisplaySex != "")
            {
                newListNhanVien = newListNhanVien.Where(x => x.Staff.sex == DisplaySex).ToList();
            }

            if (DisplayPosition != null && DisplayPosition != "")
            {
                newListNhanVien = newListNhanVien.Where(x => x.Position == DisplayPosition).ToList();
            }

            ListEmployees = new ObservableCollection<ShowNhanVien>(newListNhanVien);
        }
    }
}
