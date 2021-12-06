using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using bookStoreManagetment.Model;
using MaterialDesignThemes.Wpf;

namespace bookStoreManagetment.ViewModel
{
    public class ShowInforStaff : BaseViewModel
    {
        public employee Staff { get; set; }
        public string Password { get; set; }
        private string _PasswordChar;
        public string PasswordChar { get => _PasswordChar; set { _PasswordChar = value; OnPropertyChanged(); } }
        private string _FullName;
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }
        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }
        private ImageSource _SourceImage;
        public ImageSource SourceImage { get => _SourceImage; set { _SourceImage = value; OnPropertyChanged(); } }
    }

    public class ThongTinNhanVienViewModel :BaseViewModel
    {
        // ẩn hiện grid filter
        private ShowInforStaff _ViewEmployee;
        public ShowInforStaff ViewEmployee { get => _ViewEmployee; set { _ViewEmployee = value; OnPropertyChanged(); } }

        // title 
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        public bool AccountFail { get; set; }

        // command
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand CloseGridPasswordCommand { get; set; }
        public ICommand SavePasswordCommand { get; set; }
        public ICommand TextChangedNameAccountCommand { get; set; }
        public ICommand UploadImageNVCommand { get; set; }

        public ThongTinNhanVienViewModel()
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
                    ViewEmployee.SourceImage = null;
                    var bit = new BitmapImage(new Uri(op.FileName));
                    ViewEmployee.SourceImage = bit;
                    ViewEmployee.Staff.employeeImagePath = op.FileName;
                }
            });

            // kiểm tra tài khoản tồn tại
            TextChangedNameAccountCommand = new RelayCommand<PackIcon>((p) => { return true; }, (p) =>
            {
                var checkAcc = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == ViewEmployee.Staff.nameAccount).FirstOrDefault();
                if (checkAcc != null)
                {
                    (p as PackIcon).Visibility = Visibility.Visible;
                    AccountFail = true;
                }
                else
                {
                    (p as PackIcon).Visibility = Visibility.Collapsed;
                    AccountFail = false;
                }
            });

            // load form
            SavePasswordCommand = new RelayCommand<object>((p) => {
                if (ViewEmployee == null)
                    return false;
                if (AccountFail)
                    return false;
                if (ViewEmployee.Position == null || ViewEmployee.Staff.employeeEmail == null || ViewEmployee.Staff.nameAccount == null || ViewEmployee.Password == null || ViewEmployee.Staff.phoneNumber == null)
                    return false;
                return true; 
            }, (p) =>
            {
                var currentEmployee = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == ViewEmployee.Staff.idEmployee).FirstOrDefault();
                currentEmployee = ViewEmployee.Staff;

                var currentAcc = DataProvider.Ins.DB.accounts.Where(x => x.nameAccount == ViewEmployee.Staff.nameAccount).FirstOrDefault();
                currentAcc.nameAccount = ViewEmployee.Staff.nameAccount;
                currentAcc.passwordAccount = ViewEmployee.Password;
                DataProvider.Ins.DB.SaveChanges();
                System.Windows.MessageBox.Show("Thay đổi thông tin thành công");
            });

        }

        // hàm bổ trợ
        private void LoadData()
        {
            var newViewEmployee = new ShowInforStaff();
            newViewEmployee.Staff = new employee();
            newViewEmployee.Staff = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();
            newViewEmployee.FullName = newViewEmployee.Staff.lastName + " " + newViewEmployee.Staff.firstName;
            newViewEmployee.Password = LoggedAccount.Account.passwordAccount;
            newViewEmployee.PasswordChar = "";
            for(int i = 0; i<newViewEmployee.Password.Length; i++)
            {
                newViewEmployee.PasswordChar += "*";
            }

            if (newViewEmployee.Staff.employeeType == "admin")
            {
                newViewEmployee.Position = "Chủ Cửa Hàng";
            }
            else
            {
                newViewEmployee.Position = "Nhân Viên";
            }
            try
            {
                var uri = new Uri(newViewEmployee.Staff.employeeImagePath);
                if (uri != null)
                    newViewEmployee.SourceImage = new BitmapImage(uri);
            }
            catch
            {
                
            }
            ViewEmployee = newViewEmployee;

            Title = "Xin Chào" + ViewEmployee.FullName;
        }
    }
}
