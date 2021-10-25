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

namespace WPF_bookStoreManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> collaped_btn = new List<Button> { };
        public MainWindow()
        {

            InitializeComponent();
            setupVarible();
        }
        void setupVarible()
        {
            collaped_btn.Add(btnBH);
            collaped_btn.Add(btnQLSP);
            collaped_btn.Add(btnQLNV);
            collaped_btn.Add(btnQLKH);
            collaped_btn.Add(btnQLRP);
            collaped_btn.Add(btnQLMail);
            collaped_btn.Add(btnCDC);
            collaped_btn.Add(btnQLSK);
            collaped_btn.Add(btnQLTC);
        }
        void check_collapsed_stackpanel(Button btn, StackPanel stp, List<Button> lstptn)
        {
            if (lstptn.Contains(btn))
            {
                stp.Visibility = Visibility.Visible;
                lstptn.Remove(btn);
            }
            else
            {
                stp.Visibility = Visibility.Collapsed;
                lstptn.Add(btn);
            }
        }
        private void btnBanhang_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnBH, sub_stpBH, collaped_btn);
        }

        private void btnQLSP_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLSP, sub_stpQLSP, collaped_btn);
        }

        private void btnQLNV_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLNV, sub_stpQLNV, collaped_btn);
        }

        private void btnQLKH_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLKH, sub_stpQLKH, collaped_btn);
        }

        private void btnQLTC_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLTC, sub_stpQLTC, collaped_btn);
        }

        private void btnQLSK_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLSK, sub_stpQLSK, collaped_btn);
        }

        private void btnQLRP_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLRP, sub_stpQLRP, collaped_btn);
        }

        private void btnQLMail_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnQLMail, sub_stpQLMail, collaped_btn);
        }

        private void btnCDC_Click(object sender, RoutedEventArgs e)
        {
            check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnCloseMenu.Visibility = Visibility.Visible;
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
        }
    }
}
