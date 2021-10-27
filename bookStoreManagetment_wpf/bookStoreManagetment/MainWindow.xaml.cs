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
        private Button btnIsClicked = new Button();
        private StackPanel spnIsVisiable = new StackPanel();
        public MainWindow()
        {
            InitializeComponent();
            btnIsClicked = null;
            spnIsVisiable = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnHidden_Click(object sender, RoutedEventArgs e)
        {
            stackPanelFeatures.Visibility = (stackPanelFeatures.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }

        //    private void clickButtonEffect(Button btn, StackPanel stkPanel = null)
        //    {
        //        // tắt màu
        //        if (btnIsClicked != null)
        //        {
        //            btnIsClicked.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        //        }

        //        // tắt panel đang mở 
        //        if (spnIsVisiable != null)
        //        {
        //            spnIsVisiable.Visibility = Visibility.Collapsed;
        //        }

        //        btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        //        btnIsClicked = btn;

        //        if (stkPanel != null)
        //        {
        //            if (stkPanel != spnIsVisiable)
        //            {
        //                stkPanel.Visibility = Visibility.Visible;
        //                spnIsVisiable = stkPanel;
        //            }
        //            else
        //            {
        //                btnIsClicked.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        //                btnIsClicked = null;
        //                spnIsVisiable = null;
        //            }
        //        }
        //        else
        //        {
        //            spnIsVisiable = null;
        //        }
        //    }

        //    private void btnQLVatPham_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn, stackPanelSubMenuQLVP);
        //    }

        //    private void btnBanVatPham_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn, stackPanelSubMenuBanVP);
        //    }

        //    private void btnQLNhanVien_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn, stackPanelSubMenuQLNV);
        //    }

        //    private void btnQLKhachHang_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn, stackPanelSubMenuQLKH);
        //    }

        //    private void btnQLThuChi_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn, stackPanelSubMenuQLThuChi);
        //    }

        //    private void btnDashboard_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn);
        //    }

        //    private void btnQLBaoCao_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn);
        //    }

        //    private void btnQLSuKien_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn);
        //    }

        //    private void btnQLMail_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn);
        //    }

        //    private void btnCaiDatChung_Click(object sender, RoutedEventArgs e)
        //    {
        //        Button btn = sender as Button;
        //        clickButtonEffect(btn);
        //    }
    }
}
