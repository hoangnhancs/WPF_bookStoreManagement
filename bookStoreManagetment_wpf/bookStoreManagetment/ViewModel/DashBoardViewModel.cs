using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using bookStoreManagetment.Model;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace bookStoreManagetment.ViewModel
{
    public class DashBoardViewModel : BaseViewModel
    {
        public ChartValues<turnoverinsevendays> Results { get; set; }
        public ObservableCollection<string> Labels { get; set; }
        public Func<double, string> MillionFormatter { get; set; }

        private ObservableCollection<TopProduct> _topproducts;
        public ObservableCollection<TopProduct> TopProducts { get => _topproducts; set { _topproducts = value; OnPropertyChanged(); } }

        private ObservableCollection<turnoverinsevendays> _revenues;
        public ObservableCollection<turnoverinsevendays> Revenues { get => _revenues; set { _revenues = value; OnPropertyChanged(); } }

        public List<string> soluong;
        public List<string> ID;

        public object Mapper { get; set; }

        private string _revenue;
        public string Revenue { get => _revenue; set { _revenue = value; OnPropertyChanged(); } }

        private string _neworder;
        public string NewOrder { get => _neworder; set { _neworder = value; OnPropertyChanged(); } }

        private string _return;
        public string Return { get => _return; set { _return = value; OnPropertyChanged(); } }

        private string _billpayment;
        public string BillPayment { get => _billpayment; set { _billpayment = value; OnPropertyChanged(); } }

        private string _nametop1;
        public string NameTop1 { get => _nametop1; set { _nametop1 = value; OnPropertyChanged(); } }

        public DashBoardViewModel()
        {
            // Doanh thu hàng này
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int years = DateTime.Now.Year;
            var revenue = DataProvider.Ins.DB.profitSummaries.Where(p => p.day.Day == day && p.day.Month == month && p.day.Year == years && p.billType == "export").Select(p => p.rootPrice).ToList();
            if (revenue != null)
            {
                Revenue = string.Format(new CultureInfo("vi-VN"), "{0:0,000 đ}", revenue.Sum());

            }
            else
            {
                Revenue = string.Format(new CultureInfo("vi-VN"), "{0:0, đ}", 0);
            }

            // đơn hàng mới
            NewOrder = DataProvider.Ins.DB.sellBills.Where(p => p.sellDate.Day == day && p.sellDate.Month == month && p.sellDate.Year == years).Count().ToString();

            //đơn trả
            Return = DataProvider.Ins.DB.khachtrahangs.Where(p => p.sellDate.Day == day && p.sellDate.Month == month && p.sellDate.Year == years).Count().ToString();

            // phiếu chi
            BillPayment = DataProvider.Ins.DB.profitSummaries.Where(p => p.day.Day == day && p.day.Month == month && p.day.Year == years && p.billType == "import").Count().ToString();

            var TopProduct = DataProvider.Ins.DB.sellBills.GroupBy(p => p.idItem).Select(pa => new { idItem = pa.Key, Sum = pa.Sum(s => s.number) }).OrderByDescending(c => c.Sum).Take(6);

            TopProducts = new ObservableCollection<TopProduct>();

            foreach (var data in TopProduct)
            {
                int gia = DataProvider.Ins.DB.items.Where(p => p.idItem == data.idItem).Select(p => p.sellPriceItem).FirstOrDefault();
                var cell = DataProvider.Ins.DB.items.Where(p => p.idItem == data.idItem).FirstOrDefault();

                ImageSource photo = null;
                try
                {
                        photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Image\\" + cell.imageItem));
                }
                catch
                {
                    try
                    {
                        photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Image\\" + cell.nameItem + ".jpg"));

                    }
                    catch
                    {
                        try
                        {
                            photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Image\\không có ảnh.jpg"));
                        }
                        catch
                        {

                        }

                    }
                }
                TopProducts.Add(new TopProduct()
                {
                    nameItem = cell.nameItem,
                    priceItem = string.Format(new CultureInfo("vi-VN"), "{0:0,0 đ}", gia),
                    Photo = photo
                });


            }

            Mapper = Mappers.Xy<turnoverinsevendays>()
                .X((p, index) => index)
                .Y(p => p.Revenue);

            var record = DataProvider.Ins.DB.profitSummaries.Where(p => p.billType == "export").GroupBy(p => p.day).Select(pa => new { Day = pa.Key, Sum = pa.Sum(s => s.rootPrice) }).OrderByDescending(c => c.Day).Take(7);
            Revenues = new ObservableCollection<turnoverinsevendays>();

            foreach (var data in record)
            {
                Revenues.Add(new turnoverinsevendays() { Day = data.Day.Day.ToString() + "-" + data.Day.Month.ToString(), Revenue = data.Sum });
                Console.WriteLine(data.Sum);

            }

            Revenues = new ObservableCollection<turnoverinsevendays>(Revenues.Reverse());

            Results = Revenues.AsChartValues();
            Labels = new ObservableCollection<string>(Revenues.Select(x => x.Day));

            MillionFormatter = value => (value).ToString("N") + "";



        }
    }

    public class TopProduct
    {
        public string nameItem { get; set; }
        public string priceItem { get; set; }
        public ImageSource Photo { get; set; }
    }

    public class turnoverinsevendays
    {
        public string Day { get; set; }
        public int Revenue { get; set; }
    }
}
