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
        public ICommand cbbStatusChangedCommand { get; set; }
        public string cbbStatusValue { get; set; }
        private string _tbsv;
        public string textBoxSearchValue 
        {
            get { return _tbsv; }
            set
            {
                _tbsv = value;
                OnPropertyChanged();
            }
        }
        public ICommand searchEngineer { get; set; }
        //phan nay cua grid danh sach nha cung cap

        //phan nay cua grid add nha cung cap
        private string _idsup;
        public string idSup 
        {
            get { return _idsup; }
            set
            {
                _idsup = value;
                OnPropertyChanged();
            }
        }
        private string _namesup;
        public string nameSup 
        {
            get { return _namesup; }
            set
            {
                _namesup = value;
                OnPropertyChanged();
            }
        }
        private string _t;
        public string tinh
        {
            get { return _t; }
            set
            {
                _t = value;
                OnPropertyChanged();
            }
        }
        private string _h;
        public string huyen
        {
            get { return _h; }
            set
            {
                _h = value;
                OnPropertyChanged();
            }
        }
        private string _x;
        public string xa 
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }
        private string _sn;
        public string sonha 
        {
            get { return _sn; }
            set
            {
                _sn = value;
                OnPropertyChanged();
            }
        }
        private string _adr;
        public string addressSup 
        {
            get { return _adr; }
            set
            {
                _adr = value;
                OnPropertyChanged();
            }
        }
        private string _em;
        public string emailSup
        {
            get { return _em; }
            set
            {
                _em = value;
                OnPropertyChanged();
            }
        }
        private string _phone;
        public string phoneSup 
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        private string _fax;
        public string fax
        {
            get { return _fax; }
            set
            {
                _fax = value;
                OnPropertyChanged();
            }
        }
        private string _mst;
        public string masothue
        {
            get { return _mst; }
            set
            {
                _mst = value;
                OnPropertyChanged();
            }
        }
        private string _ws;
        public string website
        {
            get { return _ws; }
            set
            {
                _ws = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadInsertInforCommand { get; set; }
        public ICommand insertSupplierCommand { get; set; }
        public ICommand btnAddSupCommand { get; set; }
        public ICommand btnThoatCommand { get; set; }
        public string _function { get; set; }


        bool changed = false;
        private Visibility _dsnhacungcapvisible { get; set; }
        public Visibility DSNhacungcapVisible
        {
            get { return _dsnhacungcapvisible; }
            set
            {
                _dsnhacungcapvisible = value;
                OnPropertyChanged();
            }
        }
        private Visibility _editnhacungcapvisible;
        public Visibility EditNhacungcapVisible
        {
            get { return _editnhacungcapvisible; }
            set
            {
                _editnhacungcapvisible = value;
                OnPropertyChanged();
            }
        }
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
                EditNhacungcapVisible = Visibility.Collapsed;
                DSNhacungcapVisible = Visibility.Visible;
                SearchEngineer(textBoxSearchValue, cbbStatusValue);
            });
            //delete nha cung cap
            btnDeleteNCCClickCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {

                SelectedItem = p as Inventory;
                string selectedID = SelectedItem.Supplier.idSupplier;
                
                if (MessageBox.Show("Bạn có muốn xóa nhà cung cấp này?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                    var x = (from y in DataProvider.Ins.DB.suppliers where y.idSupplier == selectedID select y).FirstOrDefault();
                    if(x!=null)
                    {
                        DataProvider.Ins.DB.suppliers.Remove(x);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }

                SearchEngineer(textBoxSearchValue, cbbStatusValue);
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
                MessageBox.Show(lstadd[0] + "," + lstadd[1] + "," + lstadd[2] + "," + lstadd[3]);
                sonha = lstadd[0];
                xa = lstadd[1].Replace(" xã ", "");
                huyen = lstadd[2].Replace(" huyện ", "");
                tinh = lstadd[3].Replace(" tỉnh ", "");
                phoneSup = SelectedItem.Supplier.phoneNumberSupplier;
                EditNhacungcapVisible = Visibility.Visible;
                DSNhacungcapVisible = Visibility.Collapsed;
            });
            //collapsed select grid

            //event text box search thay doi, moi lan thay doi cap nhat lai gia tri bien textBoxSearchValue

            //event combo box trang thai thay doi, moi lan thay doi cap nhat lai gia tri bien cbbStatusValue
            cbbStatusChangedCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ComboBoxItem cbi = (ComboBoxItem)(p as ComboBox).SelectedItem;
                string value = cbi.Content.ToString();
                cbbStatusValue = value;
            });
            //load function is add or edit

            btnAddSupCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                _function = (p as Button).Name;
                refreshall();
                EditNhacungcapVisible = Visibility.Visible;
                DSNhacungcapVisible = Visibility.Collapsed;
            });
            btnThoatCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                refreshall();
                EditNhacungcapVisible = Visibility.Collapsed;
                DSNhacungcapVisible = Visibility.Visible;
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



            insertSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => addSupExcuteQuery());


            //ham them nha cung cap bang query
            void addSupExcuteQuery()
            {
                string newid;

                addressSup = String.Format("{0}, xã {1}, huyện {1}, tỉnh {2}", sonha, xa, huyen, tinh);

                if (nameSup != "" && tinh!="" && huyen!="" && xa!="" && sonha!="" && emailSup != "" && phoneSup != "")
                {
                    if (_function == "btnAddSuplier")
                    {
                        int count;

                        string _lastid = DataProvider.Ins.DB.suppliers.OrderByDescending(p => p.idSupplier).First().idSupplier;
                        count = Convert.ToInt32(_lastid.Replace("NCC", "")) + 1;

                        if (count > 99)
                        {
                            newid = "NCC" + count.ToString();
                        }
                        else
                        {
                            if (count > 9)
                            {
                                newid = "NCC0" + count.ToString();
                            }
                            else
                                newid = "NCC00" + count.ToString();
                        }
                        //string query = "insert into supplier values (N'" + newid + "', N'" + nameSup + "', N'" + addressSup + "', N'" + emailSup + "', N'" + phoneSup + "', N'Đang hợp tác')";
                        //DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                        supplier _tmp = new supplier();
                        _tmp.idSupplier = newid;
                        _tmp.nameSupplier = nameSup;
                        _tmp.addressSupplier = addressSup;
                        _tmp.emailSupplier = emailSup;
                        _tmp.phoneNumberSupplier = phoneSup;
                        _tmp.statusSupplier = "Đang hợp tác";
                        DataProvider.Ins.DB.suppliers.Add(_tmp);
                        DataProvider.Ins.DB.SaveChanges();
                        SearchEngineer(textBoxSearchValue, cbbStatusValue);
                        refreshall();
                        MessageBox.Show("Thêm nhà cung cấp thành công");
                        
                    }
                    else
                    {
                        var res = DataProvider.Ins.DB.suppliers.SingleOrDefault(i => i.idSupplier == idSup);
                        if (res != null)
                        {
                            res.nameSupplier = nameSup;
                            res.addressSupplier = addressSup;
                            res.emailSupplier = emailSup;
                            res.phoneNumberSupplier = phoneSup;
                            
                            DataProvider.Ins.DB.SaveChanges();
                        }
                        MessageBox.Show("Cập nhật thành công");
                    }

                }
                else
                    MessageBox.Show("Vui lòng điền đủ thông tin");

            }
                //ham refresh cac bien lien quan khi them nha cung cap thanh cong
            void refreshall()
            {
                idSup = "";
                tinh = "";
                huyen = "";
                xa = "";
                sonha = "";
                phoneSup = "";
                emailSup = "";
                nameSup = "";
                fax = "";
                masothue = "";
                website = "";
            }
        }
    }
}
