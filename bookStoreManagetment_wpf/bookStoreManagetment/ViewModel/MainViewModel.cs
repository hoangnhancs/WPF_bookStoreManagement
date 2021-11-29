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

        public string IDUser { get; set; }
        public ICommand LoadedMainWindowCommand { get; set; }
        public ICommand LoadedDashBoardCommand { get; set; }
        public ICommand ClosedMainWindowCommand { get; set; }
        public ICommand AccountMainWindowCommand { get; set; }
        public ICommand DashboardClickCommand { get; set; }
        public ICommand KiemhangClickCommand { get; set; }
        public ICommand NhacungcapClickCommand { get; set; }
        public ICommand QuanlyMailCommand { get; set; }
        public ICommand OpenSubMenuCommand { get; set; }
        public ICommand ChangeColorOpenedSTP { get; set; }

        public ICommand DashBoardClickCommand { get; set; }
        public ICommand ListofProductsClickCommand { get; set; }
        public ICommand ImportGoodsClickCommand { get; set; }

        public ICommand openPhieuThuUCCommand { get; set; }
        public ICommand openPhieuChiUCCommand { get; set; }
        public ICommand openDSThuChiUCCommand { get; set; }
        public ICommand openCaiDatChungUCCommand { get; set; }

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

            // hàm load form
            LoadedMainWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
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
                }
            });

            // load Dash Board 
            LoadedDashBoardCommand = new RelayCommand<Grid>((p) => { return true; }, (p) => {
                AddChildUC(p, new DashBoardUC());
            });

            // hàm đóng form => đảm bảo không có form con nào còn mở
            ClosedMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                    App.Current.Windows[intCounter].Close();
            });

            // hàm check đăng nhập thành công => đổi account
            AccountMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IDUser != "null")
                {
                    var user = DataProvider.Ins.DB.accounts
                                                  .Where(acc => (acc.idAccount.ToString() == IDUser))
                                                  .FirstOrDefault();
                    string nameAccount = user.nameAccount.ToString();
                    (p as Chip).Content = nameAccount;
                    (p as Chip).Icon = (nameAccount[0]).ToString().ToUpper();
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
                    var brush = (Brush)converter.ConvertFromString("#0000EE");
                    (p as Button).Background = brush;
                    openbtn.Add((p as Button));
                }
                else
                {
                    var brush = System.Windows.Media.Brushes.Transparent;
                    (p as Button).Background = brush;
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
    }
}