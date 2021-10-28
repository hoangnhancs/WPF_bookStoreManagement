using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bookStoreManagetment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoginWindow newLogin = new LoginWindow();
            newLogin.ShowDialog();
        }

        private void btnHidden_Click(object sender, RoutedEventArgs e)
        {
            btnHidden.Visibility = Visibility.Collapsed;
            btnOpen.Visibility = Visibility.Visible;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            btnHidden.Visibility = Visibility.Visible;
            btnOpen.Visibility = Visibility.Collapsed;
        }
    }
}
