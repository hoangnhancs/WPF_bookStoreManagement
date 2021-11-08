using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class NhacungcapViewMode : BaseViewModel
    {
        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }

        public ICommand LoadNhacungcapCommand { get; set; }
        public ICommand btnDeleteNCCClickCommand { get; set; }
        public ICommand btnEditNCCClickCommand { get; set; }

        public NhacungcapViewMode()
        {
            LoadNhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LoadData();
            });

            btnDeleteNCCClickCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                MessageBox.Show("hi");
            });
            void LoadData(int status=2)
            {
                InventoryList = new ObservableCollection<Inventory>();
                
                if (status == 2)
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers;
                    foreach (var ncc in lstNhacungcap)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Supplier = ncc;
                        InventoryList.Add(_Inventory);
                    }
                }
                else
                {
                    if(status==1)
                    {
                        var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i=>i.statusSupplier=="Đang hợp tác");
                        foreach (var ncc in lstNhacungcap)
                        {
                            Inventory _Inventory = new Inventory();
                            _Inventory.Supplier = ncc;
                            InventoryList.Add(_Inventory);
                        }
                    }
                    else
                    {
                        var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i => i.statusSupplier == "Ngừng hợp tác");
                        foreach (var ncc in lstNhacungcap)
                        {
                            Inventory _Inventory = new Inventory();
                            _Inventory.Supplier = ncc;
                            InventoryList.Add(_Inventory);
                        }
                    }
                }
            }
        }
    }
}
