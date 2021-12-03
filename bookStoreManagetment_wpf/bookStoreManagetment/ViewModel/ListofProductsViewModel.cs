using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace bookStoreManagetment.ViewModel
{
    public class Product
    {
        public item Item { get; set; }
        public string typeItem { get; set; }
    }
    public class ListofProductsViewModel : BaseViewModel
    {
        #region Nhân chỉ phân trang
        //Page Property
        private ObservableCollection<Product> _DivInventoryList;
        public ObservableCollection<Product> DivInventoryList { get => _DivInventoryList; set { _DivInventoryList = value; OnPropertyChanged(); } }

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


        #endregion

        public ICommand ClickHiddenCommand { get; set; }
        public ICommand ClickAddProductCommand { get; set; }
        public ICommand ClickEditProductCommand { get; set; }
        public ICommand TextChangedSearchCommand { get; set; }
        public ICommand comboBoxCatalogofProductCommand { get; set; }
        public ICommand ClickRemoveProductCommand { get; set; }
        public ICommand ClickEditUpdateProductCommand { get; set; }
        public ICommand clickRefreshofProductCommand { get; set; }

        public ICommand OpenFilterCommand { get; set; }
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }
        public ICommand SelectionChangedNhomTheLoaiFilterCommand { get; set; }
        public ICommand ClickUploadImageCommand { get; set; }

        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }


        private ObservableCollection<Product> _listofproduct;
        public ObservableCollection<Product> ListofProduct { get => _listofproduct; set { _listofproduct = value; OnPropertyChanged(); } }

        private List<Product> _listAllproduct;
        public List<Product> ListAllProduct { get => _listAllproduct; set { _listAllproduct = value; OnPropertyChanged(); } }

        private List<Product> _backuplistAllproduct;
        public List<Product> BackUpListAllProduct { get => _backuplistAllproduct; set { _backuplistAllproduct = value; OnPropertyChanged(); } }

        

        private string _displaynametype;
        public string DisplayNameType { get => _displaynametype; set { _displaynametype = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _typeitemadd;
        public ObservableCollection<string> TypeItemAdd { get => _typeitemadd; set { _typeitemadd = value; OnPropertyChanged(); } }

        private List<string> _backuptypeitem;
        public List<string> BackupTypeItem { get => _backuptypeitem; set { _backuptypeitem = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _catalogitem;
        public ObservableCollection<string> CatalogItem { get => _catalogitem; set { _catalogitem = value; OnPropertyChanged(); } }

        private string _catalogproductsedit;
        public string CatalogProductsEdit { get => _catalogproductsedit; set { _catalogproductsedit = value; OnPropertyChanged(); } }

        private string _nameproductsedit;
        public string NameProductsEdit { get => _nameproductsedit; set { _nameproductsedit = value; OnPropertyChanged(); } }

        private string _skuproductsedit;
        public string SKUProductsEdit { get => _skuproductsedit; set { _skuproductsedit = value; OnPropertyChanged(); } }

        private string _typeproductsedit;
        public string TypeProductsEdit { get => _typeproductsedit; set { _typeproductsedit = value; OnPropertyChanged(); } }

        private string _barcodeproductsedit;
        public string BarcodeProductsEdit { get => _barcodeproductsedit; set { _barcodeproductsedit = value; OnPropertyChanged(); } }

        private int _priceproductsedit;
        public int PriceProductsEdit { get => _priceproductsedit; set { _priceproductsedit = value; OnPropertyChanged(); } }

        private string _unitproductsedit;
        public string UnitProductsEdit { get => _unitproductsedit; set { _unitproductsedit = value; OnPropertyChanged(); } }

        private string _imageproductsedit;
        public string ImageProductsEdit { get => _imageproductsedit; set { _imageproductsedit = value; OnPropertyChanged(); } }

        private string _descriptionproductsedit;
        public string DescriptionProductsEdit { get => _descriptionproductsedit; set { _descriptionproductsedit = value; OnPropertyChanged(); } }

        private string _catalogproductsadd;
        public string CatalogProductsAdd { get => _catalogproductsadd; set { _catalogproductsadd = value; OnPropertyChanged(); } }

        private string _nameproductsadd;
        public string NameProductsAdd { get => _nameproductsadd; set { _nameproductsadd = value; OnPropertyChanged(); } }

        private string _typeproductsadd;
        public string TypeProductsAdd { get => _typeproductsadd; set { _typeproductsadd = value; OnPropertyChanged(); } }

        private string _skuproductsadd;
        public string SKUProductsAdd { get => _skuproductsadd; set { _skuproductsadd = value; OnPropertyChanged(); } }

        private string _barcodeproductsadd;
        public string BarcodeProductsAdd { get => _barcodeproductsadd; set { _barcodeproductsadd = value; OnPropertyChanged(); } }

        private int _priceproductsadd;
        public int PriceProductsAdd { get => _priceproductsadd; set { _priceproductsadd = value; OnPropertyChanged(); } }

        private string _unitproductsadd;
        public string UnitProductAdd { get => _unitproductsadd; set { _unitproductsadd = value; OnPropertyChanged(); } }

        private string _imageproductsadd;
        public string ImageProductsAdd { get => _imageproductsadd; set { _imageproductsadd = value; OnPropertyChanged(); } }

        private string _descriptionproductsadd;
        public string DescriptionProductsAdd { get => _descriptionproductsadd; set { _descriptionproductsadd = value; OnPropertyChanged(); } }

        private string _selected;
        public string Selected { get => _selected; set { _selected = value; OnPropertyChanged(); } }

        private string _query;
        public string Query { get => _query; set { _query = value; OnPropertyChanged(); } }

        public string sku;
        // Bộ lọc - Danh mục
        private ObservableCollection<string> _danhmucsanpham;
        public ObservableCollection<string> DanhMucSanPham { get => _danhmucsanpham; set { _danhmucsanpham = value; OnPropertyChanged(); } }

        // nhóm Danh mục filter
        private string _DisplayGroupType;
        public string DisplayGroupType { get => _DisplayGroupType; set { _DisplayGroupType = value; OnPropertyChanged(); } }

        //Bộ lọc - Danh sánh thể loại theo danh mục
        private ObservableCollection<string> _typeitem;
        public ObservableCollection<string> ListTypeItem { get => _typeitem; set { _typeitem = value; OnPropertyChanged(); } }

        //Biến khoảng giá của bộ lọc
        private int _khoanggiatruoc;
        public int KhoangGiaTruoc { get => _khoanggiatruoc; set { _khoanggiatruoc = value; OnPropertyChanged(); } }

        private int _khoanggiasau;
        public int KhoangGiaSau { get => _khoanggiasau; set { _khoanggiasau = value; OnPropertyChanged(); } }

        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }

        // foreground
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }


        //uoload ảnh
        private BitmapImage _imageviewer;
        public BitmapImage ImageViewer { get => _imageviewer; set { _imageviewer = value; OnPropertyChanged(); } }

        public string FileNameImage;

        public ListofProductsViewModel()
        {
            ListAllProduct = new List<Product>();
            foreach (var data in DataProvider.Ins.DB.items)
            {
                if (data.typeItem == "book")
                {
                    var type = DataProvider.Ins.DB.bookInformations.Where(p => p.idBook == data.idItem);
                    ListAllProduct.Add(new Product() { Item = data, typeItem = type.Select(p => p.typeContent).FirstOrDefault()});
                }
                else
                {
                    var type = DataProvider.Ins.DB.studytoolsInformations.Where(p => p.idStudyTool == data.idItem);
                    ListAllProduct.Add(new Product() { Item = data, typeItem = type.Select(p => p.typecontent).FirstOrDefault() });
                }

            }


            List<string> backupDanhmuc = new List<string>();

            backupDanhmuc.Add("book");
            backupDanhmuc.Add("Dụng cụ học tập");

            DanhMucSanPham = new ObservableCollection<string>(backupDanhmuc);

            ListofProduct = new ObservableCollection<Product>(ListAllProduct);
            BackUpListAllProduct = new List<Product>(ListAllProduct);
            //ListofProduct = new ObservableCollection<item>(ListAllProduct);
            BackupTypeItem = new List<string>(DataProvider.Ins.DB.bookInformations.Select(p => p.typeContent).ToList().Distinct().ToList());
            ListTypeItem = new ObservableCollection<String>();
            TypeItemAdd = new ObservableCollection<string>(BackupTypeItem);
            CatalogItem = new ObservableCollection<String>(DataProvider.Ins.DB.items.Select(p => p.typeItem).ToList().Distinct().ToList());
            // Load bộ lọc
            IsFilter = Visibility.Collapsed;
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
            ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");

            NumRowEachPageTextBox = "5";
            NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
            currentpage = 1;
            pack_page = 1;
            settingButtonNextPrev();




            ClickUploadImageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
                dlg.InitialDirectory = "c:\\";
                dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
                dlg.RestoreDirectory = true;

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFileName = dlg.FileName;
                    FileNameImage = selectedFileName;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedFileName);
                    bitmap.EndInit();
                    ImageViewer = bitmap;
                    //ImageViewer1.Source = bitmap;
                }
            });

            // đóng filter grid
            CloseFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
            });

            // xoá filter
            DeleteFilterCommand = new RelayCommand<object>((p) => {
                if (DisplayGroupType != null || DisplayNameType != null || KhoangGiaTruoc >= 0 || KhoangGiaSau > KhoangGiaTruoc)
                    return true;
                return false;
            }, (p) =>
            {
                KhoangGiaTruoc = 0;
                KhoangGiaSau = 0;
                DisplayNameType = "";
                DisplayNameType = null;
                DisplayGroupType = "";
                DisplayGroupType = null;
                ListofProduct = new ObservableCollection<Product>(BackUpListAllProduct);
                BackgroudFilter = (Brush)bc.ConvertFromString("#00FFFFFF");
                ForegroudFilter = (Brush)bc.ConvertFromString("#FF000000");
            });

            // sự kiện thay đổi lựu chọn nhóm người nhận filter
            SelectionChangedNhomTheLoaiFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var newListDoiTuong = new List<string>();
                if (DisplayGroupType == "book")
                {
                    newListDoiTuong = BackupTypeItem;
                }
                else if (DisplayGroupType == "Dụng cụ học tập")
                {
                    newListDoiTuong = DataProvider.Ins.DB.studytoolsInformations.Select(pa => pa.typecontent).ToList().Distinct().ToList();
                }
                ListTypeItem = new ObservableCollection<string>( newListDoiTuong );

            });

            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (DisplayGroupType != null || DisplayNameType != null)
                    return true;
                if (KhoangGiaTruoc >= 0  && KhoangGiaSau >= KhoangGiaTruoc)
                    return true;
                
                return false;
            }, (p) =>
            {
                Filter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#FF008000");
                ForegroudFilter = (Brush)bc.ConvertFromString("#CCFFFFFF");
                currentpage = 1;
                pack_page = 1;
                settingButtonNextPrev();
            });


            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
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

            ClickAddProductCommand = new RelayCommand<object>((p) => {
                Console.WriteLine(Int32.Parse(PriceProductsAdd.ToString()));
                return true;
            }, (p) =>
            {
                item Item = new item()
                {
                    idItem = SKUProductsAdd,
                    nameItem = NameProductsAdd,
                    linkItem = "Đang cập nhật",
                    imageItem = "FileNameImage",
                    importPriceItem = 0,
                    sellPriceItem = Int32.Parse(PriceProductsAdd.ToString()),
                    descriptionItem = DescriptionProductsAdd,
                    barcode = BarcodeProductsAdd,
                    quantity = 100,
                    typeItem = CatalogProductsAdd,
                    supplierItem = "NCC002",
                    unit = UnitProductAdd
                };

                DataProvider.Ins.DB.items.Add(Item);

                Product product = new Product()
                {
                    Item = Item,
                    typeItem = TypeProductsAdd
                };

                if (CatalogProductsAdd == "book")
                {
                    bookInformation bookinformation = new bookInformation()
                    {
                        idBook = SKUProductsAdd,
                        typeContent = TypeProductsAdd,
                        typeMaterial = "giấy",
                        size = "Đang cập nhật",
                        numberOfPages = 250,
                        author = "Đang cập nhật",
                        translator = "Đang cập nhật",
                        NXB = "Đang cập nhật",
                        NPH = "Đang cập nhật"
                    };
                    DataProvider.Ins.DB.bookInformations.Add(bookinformation);
                }
                else
                {
                    studytoolsInformation studytoolsinformation = new studytoolsInformation()
                    {
                        idStudyTool = SKUProductsAdd,
                        typecontent = TypeProductsAdd,
                        origin = "",
                        distributor = "",
                    };
                    DataProvider.Ins.DB.studytoolsInformations.Add(studytoolsinformation);
                }

                DataProvider.Ins.DB.SaveChanges();
                BackUpListAllProduct.Add(product);
                ListAllProduct.Add(product);
                ListofProduct = new ObservableCollection<Product>(ListAllProduct);
                (p as DataGrid).Items.Refresh();
                MessageBox.Show("Thêm thành công!");
            });

            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {

                    if (Query == "")
                    {
                        ListAllProduct = new List<Product>(BackUpListAllProduct);
                        ListofProduct = new ObservableCollection<Product>(ListAllProduct);
                    }
                    else
                    {
                        ListofProduct = new ObservableCollection<Product>();
                        foreach (var cellItems in ListAllProduct)
                        {
                            string nameItem = cellItems.Item.nameItem.Trim().ToLower();
                            string idItem = cellItems.Item.idItem.Trim().ToLower();
                            if (nameItem.Contains(Query.Trim().ToLower()) || idItem.Contains(Query.Trim().ToLower()))
                            {
                                ListofProduct.Add(cellItems);
                            }
                        }
                    }
                    currentpage = 1;
                    pack_page = 1;
                    settingButtonNextPrev();
                }
            });

            comboBoxCatalogofProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as ComboBox).SelectedItem as String;
                    if (query == "Tất cả sản phẩm" && Query == "")
                    {
                        ListAllProduct = new List<Product>(BackUpListAllProduct);
                        ListofProduct = new ObservableCollection<Product>(ListAllProduct);
                    }
                    else
                    {
                        ListofProduct = new ObservableCollection<Product>();
                        foreach (Product cellItems in ListAllProduct)
                        {
                            string nameItem = cellItems.Item.nameItem.Trim().ToLower();
                            string nameTypeItem = cellItems.typeItem.Trim().ToLower();
                            if (nameItem.Contains(Query.Trim().ToLower()) && nameTypeItem.Contains(query.Trim().ToLower()))
                            {
                                ListofProduct.Add(cellItems);
                            }
                        }
                    }

                }
            });

            ClickEditProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var selected = (p as Product);
                var cellItem = DataProvider.Ins.DB.items.Where(pa => pa.idItem == selected.Item.idItem);
                CatalogProductsEdit = cellItem.Select(pa => pa.typeItem).FirstOrDefault();
                TypeProductsEdit = selected.typeItem;
                NameProductsEdit = selected.Item.nameItem;
                SKUProductsEdit = selected.Item.idItem;
                sku = SKUProductsEdit;
                BarcodeProductsEdit = cellItem.Select(pa => pa.barcode).FirstOrDefault();
                PriceProductsEdit = selected.Item.sellPriceItem;
                UnitProductsEdit = cellItem.Select(pa => pa.unit).FirstOrDefault();
                DescriptionProductsEdit = cellItem.Select(pa => pa.descriptionItem).FirstOrDefault();

            });

            ClickEditUpdateProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var cellItem = DataProvider.Ins.DB.items.Where(x => x.idItem == sku).SingleOrDefault();

                //cellItem.idItem = SKUProductsEdit;
                cellItem.typeItem = CatalogProductsEdit;
                cellItem.nameItem = NameProductsEdit;
                cellItem.barcode = BarcodeProductsEdit;
                cellItem.sellPriceItem = PriceProductsEdit;
                cellItem.unit = UnitProductsEdit;
                cellItem.descriptionItem = DescriptionProductsEdit;

                if (CatalogProductsEdit == "book")
                {
                    var cellItemEdit = DataProvider.Ins.DB.bookInformations.Where(x => x.idBook == sku).SingleOrDefault();
                    //cellItemEdit.idBook = SKUProductsEdit;
                    cellItemEdit.typeContent = TypeProductsEdit;

                    var cellitemEdit = ListofProduct.Where(x => x.Item.idItem == sku).SingleOrDefault();
                    cellitemEdit.typeItem = TypeProductsEdit;
                    //cellitemEdit.idItem = SKUProductsEdit;
                    cellitemEdit.Item.nameItem = NameProductsEdit;
                    cellitemEdit.Item.sellPriceItem = PriceProductsEdit;
                    sku = SKUProductsEdit;
                }
                else
                {
                    var cellItemEdit = DataProvider.Ins.DB.studytoolsInformations.Where(x => x.idStudyTool == sku).SingleOrDefault();
                    //cellItemEdit.idStudyTool = SKUProductsEdit;
                    cellItemEdit.typecontent = TypeProductsEdit;

                    var cellitemEdit = ListofProduct.Where(x => x.Item.idItem == sku).SingleOrDefault();
                    cellitemEdit.typeItem = TypeProductsEdit;
                    //cellitemEdit.idItem = SKUProductsEdit;
                    cellitemEdit.Item.nameItem = NameProductsEdit;
                    cellitemEdit.Item.sellPriceItem = PriceProductsEdit;
                    sku = SKUProductsEdit;
                }

                DataProvider.Ins.DB.SaveChanges();

                (p as DataGrid).Items.Refresh();
                MessageBox.Show("Cập nhật thành công!");
            });

            clickRefreshofProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListofProduct = new ObservableCollection<Product>(BackUpListAllProduct);
                Selected = "Tất cả sản phẩm";
                Query = "";
            });

            ClickRemoveProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var cellItem = p as Product;
                var cellTableItem = DataProvider.Ins.DB.items.Where(x => x.idItem == cellItem.Item.idItem).SingleOrDefault();
                if (cellTableItem.typeItem == "book")
                {
                    var cellitem = DataProvider.Ins.DB.bookInformations.Where(x => x.idBook == cellTableItem.idItem).SingleOrDefault();
                    DataProvider.Ins.DB.items.Remove(cellTableItem);
                    DataProvider.Ins.DB.bookInformations.Remove(cellitem);
                }
                else if (cellTableItem.typeItem == "Văn phòng phẩm")
                {
                    var cellitem = DataProvider.Ins.DB.studytoolsInformations.Where(x => x.idStudyTool == cellTableItem.idItem).SingleOrDefault();
                    DataProvider.Ins.DB.items.Remove(cellTableItem);
                    DataProvider.Ins.DB.studytoolsInformations.Remove(cellitem);
                }
                BackUpListAllProduct.Remove(cellItem);
                ListAllProduct.Remove(cellItem);
                ListofProduct.Remove(cellItem);
                DataProvider.Ins.DB.SaveChanges();
            });

            tbNumRowEachPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                currentpage = 1;
                //LoadData();
                Filter();
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

                Filter();
                settingButtonNextPrev();
            });

        }

        void settingButtonNextPrev()
        {
            int ilc = ListofProduct.Count();
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
                DivInventoryList = new ObservableCollection<Product>();
                DivInventoryList.Clear();
                int startPos = (currentpage - 1) * NumRowEachPage;
                int endPos = currentpage * NumRowEachPage - 1;
                if (endPos >= ilc)
                    endPos = ilc - 1;

                int flag = 0;
                foreach (var item in ListofProduct)
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
                    if ((pack_page * 3) > maxpage)
                        BtnPage3.PageVisi = Visibility.Collapsed;
                    if ((pack_page * 3 - 1) > maxpage)
                        BtnPage2.PageVisi = Visibility.Collapsed;
                }

            }
        }

        private void Filter()
        {
            List<Product> newListExportBill = ListAllProduct;
            if (DisplayGroupType != null && DisplayGroupType != "")
            {
                newListExportBill = newListExportBill.Where(x => x.Item.typeItem == DisplayGroupType).ToList();
            }

            if (DisplayNameType != null && DisplayNameType != "")
            {
                newListExportBill = newListExportBill.Where(x => x.typeItem == DisplayNameType).ToList();
            }

            if (KhoangGiaTruoc >= 0 && KhoangGiaSau > KhoangGiaTruoc)
            {
                List<Product> temp = new List<Product>();
                foreach (var bill in newListExportBill)
                {
                    int gia = bill.Item.sellPriceItem;
                    if (gia >= KhoangGiaTruoc
                     && gia <= KhoangGiaSau)
                    {
                        temp.Add(bill);
                    }
                }
                newListExportBill = temp;
            }

            ListofProduct = new ObservableCollection<Product>(newListExportBill);
        }
    }
}
