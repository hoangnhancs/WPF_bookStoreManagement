using bookStoreManagetment.Model;
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
            //using (var db = new bookStoreManagementEntities())
            //{
            //    var select = from s in db.items select s;
            //    foreach (var data in select)
            //    {
            //        Console.WriteLine("ID:{0}", data.typeItem);
            //        Console.WriteLine("Name:{0}", data.typeItem);

            //    }
            //    var n = DataProvider.Ins.DB.items.Select((u) => u.typeItem).ToList().Distinct().ToList();
            //    Console.WriteLine("____________________________________________________________________");
            //    Console.WriteLine(string.Join(",", n));
            //}
        }

        private void btnQLMail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQLKH_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
