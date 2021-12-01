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
        public CheckItemsViewModel()
        {
            AllStaff = new List<string>();
            AllStaff.Add(DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault().lastName);
            Title = "Danh Sách Phiếu Kiểm Hàng";

            InventoryList = new ObservableCollection<Inventory>();

            // load form
            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
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
                        if (inventory.Count == 0)
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

                DisplayNhanVien = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault().lastName;
                DisplayNgay = null;
                DisplayMaPhieu = null;
                Note = null;
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
                foreach (var infor in temp.InforItems)
                {
                    InventoryList.Add(new Inventory()
                    {
                        Item = DataProvider.Ins.DB.items.Where(x => x.idItem == infor.idItem).FirstOrDefault(),
                        Count = infor.quantityItem
                    });
                }
                State = Visibility.Collapsed;
                isReadOnly = false;
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
                        Title = "Danh Sách Phiếu Kiểm Hàng > Kiểm Hàng";
                    else if (grid.Name == "gridAddItems")
                        Title = "Danh Sách Phiếu Kiểm Hàng > Kiểm Hàng > Thêm Sản Phẩm";
                    else
                        Title = "Danh Sách Phiếu Kiểm Hàng";
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

                        int i = checkHasSanPham(cellItems.Items.idItem);
                        if (i != -1)
                        {
                            _Inventory.Count = InventoryList[i].Count;
                        }
                        else
                        {
                            _Inventory.Count = 0;
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
                    int idCount = ListCheckSheets.Count() + 1;
                    foreach (var inventory in InventoryList)
                    {
                        checkItem temp = new checkItem()
                        {
                            note = Note,
                            idCheckItems = "PKH" + idCount.ToString(),
                            dateCheckItems = DateTime.Now,
                            idEmployee = DataProvider.Ins.DB.employees.Where(x => x.lastName == DisplayNhanVien).FirstOrDefault().idEmployee,
                            idItem = inventory.Item.idItem,
                            quantityItem = inventory.Count
                        };

                        DataProvider.Ins.DB.checkItems.Add(temp);
                        DataProvider.Ins.DB.SaveChanges();

                        addCheckSheet(temp);
                        i++;
                    }
                    InventoryList.Clear();
                    ShowItems.Clear();
                });
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
            var listEmployee = DataProvider.Ins.DB.employees;
            foreach (var employee in listEmployee)
            {
                if (AllStaff.Contains(employee.lastName) == false)
                {
                    AllStaff.Add(employee.lastName);
                }
            }

            // load số trang
            NumItems = ListInterger.CreateListInterger(ListCheckSheets.Count + 1);
            DisplayNumOfPages = ListCheckSheets.Count + 1;
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
                            quantityItem = ckItem.quantityItem
                    }},
                    nameEmployee = DataProvider.Ins.DB.employees.Where(x => x.idEmployee == ckItem.idEmployee).FirstOrDefault().lastName
                });
            }
            else
            {
                ListCheckSheets[i].InforItems.Add(new inforItem
                {
                    idItem = ckItem.idItem,
                    quantityItem = ckItem.quantityItem
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

    }
}