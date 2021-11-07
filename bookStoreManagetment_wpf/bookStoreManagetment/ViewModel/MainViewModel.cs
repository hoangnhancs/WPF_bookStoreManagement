﻿using bookStoreManagetment.Model;
using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public string IDUser { get; set; }
        public ICommand LoadedMainWindowCommand { get; set; }
        public ICommand ClosedMainWindowCommand { get; set; }
        public ICommand AccountMainWindowCommand { get; set; }
        public ICommand DashboardClickCommand { get; set; }

        public MainViewModel()
        {
            // người đăng nhập hiện tại
            IDUser = "null";

            // hàm load form khác
            LoadedMainWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                // hiện form login
                CheckItemsWindow newCheckItems = new CheckItemsWindow();
                newCheckItems.ShowDialog();
            });

            //// hàm load form
            //LoadedMainWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    // ẩn form chính
            //    p.Hide();

            //    // hiện form login
            //    LoginWindow newLogin = new LoginWindow();
            //    newLogin.ShowDialog();

            //    // lấy dữ liệu từ form login
            //    var loginVM = newLogin.DataContext as LoginViewModel;
            //    if (loginVM.IsLogin)
            //    {
            //        p.Show();
            //        IDUser = loginVM.IDUser.ToString();
            //    }
            //    if (loginVM.IsClose)
            //    {
            //        p.Close();
            //    }
            //});

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

            // Hàm mở form dashboard
            DashboardClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CheckItemsWindow checkItemsForm = new CheckItemsWindow();
                object checkItemsContent = checkItemsForm.Content;
                checkItemsForm.Content = null;
                (p as Grid).Children.Add(checkItemsContent as UIElement);
            });
        }
    }
}