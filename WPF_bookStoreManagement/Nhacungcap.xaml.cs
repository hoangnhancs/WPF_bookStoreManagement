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
using System.Windows.Shapes;
using WPF_bookStoreManagement.DAO;
using WPF_bookStoreManagement.DTO;

namespace WPF_bookStoreManagement
{
    /// <summary>
    /// Interaction logic for Nhacungcap.xaml
    /// </summary>
    public partial class Nhacungcap : Window
    {
        public Nhacungcap()
        {
            InitializeComponent();
            fillDataintoDataGrid();
        }
        void fillDataintoDataGrid()
        {
            List<NhacungcapDTO> lstNcc = NhacungcapDAO.Instance.getallNhacungcap();
            lstViewNhacungcap.ItemsSource = lstNcc;
            
        }

        private void btnDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            //string s = lstViewNhacungcap.SelectedItem.ToString();
            //test_label.Content = s;
            //Console.WriteLine(s);
        }
    }
}
