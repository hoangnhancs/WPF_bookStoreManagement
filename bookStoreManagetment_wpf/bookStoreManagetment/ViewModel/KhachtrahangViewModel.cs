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

namespace bookStoreManagetment.ViewModel
{
    public class KhachtrahangViewModel : BaseViewModel
    {
        private ObservableCollection<TrahangInfor> _InventoryList;
        public ObservableCollection<TrahangInfor> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }
        public itemtrave SelectedItemTrave { get; set; }
        public TrahangInfor SelectedItem { get; set; }
        public ICommand LoadDSKhachtrahangCommand { get; set; }

        public ICommand refreshDataGrid { get; set; }
        public ICommand tbMahoadonChangedCommand { get; set; }
        public ICommand defindSelectedItemTrave { get; set; }
        public ICommand saveTextBoxSLTVCommand { get; set; }
        public ICommand saveKhachTrahangCommand { get; set; }
        public ICommand huyKhachtrahangCommand { get; set; }
        public ICommand taoDonhangtraCommand { get; set; }
        public ICommand btnChitietClickCommand { get; set; }
        public ICommand btnThoatCommand { get; set; }
        public ICommand btnXuatfileCommand { get; set; }
        public ICommand tbSearchChangedCommand { get; set; }
        public ICommand cbbTenKHChangedCommand { get; set; }
        public ICommand cbbTenNVChangedCommand { get; set; }
        public ICommand SearchEngineer { get; set; }
        public ICommand OpenFilterCommand { get; set; }
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }
        public int testsltv { get; set; }
        public int begin = DateTime.Now.Minute;
        public int end { get; set; }
        private string _mahoadon;
        public string Mahoadon
        {
            get { return _mahoadon; }
            set
            {
                _mahoadon = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<itemtrave> _danhsachsanpham;
        public ObservableCollection<itemtrave> Danhsachsanpham
        {
            get { return _danhsachsanpham; }
            set
            {
                _danhsachsanpham = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<itemtrave> _chitietdanhsachsanpham;
        public ObservableCollection<itemtrave> ChitietDanhsachsanpham
        {
            get { return _chitietdanhsachsanpham; }
            set
            {
                _chitietdanhsachsanpham = value;
                OnPropertyChanged();
            }
        }
        private string _tennhanvien;
        public string TenNhanVien
        {
            get { return _tennhanvien; }
            set
            {
                _tennhanvien = value;
                OnPropertyChanged();
            }
        }
        private string _lido;
        public string LiDo
        {
            get { return _lido; }
            set
            {
                _lido = value;
                OnPropertyChanged();
            }
        }
        private DateTime _ngaytra;
        public DateTime NgayTra
        {
            get { return _ngaytra; }
            set
            {
                _ngaytra = value;
                OnPropertyChanged();
            }
        }
        private Visibility _dskhachtrahangvisible;
        public Visibility DSKhachtrahangVisible
        {
            get { return _dskhachtrahangvisible; }
            set
            {
                _dskhachtrahangvisible = value;
                OnPropertyChanged();
            }
        }
        private Visibility _chitiethoadontravisible;
        public Visibility ChitietHoadontraVisible
        {
            get { return _chitiethoadontravisible; }
            set
            {
                _chitiethoadontravisible = value;
                OnPropertyChanged();
            }
        }
        private Visibility _themkhachtrahangvisible;
        public Visibility ThemKhachtrahangVisible
        {
            get { return _themkhachtrahangvisible; }
            set
            {
                _themkhachtrahangvisible = value;
                OnPropertyChanged();
            }
        }

        private string _ghichu;
        public string GhiChu
        {
            get { return _ghichu; }
            set
            {
                _ghichu = value;
                OnPropertyChanged();
            }
        }
        private string _chitietmahoadon;
        public string ChitietMahoadon
        {
            get { return _chitietmahoadon; }
            set
            {
                _chitietmahoadon = value;
                OnPropertyChanged();
            }
        }
        private string _chitietnhanvien;
        public string Chitietnhanvien
        {
            get { return _chitietnhanvien; }
            set 
            {
                _chitietnhanvien = value;
                OnPropertyChanged();
            }
        }
        private string _chitietlido;
        public string Chitietlido
        {
            get { return _chitietlido; }
            set
            {
                _chitietlido = value;
                OnPropertyChanged();
            }
        }
        private DateTime _chitietthoigiandathang;
        public DateTime ChitietThoigianDathang
        {
            get { return _chitietthoigiandathang; }
            set
            {
                _chitietthoigiandathang = value;
                OnPropertyChanged();
            }
        }
        private DateTime _chitietthoigiantrahang;
        public DateTime ChitietThoigianTrahang
        {
            get { return _chitietthoigiantrahang; }
            set
            {
                _chitietthoigiantrahang = value;
                OnPropertyChanged();
            }
        }
        private string _tbsearchvalue;
        public string TextBoxSearchValue
        {
            get { return _tbsearchvalue; }
            set
            {
                _tbsearchvalue = value;
                OnPropertyChanged();
            }
        }
        private string _cbbnhanvienphutrachvalue;
        public string ComboBoxNhanvienphutrachValue
        {
            get { return _cbbnhanvienphutrachvalue; }
            set
            {
                _cbbnhanvienphutrachvalue = value;
                OnPropertyChanged();
            }
        }
        private string _cbbtenkhachhang;
        public string ComboBoxTenKhachhang
        {
            get { return _cbbtenkhachhang; }
            set
            {
                _cbbtenkhachhang = value;
                OnPropertyChanged();
            }
        }
        public string tmpcbbnhanviensearch { get; set; }
        public string tmpcbbkhachhangsearch { get; set; }
        public List<itemtrave> lstitemtrave { get; set; }
        public int total_price { get; set; }
        private ObservableCollection<string> _tenkhachhanglist;
        public ObservableCollection<string> TenkhachhangList
        {
            get { return _tenkhachhanglist; }
            set
            {
                _tenkhachhanglist = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _tennhanvienlist;
        public ObservableCollection<string> TennhanvienList
        {
            get { return _tennhanvienlist; }
            set
            {
                _tennhanvienlist = value;
                OnPropertyChanged();
            }
        }
        public KhachtrahangViewModel()
        {
            LoadDSKhachtrahangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
                LoadData();
                DSKhachtrahangVisible = Visibility.Visible;
                ThemKhachtrahangVisible = Visibility.Collapsed;
                ChitietHoadontraVisible = Visibility.Collapsed;
                NgayTra = DateTime.Now.Date;
                TextBoxSearchValue = "";
                ComboBoxTenKhachhang = "";
                ComboBoxNhanvienphutrachValue = "";
                //MessageBox.Show(InventoryList.Count().ToString());
            });

            refreshDataGrid = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
                (p as DataGrid).ItemsSource = InventoryList;
                (p as DataGrid).Items.Refresh();
            });
            tbMahoadonChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadData();
            });
            defindSelectedItemTrave = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItemTrave = p as itemtrave;
            });
            saveTextBoxSLTVCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Console.WriteLine(SelectedItemTrave.Item.idItem);
                if ((p as TextBox).Text != "")
                {
                    try
                    {
                        testsltv = Convert.ToInt32((p as TextBox).Text);
                        if (testsltv > SelectedItemTrave.BuyNumber) 
                        {
                            MessageBox.Show("Vui lòng nhập giá trị bé hơn số lượng mua");
                            testsltv = SelectedItemTrave.TraveNumber;
                            (p as TextBox).Text = SelectedItemTrave.BuyNumber.ToString();
                        }
                        else
                        {
                            for(int i=0;i<lstitemtrave.Count;i++)
                            {
                                if (lstitemtrave[i].Item.idItem == SelectedItemTrave.Item.idItem)
                                    lstitemtrave[i].TraveNumber = testsltv;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Vui lòng nhập giá trị là số nguyên");
                        testsltv = SelectedItemTrave.BuyNumber;
                        (p as TextBox).Text = SelectedItemTrave.BuyNumber.ToString();
                    }
                }
                else
                {

                }
            });
            saveKhachTrahangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Danhsachsanpham = new ObservableCollection<itemtrave>(lstitemtrave);
                DateTime now = DateTime.Now;
                string d = now.Day.ToString();
                string m = now.Month.ToString();
                string y = now.Year.ToString();
                string h = now.Hour.ToString();
                string min = now.Minute.ToString();
                string s = now.Second.ToString();
                string idbilltra = "BILL" + d + m + y + h + min + s;

                foreach (var i in Danhsachsanpham)
                {

                    int _tmpnum = i.TraveNumber;
                    var _tmpidCus = DataProvider.Ins.DB.Database.SqlQuery<string>("Select idCustomer from sellbill where billCodeSell=N'" + Mahoadon + "'").FirstOrDefault();
                    var _tmpfirstnameCus = DataProvider.Ins.DB.Database.SqlQuery<String>("Select firstName from custommer where idCustommer=N'" + _tmpidCus + "'").FirstOrDefault();
                    var _tmplastnameCus = DataProvider.Ins.DB.Database.SqlQuery<String>("Select lastName from custommer where idCustommer=N'" + _tmpidCus + "'").FirstOrDefault();
                    string _tmpnameCus = _tmplastnameCus + " " + _tmpfirstnameCus;
                    var _tmpdiscount = DataProvider.Ins.DB.Database.SqlQuery<int>("Select discount from sellbill where billCodeSell=N'" + Mahoadon + "'").FirstOrDefault();
                    var _unit = DataProvider.Ins.DB.Database.SqlQuery<String>("Select unit from item where iditem=N'" + i.Item.idItem + "'").FirstOrDefault();
                    khachtrahang _tmp = new khachtrahang();
                    _tmp.billCodeTra = idbilltra;
                    _tmp.billCodeSell = Mahoadon;
                    _tmp.nameCustomer = _tmpnameCus;
                    _tmp.number = _tmpnum;
                    _tmp.sellDate = now;
                    _tmp.trangthai = "Đã trả hàng";
                    _tmp.unit = i.Item.unit;
                    _tmp.unitPrice = i.Item.sellPriceItem;
                    _tmp.idCustomer = _tmpidCus;
                    _tmp.idItem = i.Item.idItem;
                    _tmp.discount = _tmpdiscount;
                    _tmp.lido = LiDo;
                    _tmp.nameEmployee = TenNhanVien;
                    DataProvider.Ins.DB.khachtrahangs.Add(_tmp);
                    DataProvider.Ins.DB.SaveChanges(); //cập nhật bảng tả hàng
                    var res = DataProvider.Ins.DB.items.SingleOrDefault(t => t.idItem == i.Item.idItem);
                    if (res != null)
                    {
                        res.quantity = res.quantity + _tmpnum;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                }
                clearData();

            });

            huyKhachtrahangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DSKhachtrahangVisible = Visibility.Visible;
                ThemKhachtrahangVisible = Visibility.Collapsed;
                ChitietHoadontraVisible = Visibility.Collapsed;
                clearData();
                searchEngineer(TextBoxSearchValue, ComboBoxTenKhachhang, ComboBoxNhanvienphutrachValue);
            });
            taoDonhangtraCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DSKhachtrahangVisible = Visibility.Collapsed;
                ThemKhachtrahangVisible = Visibility.Visible;
                ChitietHoadontraVisible = Visibility.Collapsed;
                //LoadData();
            });
            btnChitietClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ChitietHoadontraVisible = Visibility.Visible;
                ThemKhachtrahangVisible = Visibility.Collapsed;
                DSKhachtrahangVisible = Visibility.Collapsed;

                SelectedItem = (p as TrahangInfor);
                ChitietMahoadon = SelectedItem.BillcodeSell;
                ChitietDanhsachsanpham = new ObservableCollection<itemtrave>();
                var lstItem = DataProvider.Ins.DB.khachtrahangs.Where(i => i.billCodeSell == SelectedItem.BillcodeSell);
                //Console.WriteLine(Mahoadon + lstItem.Count().ToString());
                foreach (var item in lstItem)
                {
                    string _iditem = item.idItem;
                    item _tempitem = DataProvider.Ins.DB.items.Where(i => i.idItem == _iditem).FirstOrDefault();
                    itemtrave _newitemtrave = new itemtrave();
                    _newitemtrave.Item = _tempitem;
                    //_newitemtrave.BuyNumber = item.number;
                    _newitemtrave.TraveNumber = item.number;
                    ChitietDanhsachsanpham.Add(_newitemtrave);
                    ChitietThoigianTrahang = item.sellDate;
                    Chitietnhanvien = item.nameEmployee;
                    Chitietlido = item.lido;
                    
                }
                ChitietThoigianDathang = DataProvider.Ins.DB.Database.SqlQuery<DateTime>("select selldate from sellbill where billcodesell=N'" + ChitietMahoadon + "'").FirstOrDefault();
                GhiChu = DataProvider.Ins.DB.Database.SqlQuery<String>("select note from sellbill where billcodesell=N'" + ChitietMahoadon + "'").FirstOrDefault();
            });
            btnThoatCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DSKhachtrahangVisible = Visibility.Visible;
                ThemKhachtrahangVisible = Visibility.Collapsed;
                ChitietHoadontraVisible = Visibility.Collapsed;
                //LoadData();
            });
            btnXuatfileCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                if (excelApp != null)
                {
                    Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                    Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets.Add();

                    excelWorksheet.Cells[1, 1] = "STT";
                    excelWorksheet.Cells[1, 2] = "Mã hóa đơn trả";
                    excelWorksheet.Cells[1, 3] = "Mã hóa đơn mua";
                    excelWorksheet.Cells[1, 4] = "Mã sản phẩm";
                    excelWorksheet.Cells[1, 5] = "Tên sản phẩm";
                    excelWorksheet.Cells[1, 6] = "Đơn vị";
                    excelWorksheet.Cells[1, 7] = "Số lượng trả";
                    excelWorksheet.Cells[1, 8] = "Đơn giá";
                    excelWorksheet.Cells[1, 9] = "Thành tiền";
                    excelWorksheet.Cells[1, 10] = "Tên khách hàng";
                    excelWorksheet.Cells[1, 11] = "Số điện thoại";
                    excelWorksheet.Cells[1, 12] = "Địa chỉ";
                    excelWorksheet.Cells[1, 13] = "Tổng tiền hóa đơn";
                    excelWorksheet.Cells[1, 14] = "Ngày đặt hàng";
                    excelWorksheet.Cells[1, 15] = "Ngày giao hàng";
                    excelWorksheet.Cells[1, 16] = "Ngày trả hàng";
                    excelWorksheet.Cells[1, 17] = "Nhân viên";
                    excelWorksheet.Cells[1, 18] = "Trạng thái";
                    excelWorksheet.Cells[1, 19] = "Lí do";
                    excelWorksheet.Cells[1, 20] = "Tag";
                    excelWorksheet.Cells[1, 21] = "Ghi chú";

                    var lstKhachtarhang = DataProvider.Ins.DB.khachtrahangs;
                    int idrow = 2;
                    //int total_price;
                    
                    foreach (var row in lstKhachtarhang)
                    {

                        //string _tmpNameItem = DataProvider.Ins.DB.Database.SqlQuery<string>("select nameitem from item where iditem=N'" + row.idItem + "'").First();
                        string _tmpNameItem = DataProvider.Ins.DB.items.Where(i => i.idItem == row.idItem).First().nameItem;
                        string _tmpngaydathang = DataProvider.Ins.DB.sellBills.Where(i => i.billCodeSell == row.billCodeSell).First().sellDate.ToString();
                        string _tmpngaygiaohang = DataProvider.Ins.DB.sellBills.Where(i => i.billCodeSell == row.billCodeSell).First().deliveryDate.ToString();
                        string _tmptag = DataProvider.Ins.DB.sellBills.Where(i => i.billCodeSell == row.billCodeSell).First().tag;
                        string _tmpnote = DataProvider.Ins.DB.sellBills.Where(i => i.billCodeSell == row.billCodeSell).First().note;
                        string _tmpsdt = DataProvider.Ins.DB.custommers.Where(i => i.idCustommer == row.idCustomer).First().phoneNumber;
                        string _tmpdiachi = DataProvider.Ins.DB.custommers.Where(i => i.idCustommer == row.idCustomer).First().custommerAddress;
                        var lstnumber = DataProvider.Ins.DB.khachtrahangs.Where(i => i.billCodeTra == row.billCodeTra);
                        total_price = 0;
                        foreach (var ele in lstnumber)
                        {
                            total_price += ele.number * ele.unitPrice;
                        }
                        excelWorksheet.Cells[idrow, 1] = row.idTrahang;
                        excelWorksheet.Cells[idrow, 2] = row.billCodeTra;
                        excelWorksheet.Cells[idrow, 3] = row.billCodeSell;
                        excelWorksheet.Cells[idrow, 4] = row.idItem;
                        excelWorksheet.Cells[idrow, 5] = _tmpNameItem;
                        excelWorksheet.Cells[idrow, 6] = row.unit;
                        excelWorksheet.Cells[idrow, 7] = row.number;
                        excelWorksheet.Cells[idrow, 8] = row.unitPrice;
                        excelWorksheet.Cells[idrow, 9] = row.number * row.unitPrice;
                        excelWorksheet.Cells[idrow, 10] = row.nameCustomer;
                        excelWorksheet.Cells[idrow, 11] = _tmpsdt;
                        excelWorksheet.Cells[idrow, 12] = _tmpdiachi;
                        excelWorksheet.Cells[idrow, 13] = total_price;
                        excelWorksheet.Cells[idrow, 14] = _tmpngaydathang;
                        excelWorksheet.Cells[idrow, 15] = _tmpngaygiaohang;
                        excelWorksheet.Cells[idrow, 16] = row.sellDate;
                        excelWorksheet.Cells[idrow, 17] = row.trangthai;
                        excelWorksheet.Cells[idrow, 18] = row.lido;
                        excelWorksheet.Cells[idrow, 19] = _tmptag;
                        excelWorksheet.Cells[idrow, 20] = _tmpnote;
                        idrow++;
                    }
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    if (dlg.ShowDialog() == true)
                        //File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
                        excelApp.ActiveWorkbook.SaveAs(dlg.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

                    excelWorkbook.Close();
                    excelApp.Quit();

                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            });
            tbSearchChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                searchEngineer(TextBoxSearchValue,ComboBoxTenKhachhang,"");
            });
            cbbTenKHChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if ((p as ComboBox).SelectedItem != null)
                {
                    ComboBoxTenKhachhang = (p as ComboBox).SelectedItem.ToString();
                    Console.WriteLine(TextBoxSearchValue, ComboBoxTenKhachhang);
                    searchEngineer(TextBoxSearchValue, ComboBoxTenKhachhang, ComboBoxNhanvienphutrachValue);
                }
            });
            SearchEngineer = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                searchEngineer(TextBoxSearchValue, ComboBoxTenKhachhang, ComboBoxNhanvienphutrachValue);
            });
            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
            });
            void LoadData()
            {
                InventoryList = new ObservableCollection<TrahangInfor>();
                Danhsachsanpham = new ObservableCollection<itemtrave>();
                lstitemtrave = new List<itemtrave>();
                InventoryList.Clear();
                var lstTrahang = DataProvider.Ins.DB.khachtrahangs.GroupBy(x => x.billCodeSell).Select(g => new { billCodeSell = g.Key });
                foreach (var item in lstTrahang)
                {
                    TrahangInfor _Inventory = new TrahangInfor();
                    string billcodesell = item.billCodeSell;
                    string _namecus = DataProvider.Ins.DB.Database.SqlQuery<String>("select nameCustomer from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    string _status = DataProvider.Ins.DB.Database.SqlQuery<String>("select trangthai from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    string _lido = DataProvider.Ins.DB.Database.SqlQuery<String>("select lido from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    DateTime _ngaytra = DataProvider.Ins.DB.Database.SqlQuery<DateTime>("select sellDate from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    _Inventory.BillcodeSell = billcodesell;
                    _Inventory.NameCustomer = _namecus;
                    _Inventory.LiDo = _lido;
                    _Inventory.TrangThai = _status;
                    _Inventory.SellDate = _ngaytra;
                    InventoryList.Add(_Inventory);
                }

                Danhsachsanpham.Clear();
                
                var lstItem = DataProvider.Ins.DB.sellBills.Where(i => i.billCodeSell == Mahoadon);
                //Console.WriteLine(Mahoadon + lstItem.Count().ToString());
                foreach (var item in lstItem)
                {
                    string _iditem = item.idItem;
                    item _tempitem = DataProvider.Ins.DB.items.Where(i => i.idItem == _iditem).FirstOrDefault();
                    itemtrave _newitemtrave = new itemtrave();
                    _newitemtrave.Item = _tempitem;
                    _newitemtrave.BuyNumber = item.number;
                    _newitemtrave.TraveNumber = item.number;
                    Danhsachsanpham.Add(_newitemtrave);
                }
                lstitemtrave = Danhsachsanpham.ToList();
                TennhanvienList = new ObservableCollection<string>();
                TenkhachhangList = new ObservableCollection<string>();
                TennhanvienList.Add("Tất cả");
                TenkhachhangList.Add("Tất cả");
                var _tmplsttennhanvien = DataProvider.Ins.DB.employees.GroupBy(x => new { x.lastName, x.firstName }).Select(g => new { firstName = g.Key.firstName, lastName = g.Key.lastName });
                foreach(var _i in _tmplsttennhanvien)
                {
                    TennhanvienList.Add(_i.lastName+" "+_i.firstName);
                }
                var _tmplsttenkhachhang = DataProvider.Ins.DB.custommers.GroupBy(x => new { x.lastName, x.firstName }).Select(g => new { firstName = g.Key.firstName, lastName = g.Key.lastName });
                foreach (var _i in _tmplsttenkhachhang)
                {
                    TenkhachhangList.Add(_i.lastName + " " + _i.firstName);
                }
            }
            void searchEngineer(string tbs, string nkh, string nnv)
            {
                InventoryList = new ObservableCollection<TrahangInfor>();
                if (nnv == "Tất cả" || nnv == "") 
                    tmpcbbnhanviensearch = "";
                else
                    tmpcbbnhanviensearch = nnv;
                if (nkh == "Tất cả" || nkh == "") 
                    tmpcbbkhachhangsearch = "";
                else
                    tmpcbbkhachhangsearch = nkh;
                
                //Console.WriteLine(tmpcbbkhachhangsearch + TextBoxSearchValue);
                //var lstkhachtrahang = DataProvider.Ins.DB.khachtrahangs.Where(i => i.billCodeSell.Contains(TextBoxSearchValue) && i.nameEmployee.Contains(tmpcbbnhanviensearch) && i.nameCustomer.Contains(tmpcbbkhachhangsearch));
                var lstTrahang = DataProvider.Ins.DB.khachtrahangs.GroupBy(x => x.billCodeSell).Select(g => new { billCodeSell = g.Key }).Where(i => i.billCodeSell.Contains(tbs));
                foreach (var item in lstTrahang)
                {
                    TrahangInfor _Inventory = new TrahangInfor();
                    string billcodesell = item.billCodeSell;
                    string _namecus = DataProvider.Ins.DB.Database.SqlQuery<String>("select nameCustomer from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    string _nameemp = DataProvider.Ins.DB.Database.SqlQuery<String>("select nameEmployee from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    string _status = DataProvider.Ins.DB.Database.SqlQuery<String>("select trangthai from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    string _lido = DataProvider.Ins.DB.Database.SqlQuery<String>("select lido from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    DateTime _ngaytra = DataProvider.Ins.DB.Database.SqlQuery<DateTime>("select sellDate from khachtrahang where billCodeSell=N'" + billcodesell + "'").FirstOrDefault();
                    //Console.WriteLine(_namecus + ".");
                    //MessageBox.Show(_namecus + " " + tmpcbbkhachhangsearch);
                    if (_namecus.Contains(tmpcbbkhachhangsearch) && _nameemp.Contains(tmpcbbnhanviensearch)) 
                    {
                        _Inventory.BillcodeSell = billcodesell;
                        _Inventory.NameCustomer = _namecus;
                        _Inventory.LiDo = _lido;
                        _Inventory.TrangThai = _status;
                        _Inventory.SellDate = _ngaytra;
                        InventoryList.Add(_Inventory);
                    }
                }
            }
            void clearData()
            {
                LiDo = "";
                TenNhanVien = "";
                Mahoadon = "";
                Danhsachsanpham.Clear();
                lstitemtrave.Clear();
            }

        }
    }
}
