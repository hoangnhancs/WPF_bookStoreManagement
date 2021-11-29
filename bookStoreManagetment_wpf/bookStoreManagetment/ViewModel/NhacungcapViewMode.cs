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
//using System.Windows.Forms;
using System.Windows.Input;


namespace bookStoreManagetment.ViewModel
{
    public class NhacungcapViewMode : BaseViewModel
    {
        public Grid openedGrid { get; set; }
        //phan nay cua grid danh sach nha cung cap
        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }
        public Inventory SelectedItem { get; set; }
        public ICommand LoadNhacungcapCommand { get; set; }
        public ICommand btnDeleteNCCClickCommand { get; set; }
        public ICommand btnEditNCCClickCommand { get; set; }
        public ICommand CollapsedGridCommand { get; set; }
        public ICommand VisibleGridCommand { get; set; }
        public ICommand searchTBchangedCommand { get; set; }
        public ICommand cbbStatusChangedCommand { get; set; }
        public string cbbStatusValue { get; set; }
        public string textBoxSearchValue { get; set; }
        public ICommand searchEngineer { get; set; }
        //phan nay cua grid danh sach nha cung cap

        //phan nay cua grid add nha cung cap
        string idSup { get; set; }
        string nameSup { get; set; }
        string tinh { get; set; }
        string huyen { get; set; }
        string xa { get; set; }
        string sonha { get; set; }
        string addressSup { get; set; }
        string emailSup { get; set; }
        string phoneSup { get; set; }
        public ICommand LoadInsertInforCommand { get; set; }
        public ICommand insertSupplierCommand { get; set; }
        public ICommand reloadTextBoxCommand { get; set; }
        public string _function { get; set; }
        public ICommand loadFunctionCommand { get; set; }
        public ICommand canEditCommand { get; set; }
        bool changed = false;

        //phan nay cua grid add nha cung cap

        //phan nay cua edit nha cung cap


        public NhacungcapViewMode()
        {
            //phan nay cua danh sach nha cung cap

            //load nha cung cap user control
            LoadNhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                textBoxSearchValue = "";
                cbbStatusValue = "Tất cả";
                openedGrid = new Grid();
                openedGrid.Name = "gridAddNCC";
                SearchEngineer(textBoxSearchValue, cbbStatusValue);
            });
            //delete nha cung cap
            btnDeleteNCCClickCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {

                SelectedItem = p as Inventory;
                string selectedID = SelectedItem.Supplier.idSupplier;
                //string query = "delete from supplier where idSupplier=N'" + selectedID + "'";
                string query = "update supplier set statusSupplier=N'Ngừng hợp tác' where idSupplier=N'" + selectedID + "'";
                if (MessageBox.Show("Bạn có muốn xóa nhà cung cấp này?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                

            });
            //edit nha cung cap
            btnEditNCCClickCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                _function = "btnEdit";
                SelectedItem = p as Inventory;
                changed = false;
                idSup = SelectedItem.Supplier.idSupplier;
                nameSup = SelectedItem.Supplier.nameSupplier;
                addressSup = SelectedItem.Supplier.addressSupplier;
                emailSup = SelectedItem.Supplier.emailSupplier;
                string[] lstadd = addressSup.Split(',');
                sonha = lstadd[0];
                xa = lstadd[1];
                huyen = lstadd[2];
                tinh = lstadd[3];
                phoneSup = SelectedItem.Supplier.phoneNumberSupplier;
                //Console.WriteLine(idSup);
            });
            //collapsed select grid
            CollapsedGridCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                if (nameSup == "" && emailSup == "" && addressSup == "" && phoneSup == "" && idSup == "" && tinh == "" && huyen == "" && xa == "" && sonha == "")
                {
                    (p as Grid).Visibility = Visibility.Collapsed;

                }
                else
                {
                    if (MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (p as Grid).Visibility = Visibility.Collapsed;
                        refreshall();
                    }
                }

            });
            //visible select grid
            VisibleGridCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                (p as Grid).Visibility = Visibility.Visible;
                refreshall();
            });
            //event text box search thay doi, moi lan thay doi cap nhat lai gia tri bien textBoxSearchValue
            searchTBchangedCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                textBoxSearchValue = (p as TextBox).Text;
            });
            //event combo box trang thai thay doi, moi lan thay doi cap nhat lai gia tri bien cbbStatusValue
            cbbStatusChangedCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ComboBoxItem cbi = (ComboBoxItem)(p as ComboBox).SelectedItem;
                string value = cbi.Content.ToString();
                cbbStatusValue = value;
            });
            //load function is add or edit
            loadFunctionCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                _function = (p as Button).Name;
                MessageBox.Show(_function);
            });
            canEditCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                (p as TextBox).IsReadOnly = false;
            });
            //query tim kiem nhung nha cung cap thoa man
            searchEngineer = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                SearchEngineer(textBoxSearchValue, cbbStatusValue);
            });
            //ham tiem kiem
            void SearchEngineer(string tbsearchval, string cbbstatusval)
            {
                InventoryList = new ObservableCollection<Inventory>();
                if (cbbstatusval != "Tất cả")
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i => (i.nameSupplier.Contains(tbsearchval) || i.idSupplier.Contains(tbsearchval)) && i.statusSupplier == cbbstatusval);
                    foreach (var ncc in lstNhacungcap)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Supplier = ncc;
                        InventoryList.Add(_Inventory);
                    }
                }
                else
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i => i.nameSupplier.Contains(tbsearchval) || i.idSupplier.Contains(tbsearchval));
                    foreach (var ncc in lstNhacungcap)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Supplier = ncc;
                        InventoryList.Add(_Inventory);
                    }
                }
            }
            //phan nay cua danh sach nha cung cap


            //phan nay cua delete, edit nha cung cap


            LoadInsertInforCommand = new RelayCommand<object>((p) => { return true; }, (p) => changeInfor(p));
            reloadTextBoxCommand = new RelayCommand<object>((p) => { return true; }, (p) => reloadTextBox(p));
            insertSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => addSupExcuteQuery());
            //moi lan text box thong tin nha cung cap thay doi, se bat su kien, cap nhat lai cac bien
            void changeInfor(object obj)
            {
                switch ((obj as TextBox).Name)
                {
                    case "idSuptb":
                        idSup = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "nameSuptb":
                        nameSup = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "tinhAddressSuptb":
                        tinh = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "huyenAddressSuptb":
                        huyen = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "xaAddressSuptb":
                        xa = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "sonhaAddressSuptb":
                        sonha = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "mailSuptb":
                        emailSup = (obj as TextBox).Text;
                        changed = true;
                        break;
                    case "phoneSuptb":
                        phoneSup = (obj as TextBox).Text;
                        changed = true;
                        break;
                }
            }
            //su kien xoa text trong text box khi them nha cung cap xong
            void reloadTextBox(object obj)
            {

                switch ((obj as TextBox).Name)
                {
                    case "idSuptb":
                        (obj as TextBox).Text = idSup;
                        if (_function == "btnEdit")
                            (obj as TextBox).IsReadOnly = true;
                        else
                            (obj as TextBox).IsReadOnly = false;
                        break;
                    case "nameSuptb":
                        (obj as TextBox).Text = nameSup;
                        break;
                    case "tinhAddressSuptb":
                        (obj as TextBox).Text = tinh;
                        break;
                    case "huyenAddressSuptb":
                        (obj as TextBox).Text = huyen;
                        break;
                    case "xaAddressSuptb":
                        (obj as TextBox).Text = xa;
                        break;
                    case "sonhaAddressSuptb":
                        (obj as TextBox).Text = sonha;
                        break;
                    case "mailSuptb":
                        (obj as TextBox).Text = emailSup;
                        break;
                    case "phoneSuptb":
                        (obj as TextBox).Text = phoneSup;
                        break;
                }
            }
            //ham them nha cung cap bang query
            void addSupExcuteQuery()
            {
                bool flag = false;

                addressSup = sonha + "," + xa + "," + huyen + "," + tinh;
                
                if (idSup!=""&&nameSup != "" && addressSup != "   " && emailSup != "" && phoneSup != "")
                {
                    if (_function == "btnAddSuplier")
                    {
                        string query = "insert into supplier values (N'" + idSup + "', N'" + nameSup + "', N'" + addressSup + "', N'" + emailSup + "', N'" + phoneSup + "', N'Đang hợp tác')";
                        foreach (var item in InventoryList)
                        {
                            if (idSup == item.Supplier.idSupplier)
                            {
                                flag = false;
                                MessageBox.Show("Mã nhà cung cấp đã tồn tại");
                                break;

                            }
                            else
                                flag = true;
                        }
                        if (flag == true)
                        {
                            //flag = true;
                            DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                            SearchEngineer(textBoxSearchValue, cbbStatusValue);
                            refreshall();
                            MessageBox.Show("Thêm nhà cung cấp thành công");
                        }
                    }
                    else
                    {
                        string query = "update supplier set nameSupplier=N'" + nameSup + "',addressSupplier=N'" + addressSup + "',emailSupplier=N'" + emailSup + "',phoneNumberSupplier=N'" + phoneSup + "' where idSupplier=N'" + idSup + "'";
                        //Console.WriteLine(query);
                        if (changed == true)
                        {
                            DataProvider.Ins.DB.SaveChanges();
                            DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                            
                            MessageBox.Show("Cập nhật thành công");
                            SearchEngineer(textBoxSearchValue, cbbStatusValue);
                            foreach(var item in InventoryList)
                            {
                                MessageBox.Show(item.Supplier.nameSupplier);
                            }
                        }
                        else
                            MessageBox.Show("Bạn chưa hay đổi bất kì thông tin nào");
                    }

                }
                else
                    MessageBox.Show("Vui lòng điền đủ thông tin");

            }
                //ham refresh cac bien lien quan khi them nha cung cap thanh cong
            void refreshall()
            {
                idSup = "";
                addressSup = "";
                tinh = "";
                huyen = "";
                xa = "";
                sonha = "";
                phoneSup = "";
                emailSup = "";
                nameSup = "";
            }
        }
    }
}
