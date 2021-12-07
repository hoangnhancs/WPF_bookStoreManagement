using bookStoreManagetment.Model;
using GridViewExport.Behaviors;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        public string MaPhieu { get; set; }
        public string LoaiPhieu { get; set; }
        public DateTime NgayGhiNhan { get; set; }
        public string DoiTuong { get; set; }
        public string HinhThucThanhToan { get; set; }
        public string TenPhieu { get; set; }
    }
    public class DSThuChiViewModel:BaseViewModel
    {
        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }

        // tổng hợp
        private Summary _Report;
        public Summary Report { get => _Report; set { _Report = value; OnPropertyChanged(); } }

        // danh sách tất cả nhân viên
        private List<string> _GroupType;
        public List<string> GroupType { get => _GroupType; set { _GroupType = value; OnPropertyChanged(); } }

        // danh sách loại phiếu
        private List<string> _TypeSheet;
        public List<string> TypeSheet { get => _TypeSheet; set { _TypeSheet = value; OnPropertyChanged(); } }

        // danh sách loại phiếu
        private List<string> _TypePayment;
        public List<string> TypePayment { get => _TypePayment; set { _TypePayment = value; OnPropertyChanged(); } }

        // nhân viên đang được chọn
        private string _DisplayGroupType;
        public string DisplayGroupType { get => _DisplayGroupType; set { _DisplayGroupType = value; OnPropertyChanged(); } }

        // ngày bắt đầu
        private string _displayBeginDay;
        public string displayBeginDay { get => _displayBeginDay; set { _displayBeginDay = value; OnPropertyChanged(); } }

        // ngày kết thúc
        private string _displayEndDay;
        public string displayEndDay { get => _displayEndDay; set { _displayEndDay = value; OnPropertyChanged(); } }

        // ngày kết thúc
        private string _DisplaySheet;
        public string DisplaySheet { get => _DisplaySheet; set { _DisplaySheet = value; OnPropertyChanged(); } }

        // ngày kết thúc
        private string _DisplayPayment;
        public string DisplayPayment { get => _DisplayPayment; set { _DisplayPayment = value; OnPropertyChanged(); } }


        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }

        // danh sách tất cả đơn nhập xuất
        private ObservableCollection<Sheet> backupListSheet;
        private ObservableCollection<Sheet> _ListSheet;
        public ObservableCollection<Sheet> ListSheet { get => _ListSheet; set { _ListSheet = value; OnPropertyChanged(); } }

        // list commnad
        public ICommand LoadedUserControlCommand { get; set; }
        public ICommand SelectionChangedTypeSheetCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand ClearFilterDayCommand { get; set; }
        public ICommand SearchFilterDayCommand { get; set; }
        public ICommand SelectedDateChangedEndDayCommand { get; set; }
        public ICommand ExportFileCommand { get; set; }
        public DSThuChiViewModel()
        {
            // load form
            LoadedUserControlCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // xuất file 
            ExportFileCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DataGridExport.ExportDataGrid(p);
            });

            // filter ngày bắt đầu
            ClearFilterDayCommand = new RelayCommand<object>(
                (p) => {
                    if (displayBeginDay != null)
                    {
                        return true;
                    }
                    if (displayEndDay != null)
                    {
                        string currentDay = DateTime.Now.ToString();
                        if (displayEndDay.Split(' ')[0] != currentDay.Split(' ')[0])
                            return true;
                    }
                    return false;
                }, (p) =>
                {
                    displayEndDay = DateTime.Now.ToString();
                    displayBeginDay = null;
                    ListSheet = backupListSheet;
                    Report = Calc_Report();
                });

            // filter ngày bắt đầu
            SearchFilterDayCommand = new RelayCommand<object>(
                (p) => {
                if ((displayBeginDay == null || displayBeginDay == "") || (displayEndDay == null || displayEndDay == ""))
                    {
                        return false;
                    }
                    return true;
                }, (p) =>
                {
                    if (displayBeginDay == null || displayBeginDay == "")
                    {
                        Filter();
                    }
                    else
                    {
                        ObservableCollection<Sheet> newListSheet = new ObservableCollection<Sheet>();
                        foreach (var sheet in ListSheet)
                        {
                            if (DateTime.Compare(DateTime.ParseExact(sheet.ProfitSummary.day.Date.ToString().Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture), DateTime.ParseExact(displayBeginDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) >= 0
                                && DateTime.Compare(DateTime.ParseExact(sheet.ProfitSummary.day.Date.ToString().Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture), DateTime.ParseExact(displayEndDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) <= 0)
                            {
                                newListSheet.Add(sheet);
                            }
                        }
                        ListSheet = newListSheet;
                    }
                    Report = Calc_Report();
                });

            // filter 
            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (DisplaySheet != null || DisplayGroupType != null || DisplayPayment != null)
                    return true;
                return false;
            }, (p) =>
            {
                Filter();
                Report = Calc_Report();
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d75c1e");
            });

            // xoá filter
            DeleteFilterCommand = new RelayCommand<object>((p) => {
                if (DisplaySheet != null || DisplayGroupType != null || DisplayPayment != null)
                    return true;
                return false;
            }, (p) =>
            {
                DisplaySheet = "";
                DisplaySheet = null;
                DisplayPayment = "";
                DisplayPayment = null;
                DisplayGroupType = "";
                DisplayGroupType = null;
                ListSheet = backupListSheet;
                Report = Calc_Report();
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // đóng filter grid
            CloseFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
            });

            // đóng filter grid
            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
            });

            // sự kiện thay đổi lựa chọn loại phiếu thu
            SelectionChangedTypeSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DisplaySheet == "Thu")
                {
                    GroupType = new List<string> {
                        "Khách Hàng"
                    };
                    DisplayGroupType = "Khách Hàng";
                }
                else
                {
                    List<string> newGroupType = new List<string>();
                    foreach(var profit in DataProvider.Ins.DB.profitSummaries.ToList())
                    {
                        if (newGroupType.Contains(profit.typeGroup) == false)
                        {
                            newGroupType.Add(profit.typeGroup);
                        }
                    }
                    GroupType = newGroupType;
                }
                
            });
        }

        private void LoadData()
        {
            // load quỹ
            Summary newReport = new Summary();
            var sheets = DataProvider.Ins.DB.profitSummaries.ToList();
            if (sheets.Count > 0)
            {
                if (sheets[0].billType.ToLower() == "import")
                {
                    newReport.OldBudget = (int)(sheets[0].budget + sheets[0].rootPrice);
                }
                else
                {
                    newReport.OldBudget = (int)(sheets[0].budget - sheets[0].rootPrice);
                }
                newReport.Budget = (int)sheets[sheets.Count - 1].budget;
                newReport.Earned = 0;
                newReport.Paid = 0;

                // load tất cả danh sách phiếu
                ListSheet = new ObservableCollection<Sheet>();
                foreach (var sheet in sheets)
                {
                    Sheet newSheet = new Sheet();
                    newSheet.ProfitSummary = sheet;
                    newSheet.MaPhieu = sheet.billCode;
                    newSheet.NgayGhiNhan = sheet.day;
                    newSheet.DoiTuong = sheet.typeGroup;
                    newSheet.HinhThucThanhToan = sheet.payment;
                    newSheet.TenPhieu = sheet.nameBill;
                    newSheet.MaChungTu = "-";

                    if (sheet.billType.ToLower() == "export")
                    {
                        newSheet.LoaiPhieu = "Thu";
                        newReport.Earned += sheet.rootPrice;
                        newSheet.ImportPrice = sheet.rootPrice.ToString();
                        newSheet.ExportPrice = "-";
                    }
                    else
                    {
                        newSheet.LoaiPhieu = "Chi";
                        newReport.Paid += sheet.rootPrice;
                        newSheet.ExportPrice = sheet.rootPrice.ToString();
                        newSheet.ImportPrice = "-";
                    }

                    ListSheet.Add(newSheet);
                }
                backupListSheet = ListSheet;

            }
            
            Report = new Summary();
            Report = newReport;

            // load danh sách loại phiếu
            TypeSheet = new List<string> { 
                "Thu",
                "Chi"
            };

            // load danh hình thức thanh toán
            TypePayment = new List<string> {
                "Tiền Mặt",
                "Thẻ"
            };

            // load ngày end 
            displayEndDay = DateTime.Now.ToString();

            // ẩn filter
            IsFilter = Visibility.Collapsed;

        }

        // hàm sắp xếp
        private void Filter()
        {
            string query = "";
            List<Sheet> newListSheet = backupListSheet.ToList();
            if (DisplaySheet != null && DisplaySheet != "")
            {
                if (DisplaySheet == "Thu")
                {
                    query = "export";
                }
                else
                {
                    query = "import";
                }
                newListSheet = newListSheet.Where(x => x.ProfitSummary.billType.ToLower() == query).ToList();
            }

            if (DisplayGroupType != null && DisplayGroupType != "" && query == "import")
            {
                newListSheet = newListSheet.Where(x => x.ProfitSummary.typeGroup.ToLower() == DisplayGroupType.ToLower()).ToList();
            }

            if (DisplayPayment != null && DisplayPayment != "")
            {
                newListSheet = newListSheet.Where(x => x.ProfitSummary.payment.ToLower() == DisplayPayment.ToLower()).ToList();
            }

            ListSheet = new ObservableCollection<Sheet>(newListSheet);
        
        }

        private Summary Calc_Report()
        {
            // load quỹ
            Summary newReport = new Summary();
            if (ListSheet.Count > 0)
            {
                if (ListSheet[0].ProfitSummary.billType.ToLower() == "export")
                {
                    newReport.OldBudget = (int)(ListSheet[0].ProfitSummary.budget - ListSheet[0].ProfitSummary.rootPrice);
                }
                else
                {
                    newReport.OldBudget = (int)(ListSheet[0].ProfitSummary.budget + ListSheet[0].ProfitSummary.rootPrice);
                }
                newReport.Earned = 0;
                newReport.Paid = 0;

                foreach (var sheet in ListSheet)
                {
                    if (sheet.ProfitSummary.billType.ToLower() == "export")
                    {
                        newReport.Earned += sheet.ProfitSummary.rootPrice;
                    }
                    else
                    {
                        newReport.Paid += sheet.ProfitSummary.rootPrice;
                    }
                }

                newReport.Budget = newReport.OldBudget + newReport.Earned - newReport.Paid;
            }
            return newReport;
        }
    }
}
