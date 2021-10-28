using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public ICommand LoadedMainWindowCommand { get; set; }
        public MainViewModel()
        {

            
            LoadedMainWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                p.Hide();
                LoginWindow newLogin = new LoginWindow();
                newLogin.ShowDialog();

                var loginVM = newLogin.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    p.Show();
                    MessageBox.Show("Bạn đã đăng nhập thành công");
                }
                if (loginVM.IsClose)
                {
                    p.Close();
                }
            });

        }
    }
}
