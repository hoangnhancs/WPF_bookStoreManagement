using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace bookStoreManagetment.UserControls
{
    /// <summary>
    /// Interaction logic for NhacungcapUC.xaml
    /// </summary>
    public partial class NhacungcapUC : UserControl
    {
        public NhacungcapUC()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inventory dataRowView = (Inventory)((Button)e.Source).DataContext;
                string id = dataRowView.Supplier.idSupplier;
                MessageBox.Show("You Clicked : " + id);
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
