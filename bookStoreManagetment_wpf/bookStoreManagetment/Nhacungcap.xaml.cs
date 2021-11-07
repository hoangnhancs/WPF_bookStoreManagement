using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace bookStoreManagetment
{
    /// <summary>
    /// Interaction logic for Nhacungcap.xaml
    /// </summary>
    public partial class Nhacungcap : Window
    {
        public Nhacungcap()
        {
            InitializeComponent();
            //Load();
        }

        public class supplier
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string phoneNumber { get; set; }
            public string Email { get; set; }
            public string Status { get; set; }

        }

        void Load()
        {
            string connectionString = @"data source=DESKTOP-NMLT1AC\SQLEXPRESS;initial catalog = bookStoreManagementDTB;integrated security = True";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from supplier", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmd.Dispose();
            adapter.Dispose();
            con.Close();
            dgNhacungcap.ItemsSource = dt.DefaultView;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
                String ID = dataRowView[0].ToString();
                String Name = dataRowView[1].ToString();
                MessageBox.Show("You Clicked : " + ID + "\r\nName : " + Name);
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            */
        }
    }
}
