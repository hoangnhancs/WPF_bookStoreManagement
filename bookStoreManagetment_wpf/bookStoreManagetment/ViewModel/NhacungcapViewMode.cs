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
        public Inventory SelectedItem { get; set; }

        public ICommand LoadNhacungcapCommand { get; set; }
        public ICommand btnDeleteNCCClickCommand { get; set; }
        public ICommand btnEditNCCClickCommand { get; set; }
        public ICommand cbbStatusChangedCommand { get; set; }
        public ICommand refreshDatagridCommand { get; set; }

        public NhacungcapViewMode()
        {
            LoadNhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LoadDataWithQuery("Tất cả");
            });
            btnDeleteNCCClickCommand = new RelayCommand<Object>((p) => { return true; }, (p) => {

                SelectedItem = p as Inventory;
                //SelectedItem.Supplier.statusSupplier = "Ngừng hợp tác";
                string selectedID = SelectedItem.Supplier.idSupplier;
                string query = "update supplier set statusSupplier=N'Ngừng hợp tác' where idSupplier=N'" + selectedID + "'";
                DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                //ctx.SqlQuery("update supplier set statusSupplier=N'Ngừng cung cấp' where idSupplier=");
                MessageBox.Show(query);

            });
            cbbStatusChangedCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => {
                LoadDataWithQuery(p.Text);
            });
            refreshDatagridCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) => {
                p.Items.Refresh();
                MessageBox.Show("hi");
            });

            void LoadDataWithQuery(string query)
            {
                InventoryList = new ObservableCollection<Inventory>();
                if (query != "Tất cả")
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i => i.statusSupplier == query);
                    foreach (var ncc in lstNhacungcap)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Supplier = ncc;
                        InventoryList.Add(_Inventory);
                    }
                }
                else
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers;
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
