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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using WPF_bookStoreManagement;

namespace WPF_bookStoreManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> collaped_btn = new List<Button> { };
        bool isMenuOpen = false;
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
            collaped_btn.Add(btnDashboard);
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
        void closeallMenu()
        {
            sub_stpBH.Visibility = Visibility.Collapsed;
            sub_stpQLSP.Visibility = Visibility.Collapsed;
            sub_stpQLNV.Visibility = Visibility.Collapsed;
            sub_stpQLKH.Visibility = Visibility.Collapsed;
            sub_stpQLRP.Visibility = Visibility.Collapsed;
            sub_stpQLSK.Visibility = Visibility.Collapsed;
            sub_stpQLMail.Visibility = Visibility.Collapsed;
            sub_stpQLTC.Visibility = Visibility.Collapsed;
            sub_stpCDC.Visibility = Visibility.Collapsed;
            collaped_btn = new List<Button>();
            setupVarible();
        }
        void zoomStackpanelMain()
        {
            if (isMenuOpen==true)
            {
                Storyboard s = (Storyboard)TryFindResource("ZoomOutStackpanelMain");
                s.Begin();
            }
            else
            {
                Storyboard s = (Storyboard)TryFindResource("ZoomInStackpanelMain");
                s.Begin();
            }
        }
        private void btnBanhang_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                check_collapsed_stackpanel(btnBH, sub_stpBH, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                check_collapsed_stackpanel(btnBH, sub_stpBH, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }

        }

        private void btnQLSP_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                check_collapsed_stackpanel(btnQLSP, sub_stpQLSP, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                check_collapsed_stackpanel(btnQLSP, sub_stpQLSP, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
                
        }

        private void btnQLNV_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                check_collapsed_stackpanel(btnQLNV, sub_stpQLNV, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                check_collapsed_stackpanel(btnQLNV, sub_stpQLNV, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnQLKH_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                check_collapsed_stackpanel(btnQLKH, sub_stpQLKH, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                check_collapsed_stackpanel(btnQLKH, sub_stpQLKH, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnQLTC_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                check_collapsed_stackpanel(btnQLTC, sub_stpQLTC, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                check_collapsed_stackpanel(btnQLTC, sub_stpQLTC, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnQLSK_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                //check_collapsed_stackpanel(btnQLSK, sub_stpQLSK, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                //check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnQLRP_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                //check_collapsed_stackpanel(btnQLRP, sub_stpQLRP, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                //check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnQLMail_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                //check_collapsed_stackpanel(btnQLMail, sub_stpQLMail, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            else
            {
                //check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnCDC_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                //collaped_btn.Add(btnCDC);
                check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();

            }
            else
            {
                //check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                zoomStackpanelMain();
            }
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == true)
            {
                btnOpenMenu.Visibility = Visibility.Visible;
                btnCloseMenu.Visibility = Visibility.Collapsed;
                closeallMenu();
                isMenuOpen = false;
                zoomStackpanelMain();
                //test_block.Text = collaped_btn.Count.ToString();
            }
            
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuOpen == false)
            {
                Storyboard s = (Storyboard)TryFindResource("MenuOpen");
                s.Begin();
                //collaped_btn.Add(btnDashboard);
                //check_collapsed_stackpanel(btnDashboard, stpDashboard, collaped_btn);
                btnOpenMenu.Visibility = Visibility.Collapsed;
                btnCloseMenu.Visibility = Visibility.Visible;
                isMenuOpen = true;
                //test_block.Text = collaped_btn.Count.ToString();
                zoomStackpanelMain();
            }
            else
            {
                //check_collapsed_stackpanel(btnCDC, sub_stpCDC, collaped_btn);
                //test_block.Text = collaped_btn.Count.ToString();
            }
        }
        private void chipAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNCC_Click(object sender, RoutedEventArgs e)
        {
            Nhacungcap nccWin = new Nhacungcap();
            stackpanelMain.Children.Clear();
            object content = nccWin.Content;
            nccWin.Content = null;
            nccWin.Close();
            this.stackpanelMain.Children.Add(content as UIElement);
        }
    }
}
