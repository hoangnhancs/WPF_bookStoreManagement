using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            int revenue = DataProvider.Ins.DB.profitSummaries.Where(p => p.day.Day == 1 && p.billType == "export").Select(p => p.rootPrice).Sum();
            Revenue = string.Format(new CultureInfo("vi-VN"), "{0:0,0 đ}", revenue);

            NewOrder = DataProvider.Ins.DB.sellBills.Where(p => p.sellDate.Day == 11).Count().ToString();

            Return = DataProvider.Ins.DB.khachtrahangs.Count().ToString();

            BillPayment = DataProvider.Ins.DB.profitSummaries.Where(p => p.day.Day == 1 && p.billType == "export").Count().ToString();

            var TopProduct = DataProvider.Ins.DB.sellBills.GroupBy(p => p.idItem).Select(pa => new { idItem = pa.Key, Sum = pa.Sum(s => s.number) }).OrderByDescending(c => c.Sum).Take(5);

            //NameTop1 = DataProvider.Ins.DB.items.Where(p => p.idItem == TopProduct.Select(pa => pa.idItem).Take(2).FirstOrDefault()).Select(p => p.nameItem).FirstOrDefault();

            TopProducts = new ObservableCollection<TopProduct>();

            foreach (var data in TopProduct)
            {
                int gia = DataProvider.Ins.DB.items.Where(p => p.idItem == data.idItem).Select(p => p.sellPriceItem).FirstOrDefault();
                TopProducts.Add(new TopProduct() { nameItem = DataProvider.Ins.DB.items.Where(p => p.idItem == data.idItem).Select(p => p.nameItem).FirstOrDefault(), priceItem = string.Format(new CultureInfo("vi-VN"), "{0:0,0 đ}", gia) });
            }



            //Console.WriteLine(TopProduct.Select(pa => pa.idItem).Take(1).FirstOrDefault().ToString());

            Mapper = Mappers.Xy<turnoverinsevendays>()
                .X((p, index) => index)
                .Y(p => p.Revenue);

            //lets take the first 15 records by default;
            var records = DataProvider.Ins.DB.profitSummaries.GroupBy(p => p.day).Select(pa => new { Day = pa.Key, Sum = pa.Sum(s => s.rootPrice) }).OrderByDescending(c => c.Day).Take(7);

            Revenues = new ObservableCollection<turnoverinsevendays>();

            foreach (var data in records)
            {
                Revenues.Add(new turnoverinsevendays() { Day = data.Day.Day.ToString() + "-" + data.Day.Month.ToString(), Revenue = data.Sum });
            }

            Revenues = new ObservableCollection<turnoverinsevendays>(Revenues.Reverse());

            Results = Revenues.AsChartValues();
            Labels = new ObservableCollection<string>(Revenues.Select(x => x.Day));

            MillionFormatter = value => (value).ToString("N") + "";

            //Revenue = string.Format(“{ 0:0,0 vnđ}”, 20000000); //kết quả sẽ là 20,000,000 vnđ.



        }
    }

    public class TopProduct
    {
        public string nameItem { get; set; }
        public string priceItem { get; set; }
    }

    public class turnoverinsevendays
    {
        public string Day { get; set; }
        public int Revenue { get; set; }
    }
}
