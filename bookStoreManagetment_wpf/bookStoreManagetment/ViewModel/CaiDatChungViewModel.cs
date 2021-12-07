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
using System.Windows.Media;

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
        // query tìm kiếm
        private string _Query;
        public string Query { get => _Query; set { _Query = value; OnPropertyChanged(); } }

        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }

        // kiểm tra xem là thêm hay là chỉnh sửa
        private bool isEdit { get; set; }

        // xem phiếu
        private bool _IsReadOnly;
        public bool IsReadOnly { get => _IsReadOnly; set { _IsReadOnly = value; OnPropertyChanged(); } }
        private bool _IsEnable;
        public bool IsEnable { get => _IsEnable; set { _IsEnable = value; OnPropertyChanged(); } }

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

        // ngày bắt đầu
        private string _displayBeginDay;
        public string displayBeginDay { get => _displayBeginDay; set { _displayBeginDay = value; OnPropertyChanged(); } }

        // ngày kết thúc
        private string _displayEndDay;
        public string displayEndDay { get => _displayEndDay; set { _displayEndDay = value; OnPropertyChanged(); } }

        // nhân viên đang được chọn
        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        // xem quy định
        private Rule _ViewRule;
        public Rule ViewRule { get => _ViewRule; set { _ViewRule = value; OnPropertyChanged(); } }

        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }
        // foreground
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }


        // danh sách tất cả quy định
        private ObservableCollection<Rule> backupListRules;
        private ObservableCollection<Rule> _ListRules;
        public ObservableCollection<Rule> ListRules { get => _ListRules; set { _ListRules = value; OnPropertyChanged(); } }

        private Visibility _ErrorPhanQuyen;
        public Visibility ErrorPhanQuyen { get => _ErrorPhanQuyen; set { _ErrorPhanQuyen = value; OnPropertyChanged(); } }

        // list commnad
        public ICommand LoadedCheckItemsCommand { get; set; }
        public ICommand ClickShowHideGridCommand { get; set; }
        public ICommand ShowSettingCommand { get; set; }
        public ICommand DeleteRuleCommand { get; set; }
        public ICommand AddSettingCommand { get; set; }
        public ICommand SaveSettingCommand { get; set; }
        public ICommand CheckSaveCommand { get; set; }
        public ICommand EditSettingCommand { get; set; }
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }
        public ICommand ExitAddRulesCommand { get; set; }

        public ICommand CheckPhanQuyenCommand { get; set; }

        public CaiDatChungViewModel()
        {
            // load form
            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
                
            });

            // load form
            ExitAddRulesCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                displayBeginDay = null;
                displayEndDay = null;
                DisplayStaff = null;
                ListRules = backupListRules;

                var bc = new BrushConverter();
                (p as Button).Background = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // textchanged tìm kiếm 
            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Query != "" && Query != null)
                {
                    ObservableCollection<Rule> newListRules = new ObservableCollection<Rule>();
                    foreach (var rule in backupListRules)
                    {
                        if (rule.Setting.nameSetting.ToLower().Contains(Query) || rule.Setting.idSetting.ToLower().Contains(Query))
                        {
                            newListRules.Add(rule);
                        }
                    }
                    ListRules = newListRules;
                }
                else
                {
                    Filter();
                }
            });


            // đóng filter grid
            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
            });

            // đóng filter grid
            CloseFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
            });


            // xoá filter
            DeleteFilterCommand = new RelayCommand<object>((p) => {
                if (DisplayStaff != null || displayEndDay != null || displayBeginDay != null)
                    return true;
                return false;
            }, (p) =>
            {
                displayBeginDay = null;
                displayEndDay = null;
                DisplayStaff = null;
                ListRules = backupListRules;

                var bc = new BrushConverter();
                (p as Button).Background = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // filter 
            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (displayEndDay != null && displayBeginDay != null)
                    return true;

                if (DisplayStaff != null)
                {
                    if (displayEndDay == null && displayBeginDay == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }, (p) =>
            {
                Filter();
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d75c1e");
            });

            // chỉnh sửa quy định
            EditSettingCommand = new RelayCommand<object>((p) => {
                if (Permission.ChinhSuaQuyDinh)
                {
                    ErrorPhanQuyen = Visibility.Collapsed;
                    return true;
                }
                ErrorPhanQuyen = Visibility.Visible;
                return false;
            }, (p) =>
            {
                IsReadOnly = false;
                IsEnable = true;
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
                (p) => { });

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
                    backupListRules.Add(ViewRule);
                }
                DataProvider.Ins.DB.SaveChanges();

                // reset filter
                DisplayStaff = null;
                displayBeginDay = null;
                displayEndDay = null;
                Query = null;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");

            });

            // xem quy định
            ShowSettingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ViewRule = (p as Rule);

                // chỉnh title
                Title = "Danh Sách Quy Định > " + ViewRule.Setting.idSetting;

                // ẩn nút lưu
                IsSaveRule = Visibility.Collapsed;

                IsReadOnly = true;
                IsEnable = false;
            });

            // xem quy định
            AddSettingCommand = new RelayCommand<object>((p) => {

                if (Permission.ChinhSuaQuyDinh)
                {
                    ErrorPhanQuyen = Visibility.Collapsed;
                    return true;
                }
                ErrorPhanQuyen = Visibility.Visible;
                return false;
            }, (p) =>
            {
                IsReadOnly = false;
                IsEnable = true;

                // tìm id phù hợp
                int startID = backupListRules.Count;
                string codeSetting = "";
                do
                {
                    codeSetting = "QD" + getNextCode.getCode(startID);
                    var checkCode = DataProvider.Ins.DB.settings.Where(x => x.idSetting == codeSetting).FirstOrDefault();
                    if (checkCode == null)
                    {
                        break;
                    }
                    startID++;
                } while (true);


                Rule newViewRule = new Rule();
                var currentAcc = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();
                newViewRule.Setting = new setting()
                {
                    idSetting = codeSetting,
                    createdateSetting = DateTime.Now,
                    idEmployee = currentAcc.idEmployee,
                };
                newViewRule.FullNameEmployee = currentAcc.lastName + " " + currentAcc.firstName;
                ViewRule = newViewRule;

                // chỉnh title
                Title = "Danh Sách Quy Định > Thêm Quy Định";

                // reset
                displayBeginDay = null;
                displayEndDay = null;
                DisplayStaff = null;
                ListRules = backupListRules;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // xoá dữ liệu cài đặt
            DeleteRuleCommand = new RelayCommand<object>((p) => {
                if (Permission.ChinhSuaQuyDinh)
                {
                    ErrorPhanQuyen = Visibility.Collapsed;
                    return true;
                }
                ErrorPhanQuyen = Visibility.Visible;
                return false;
            }, (p) =>
            {
                // xác nhận xoá
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá quy định này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var delSetting = p as Rule;

                    // xoá khỏi database
                    foreach (var set in DataProvider.Ins.DB.settings.ToList())
                    {
                        if (set.idSetting == delSetting.Setting.idSetting)
                        {
                            DataProvider.Ins.DB.settings.Remove(set);
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();

                    // xoá khỏi list show
                    foreach (var set in ListRules.ToList())
                    {
                        if (set.Setting.idSetting == delSetting.Setting.idSetting)
                        {
                            ListRules.Remove(set);
                            backupListRules.Remove(set);
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
            //DisplayStaff = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault().lastName;
            var staffs = DataProvider.Ins.DB.employees.ToList();
            foreach (var staff in staffs)
            {
                AllStaff.Add(staff.lastName + " " + staff.firstName);
            }

            // load list quy định
            ListRules = new ObservableCollection<Rule>();
            foreach (var set in DataProvider.Ins.DB.settings.ToList())
            {
                Rule newRule = new Rule();
                newRule.Setting = new setting();
                newRule.Setting = set;
                var staff = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == set.idEmployee).FirstOrDefault();
                newRule.FullNameEmployee = staff.lastName + " " + staff.firstName;
                ListRules.Add(newRule);
            }
            backupListRules = ListRules;

            // ẩn filter
            IsFilter = Visibility.Collapsed;

            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");

        }

        private void Filter()
        {
            List<Rule> newListRules = backupListRules.ToList();
            if (DisplayStaff != null)
            {
                newListRules = newListRules.Where(x => x.FullNameEmployee == DisplayStaff).ToList();
            }

            if (displayBeginDay != null)
            {
                List<Rule> temp = new List<Rule>();
                foreach (var rule in newListRules)
                {
                    if (DateTime.Compare(rule.Setting.createdateSetting, DateTime.ParseExact(displayBeginDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) >= 0
                     && DateTime.Compare(rule.Setting.createdateSetting, DateTime.ParseExact(displayEndDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) <= 0)
                    {
                        temp.Add(rule);
                    }
                }
                newListRules = temp;
            }

            ListRules = new ObservableCollection<Rule>(newListRules);
        }
    }
}