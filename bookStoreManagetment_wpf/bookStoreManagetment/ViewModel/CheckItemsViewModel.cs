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
    public class CheckItemsViewModel : BaseViewModel
    {
        private List<string> _AllStaff;
        public List<string> AllStaff { get => _AllStaff; set { _AllStaff = value; OnPropertyChanged(); } }

        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }

        public ICommand LoadedCheckItemsCommand { get; set; }
        public ICommand ClickHiddenCommand { get; set; }
        public CheckItemsViewModel()
        {
            
            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
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
        }

        private void LoadData()
        {
            AllStaff = new List<string>();
            var listAccount = DataProvider.Ins.DB.accounts;
            foreach(var account in listAccount)
            {
                AllStaff.Add(account.nameAccount.ToString());
            }

            InventoryList = new ObservableCollection<Inventory>();
            var listSummary = DataProvider.Ins.DB.items;

            foreach(var item in listSummary)
            {
                var _item = DataProvider.Ins.DB.itemSummaries.Where(i => item.idItem == i.idItem).FirstOrDefault();
                
                Inventory _Inventory = new Inventory();
                _Inventory.Item = item;

                if (_item != null)
                {
                    _Inventory.Count = _item.quantityBefore - _item.quantityBefore;
                }
                else
                {
                    _Inventory.Count = 0;
                }
                _Inventory.Note = "";
                InventoryList.Add(_Inventory);
            }

        }
    }
}
