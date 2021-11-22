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
    public class Product
    {
        public string idItem { get; set; }
        public string nameItem { get; set; }
        public string typeItem { get; set; }
        public int quantity { get; set; }
        public int priceItem { get; set; }
    }
    public class ListofProductsViewModel : BaseViewModel
    {

        public ICommand ClickHiddenCommand { get; set; }
        public ICommand ClickAddProductCommand { get; set; }
        public ICommand ClickEditProductCommand { get; set; }
        public ICommand textBoxSearchListofProductCommand { get; set; }
        public ICommand comboBoxCatalogofProductCommand { get; set; }
        public ICommand ClickRemoveProductCommand { get; set; }
        public ICommand ClickEditUpdateProductCommand { get; set; }
        public ICommand clickRefreshofProductCommand { get; set; }


        private ObservableCollection<Product> _listofproduct;
        public ObservableCollection<Product> ListofProduct { get => _listofproduct; set { _listofproduct = value; OnPropertyChanged(); } }

        private List<Product> _listAllproduct;
        public List<Product> ListAllProduct { get => _listAllproduct; set { _listAllproduct = value; OnPropertyChanged(); } }

        private List<Product> _backuplistAllproduct;
        public List<Product> BackUpListAllProduct { get => _backuplistAllproduct; set { _backuplistAllproduct = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _typeitem;
        public ObservableCollection<string> TypeItem { get => _typeitem; set { _typeitem = value; OnPropertyChanged(); } }

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

        private string _comboboxsearch;
        public string comBoBoxSearch { get => _comboboxsearch; set { _comboboxsearch = value; OnPropertyChanged(); } }

        public string sku;

        private ObservableCollection<string> _catalogitem1;
        public ObservableCollection<string> CatalogItem1 { get => _catalogitem1; set { _catalogitem1 = value; OnPropertyChanged(); } }

        public ListofProductsViewModel()
        {
            ListAllProduct = new List<Product>();
            foreach (var data in DataProvider.Ins.DB.items)
            {
                if (data.typeItem == "book")
                {
                    var type = DataProvider.Ins.DB.bookInformations.Where(p => p.idBook == data.idItem);
                    ListAllProduct.Add(new Product() { idItem = data.idItem, nameItem = data.nameItem, typeItem = type.Select(p => p.typeContent).FirstOrDefault(), quantity = data.quantity, priceItem = data.importPriceItem });
                } 
                else
                {
                    var type = DataProvider.Ins.DB.studytoolsInformations.Where(p => p.idStudyTool == data.idItem);
                    ListAllProduct.Add(new Product() { idItem = data.idItem, nameItem = data.nameItem, typeItem = type.Select(p => p.typeContent).FirstOrDefault(), quantity = data.quantity, priceItem = data.importPriceItem });
                }
                
            }

            ListofProduct = new ObservableCollection<Product>(ListAllProduct);
            BackUpListAllProduct = new List<Product>(ListAllProduct);
            //ListofProduct = new ObservableCollection<item>(ListAllProduct);
            BackupTypeItem = new List<string>();
            BackupTypeItem.Add("Tất cả sản phẩm");
            BackupTypeItem.AddRange(DataProvider.Ins.DB.bookInformations.Select(p => p.typeContent).ToList().Distinct().ToList());
            TypeItem = new ObservableCollection<String>(BackupTypeItem);
            BackupTypeItem.Remove("Tất cả sản phẩm");
            TypeItemAdd = new ObservableCollection<string>(BackupTypeItem);
            CatalogItem = new ObservableCollection<String>(DataProvider.Ins.DB.items.Select(p => p.typeItem).ToList().Distinct().ToList());

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
                    imageItem = "Đang cập nhật",
                    importPriceItem = Int32.Parse(PriceProductsAdd.ToString()),
                    sellPriceItem = 1000000,
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
                    idItem = SKUProductsAdd,
                    nameItem = NameProductsAdd,
                    typeItem = TypeProductsAdd,
                    quantity = 100,
                    priceItem = Int32.Parse(PriceProductsAdd.ToString())
                };

                if (CatalogProductsAdd == "book")
                {
                    bookInformation bookinformation = new bookInformation()
                    {
                        idInformation = DataProvider.Ins.DB.bookInformations.Count() + 1,
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
                        idInformation = DataProvider.Ins.DB.studytoolsInformations.Count() + 1,
                        idStudyTool = SKUProductsAdd,
                        typeContent = TypeProductsAdd,
                        origin = "",
                        distributor="",
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

            textBoxSearchListofProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text.Trim().ToLower();
                    
                    if (query == "")
                    {
                        ListAllProduct = new List<Product>(BackUpListAllProduct);
                        ListofProduct = new ObservableCollection<Product>(ListAllProduct);
                    }
                    else
                    {
                        ListofProduct = new ObservableCollection<Product>();
                        foreach (var cellItems in ListAllProduct)
                        {
                            string nameItem = cellItems.nameItem.Trim().ToLower();

                            if (nameItem.Contains(query))
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
                    if (query == "Tất cả sản phẩm")
                    {
                        ListAllProduct = new List<Product>(BackUpListAllProduct);
                        ListofProduct = new ObservableCollection<Product>(ListAllProduct);
                    }
                    else
                    {
                        ListofProduct = new ObservableCollection<Product>();
                        foreach (Product cellItems in ListAllProduct)
                        {
                            string nameTypeItem = cellItems.typeItem.Trim().ToLower();
                            if (nameTypeItem.Contains(query.Trim().ToLower()))
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
                var cellItem = DataProvider.Ins.DB.items.Where(pa => pa.idItem == selected.idItem);
                CatalogProductsEdit = cellItem.Select(pa => pa.typeItem).FirstOrDefault();
                TypeProductsEdit = selected.typeItem;
                NameProductsEdit = selected.nameItem;
                SKUProductsEdit = selected.idItem;
                sku = SKUProductsEdit;
                BarcodeProductsEdit = cellItem.Select(pa => pa.barcode).FirstOrDefault();
                PriceProductsEdit = selected.priceItem;
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
                cellItem.importPriceItem = PriceProductsEdit;
                cellItem.unit = UnitProductsEdit;
                cellItem.descriptionItem = DescriptionProductsEdit;

                if (CatalogProductsEdit == "book")
                {
                    var cellItemEdit = DataProvider.Ins.DB.bookInformations.Where(x => x.idBook == sku).SingleOrDefault();
                    //cellItemEdit.idBook = SKUProductsEdit;
                    cellItemEdit.typeContent = TypeProductsEdit;

                    var cellitemEdit = ListofProduct.Where(x => x.idItem == sku).SingleOrDefault();
                    cellitemEdit.typeItem = TypeProductsEdit;
                    //cellitemEdit.idItem = SKUProductsEdit;
                    cellitemEdit.nameItem = NameProductsEdit;
                    cellitemEdit.priceItem = PriceProductsEdit;
                    sku = SKUProductsEdit;
                }    
                else
                {
                    var cellItemEdit = DataProvider.Ins.DB.studytoolsInformations.Where(x => x.idStudyTool == sku).SingleOrDefault();
                    //cellItemEdit.idStudyTool = SKUProductsEdit;
                    cellItemEdit.typeContent = TypeProductsEdit;

                    var cellitemEdit = ListofProduct.Where(x => x.idItem == sku).SingleOrDefault();
                    cellitemEdit.typeItem = TypeProductsEdit;
                    //cellitemEdit.idItem = SKUProductsEdit;
                    cellitemEdit.nameItem = NameProductsEdit;
                    cellitemEdit.priceItem = PriceProductsEdit;
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
                comBoBoxSearch = "";
            });

            ClickRemoveProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var cellItem = p as Product;
                var cellTableItem = DataProvider.Ins.DB.items.Where(x => x.idItem == cellItem.idItem).SingleOrDefault();
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
    }
}
