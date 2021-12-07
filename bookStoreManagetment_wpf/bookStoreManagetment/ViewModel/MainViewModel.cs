using bookStoreManagetment.Model;
using bookStoreManagetment.UserControls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace bookStoreManagetment.ViewModel
{
    public class MainViewModel : BaseViewModel
    { 
        private UserControl _ChildUserControl;
        public UserControl ChildUserControl { get => _ChildUserControl; set { _ChildUserControl = value; OnPropertyChanged(); } }
        private object _selectedViewModel;
        public object SelectedViewModel { get => _selectedViewModel; set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); } }

        // ẩn hiện grid filter
        private Visibility _VisibilityGridPassword;
        public Visibility VisibilityGridPassword { get => _VisibilityGridPassword; set { _VisibilityGridPassword = value; OnPropertyChanged(); } }

        // ẩn hiện grid filter
        private string _DisplayPassword;
        public string DisplayPassword { get => _DisplayPassword; set { _DisplayPassword = value; OnPropertyChanged(); } }

        // ẩn hiện grid filter
        private Visibility _IsLogin;
        public Visibility IsLogin { get => _IsLogin; set { _IsLogin = value; OnPropertyChanged(); } }

        // button đã mở
        public Button ButtonClicked { get; set; }
        // button đã mở
        public Button ButtonIconClicked { get; set; }

        public string IDUser { get; set; }
        public ICommand LoadedMainWindowCommand { get; set; }
        public ICommand LoadedDashBoardCommand { get; set; }
        public ICommand ClosedMainWindowCommand { get; set; }
        public ICommand AccountMainWindowCommand { get; set; }
        public ICommand DashboardClickCommand { get; set; }
        public ICommand KiemhangClickCommand { get; set; }
        public ICommand NhacungcapClickCommand { get; set; }
        public ICommand QuanlyMailCommand { get; set; }

        public ICommand KhachtrahangCommand { get; set; }

        public ICommand DSHoaDonClickCommand { get; set; }

        public ICommand OpenSubMenuCommand { get; set; }
        public ICommand ChangeColorOpenedSTP { get; set; }

        public ICommand DashBoardClickCommand { get; set; }
        public ICommand ListofProductsClickCommand { get; set; }
        public ICommand ImportGoodsClickCommand { get; set; }

        public ICommand openPhieuThuUCCommand { get; set; }
        public ICommand openPhieuChiUCCommand { get; set; }
        public ICommand openDSThuChiUCCommand { get; set; }
        public ICommand openCaiDatChungUCCommand { get; set; }

        public ICommand ChangeColorButtonClickCommand { get; set; }
        public ICommand ChangeColorButtonIconClickCommand { get; set; }
        public ICommand ShowHideMenuCommand { get; set; }

        public ICommand ShowHideLoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand openDSNhanVienUCCommand { get; set; }
        public ICommand openDSKhachHangUCCommand { get; set; }
        public ICommand ViewInformationLogedAccountCommand { get; set; }
        public ICommand CloseCheckPasswordLogedAccountCommand { get; set; }
        public ICommand CheckPasswordLogedAccountCommand { get; set; }


        public List<StackPanel> opensubstp = new List<StackPanel>();
        public List<Button> openbtn = new List<Button>();
        public List<Window> openWindow = new List<Window>();
        public Window isOpenningWindow = new Window();
        public Inventory selectedEditInventory { get; set; }
        private ICommand _menucommand;
        public ICommand MenuCommand
        {
            get
            {
                if (_menucommand == null)
                {
                    _menucommand = new RelayCommand<object>((p) => { return true; }, (p) => SwitchViews(p));
                }
                return _menucommand;
            }
        }

        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "Nhacungcap":
                    SelectedViewModel = new NhacungcapViewMode();
                    break;
                case "AddNhacungcap":
                    SelectedViewModel = new AddSupplierViewModel();
                    break;
                case "EditNhacungcap":
                    SelectedViewModel = new EditSupplierViewModel();
                    break;

            }
        }
        public void passInvNCCtoMain(object obj)
        {
            selectedEditInventory = (obj as Inventory);
            MessageBox.Show(selectedEditInventory.Supplier.idSupplier);
        }

        public MainViewModel()
        {
            // người đăng nhập hiện tại
            IDUser = "null";

            // đóng ô kiểm tra pass
            CloseCheckPasswordLogedAccountCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VisibilityGridPassword = Visibility.Collapsed;
                DisplayPassword = null;
            });

            // kiểm tra pass
            CheckPasswordLogedAccountCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VisibilityGridPassword = Visibility.Visible;
                IsLogin = Visibility.Collapsed;
            });


            // hàm load form
            LoadedMainWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                RestartApp(p);
            });

            // hàm load form
            ViewInformationLogedAccountCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DisplayPassword == LoggedAccount.Account.passwordAccount)
                {
                    AddChildUC(p as Grid, new ThongTinNhanVienUC());
                    VisibilityGridPassword = Visibility.Collapsed;
                    DisplayPassword = null;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập chính xác mật khẩu");
                }
            });

            // Đăng xuất 
            LogoutCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                // xác nhận xoá
                MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    RestartApp(p);
                }
            });

            // chane color button command
            ShowHideLoginCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (IsLogin == Visibility.Visible)
                {
                    IsLogin = Visibility.Collapsed;
                }
                else
                    IsLogin = Visibility.Visible;
            });


            // chane color button command
            ShowHideMenuCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                var _grid = (p as Grid);
                if (_grid.Visibility == Visibility.Collapsed)
                {
                    _grid.Visibility = Visibility.Visible;
                }
                else
                {
                    _grid.Visibility = Visibility.Collapsed;
                }
            });

            // chane color button command
            ChangeColorButtonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                
                if (p != null)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    if (ButtonClicked != null)
                    {
                        ButtonClicked.Foreground = (Brush)converter.ConvertFromString("#FF495B67");
                    }
                    (p as Button).Foreground = (Brush)converter.ConvertFromString("#1e98d7");
                    ButtonClicked = (p as Button);
                }            
            });

            // chane color button command
            ChangeColorButtonIconClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {

                if (p != null)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    if (ButtonIconClicked != null)
                    {
                        ButtonIconClicked.Foreground = (Brush)converter.ConvertFromString("#FF495B67");
                    }
                    var check = (p as Button).Name;
                    (p as Button).Foreground = (Brush)converter.ConvertFromString("#1e98d7");
                    ButtonIconClicked = (p as Button);
                }
            });

            // load Dash Board 
            LoadedDashBoardCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                AddChildUC(p, new DashBoardUC());
            });

            // hàm đóng form => đảm bảo không có form con nào còn mở
            ClosedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                App.Current.Shutdown();
            });
            
            // hàm check đăng nhập thành công => đổi account
            AccountMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IDUser != "null")
                {
                    var user = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();
                    var listLastname = user.lastName.Split(' ');
                    string nameAccount =  listLastname[listLastname.Length-1] + " " + user.firstName ;
                    (p as Chip).Content = nameAccount;
                    (p as Chip).Icon = (user.firstName[0]).ToString().ToUpper();
                }
            });

            DashBoardClickCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                
                AddChildUC(p, new DashBoardUC());

            });

            // danh sách sản phẩm
            ListofProductsClickCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                AddChildUC(p, new ListofProductUC());
            });

            ImportGoodsClickCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                AddChildUC(p, new ImportGoodsUC());
            });


            // is check box
            KiemhangClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new CheckItemsUC());
            });

            // mở phiêu thu usercontrols
            openPhieuThuUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new PhieuThuUC());
            });

            // mở ds thu chi usercontrols
            openDSThuChiUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new DSThuChiUC());
            });

            // mở phieu chi usercontrols
            openPhieuChiUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new PhieuChiUC());
            });

            // mở Cài đặt chung usercontrols
            openCaiDatChungUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new CaiDatChungUC());
            });

            NhacungcapClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new NhacungcapUC());
            });

            QuanlyMailCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new mailUC());
            });

            KhachtrahangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddChildUC(p as Grid, new KhachtrahangUC());
            });

            DSHoaDonClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new DSHoaDonUC());

            });

            openDSNhanVienUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new DSNhanVien());

            });

            openDSKhachHangUCCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AddChildUC(p as Grid, new DSKhachHangUCxaml());

            });

            OpenSubMenuCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (!opensubstp.Contains((p as StackPanel)))
                {
                    (p as StackPanel).Visibility = Visibility.Visible;
                    opensubstp.Add(p as StackPanel);
                }
                else
                {
                    (p as StackPanel).Visibility = Visibility.Collapsed;
                    opensubstp.Remove((p as StackPanel));
                }
            });

            ChangeColorOpenedSTP = new RelayCommand<object>((p) => { return true; }, (p) =>
            {   if (!openbtn.Contains((p as Button)))
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#d75c1e");
                    (p as Button).Foreground = brush;
                    openbtn.Add((p as Button));
                }
                else
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    (p as Button).Foreground = (Brush)converter.ConvertFromString("#FF495B67");
                    openbtn.Remove((p as Button));
                }
            });

        }


        // hàm thêm user control con vào grid
        public void AddChildUC(Grid p, UserControl childUC)
        {
            ChildUserControl = childUC;
            p.Children.Clear();
            p.Children.Add(ChildUserControl);
        }

        // restart
        private void RestartApp(Window p)
        {
            // ẩn checkpass
            VisibilityGridPassword = Visibility.Collapsed;
            DisplayPassword = null;

            // ẩn login grid
            IsLogin = Visibility.Collapsed;

            // ẩn form chính
            p.Hide();

            // hiện form login
            LoginWindow newLogin = new LoginWindow();
            newLogin.ShowDialog();

            // lấy dữ liệu từ form login
            var loginVM = newLogin.DataContext as LoginViewModel;
            if (loginVM.IsLogin)
            {
                p.Show();
                IDUser = loginVM.IDUser.ToString();
            }
            if (loginVM.IsClose)
            {
                p.Close();
                App.Current.Shutdown();
            }

        }
    }
}