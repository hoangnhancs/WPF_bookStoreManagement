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

        private List<string> _AllStaff;
        public List<string> AllStaff { get => _AllStaff; set { _AllStaff = value; OnPropertyChanged(); } }


        private List<CellItems> backupAllItems;
        private List<CellItems> _ShowItems;
        public List<CellItems> ShowItems { get => _ShowItems; set { _ShowItems = value; OnPropertyChanged(); } }

        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }

        public ICommand LoadedCheckItemsCommand { get; set; }
        public ICommand ClickHiddenCommand { get; set; }
        public ICommand txtBoxTextChangedSearchCommand { get; set; }
        public ICommand CheckedGridSearchCommand { get; set; }
        public ICommand ClickAllSelectedCommand { get; set; }
        public ICommand ClickAllUnSelectedCommand { get; set; }
        public ICommand ClickCompletedCommand { get; set; }
        public CheckItemsViewModel()
        {
            AllStaff = new List<string>();
            AllStaff.Add(LoggedAccount.Account.nameAccount);

            InventoryList = new ObservableCollection<Inventory>();

            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });

            ClickHiddenCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var grid = (p as Grid);
                if (grid.Visibility == Visibility.Collapsed)
                {
                    grid.Visibility = Visibility.Visible;
                }
                else
                {
                    grid.Visibility = Visibility.Collapsed;
                }
            });

            txtBoxTextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text.Trim().ToLower();
                    if (query == "")
                    {
                        ShowItems = new List<CellItems>();
                        ShowItems = backupAllItems;
                    }
                    else
                    {
                        ShowItems = new List<CellItems>();
                        foreach (var cellItems in backupAllItems)
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

            CheckedGridSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var selected = (p as CellItems);
                    selected.IsSelected = true;

                    for(int i = 0; i < backupAllItems.Count; i++)
                    {
                        if (backupAllItems[i].Items == selected.Items)
                            backupAllItems[i].IsSelected = true;
                    }
                }
            });

            ClickAllSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < backupAllItems.Count; i++)
                {                        
                    backupAllItems[i].IsSelected = true;
                }
                ShowItems = backupAllItems;

                (p as DataGrid).Items.Refresh();
            });

            ClickAllUnSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < backupAllItems.Count; i++)
                {
                    backupAllItems[i].IsSelected = false;
                }
                ShowItems = backupAllItems;

                (p as DataGrid).Items.Refresh();
            });

            ClickCompletedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                foreach(var cellItems in backupAllItems)
                {
                    if (cellItems.IsSelected)
                    {
                        var _item = DataProvider.Ins.DB.itemSummaries.Where(i => cellItems.Items.idItem == i.idItem).FirstOrDefault();
                        Inventory _Inventory = new Inventory();
                        _Inventory.Item = cellItems.Items;

                        if (_item != null)
                        {
                            _Inventory.Count = _item.quantityAfter - _item.quantityBefore;
                        }
                        else
                        {
                            _Inventory.Count = 0;
                        }
                        _Inventory.Note = "";
                        InventoryList.Add(_Inventory);
                    }
                }
            });
        }

        private void LoadData()
        {
            backupAllItems = new List<CellItems>();

            var listItems = DataProvider.Ins.DB.items;
            foreach (var item in listItems)
            {
                CellItems newCell = new CellItems();
                newCell.Items = item;
                newCell.IsSelected = false;

                backupAllItems.Add(newCell);
            }

            ShowItems = new List<CellItems>();
            ShowItems = backupAllItems;

            var listAccount = DataProvider.Ins.DB.accounts;
            foreach (var account in listAccount)
            {
                if (account.nameAccount != AllStaff[0])
                    AllStaff.Add(account.nameAccount.ToString());
            }
        }

    }
}
