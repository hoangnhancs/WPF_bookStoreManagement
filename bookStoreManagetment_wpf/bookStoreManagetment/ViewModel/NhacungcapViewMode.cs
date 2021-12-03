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
using System.Windows.Media;

namespace bookStoreManagetment.ViewModel
{
    public class NhacungcapViewMode : BaseViewModel
    {

        //phan nay cua grid danh sach nha cung cap
        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }
        public Inventory SelectedItem { get; set; }
        public ICommand LoadNhacungcapCommand { get; set; }

        #region "filter"
        //trang thai loc
        private string _currentStatus;
        public string CurrentStatus { get { return _currentStatus; } set { _currentStatus = value; OnPropertyChanged(); } }
        //combobox trang thai
        private ObservableCollection<string> _lstStatus;
        public ObservableCollection<string> ListStatus { get { return _lstStatus; } set { _lstStatus = value; OnPropertyChanged(); } }
        //text box search
        private string _tbsv;
        public string textBoxSearchValue { get { return _tbsv; } set { _tbsv = value; OnPropertyChanged(); } }
        //search command su dung textboxsearch va trang thai
        public ICommand searchEngineer { get; set; }
        #endregion


        #region "insert,update,delete defind"
        //information 
        private string _idsup;
        public string idSup { get { return _idsup; } set { _idsup = value; OnPropertyChanged(); }}
        private string _namesup;
        public string nameSup { get { return _namesup; } set { _namesup = value; OnPropertyChanged(); }}
        private string _t;
        public string tinh { get { return _t; } set { _t = value; OnPropertyChanged(); }}
        private string _h;
        public string huyen { get { return _h; } set { _h = value; OnPropertyChanged(); }}
        private string _x;
        public string xa { get { return _x; } set { _x = value; OnPropertyChanged(); }}
        private string _sn;
        public string sonha { get { return _sn; } set { _sn = value; OnPropertyChanged(); }}
        private string _adr;
        public string addressSup { get { return _adr; } set { _adr = value; OnPropertyChanged(); }}
        private string _em;
        public string emailSup { get { return _em; } set { _em = value; OnPropertyChanged(); }}
        private string _phone;
        public string phoneSup { get { return _phone; } set { _phone = value; OnPropertyChanged(); }}
        private string _fax;
        public string fax { get { return _fax; } set { _fax = value; OnPropertyChanged(); }}
        private string _mst;
        public string masothue { get { return _mst; }set{ _mst = value; OnPropertyChanged(); }}
        private string _ws;
        public string website { get { return _ws; } set { _ws = value; OnPropertyChanged(); }}
        //information 

        //load interface add supplier
        public ICommand btnAddSupCommand { get; set; }
        //add supplier information
        public ICommand insertSupplierCommand { get; set; }
        //load interface edit supplier
        public ICommand btnEditSuplierCommand { get; set; }
        //update supplier command
        public ICommand updateSupplierCommand { get; set; }
        //delete supplier command
        public ICommand btnDeleteSupplierCommand { get; set; }
        #endregion


        #region "manipulation"
        //exit edit, exit add nha cung cap
        public ICommand btnExitCommand { get; set; }
        //mo grid filter 
        public ICommand OpenFilterCommand { get; set; }
        //an hien grid danh sach nha cung cap
        private Visibility _dsnhacungcapvisible { get; set; }
        public Visibility DSNhacungcapVisible { get { return _dsnhacungcapvisible; } set { _dsnhacungcapvisible = value; OnPropertyChanged(); } }
        //an hien grid them, chinh sua nha cung cap
        private Visibility _editnhacungcapvisible;
        public Visibility EditNhacungcapVisible { get { return _editnhacungcapvisible; } set { _editnhacungcapvisible = value; OnPropertyChanged(); } }
        //an hien nut cap nhat
        private Visibility _btnupdatevisible;
        public Visibility ButtonUpdateVisible { get { return _btnupdatevisible; } set { _btnupdatevisible = value; OnPropertyChanged(); } }
        //an hien nut them
        private Visibility _btninsertvisible;
        public Visibility ButtonInsertVisible { get { return _btninsertvisible; } set { _btninsertvisible = value; OnPropertyChanged(); } }
        //an hien grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }
        //thay doi background filter
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }
        // thay doi foreground filter
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }
        
        #endregion


        public NhacungcapViewMode()
        {
            //phan nay cua danh sach nha cung cap

            //load nha cung cap user control
            LoadNhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                // ẩn grid filter
                IsFilter = Visibility.Collapsed;

                // set màu cho nút filter
                var bc = new BrushConverter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");

                textBoxSearchValue = "";
                CurrentStatus = "";
                IsFilter = Visibility.Collapsed;
                EditNhacungcapVisible = Visibility.Collapsed;
                DSNhacungcapVisible = Visibility.Visible;
                LoadListStatus();
                
                SearchEngineer(textBoxSearchValue, CurrentStatus);
            });


            #region "implement insert,update,delete"
            btnAddSupCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                refreshall();
                EditNhacungcapVisible = Visibility.Visible;
                DSNhacungcapVisible = Visibility.Collapsed;
                ButtonInsertVisible = Visibility.Visible;
                ButtonUpdateVisible = Visibility.Collapsed;
            });
            insertSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => addSupExcuteQuery());
            btnEditSuplierCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                ButtonInsertVisible = Visibility.Collapsed;
                ButtonUpdateVisible = Visibility.Visible;
                DSNhacungcapVisible = Visibility.Collapsed;
                EditNhacungcapVisible = Visibility.Visible;
                SelectedItem = p as Inventory;
                idSup = SelectedItem.Supplier.idSupplier;
                nameSup = SelectedItem.Supplier.nameSupplier;
                addressSup = SelectedItem.Supplier.addressSupplier;
                emailSup = SelectedItem.Supplier.emailSupplier;
                string[] lstadd = addressSup.Split(',');
                sonha = lstadd[0];
                xa = lstadd[1].Replace(" xã ", "");
                huyen = lstadd[2].Replace(" huyện ", "");
                tinh = lstadd[3].Replace(" tỉnh ", "");
                phoneSup = SelectedItem.Supplier.phoneNumberSupplier;
                EditNhacungcapVisible = Visibility.Visible;
                DSNhacungcapVisible = Visibility.Collapsed;
            });
            updateSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                updateSupplier();
            });
            btnDeleteSupplierCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {

                SelectedItem = p as Inventory;
                string selectedID = SelectedItem.Supplier.idSupplier;

                if (MessageBox.Show("Bạn có muốn xóa nhà cung cấp này?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                    var x = (from y in DataProvider.Ins.DB.suppliers where y.idSupplier == selectedID select y).FirstOrDefault();
                    if (x != null)
                    {
                        DataProvider.Ins.DB.suppliers.Remove(x);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }

                SearchEngineer(textBoxSearchValue, CurrentStatus);
            });
            //ham them nha cung cap
            void addSupExcuteQuery()
            {
                addressSup = String.Format("{0}, xã {1}, huyện {1}, tỉnh {2}", sonha, xa, huyen, tinh);
                Console.WriteLine(addressSup);
                string newid;
                if (nameSup != "" && tinh != "" && huyen != "" && xa != "" && sonha != "" && emailSup != "" && phoneSup != "")
                {

                    int count;
                    if (DataProvider.Ins.DB.suppliers.Count() > 0)
                    {
                        string _lastid = DataProvider.Ins.DB.suppliers.OrderByDescending(p => p.idSupplier).First().idSupplier;
                        count = Convert.ToInt32(_lastid.Replace("NCC", "")) + 1;
                    }
                    else
                        count = 1;
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
                    supplier _tmp = new supplier();
                    _tmp.idSupplier = newid;
                    _tmp.nameSupplier = nameSup;
                    _tmp.addressSupplier = addressSup;
                    _tmp.emailSupplier = emailSup;
                    _tmp.phoneNumberSupplier = phoneSup;
                    _tmp.statusSupplier = "Đang hợp tác";
                    //_tmp.fax = fax;
                    //_tmp.website = website;
                    //_tmp.masothue = masothue;
                    DataProvider.Ins.DB.suppliers.Add(_tmp);
                    DataProvider.Ins.DB.SaveChanges();
                    SearchEngineer(textBoxSearchValue, CurrentStatus);
                    refreshall();
                    MessageBox.Show("Thêm nhà cung cấp thành công");
                }
                else
                    MessageBox.Show("Vui lòng điền đủ thông tin");

            }
            //ham update nha cung cap
            void updateSupplier()
            {
                var res = DataProvider.Ins.DB.suppliers.SingleOrDefault(i => i.idSupplier == idSup);
                addressSup = String.Format("{0}, xã {1}, huyện {1}, tỉnh {2}", sonha, xa, huyen, tinh);
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
            #endregion

            #region "implement filter"
            searchEngineer = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                SearchEngineer(textBoxSearchValue, CurrentStatus);
            });
            //ham tiem kiem
            void SearchEngineer(string tbsearchval, string cbbstatusval)
            {
                
                InventoryList = new ObservableCollection<Inventory>();
                if (cbbstatusval == "Tất cả" || cbbstatusval == "") 
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i => i.nameSupplier.Contains(tbsearchval) || i.idSupplier.Contains(tbsearchval));
                    foreach (var ncc in lstNhacungcap)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Supplier = ncc;
                        InventoryList.Add(_Inventory);
                    }
                }
                else
                {
                    var lstNhacungcap = DataProvider.Ins.DB.suppliers.Where(i => (i.nameSupplier.Contains(tbsearchval) || i.idSupplier.Contains(tbsearchval)) && i.statusSupplier == cbbstatusval);
                    foreach (var ncc in lstNhacungcap)
                    {
                        Inventory _Inventory = new Inventory();
                        _Inventory.Supplier = ncc;
                        InventoryList.Add(_Inventory);
                    }
                }
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
            void LoadListStatus()
            {
                ListStatus = new ObservableCollection<string>();
                ListStatus.Add("Tất cả");
                ListStatus.Add("Đang hợp tác");
                ListStatus.Add("Ngừng hợp tác");
            }
            #endregion

            #region "implement manupilation"
            btnExitCommand = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                refreshall();
                EditNhacungcapVisible = Visibility.Collapsed;
                DSNhacungcapVisible = Visibility.Visible;
            });
            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
            });
            #endregion

        }
    }
}
