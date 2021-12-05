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


        public ICommand ClickHiddenCommand { get; set; }
        public ICommand ClickHiddenFilterCommand { get; set; }
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
        public ICommand EditClickUploadImageCommand { get; set; }

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
        private ImageSource _imageviewer;
        public ImageSource ImageViewer { get => _imageviewer; set { _imageviewer = value; OnPropertyChanged(); } }

        private ImageSource _imagevieweredit;
        public ImageSource ImageViewerEdit { get => _imagevieweredit; set { _imagevieweredit = value; OnPropertyChanged(); } }

        public string FileNameImage;

        public ListofProductsViewModel()
        {
            ListAllProduct = new List<Product>();
            foreach (var data in DataProvider.Ins.DB.items)
            {
                if (data.typeItem == "book")
                {
                    var type = DataProvider.Ins.DB.bookInformations.Where(p => p.idBook == data.idItem);
                    ListAllProduct.Add(new Product() { Item = data, typeItem = type.Select(p => p.typeContent).FirstOrDefault() });
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
            BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");

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

            EditClickUploadImageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
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
                    ImageViewerEdit = bitmap;
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
                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
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
                ListTypeItem = new ObservableCollection<string>(newListDoiTuong);

            });

            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (DisplayGroupType != null || DisplayNameType != null)
                    return true;
                if (KhoangGiaTruoc >= 0 && KhoangGiaSau >= KhoangGiaTruoc)
                    return true;

                return false;
            }, (p) =>
            {
                Filter();
                BackgroudFilter = (Brush)bc.ConvertFromString("#d75c1e");
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

            ClickHiddenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
                int c = DataProvider.Ins.DB.items.Count() + 1;
                string name = c < 10 ? "BOOK00" + c : c < 100 ? "BOOK0" + c : "BOOK" + c;
                SKUProductsAdd = name;
                DescriptionProductsAdd = "Đang cập nhật";
                ImageViewer = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\không có ảnh.jpg"));
            });

            ClickAddProductCommand = new RelayCommand<object>((p) => {

                if (BarcodeProductsAdd != null && NameProductsAdd != null && CatalogProductsAdd != null && UnitProductAdd != null && TypeProductsAdd != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                string nameimage = NameProductsAdd + ".jpg";
                item Item = new item()
                {
                    idItem = SKUProductsAdd,
                    nameItem = NameProductsAdd,
                    linkItem = "Đang cập nhật",
                    imageItem = nameimage,
                    importPriceItem = 0,
                    sellPriceItem = Int32.Parse(PriceProductsAdd.ToString()),
                    descriptionItem = DescriptionProductsAdd,
                    barcode = BarcodeProductsAdd,
                    quantity = 100,
                    typeItem = CatalogProductsAdd,
                    supplierItem = "NCC002",
                    unit = UnitProductAdd
                };

                nameimage = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + nameimage);

                using (var fileStream = new FileStream(nameimage, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageViewer));
                    encoder.Save(fileStream);
                }

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

                int c = DataProvider.Ins.DB.items.Count() + 1;
                string name = c < 10 ? "BOOK00" + c : c < 100 ? "BOOK0" + c : "BOOK" + c;
                CatalogProductsAdd = null;
                TypeItemAdd = null;
                NameProductsAdd = "";
                SKUProductsAdd = name;
                BarcodeProductsAdd = null;
                PriceProductsAdd = 0;
                UnitProductAdd = null;
                ImageViewer = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\không có ảnh.jpg"));
                DescriptionProductsAdd = "Đang cập nhật";
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

                ImageSource photo = null;
                try
                {
                    photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + selected.Item.nameItem + ".jpg"));
                }
                catch (Exception ex)
                {
                    photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\không có ảnh.jpg"));

                }
                ImageViewerEdit = photo;

            });

            ClickEditUpdateProductCommand = new RelayCommand<object>((p) => {
                if (BarcodeProductsEdit != null && NameProductsEdit != null && CatalogProductsEdit != null && UnitProductsEdit != null && TypeProductsEdit != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {

                var cellItem = DataProvider.Ins.DB.items.Where(x => x.idItem == sku).SingleOrDefault();

                //cellItem.idItem = SKUProductsEdit;
                cellItem.typeItem = CatalogProductsEdit;
                cellItem.nameItem = NameProductsEdit;
                cellItem.imageItem = NameProductsEdit + ".jpg";
                cellItem.barcode = BarcodeProductsEdit;
                cellItem.sellPriceItem = PriceProductsEdit;
                cellItem.unit = UnitProductsEdit;
                cellItem.descriptionItem = DescriptionProductsEdit;

                System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + cellItem.imageItem);
                string nameimage = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + NameProductsEdit + ".jpg");

                using (var fileStream = new FileStream(nameimage, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageViewer));
                    encoder.Save(fileStream);
                }


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
