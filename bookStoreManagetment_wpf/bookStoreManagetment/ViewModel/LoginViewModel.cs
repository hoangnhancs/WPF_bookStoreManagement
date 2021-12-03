using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {   public bool IsLogin { get; set; }
        public bool IsClose { get; set; }

        public String User { get; set; }
        public String Password { get; set; }
        public int IDUser { get; set; }

        public ICommand CheckedCommand { get; set; }
        public ICommand UnCheckedCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LogindCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand UserChangedCommand { get; set; }
        public LoginViewModel()
        {
            IDUser = -1;
            IsClose = false;
            IsLogin = false;
            Password = "";
            // is check box
            CheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                TextBox txtBoxPassword = (p as TextBox);
                txtBoxPassword.Text = Password;
                txtBoxPassword.Visibility = Visibility.Visible;
            });
            UnCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                TextBox txtBoxPassword = (p as TextBox);
                txtBoxPassword.Visibility = Visibility.Collapsed;
            });

            // Change character user/password
            PasswordChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if ((p as PasswordBox) != null)
                    Password = (p as PasswordBox).Password;
                else
                    Password = (p as TextBox).Text;
            });
            UserChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                var _user = (p as TextBox);
                if (_user != null)
                    User = _user.Text;
            });

            // click login
            LogindCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {

                // query to database to find list account 
                var lstAccount = DataProvider.Ins.DB.accounts
                                                    .Where(acc => 
                                                                (acc.nameAccount == User) && 
                                                                (acc.passwordAccount == Password))
                                                    .ToList();
                // check has account in dtb
                if (lstAccount.Count > 0)
                {
                    IDUser = lstAccount[0].idAccount;
                    LoggedAccount.Account = lstAccount[0];
                    Permission.createPermission();
                    IsLogin = true;
                    if (p != null)
                        p.Close();
                }  
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng tài khoản/ mật khẩu");
                }
            });

            // exit form
            ExitCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (!IsLogin)
                {
                    IsClose = true;
                }
                if (p != null)
                    p.Close();
            });
        }   
    }
}
