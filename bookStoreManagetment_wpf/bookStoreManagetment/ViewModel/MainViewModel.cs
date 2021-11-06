using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace bookStoreManagetment.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadedMainWindowCommand { get; set; }
        public ICommand DashboardClickCommand { get; set; }
        public ICommand OpenSubMenuCommand { get; set; }
        public ICommand ChangeColorOpenedSTP { get; set; }
        public List<StackPanel> opensubstp = new List<StackPanel>();
        public List<Button> openbtn = new List<Button>();
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


            // is check box
            DashboardClickCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CheckItemsWindow checkItemsForm = new CheckItemsWindow();
                object checkItemsContent = checkItemsForm.Content;
                checkItemsForm.Content = null;
                (p as Grid).Children.Add(checkItemsContent as UIElement);
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
    }
}
