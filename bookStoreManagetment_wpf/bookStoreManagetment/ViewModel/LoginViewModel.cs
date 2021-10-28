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

        public ICommand CheckedCommand { get; set; }
        public ICommand UnCheckedCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LogindCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand UserChangedCommand { get; set; }
        public LoginViewModel()
        {
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
                User = (p as TextBox).Text;
            });

            // click login
            LogindCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IsLogin = true;
                if (p != null)
                    p.Close();
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
