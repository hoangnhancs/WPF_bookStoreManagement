using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class NhacungcapViewMode : BaseViewModel
    {
        public ICommand LoadNhacungcapCommand { get; set; }
        public ICommand btnDeleteNCCClickCommand { get; set; }

        public NhacungcapViewMode()
        {
            LoadNhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                string connectionString = @"data source=DESKTOP-NMLT1AC\SQLEXPRESS;initial catalog = bookStoreManagementDTB;integrated security = True";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Select * from supplier", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmd.Dispose();
                adapter.Dispose();
                con.Close();
                (p as DataGrid).ItemsSource = dt.DefaultView;
            });
        }
    }
}
