using bookStoreManagetment.Model;
using bookStoreManagetment.UserControls;
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
    public class CellItems
    {
        public item Items { get; set; }
        public bool IsSelected { get; set; }
    }

    public class CheckItemsViewModel : BaseViewModel
    {

        private bool _finish;
        public bool Finish { get => _finish; set { _finish = value; OnPropertyChanged(); } }

        private bool _isReadOnly;
        public bool isReadOnly { get => _isReadOnly; set { _isReadOnly = value; OnPropertyChanged(); } }

        private Visibility _State;
        public Visibility State { get => _State; set { _State = value; OnPropertyChanged(); } }

        private List<int> _NumItems;
        public List<int> NumItems { get => _NumItems; set { _NumItems = value; OnPropertyChanged(); } }

        private int _DisplayNumOfPages;
        public int DisplayNumOfPages { get => _DisplayNumOfPages; set { _DisplayNumOfPages = value; OnPropertyChanged(); } }

        private string _Title;
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private ObservableCollection<CheckItemSheet> _backupListCheckSheet;
        public ObservableCollection<CheckItemSheet> BackupListCheckSheet { get => _backupListCheckSheet; set { _backupListCheckSheet = value; OnPropertyChanged(); } }
        private ObservableCollection<CheckItemSheet> _ListCheckSheets;
        public ObservableCollection<CheckItemSheet> ListCheckSheets { get => _ListCheckSheets; set { _ListCheckSheets = value; OnPropertyChanged(); } }

        private List<string> _AllStaff;
        public List<string> AllStaff { get => _AllStaff; set { _AllStaff = value; OnPropertyChanged(); } }


        private string _DisplayMaPhieu;
        public string DisplayMaPhieu { get => _DisplayMaPhieu; set { _DisplayMaPhieu = value; OnPropertyChanged(); } }

        private string _DisplayNhanVien;
        public string DisplayNhanVien { get => _DisplayNhanVien; set { _DisplayNhanVien = value; OnPropertyChanged(); } }

        private string _DisplayNgay;
        public string DisplayNgay { get => _DisplayNgay; set { _DisplayNgay = value; OnPropertyChanged(); } }

        private List<string> _MaPhieu;
        public List<string> MaPhieu { get => _MaPhieu; set { _MaPhieu = value; OnPropertyChanged(); } }

        // filter
        // ngày bắt đầu
        private string _displayBeginDay;
        public string displayBeginDay { get => _displayBeginDay; set { _displayBeginDay = value; OnPropertyChanged(); } }
        // ngày kết thúc
        private string _displayEndDay;
        public string displayEndDay { get => _displayEndDay; set { _displayEndDay = value; OnPropertyChanged(); } }
        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }
        // foreground
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }
        // query tìm kiếm
        private string _Query;
        public string Query { get => _Query; set { _Query = value; OnPropertyChanged(); } }
        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }

        private List<CellItems> _backupAllItems;
        public List<CellItems> BackupAllItems { get => _backupAllItems; set { _backupAllItems = value; OnPropertyChanged(); } }
        private List<CellItems> _ShowItems;
        public List<CellItems> ShowItems { get => _ShowItems; set { _ShowItems = value; OnPropertyChanged(); } }

        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }

        public ICommand LoadedCheckItemsCommand { get; set; }
        public ICommand ClickHiddenCommand { get; set; }
        public ICommand txtBoxTextChangedSearchCommand { get; set; }
        public ICommand CheckedGridSearchCommand { get; set; }
        public ICommand UnCheckedGridSearchCommand { get; set; }
        public ICommand ClickAllSelectedCommand { get; set; }
        public ICommand ClickAllUnSelectedCommand { get; set; }
        public ICommand ClickCompletedCommand { get; set; }
        public ICommand ClickAddCheckSheetCommand { get; set; }
        public ICommand ClickSearchCheckSheetsCommand { get; set; }
        public ICommand clearAllFielSearchCommand { get; set; }
        public ICommand ViewCheckSheetCommand { get; set; }
        public ICommand DeleteCheckSheetCommand { get; set; }
        public ICommand LoadAllItemsCommand { get; set; }
        public ICommand checkBtnAddCheckSheet { get; set; }

        //filter
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }
        #region "page"
        //Page Property
        private ObservableCollection<CheckItemSheet> _DivInventoryList;
        public ObservableCollection<CheckItemSheet> DivInventoryList { get => _DivInventoryList; set { _DivInventoryList = value; OnPropertyChanged(); } }

        private Visibility _3cham1Visible;
        public Visibility Bacham1Visible
        {
            get { return _3cham1Visible; }
            set
            {
                _3cham1Visible = value;
                OnPropertyChanged();
            }
        }
        private Visibility _3cham2Visible;
        public Visibility Bacham2Visible
        {
            get { return _3cham2Visible; }
            set
            {
                _3cham2Visible = value;
                OnPropertyChanged();
            }
        }
        public int maxpage { get; set; }
        public int max_pack_page { get; set; }
        public int pack_page { get; set; }
        public int currentpage = 1;
        private string _numRowEachPageTextBox;
        public string NumRowEachPageTextBox
        {
            get { return _numRowEachPageTextBox; }
            set
            {
                _numRowEachPageTextBox = value;
                OnPropertyChanged();
            }
        }
        public int NumRowEachPage;
        private page btnPage1;
        public page BtnPage1
        {
            get { return btnPage1; }
            set
            {
                btnPage1 = value;
                OnPropertyChanged();
            }
        }
        private page btnPage2;
        public page BtnPage2
        {
            get { return btnPage2; }
            set
            {
                btnPage2 = value;
                OnPropertyChanged();
            }
        }
        private page btnPage3;
        public page BtnPage3
        {
            get { return btnPage3; }
            set
            {
                btnPage3 = value;
                OnPropertyChanged();
            }
        }

        private bool _leftVisi;
        public bool LeftVisi
        {
            get { return _leftVisi; }
            set
            {
                _leftVisi = value;
                OnPropertyChanged();
            }
        }
        private bool _rightVisi;
        public bool RightVisi
        {
            get { return _rightVisi; }
            set
            {
                _rightVisi = value;
                OnPropertyChanged();
            }
        }

        public ICommand tbNumRowEachPageCommand { get; set; }
        public ICommand btnNextClickCommand { get; set; }
        public ICommand btnendPageCommand { get; set; }
        public ICommand btnfirstPageCommand { get; set; }
        public ICommand btnPrevPageCommand { get; set; }
        public ICommand btnLoc2Command { get; set; }

        //Page Property
        #endregion //here  
        public CheckItemsViewModel()
        {

            Title = "Danh Sách Phiếu Kiểm Hàng";

            InventoryList = new ObservableCollection<Inventory>();

            // load form
            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
                //NumRowEachPageTextBox = "5";
                //NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
                //currentpage = 1;
                //pack_page = 1;
                //settingButtonNextPrev();
            });

            // textchanged tìm kiếm 
            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Query != "" && Query != null)
                {
                    ObservableCollection<CheckItemSheet> newListCheckSheet = new ObservableCollection<CheckItemSheet>();
                    foreach (var checkSheet in ListCheckSheets)
                    {
                        if (checkSheet.codeCheckItem.ToLower().Contains(Query))
                        {
                            newListCheckSheet.Add(checkSheet);
                        }
                    }
                    ListCheckSheets = newListCheckSheet;
                }
                else
                {
                    Filter();
                }

            });

            // filter
            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (displayEndDay != null && displayBeginDay != null)
                    return true;
                if (DisplayNhanVien != null)
                    return true;
                return false;
            }, (p) =>
            {
                Filter();
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#FF008000");
                ForegroudFilter = (Brush)bc.ConvertFromString("#DDFFFFFF");
                //settingButtonNextPrev();
            });

            // đóng/mở filter grid
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
                if (DisplayNhanVien != null || displayEndDay != null || displayBeginDay != null)
                    return true;
                return false;
            }, (p) =>
            {
                displayBeginDay = null;
                displayEndDay = null;
                DisplayNhanVien = "";
                DisplayNhanVien = null;
                Query = "";
                ListCheckSheets = BackupListCheckSheet;

                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
            });

            // kiểm tra điều kiện nút thêm sản phẩm
            checkBtnAddCheckSheet = new RelayCommand<object>(
                (p) => {
                    if (InventoryList.Count == 0 || DisplayNhanVien == null)
                    {
                        return false;
                    }

                    foreach (var inventory in InventoryList)
                    {
                        if (inventory.NewQuantity == 0)
                        {
                            return false;
                        }
                    }

                    return true;
                },
                (p) =>
                {

                });

            // load tất cả sản phẩm
            LoadAllItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // gán nv bằng nv đăng nhập
                var currentEmployee = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();
                DisplayNhanVien = currentEmployee.lastName + " " + currentEmployee.firstName;

                // load tất cả sản phẩm
                BackupAllItems = new List<CellItems>();
                var listItems = DataProvider.Ins.DB.items;
                foreach (var item in listItems)
                {
                    CellItems newCell = new CellItems();
                    newCell.Items = item;
                    newCell.IsSelected = false;

                    BackupAllItems.Add(newCell);
                }
                ShowItems = new List<CellItems>();
                ShowItems = BackupAllItems;

                var currentAcc = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault();
                DisplayNhanVien = currentAcc.lastName + " " + currentAcc.firstName;
                DisplayNgay = null;
                DisplayMaPhieu = null;
                Note = null;
                InventoryList.Clear();
                ListCheckSheets = BackupListCheckSheet;

            });

            // xoá checksheet
            DeleteCheckSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var temp = p as CheckItemSheet;

                ListCheckSheets.Remove(temp);

                var deleteItem = DataProvider.Ins.DB.checkItems.Where(x => x.idCheckItems == temp.codeCheckItem).ToList();

                foreach (var del in deleteItem)
                {
                    DataProvider.Ins.DB.checkItems.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }

            });

            // chỉnh sửa checksheet
            ViewCheckSheetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var temp = p as CheckItemSheet;
                DisplayNhanVien = temp.nameEmployee;
                InventoryList.Clear();
                var listItem = DataProvider.Ins.DB.checkItems.Where(x => x.idCheckItems == temp.codeCheckItem).ToList();
                foreach (var infor in listItem)
                {
                    InventoryList.Add(new Inventory()
                    {
                        Item = DataProvider.Ins.DB.items.Where(x => x.idItem == infor.idItem).FirstOrDefault(),
                        NewQuantity = (int)infor.newQuantityItem,
                        OldQuantity = (int)infor.oldQuantityItem
                    });
                }
                Note = listItem[0].note;
                State = Visibility.Collapsed;
                isReadOnly = false;
                Title = "Danh Sách Phiếu Kiểm Hàng > " + temp.codeCheckItem;
            });

            // xoá tất cả trường tiềm kiếm
            clearAllFielSearchCommand = new RelayCommand<object>(
                (p) => {
                    if (DisplayNhanVien != null || DisplayNgay != null || DisplayMaPhieu != null)
                        return true;
                    return false;
                },
                (p) =>
                {
                    DisplayNhanVien = null;
                    DisplayNgay = null;
                    DisplayMaPhieu = null;
                    ListCheckSheets = BackupListCheckSheet;
                });


            // ẩn hiện form
            ClickHiddenCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                isReadOnly = true;
                State = Visibility.Visible;

                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                    if (grid.Name == "gridAddReport")
                    {
                        Title = "Danh Sách Phiếu Kiểm Hàng > Kiểm Hàng";
                    }
                    else if (grid.Name == "gridAddItems")
                        Title = "Danh Sách Phiếu Kiểm Hàng > Kiểm Hàng > Thêm Sản Phẩm";
                    else
                    {
                        Title = "Danh Sách Phiếu Kiểm Hàng";

                        // reset filter
                        displayBeginDay = null;
                        displayEndDay = null;
                        DisplayNhanVien = "";
                        DisplayNhanVien = null;

                        var bc = new BrushConverter();
                        BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                        ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");

                        Query = "";
                    }           
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            });


            // thay đổi text tìm kiếm 
            txtBoxTextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text.Trim().ToLower();
                    if (query == "")
                    {
                        ShowItems = new List<CellItems>();
                        ShowItems = BackupAllItems;
                    }
                    else
                    {
                        ShowItems = new List<CellItems>();
                        foreach (var cellItems in BackupAllItems)
                        {
                            string nameItem = cellItems.Items.nameItem.Trim().ToLower();

                            if (nameItem.Contains(query))
                            {
                                ShowItems.Add(cellItems);
                            }
                        }
                    }
                }
                //settingButtonNextPrev();
            });

            // chọn sản phẩm 
            CheckedGridSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var selected = (p as CellItems);
                    selected.IsSelected = true;

                    for (int i = 0; i < BackupAllItems.Count; i++)
                    {
                        if (BackupAllItems[i].Items == selected.Items)
                            BackupAllItems[i].IsSelected = true;
                    }
                }
            });


            // bỏ chọn sản phẩm 
            UnCheckedGridSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var selected = (p as CellItems);
                    selected.IsSelected = false;

                    for (int i = 0; i < BackupAllItems.Count; i++)
                    {
                        if (BackupAllItems[i].Items == selected.Items)
                            BackupAllItems[i].IsSelected = false;
                    }
                }
            });

            // chọn tất cả
            ClickAllSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < BackupAllItems.Count; i++)
                {
                    BackupAllItems[i].IsSelected = true;
                }
                ShowItems = new List<CellItems>();
                ShowItems = BackupAllItems;

                (p as DataGrid).Items.Refresh();
            });

            // bỏ chọn tất cả
            ClickAllUnSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < BackupAllItems.Count; i++)
                {
                    BackupAllItems[i].IsSelected = false;
                }
                ShowItems = new List<CellItems>();
                ShowItems = BackupAllItems;

                (p as DataGrid).Items.Refresh();
            });

            // tìm kiếm phiếu kiểm hàng
            ClickSearchCheckSheetsCommand = new RelayCommand<object>(
            (p) => {
                if (DisplayNgay != null || DisplayNhanVien != null || DisplayMaPhieu != null)
                {
                    return true;
                }
                return false;
            },
            (p) =>
            {
                List<CheckItemSheet> newListCheckSheet = BackupListCheckSheet.ToList();
                if (DisplayMaPhieu != null)
                {
                    newListCheckSheet = newListCheckSheet.Where(x => x.codeCheckItem == DisplayMaPhieu).ToList();
                }

                if (DisplayNhanVien != null)
                {
                    newListCheckSheet = newListCheckSheet.Where(x => x.nameEmployee == DisplayNhanVien).ToList();
                }

                if (DisplayNgay != null)
                {
                    newListCheckSheet = newListCheckSheet.Where(x => x.dateCheckItems.ToString().Split(' ')[0] == DisplayNgay.Split(' ')[0]).ToList();
                }

                ListCheckSheets = new ObservableCollection<CheckItemSheet>(newListCheckSheet);
            });

            // hoàn thành thêm sản phẩm
            ClickCompletedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                ObservableCollection<Inventory> temp = new ObservableCollection<Inventory>();
                foreach (var cellItems in BackupAllItems)
                {
                    if (cellItems.IsSelected)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Item = cellItems.Items;
                        _Inventory.OldQuantity = cellItems.Items.quantity;
                        int i = checkHasSanPham(cellItems.Items.idItem);
                        if (i != -1)
                        {
                            _Inventory.NewQuantity = InventoryList[i].NewQuantity;
                        }
                        else
                        {
                            _Inventory.NewQuantity = DataProvider.Ins.DB.items.Where(x => x.idItem == cellItems.Items.idItem).FirstOrDefault().quantity;
                        }
                        temp.Add(_Inventory);
                    }
                }

                InventoryList.Clear();
                InventoryList = temp;

            });

            // hoàn thành thêm phiếu kiểm hàng
            ClickAddCheckSheetCommand = new RelayCommand<object>(
                (p) => {
                    return true;
                },
                (p) =>
                {
                    int i = 0;
                    // tìm id phù hợp
                    int startID = BackupListCheckSheet.Count;
                    string code = "";
                    do
                    {
                        code = "CK" + getNextCode.getCode(startID);
                        var checkCode = DataProvider.Ins.DB.checkItems.Where(x => x.idCheckItems == code).FirstOrDefault();
                        if (checkCode == null)
                        {
                            break;
                        }
                        startID++;
                    } while (true);
                    var listDisplay = DisplayNhanVien.Split(' ');
                    var firstName = listDisplay[listDisplay.Length - 1];
                    var lastName = listDisplay[0] ;
                    for(int c = 0; c < listDisplay.Length - 1; c++)
                    {
                        if (c > 0)
                        {
                            lastName += " " + listDisplay[c];
                        }
                    }

                    foreach (var inventory in InventoryList)
                    { 
                        checkItem temp = new checkItem()
                        {
                            note = Note,
                            idCheckItems = code,
                            dateCheckItems = DateTime.Now,
                            idEmployee = DataProvider.Ins.DB.employees.Where(x => x.lastName == lastName &&
                                                                                  x.firstName == firstName
                                                                                ).FirstOrDefault().idEmployee,
                            idItem = inventory.Item.idItem,
                            oldQuantityItem = inventory.OldQuantity,
                            newQuantityItem = inventory.NewQuantity
                        };
                        var currentItem = DataProvider.Ins.DB.items.Where(x => x.idItem == inventory.Item.idItem).FirstOrDefault();
                        currentItem.quantity = inventory.NewQuantity;

                        DataProvider.Ins.DB.checkItems.Add(temp);
                        DataProvider.Ins.DB.SaveChanges();

                        var _currentItem = DataProvider.Ins.DB.items.Where(x => x.idItem == inventory.Item.idItem).FirstOrDefault();

                        addCheckSheet(temp);
                        i++;
                    }
                    InventoryList.Clear();
                    ShowItems.Clear();
                });
            //#region "select num row each page"
            //tbNumRowEachPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    currentpage = 1;
            //    //LoadData();
            //    Filter();
            //    settingButtonNextPrev();
            //});
            //btnNextClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    if (currentpage < maxpage)
            //    {
            //        currentpage += 1;
            //        if (currentpage % 3 == 0)
            //            pack_page = currentpage / 3;
            //        else
            //            pack_page = Convert.ToInt32(currentpage / 3) + 1;
            //        //MessageBox.Show("Max page is" + maxpage.ToString()+"pack_page is"+pack_page.ToString());
            //    }
            //    settingButtonNextPrev();
            //});
            //btnendPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    currentpage = maxpage;
            //    pack_page = max_pack_page;
            //    settingButtonNextPrev();
            //});
            //btnfirstPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    currentpage = 1;
            //    pack_page = 1;
            //    settingButtonNextPrev();
            //});
            //btnPrevPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    if (currentpage > 1)
            //    {
            //        currentpage -= 1;
            //        if (currentpage % 3 == 0)
            //            pack_page = currentpage / 3;
            //        else
            //            pack_page = Convert.ToInt32(currentpage / 3) + 1;
            //        //MessageBox.Show("Max page is" + maxpage.ToString()+"pack_page is"+pack_page.ToString());
            //    }
            //    settingButtonNextPrev();
            //});
            //btnLoc2Command = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{

            //    Filter();
            //    settingButtonNextPrev();
            //});

            //#endregion //here

            //#region "setting button"

            //void settingButtonNextPrev()
            //{
            //    int ilc = ListCheckSheets.Count();
            //    BtnPage1 = new page();
            //    BtnPage2 = new page();
            //    BtnPage3 = new page();

            //    //currentpage = 1;

            //    if (NumRowEachPageTextBox != "")
            //    {
            //        //init max page
            //        NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
            //        if (ilc % NumRowEachPage == 0)
            //            maxpage = ilc / NumRowEachPage;
            //        else
            //            maxpage = Convert.ToInt32((ilc / NumRowEachPage)) + 1;
            //        if (maxpage % 3 == 0)
            //            max_pack_page = maxpage / 3;
            //        else
            //            max_pack_page = Convert.ToInt32(maxpage / 3) + 1;


            //        //Init max page
            //        DivInventoryList = new ObservableCollection<CheckItemSheet>();
            //        DivInventoryList.Clear();
            //        int startPos = (currentpage - 1) * NumRowEachPage;
            //        int endPos = currentpage * NumRowEachPage - 1;
            //        if (endPos >= ilc)
            //            endPos = ilc - 1;

            //        int flag = 0;
            //        foreach (var item in ListCheckSheets)
            //        {
            //            if (flag >= startPos && flag <= endPos)
            //                DivInventoryList.Add(item);
            //            flag++;
            //        }
            //        //MessageBox.Show(DivInventoryList.Count.ToString());

            //        //Button "..." visible

            //        //MessageBox.Show("max page is" + maxpage.ToString()+"current page is"+currentpage.ToString());
            //        //MessageBox.Show("Max pack page is" + max_pack_page.ToString() + "pack_page is" + pack_page.ToString());
            //        if (max_pack_page == 1)
            //        {
            //            Bacham1Visible = Visibility.Collapsed;
            //            Bacham2Visible = Visibility.Collapsed;
            //        }
            //        else
            //        {
            //            if (pack_page == max_pack_page)
            //            {
            //                Bacham1Visible = Visibility.Visible;
            //                Bacham2Visible = Visibility.Collapsed;
            //            }
            //            else
            //            {
            //                if (pack_page == 1)
            //                {
            //                    Bacham1Visible = Visibility.Collapsed;
            //                    Bacham2Visible = Visibility.Visible;
            //                }
            //                else
            //                {
            //                    Bacham1Visible = Visibility.Visible;
            //                    Bacham2Visible = Visibility.Visible;
            //                }
            //            }
            //        }


            //        //Button "..." visible


            //        if (currentpage == 1 && maxpage == 1)
            //        {
            //            LeftVisi = false;
            //            RightVisi = true;
            //        }
            //        else
            //        {
            //            if (currentpage == maxpage)
            //            {
            //                LeftVisi = true;
            //                RightVisi = false;
            //            }
            //            else
            //            {
            //                if (currentpage == 1)
            //                {
            //                    LeftVisi = false;
            //                    RightVisi = true;
            //                }
            //                else
            //                {
            //                    LeftVisi = true;
            //                    RightVisi = true;
            //                }
            //            }
            //        }


            //        if (maxpage >= 3)
            //        {
            //            BtnPage1.PageVisi = Visibility.Visible;
            //            BtnPage2.PageVisi = Visibility.Visible;
            //            BtnPage3.PageVisi = Visibility.Visible;

            //            switch (currentpage % 3)
            //            {
            //                case 1:
            //                    BtnPage1.BackGround = Brushes.Blue;
            //                    BtnPage2.BackGround = Brushes.White;
            //                    BtnPage3.BackGround = Brushes.White;
            //                    BtnPage1.PageVal = currentpage;
            //                    BtnPage2.PageVal = currentpage + 1;
            //                    BtnPage3.PageVal = currentpage + 2;
            //                    break;
            //                case 2:
            //                    BtnPage1.BackGround = Brushes.White;
            //                    BtnPage2.BackGround = Brushes.Blue;
            //                    BtnPage3.BackGround = Brushes.White;
            //                    BtnPage1.PageVal = currentpage - 1;
            //                    BtnPage2.PageVal = currentpage;
            //                    BtnPage3.PageVal = currentpage + 1;
            //                    break;
            //                case 0:
            //                    BtnPage1.BackGround = Brushes.White;
            //                    BtnPage2.BackGround = Brushes.White;
            //                    BtnPage3.BackGround = Brushes.Blue;
            //                    BtnPage1.PageVal = currentpage - 2;
            //                    BtnPage2.PageVal = currentpage - 1;
            //                    BtnPage3.PageVal = currentpage;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            if (maxpage == 2)
            //            {
            //                BtnPage1.PageVisi = Visibility.Visible;
            //                BtnPage2.PageVisi = Visibility.Visible;
            //                BtnPage3.PageVisi = Visibility.Collapsed;
            //                switch (currentpage)
            //                {
            //                    case 1:
            //                        BtnPage1.BackGround = Brushes.Blue;
            //                        BtnPage2.BackGround = Brushes.White;
            //                        BtnPage1.PageVal = currentpage;
            //                        BtnPage2.PageVal = currentpage + 1;
            //                        break;
            //                    case 2:
            //                        BtnPage1.BackGround = Brushes.White;
            //                        BtnPage2.BackGround = Brushes.Blue;
            //                        BtnPage1.PageVal = currentpage - 1;
            //                        BtnPage2.PageVal = currentpage;
            //                        break;
            //                }
            //            }
            //            else
            //            {
            //                BtnPage1.PageVisi = Visibility.Visible;
            //                BtnPage2.PageVisi = Visibility.Collapsed;
            //                BtnPage3.PageVisi = Visibility.Collapsed;
            //                BtnPage1.PageVal = (currentpage - 1) * NumRowEachPage + 1; ;
            //                BtnPage1.BackGround = Brushes.Blue;
            //                BtnPage1.PageVal = currentpage;
            //            }
            //        }
            //        if (pack_page == max_pack_page)
            //        {
            //            switch (pack_page * 3 - maxpage)
            //            {
            //                case 1:
            //                    BtnPage3.PageVisi = Visibility.Collapsed;
            //                    break;
            //                case 2:
            //                    BtnPage2.PageVisi = Visibility.Collapsed;
            //                    BtnPage3.PageVisi = Visibility.Collapsed;
            //                    break;
            //            }
            //        }

            //    }
            //}
            //#endregion
        }


        // kiểm tra có tồn tại sản phẩm
        private int checkHasSanPham(string maSP)
        {
            for (int i = 0; i < InventoryList.Count; i++)
            {
                if (InventoryList[i].Item.idItem == maSP)
                    return i;
            }
            return -1;
        }

        // kiểm tra có tồn tại checksheet
        private int checkHasCheckSheet(string code)
        {
            for (int i = 0; i < ListCheckSheets.Count; i++)
            {
                if (ListCheckSheets[i].codeCheckItem == code)
                    return i;
            }
            return -1;
        }

        private void LoadData()
        {
            // load tất cả check sheet
            ListCheckSheets = new ObservableCollection<CheckItemSheet>();
            var listCheckItems = DataProvider.Ins.DB.checkItems.GroupBy(x => x.idCheckItems).Select(y => y.FirstOrDefault());
            foreach (var ckitems in listCheckItems)
            {
                addCheckSheet(ckitems);
            }
            BackupListCheckSheet = ListCheckSheets;

            // load tất cả người làm
            AllStaff = new List<string>();
            var listEmployee = DataProvider.Ins.DB.employees;
            foreach (var employee in listEmployee)
            {
                AllStaff.Add(employee.lastName + " " + employee.firstName);
            }

            // ẩn grid filter
            IsFilter = Visibility.Collapsed;

            // set màu cho nút filter
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
            ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
        }

        private string getFullNameEmployyee(string idEmployee)
        {
            var currentEmployee = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == idEmployee).FirstOrDefault();
            return currentEmployee.lastName + " " + currentEmployee.firstName;
        }

        // thêm check sheet
        private void addCheckSheet(checkItem ckItem)
        {
            int i = checkHasCheckSheet(ckItem.idCheckItems);
            if (i == -1)
            {
                ListCheckSheets.Add(new CheckItemSheet()
                {
                    codeCheckItem = ckItem.idCheckItems,
                    idEmployee = ckItem.idEmployee,
                    dateCheckItems = ckItem.dateCheckItems,
                    InforItems = new List<inforItem> { new inforItem
                    {
                            idItem = ckItem.idItem,
                            OldQuantityItem = (int)ckItem.oldQuantityItem,
                            NewQuantityItem = (int)ckItem.newQuantityItem
                    }},

                    nameEmployee = getFullNameEmployyee(ckItem.idEmployee)
                }) ;
            }
            else
            {
                ListCheckSheets[i].InforItems.Add(new inforItem
                {
                    idItem = ckItem.idItem,
                    OldQuantityItem = (int)ckItem.oldQuantityItem,
                    NewQuantityItem = (int)ckItem.newQuantityItem
                });
            }

            BackupListCheckSheet = ListCheckSheets;
        }

        private void upgradeNumOfPages()
        {
            // load số trang
            NumItems = ListInterger.CreateListInterger(ListCheckSheets.Count + 1);
            DisplayNumOfPages = ListCheckSheets.Count + 1;
        }

        //filter
        private void Filter()
        {
            List<CheckItemSheet> newListCheckSheet = BackupListCheckSheet.ToList();
            if (DisplayNhanVien != null && DisplayNhanVien != "")
            {
                newListCheckSheet = newListCheckSheet.Where(x => x.nameEmployee == DisplayNhanVien).ToList();
            }

            if (displayBeginDay != null)
            {
                List<CheckItemSheet> temp = new List<CheckItemSheet>();
                foreach (var checkSheet in newListCheckSheet)
                {
                    if (DateTime.Compare(checkSheet.dateCheckItems, DateTime.ParseExact(displayBeginDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) >= 0
                     && DateTime.Compare(checkSheet.dateCheckItems, DateTime.ParseExact(displayEndDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) <= 0)
                    {
                        temp.Add(checkSheet);
                    }
                }
                newListCheckSheet = temp;
            }

            ListCheckSheets = new ObservableCollection<CheckItemSheet>(newListCheckSheet);
        }

    }
}