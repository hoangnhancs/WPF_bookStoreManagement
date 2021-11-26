using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{ 
    public class Rule : BaseViewModel
    {
        public setting Setting { get; set; }
        public string FullNameEmployee { get; set; }
        public bool stateStatus { get; set; }
    }

    public class CaiDatChungViewModel : BaseViewModel
    {
        // kiểm tra xem là thêm hay là chỉnh sửa
        private bool isEdit{ get; set;}

        // ẩn hiện các nút khi chuyển grid
        private Visibility _isAddRule;
        public Visibility IsAddRule { get => _isAddRule; set { _isAddRule = value; OnPropertyChanged(); } }

        // ẩn hiện các nút khi chuyển grid
        private Visibility _IsSaveRule;
        public Visibility IsSaveRule { get => _IsSaveRule; set { _IsSaveRule = value; OnPropertyChanged(); } }

        // danh sách tất cả nhân viên
        private List<string> _AllStaff;
        public List<string> AllStaff { get => _AllStaff; set { _AllStaff = value; OnPropertyChanged(); } }

        // nhân viên đang được chọn
        private string _displayStaff;
        public string DisplayStaff { get => _displayStaff; set { _displayStaff = value; OnPropertyChanged(); } }

        // nhân viên đang được chọn
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        // xem quy định
        private Rule _ViewRule;
        public Rule ViewRule { get => _ViewRule; set { _ViewRule = value; OnPropertyChanged(); } }

        // danh sách tất cả quy định
        private ObservableCollection<Rule> _ListRules;
        public ObservableCollection<Rule> ListRules { get => _ListRules; set { _ListRules = value; OnPropertyChanged(); } }

        // list commnad
        public ICommand LoadedCheckItemsCommand { get; set; }
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand ShowSettingCommand { get; set; }
        public ICommand DeleteRuleCommand { get; set; }
        public ICommand AddSettingCommand { get; set; }
        public ICommand SaveSettingCommand { get; set; }
        public ICommand CheckSaveCommand { get; set; }
        public ICommand EditSettingCommand { get; set; }

        public CaiDatChungViewModel()
        {
            // load form
            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });

            // chỉnh sửa quy định
            EditSettingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewRule = p as Rule;
                isEdit = true;
            });

            // kiểm tra điều kiện lưu
            CheckSaveCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (ViewRule == null)
                    {
                        return false;
                    }
                    if (ViewRule.FullNameEmployee == null || ViewRule.Setting.idSetting == null || ViewRule.Setting.createdateSetting == null || ViewRule.Setting.nameSetting == null
                        || ViewRule.Setting.contentSetting == null)
                    {
                        return false;
                    }
                    if (ViewRule.FullNameEmployee == "" || ViewRule.Setting.idSetting == "" || ViewRule.Setting.nameSetting == ""
                        || ViewRule.Setting.contentSetting == "")
                    {
                        return false;
                    }
                    return true;
                }, 
                (p) =>{});

            // lưu quy định
            SaveSettingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (isEdit)
                {
                    // tìm và thay đổi giá trị trong database
                    var editSetting = DataProvider.Ins.DB.settings.Where(x => x.idSetting == ViewRule.Setting.idSetting).FirstOrDefault();
                    editSetting = ViewRule.Setting;
                    isEdit = false;
                }
                else
                {
                    // thêm vào database
                    DataProvider.Ins.DB.settings.Add(ViewRule.Setting);
                    // thêm vào show list
                    ListRules.Add(ViewRule);
                }
                DataProvider.Ins.DB.SaveChanges();
            });

            // xem quy định
            ShowSettingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewRule = (p as Rule);

                // chỉnh title
                Title = "Danh Sách Quy Định > " + ViewRule.Setting.idSetting;

                // ẩn nút lưu
                IsSaveRule = Visibility.Collapsed;
            });

            // xem quy định
            AddSettingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Rule newViewRule = new Rule();
                var currentAcc = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();
                newViewRule.Setting = new setting()
                {
                    idSetting = "QD"+getNextCode.getCode(ListRules.Count),
                    createdateSetting = DateTime.Now,
                    employee = currentAcc,
                    idEmployee = currentAcc.idEmployee,
                };
                newViewRule.FullNameEmployee = currentAcc.firstName +" "+ currentAcc.lastName;
                ViewRule = newViewRule;

                // chỉnh title
                Title = "Danh Sách Quy Định > Thêm Quy Định";
            });

            // xoá dữ liệu cài đặt
            DeleteRuleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // xác nhận xoá
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá phiếu thu này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var delSetting = p as Rule;

                    // xoá khỏi database
                    foreach(var set in DataProvider.Ins.DB.settings.ToList())
                    {
                        if (set.idSetting == delSetting.Setting.idSetting)
                        {
                            DataProvider.Ins.DB.settings.Remove(set);
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();

                    // xoá khỏi list show
                    foreach(var set in ListRules.ToList())
                    {
                        if (set.Setting.idEmployee == delSetting.Setting.idEmployee)
                        {
                            ListRules.Remove(set);
                        }
                    }
                }
            });


            // ẩn hiện grid
            ClickShowHideGridCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridListRule")
                    {
                        IsAddRule = Visibility.Visible;
                        Title = "Danh Sách Quy Định";
                    }
                    else
                    {
                        IsAddRule = Visibility.Collapsed;
                        IsSaveRule = Visibility.Visible;
                    }
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            });
        }

        private void LoadData()
        {
            // load title
            Title = "Danh Sách Quy Định";

            // load tất cả nhân viên
            AllStaff = new List<string>();
            DisplayStaff = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault().lastName;
            var staffs = DataProvider.Ins.DB.employees.ToList();
            foreach (var staff in staffs)
            {
                AllStaff.Add(staff.firstName+ " " + staff.lastName);
            }

            // load list quy định
            ListRules = new ObservableCollection<Rule>();
            foreach(var set in DataProvider.Ins.DB.settings.ToList())
            {
                Rule newRule = new Rule();
                newRule.Setting = new setting();
                newRule.Setting = set;
                var staff = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == set.idEmployee).FirstOrDefault();
                newRule.FullNameEmployee = staff.firstName + " " + staff.lastName;
                ListRules.Add(newRule);
            }
        }
    }
}
