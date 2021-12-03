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
        public ICommand clearFilter { get; set; }
        private bool _isenablefilterbutton;
        public bool isEnableFilterButton { get { return _isenablefilterbutton; } set { _isenablefilterbutton = value; OnPropertyChanged(); } }

        #region "filter"
        //trang thai loc
        private string _currentStatus;
        public string CurrentStatus { get { return _currentStatus; } set { _currentStatus = value; OnPropertyChanged(); isEnableFilterButton = true; } }
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

        #region "page"
        //Page Property
        private ObservableCollection<Inventory> _DivInventoryList;
        public ObservableCollection<Inventory> DivInventoryList { get => _DivInventoryList; set { _DivInventoryList = value; OnPropertyChanged(); } }

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

        public NhacungcapViewMode()
        {
            //phan nay cua danh sach nha cung cap

            //load nha cung cap user control
            LoadNhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                textBoxSearchValue = "";
                CurrentStatus = "";
                IsFilter = Visibility.Collapsed;
                EditNhacungcapVisible = Visibility.Collapsed;
                DSNhacungcapVisible = Visibility.Visible;
                LoadListStatus();
                SearchEngineer(textBoxSearchValue, CurrentStatus);
                //page
                NumRowEachPageTextBox = "5";
                NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
                currentpage = 1;
                pack_page = 1;
                settingButtonNextPrev();
                isEnableFilterButton = false;
                //page
            });
            clearFilter = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentStatus = "";
                isEnableFilterButton = false;
                SearchEngineer(textBoxSearchValue, CurrentStatus);
                settingButtonNextPrev();
            });
            #region "select num row each page"
            tbNumRowEachPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                currentpage = 1;
                //LoadData();
                SearchEngineer(textBoxSearchValue, CurrentStatus);
                settingButtonNextPrev();
            });
            btnNextClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (currentpage < maxpage)
                {
                    currentpage += 1;
                    if (currentpage % 3 == 0)
                        pack_page = currentpage / 3;
                    else
                        pack_page = Convert.ToInt32(currentpage / 3) + 1;
                    //MessageBox.Show("Max page is" + maxpage.ToString()+"pack_page is"+pack_page.ToString());
                }
                settingButtonNextPrev();
            });
            btnendPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                currentpage = maxpage;
                pack_page = max_pack_page;
                settingButtonNextPrev();
            });
            btnfirstPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                currentpage = 1;
                pack_page = 1;
                settingButtonNextPrev();
            });
            btnPrevPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (currentpage > 1) 
                {
                    currentpage -= 1;
                    if (currentpage % 3 == 0)
                        pack_page = currentpage / 3;
                    else
                        pack_page = Convert.ToInt32(currentpage / 3) + 1;
                    //MessageBox.Show("Max page is" + maxpage.ToString()+"pack_page is"+pack_page.ToString());
                }
                settingButtonNextPrev();
            });
            btnLoc2Command = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                SearchEngineer(textBoxSearchValue, CurrentStatus);
                settingButtonNextPrev();
            });

            #endregion //here

            #region "setting button"

            void settingButtonNextPrev()
            {
                int ilc = InventoryList.Count();
                BtnPage1 = new page();
                BtnPage2 = new page();
                BtnPage3 = new page();

                //currentpage = 1;

                if (NumRowEachPageTextBox != "")
                {
                    //init max page
                    NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
                    if (ilc % NumRowEachPage == 0)
                        maxpage = ilc / NumRowEachPage;
                    else
                        maxpage = Convert.ToInt32((ilc / NumRowEachPage)) + 1;
                    if (maxpage % 3 == 0)
                        max_pack_page = maxpage / 3;
                    else
                        max_pack_page = Convert.ToInt32(maxpage / 3) + 1;


                    //Init max page
                    DivInventoryList = new ObservableCollection<Inventory>();
                    DivInventoryList.Clear();
                    int startPos = (currentpage - 1) * NumRowEachPage;
                    int endPos = currentpage * NumRowEachPage - 1;
                    if (endPos >= ilc)
                        endPos = ilc - 1;

                    int flag = 0;
                    foreach (var item in InventoryList)
                    {
                        if (flag >= startPos && flag <= endPos)
                            DivInventoryList.Add(item);
                        flag++;
                    }
                    //MessageBox.Show(DivInventoryList.Count.ToString());

                    //Button "..." visible

                    //MessageBox.Show("max page is" + maxpage.ToString()+"current page is"+currentpage.ToString());
                    //MessageBox.Show("Max pack page is" + max_pack_page.ToString() + "pack_page is" + pack_page.ToString());
                    if (max_pack_page == 1)
                    {
                        Bacham1Visible = Visibility.Collapsed;
                        Bacham2Visible = Visibility.Collapsed;
                    }
                    else
                    {
                        if (pack_page == max_pack_page)
                        {
                            Bacham1Visible = Visibility.Visible;
                            Bacham2Visible = Visibility.Collapsed;
                        }
                        else
                        {
                            if (pack_page == 1)
                            {
                                Bacham1Visible = Visibility.Collapsed;
                                Bacham2Visible = Visibility.Visible;
                            }
                            else
                            {
                                Bacham1Visible = Visibility.Visible;
                                Bacham2Visible = Visibility.Visible;
                            }
                        }
                    }


                    //Button "..." visible


                    if (currentpage == 1 && maxpage == 1)
                    {
                        LeftVisi = false;
                        RightVisi = true;
                    }
                    else
                    {
                        if (currentpage == maxpage)
                        {
                            LeftVisi = true;
                            RightVisi = false;
                        }
                        else
                        {
                            if (currentpage == 1)
                            {
                                LeftVisi = false;
                                RightVisi = true;
                            }
                            else
                            {
                                LeftVisi = true;
                                RightVisi = true;
                            }
                        }
                    }


                    if (maxpage >= 3)
                    {
                        BtnPage1.PageVisi = Visibility.Visible;
                        BtnPage2.PageVisi = Visibility.Visible;
                        BtnPage3.PageVisi = Visibility.Visible;

                        switch (currentpage % 3)
                        {
                            case 1:
                                BtnPage1.BackGround = Brushes.Blue;
                                BtnPage2.BackGround = Brushes.White;
                                BtnPage3.BackGround = Brushes.White;
                                BtnPage1.PageVal = currentpage;
                                BtnPage2.PageVal = currentpage + 1;
                                BtnPage3.PageVal = currentpage + 2;
                                break;
                            case 2:
                                BtnPage1.BackGround = Brushes.White;
                                BtnPage2.BackGround = Brushes.Blue;
                                BtnPage3.BackGround = Brushes.White;
                                BtnPage1.PageVal = currentpage - 1;
                                BtnPage2.PageVal = currentpage;
                                BtnPage3.PageVal = currentpage + 1;
                                break;
                            case 0:
                                BtnPage1.BackGround = Brushes.White;
                                BtnPage2.BackGround = Brushes.White;
                                BtnPage3.BackGround = Brushes.Blue;
                                BtnPage1.PageVal = currentpage - 2;
                                BtnPage2.PageVal = currentpage - 1;
                                BtnPage3.PageVal = currentpage;
                                break;
                        }
                    }
                    else
                    {
                        if (maxpage == 2)
                        {
                            BtnPage1.PageVisi = Visibility.Visible;
                            BtnPage2.PageVisi = Visibility.Visible;
                            BtnPage3.PageVisi = Visibility.Collapsed;
                            switch (currentpage)
                            {
                                case 1:
                                    BtnPage1.BackGround = Brushes.Blue;
                                    BtnPage2.BackGround = Brushes.White;
                                    BtnPage1.PageVal = currentpage;
                                    BtnPage2.PageVal = currentpage + 1;
                                    break;
                                case 2:
                                    BtnPage1.BackGround = Brushes.White;
                                    BtnPage2.BackGround = Brushes.Blue;
                                    BtnPage1.PageVal = currentpage - 1;
                                    BtnPage2.PageVal = currentpage;
                                    break;
                            }
                        }
                        else
                        {
                            BtnPage1.PageVisi = Visibility.Visible;
                            BtnPage2.PageVisi = Visibility.Collapsed;
                            BtnPage3.PageVisi = Visibility.Collapsed;
                            BtnPage1.PageVal = (currentpage - 1) * NumRowEachPage + 1; ;
                            BtnPage1.BackGround = Brushes.Blue;
                            BtnPage1.PageVal = currentpage;
                        }
                    }
                    if (pack_page == max_pack_page)
                    {
                        switch(pack_page*3-maxpage)
                        {
                            case 1:
                                BtnPage3.PageVisi = Visibility.Collapsed;
                                break;
                            case 2:
                                BtnPage2.PageVisi = Visibility.Collapsed;
                                BtnPage3.PageVisi = Visibility.Collapsed;
                                break;
                        }
                    }

                }
            }
            #endregion

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
                //MessageBox.Show(lstadd[0] + "," + lstadd[1] + "," + lstadd[2] + "," + lstadd[3]);
                sonha = lstadd[0];
                xa = lstadd[1].Replace(" xã ", "");
                huyen = lstadd[2].Replace(" huyện ", "");
                tinh = lstadd[3].Replace(" tỉnh ", "");
                phoneSup = SelectedItem.Supplier.phoneNumberSupplier;
                website = SelectedItem.Supplier.website;
                fax = SelectedItem.Supplier.fax;
                masothue = SelectedItem.Supplier.masothue;
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
                if (MessageBox.Show("Bạn có muốn thêm?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                        _tmp.fax = fax;
                        _tmp.website = website;
                        _tmp.masothue = masothue;
                        DataProvider.Ins.DB.suppliers.Add(_tmp);
                        DataProvider.Ins.DB.SaveChanges();
                        SearchEngineer(textBoxSearchValue, CurrentStatus);

                        settingButtonNextPrev();
                        refreshall();
                        MessageBox.Show("Thêm nhà cung cấp thành công");
                    }
                    else
                        MessageBox.Show("Vui lòng điền đủ thông tin");
                }

            }
            //ham update nha cung cap
            void updateSupplier()
            {
                if (MessageBox.Show("Bạn có muốn cập nhật?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = DataProvider.Ins.DB.suppliers.SingleOrDefault(i => i.idSupplier == idSup);
                    addressSup = String.Format("{0}, xã {1}, huyện {1}, tỉnh {2}", sonha, xa, huyen, tinh);
                    if (res != null)
                    {
                        res.nameSupplier = nameSup;
                        res.addressSupplier = addressSup;
                        res.emailSupplier = emailSup;
                        res.phoneNumberSupplier = phoneSup;
                        res.website = website;
                        res.fax = fax;
                        res.masothue = masothue;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Cập nhật thành công");
                }
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
                if (MessageBox.Show("Bạn có muốn thoát?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    refreshall();
                    EditNhacungcapVisible = Visibility.Collapsed;
                    DSNhacungcapVisible = Visibility.Visible;
                }
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
