using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class Summary
    {
        public int OldBudget { get; set; }
        public int Earned { get; set; }
        public int Paid { get; set; }
        public int Budget { get; set; }
    }
    public class Sheet
    {
        public profitSummary ProfitSummary { get; set; }
        public string MaChungTu { get; set; }
        public string ExportPrice { get; set; }
        public string ImportPrice { get; set; }
    }
    public class DSThuChiViewModel:BaseViewModel
    {

        // tổng hợp
        private Summary _Report;
        public Summary Report { get => _Report; set { _Report = value; OnPropertyChanged(); } }


        // danh sách tất cả đơn nhập xuất
        private ObservableCollection<Sheet> _ListSheet;
        public ObservableCollection<Sheet> ListSheet { get => _ListSheet; set { _ListSheet = value; OnPropertyChanged(); } }

        // list commnad
        public ICommand LoadedUserControlCommand { get; set; }
        public DSThuChiViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });
        }

        private void LoadData()
        {
            // load quỹ
            var sheets = DataProvider.Ins.DB.profitSummaries.ToList();
            Summary newReport = new Summary();
            if (sheets[0].billType.ToLower() == "import")
            {
                newReport.OldBudget = (int)(sheets[0].budget - sheets[0].rootPrice);
            }
            else
            {
                newReport.OldBudget = (int)(sheets[0].budget + sheets[0].rootPrice);
            }
            newReport.Budget = (int)sheets[sheets.Count - 1].budget;
            newReport.Earned = 0;
            newReport.Paid = 0;

            // load tất cả danh sách phiếu
            ListSheet = new ObservableCollection<Sheet>();
            foreach(var sheet in sheets)
            {
                Sheet newSheet = new Sheet();
                newSheet.ProfitSummary = sheet;
                newSheet.MaChungTu = "-";

                if (sheet.billType.ToLower() == "import")
                {
                    newReport.Earned += sheet.rootPrice;
                    newSheet.ImportPrice = sheet.rootPrice.ToString();
                    newSheet.ExportPrice = "-";
                }
                else
                {
                    newReport.Paid += sheet.rootPrice;
                    newSheet.ExportPrice = sheet.rootPrice.ToString();
                    newSheet.ImportPrice = "-";
                }

                ListSheet.Add(newSheet);
            }

            Report = new Summary();
            Report = newReport;
        }
    }
}
