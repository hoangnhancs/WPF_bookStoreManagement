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
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace bookStoreManagetment.ViewModel
{
    public class ImportGoodsViewModel : BaseViewModel
    {




        public ICommand ClickHiddenCommand { get; set; }
        public ICommand ClickHiddenButtonCommand { get; set; }
        public ICommand comboBoxListSupplierCommand { get; set; }
        public ICommand comboBoxSupplierCommand { get; set; }
        public ICommand CheckedGridSearchCommand { get; set; }
        public ICommand ClickAllSelectedCommand { get; set; }
        public ICommand ClickAllUnSelectedCommand { get; set; }
        public ICommand txtBoxTextChangedSearchCommand { get; set; }
        public ICommand ClickCompletedCommand { get; set; }
        public ICommand TextcahngedquantyCommand { get; set; }
        public ICommand ClickEditProductAddImportGoodCommand { get; set; }
        public ICommand textchangeddetailquantityCommand { get; set; }
        public ICommand textchangeddetailpriceCommand { get; set; }
        public ICommand ClickUpdateProductImportGoodsCommand { get; set; }
        public ICommand ClickRemoveProductImportGoodsCommand { get; set; }
        public ICommand ClickEdit_RemoveProductImportGoodsCommand { get; set; }

        public ICommand comboBoxCatalogofProducts { get; set; }
        public ICommand ClickDetailImportGoodsCommand { get; set; }
        public ICommand ClickAddFormImportGoodsCommand { get; set; } // Thêm sản phẩm

        public ICommand comboBoxEditListSupplierCommand { get; set; }
        public ICommand ClickEditImportGoodsCommand { get; set; }
        public ICommand ClickEditCheckItemCommand { get; set; }
        public ICommand EdittxtBoxTextChangedSearchCommand { get; set; }
        public ICommand ClickEdit_AllSelectedCommand { get; set; }
        public ICommand ClickEdit_AllUnSelectedCommand { get; set; }
        public ICommand ClickEdit_CompletedCommand { get; set; }
        public ICommand ClickEditRemoveProducttImportGoodsCommand { get; set; }
        public ICommand ClickUpdateFormImportGoodsCommand { get; set; }
        public ICommand ClickEdit_ProductAddImportGoodCommand { get; set; }
        public ICommand textchangededitquantityCommand { get; set; }
        public ICommand textchangededitpriceCommand { get; set; }
        public ICommand ClickEdit_UpdateProductImportGoodsCommand { get; set; }
        public ICommand ClickRemoveImportGoodsCommand { get; set; }
        public ICommand CheckedEditGridSearchCommand { get; set; }

        public ICommand TextChangedSearchCommand { get; set; }
        public ICommand comboBoxselectSupplierCommand { get; set; }
        public ICommand clickRefreshofImportCommand { get; set; }

        public ICommand OpenFilterCommand { get; set; }
        public ICommand CheckFilterCommand { get; set; }
        public ICommand DeleteFilterCommand { get; set; }
        public ICommand CloseFilterCommand { get; set; }

        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }



        private ObservableCollection<String> _listsupplier;
        public ObservableCollection<String> listSupplier { get => _listsupplier; set { _listsupplier = value; OnPropertyChanged(); } }

        private ObservableCollection<CellItems> _showitems;
        public ObservableCollection<CellItems> ShowItems { get => _showitems; set { _showitems = value; OnPropertyChanged(); } }

        private List<String> _backuplistsupplier;
        public List<String> BackuplistSupplier { get => _backuplistsupplier; set { _backuplistsupplier = value; OnPropertyChanged(); } }

        private List<CellItems> _backupallitems;
        public List<CellItems> backupAllItems { get => _backupallitems; set { _backupallitems = value; OnPropertyChanged(); } }

        private ObservableCollection<Inventory> _inventorydetail;
        public ObservableCollection<Inventory> InventoryDetail { get => _inventorydetail; set { _inventorydetail = value; OnPropertyChanged(); } }

        //Danh sách đơm nhập

        private ObservableCollection<String> _listsuppliers;
        public ObservableCollection<String> ListSuppliers { get => _listsuppliers; set { _listsuppliers = value; OnPropertyChanged(); } }



        private string _textboxsearch;
        public string textBoxSearch { get => _textboxsearch; set { _textboxsearch = value; OnPropertyChanged(); } }

        private string _selected;
        public string Selected { get => _selected; set { _selected = value; OnPropertyChanged(); } }

        //

        private string _namesupplier;
        public string NameSupplier { get => _namesupplier; set { _namesupplier = value; OnPropertyChanged(); } }

        private string _emailphonenumbersupplier;
        public string EmailPhonenumberSupplier { get => _emailphonenumbersupplier; set { _emailphonenumbersupplier = value; OnPropertyChanged(); } }

        private string _addresssupplier;
        public string AddressSupplier { get => _addresssupplier; set { _addresssupplier = value; OnPropertyChanged(); } }

        //Xem chi tiết đơn

        private string _detailnamesupplier;
        public string DetailNameSupplier { get => _detailnamesupplier; set { _detailnamesupplier = value; OnPropertyChanged(); } }

        private string _detailemailphonenumbersupplier;
        public string DetailEmailPhonenumberSupplier { get => _detailemailphonenumbersupplier; set { _detailemailphonenumbersupplier = value; OnPropertyChanged(); } }

        private string _detailaddresssupplier;
        public string DetailAddressSupplier { get => _detailaddresssupplier; set { _detailaddresssupplier = value; OnPropertyChanged(); } }

        // Chỉnh sửa đơn

        private ObservableCollection<Inventory> inventorylistedit;
        public ObservableCollection<Inventory> InventoryListEdit { get => inventorylistedit; set { inventorylistedit = value; OnPropertyChanged(); } }

        private List<Inventory> _backupinventorylistedit;
        public List<Inventory> BackupInventoryListEdit { get => _backupinventorylistedit; set { _backupinventorylistedit = value; OnPropertyChanged(); } }

        private string _editnamesupplier;
        public string EditNameSupplier { get => _editnamesupplier; set { _editnamesupplier = value; OnPropertyChanged(); } }

        private string _editemailphonenumbersupplier;
        public string EditEmailPhonenumberSupplier { get => _editemailphonenumbersupplier; set { _editemailphonenumbersupplier = value; OnPropertyChanged(); } }

        private string _editaddresssupplier;
        public string EditAddressSupplier { get => _editaddresssupplier; set { _editaddresssupplier = value; OnPropertyChanged(); } }

        private int _edittotalallproducts;
        public int EditTotalAllImportGoods { get => _edittotalallproducts; set { _edittotalallproducts = value; OnPropertyChanged(); } }

        private List<CellItems> _backupeditallitems;
        public List<CellItems> backupEditAllItems { get => _backupeditallitems; set { _backupeditallitems = value; OnPropertyChanged(); } }

        private ObservableCollection<CellItems> _editshowitems;
        public ObservableCollection<CellItems> EditShowItems { get => _editshowitems; set { _editshowitems = value; OnPropertyChanged(); } }


        public string CodeSupplier { get; set; }

        //chỉnh sửa số lượng và giá
        private string _codeproduct;
        public string CodeProduct { get => _codeproduct; set { _codeproduct = value; OnPropertyChanged(); } }

        private string _unitproduct;
        public string UnitProduct { get => _unitproduct; set { _unitproduct = value; OnPropertyChanged(); } }

        private string _nameproduct;
        public string NameProduct { get => _nameproduct; set { _nameproduct = value; OnPropertyChanged(); } }

        private int _quantityproduct;
        public int QuantityProduct { get => _quantityproduct; set { _quantityproduct = value; OnPropertyChanged(); } }

        private int _priceproduct;
        public int PriceProduct { get => _priceproduct; set { _priceproduct = value; OnPropertyChanged(); } }

        private int _totalallproduct;
        public int TotalAllProduct { get => _totalallproduct; set { _totalallproduct = value; OnPropertyChanged(); } }

        // Form chỉnh sửa đơn hàng

        private string _editcodeproduct;
        public string EditCodeProduct { get => _editcodeproduct; set { _editcodeproduct = value; OnPropertyChanged(); } }

        private string _editunitproduct;
        public string EditUnitProduct { get => _editunitproduct; set { _editunitproduct = value; OnPropertyChanged(); } }

        private string _editnameproduct;
        public string EditNameProduct { get => _editnameproduct; set { _editnameproduct = value; OnPropertyChanged(); } }

        private int _editquantityproduct;
        public int EditQuantityProduct { get => _editquantityproduct; set { _editquantityproduct = value; OnPropertyChanged(); } }

        private int _editpriceproduct;
        public int EditPriceProduct { get => _editpriceproduct; set { _editpriceproduct = value; OnPropertyChanged(); } }

        private int _edittotalallproduct;
        public int EditTotalAllProduct { get => _edittotalallproduct; set { _edittotalallproduct = value; OnPropertyChanged(); } }

        // Trong xem chi tiết đơn hàng

        private ObservableCollection<InventoryGoods> _inventorylists;
        public ObservableCollection<InventoryGoods> InventoryImportGoods { get => _inventorylists; set { _inventorylists = value; OnPropertyChanged(); } }

        private List<InventoryGoods> _backupinventorylists;
        public List<InventoryGoods> BackupInventoryImportGoods { get => _backupinventorylists; set { _backupinventorylists = value; OnPropertyChanged(); } }

        private int _totalallimportgoods;
        public int TotalAllImportGoods { get => _totalallimportgoods; set { _totalallimportgoods = value; OnPropertyChanged(); } }

        private int _totalallproducts;
        public int TotalAllProducts { get => _totalallproducts; set { _totalallproducts = value; OnPropertyChanged(); } }

        // Bộ lọc

        // background
        private Brush _BackgroudFilter;
        public Brush BackgroudFilter { get => _BackgroudFilter; set { _BackgroudFilter = value; OnPropertyChanged(); } }

        // foreground
        private Brush _ForegroudFilter;
        public Brush ForegroudFilter { get => _ForegroudFilter; set { _ForegroudFilter = value; OnPropertyChanged(); } }

        // nhóm Danh mục filter
        private string _displaynamesupplier;
        public string DisplayNameSupplier { get => _displaynamesupplier; set { _displaynamesupplier = value; OnPropertyChanged(); } }

        // ẩn hiện grid filter
        private Visibility _IsFilter;
        public Visibility IsFilter { get => _IsFilter; set { _IsFilter = value; OnPropertyChanged(); } }

        // ngày bắt đầu
        private string _displayBeginDay;
        public string displayBeginDay { get => _displayBeginDay; set { _displayBeginDay = value; OnPropertyChanged(); } }
        // ngày kết thúc
        private string _displayEndDay;
        public string displayEndDay { get => _displayEndDay; set { _displayEndDay = value; OnPropertyChanged(); } }

        private string _query;
        public string Query { get => _query; set { _query = value; OnPropertyChanged(); } }

        private string _selectedsupplier;
        public string SelectedSupplier { get => _selectedsupplier; set { _selectedsupplier = value; OnPropertyChanged(); } }

        public string backupbillcode;

        public ImportGoodsViewModel()
        {
            var totalprice = Model.DataProvider.Ins.DB.importBills.GroupBy(p => new { p.billCodeImport, p.importDate, p.nameEmployee, p.idsupplier })
                                                            .Select(pa => new {
                                                                billcode = pa.Key.billCodeImport,
                                                                Sum = pa.Sum(para => para.number * para.unitPrice),
                                                                date = pa.Key.importDate.Day.ToString() + "-" + pa.Key.importDate.Month.ToString() + "-" + pa.Key.importDate.Year.ToString(),
                                                                nameeployee = pa.Key.nameEmployee,
                                                                namesupplier = pa.Key.idsupplier
                                                            });

            BackupInventoryImportGoods = new List<InventoryGoods>();
            foreach (var data in totalprice)
            {
                BackupInventoryImportGoods.Add(new InventoryGoods()
                {
                    ProfitSummary = DataProvider.Ins.DB.profitSummaries.Where(p => p.billCode == data.billcode).FirstOrDefault(),
                    CodeBill = data.billcode,
                    NameSupplier = DataProvider.Ins.DB.suppliers.Where(p => p.idSupplier == data.namesupplier).Select(pa => pa.nameSupplier).FirstOrDefault(),
                    TotalPriceGoods = data.Sum,
                    NameEmployee = data.nameeployee,
                    ImportDay = data.date
                });
            }

            InventoryImportGoods = new ObservableCollection<InventoryGoods>(BackupInventoryImportGoods);

            BackuplistSupplier = new List<String>();
            BackuplistSupplier.Add("");
            BackuplistSupplier.AddRange(DataProvider.Ins.DB.suppliers.Select(p => p.nameSupplier));

            listSupplier = new ObservableCollection<string>(BackuplistSupplier);
            InventoryList = new ObservableCollection<Inventory>();
            TotalAllProducts = 0;

            // Load bộ lọc
            displayBeginDay = null;
            displayEndDay = null;
            IsFilter = Visibility.Collapsed;
            var bc = new BrushConverter();
            BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");


            // đóng filter grid
            CloseFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsFilter = Visibility.Collapsed;
            });

            OpenFilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsFilter == Visibility.Visible)
                    IsFilter = Visibility.Collapsed;
                else
                    IsFilter = Visibility.Visible;
            });

            CheckFilterCommand = new RelayCommand<object>((p) => {
                if (displayEndDay != null && displayBeginDay != null)
                    return true;
                if (DisplayNameSupplier != null)
                    return true;
                return false;
            }, (p) =>
            {
                Filter();
            });

            DeleteFilterCommand = new RelayCommand<object>((p) => {
                if (DisplayNameSupplier != null || displayEndDay != null || displayBeginDay != null)
                    return true;
                return false;
            }, (p) =>
            {
                displayBeginDay = null;
                displayEndDay = null;
                DisplayNameSupplier = "";
                DisplayNameSupplier = null;
                InventoryImportGoods = new ObservableCollection<InventoryGoods>(BackupInventoryImportGoods);

                BackgroudFilter = (Brush)bc.ConvertFromString("#d78a1e");
            });

            // Thêm sản phẩm
            backupAllItems = new List<CellItems>();

            var listItems = DataProvider.Ins.DB.items;
            foreach (var item in listItems)
            {
                CellItems newCell = new CellItems();
                newCell.Items = item;
                newCell.IsSelected = false;

                ImageSource photo = null;
                try
                {
                    photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + item.imageItem));

                }
                catch (Exception ex)
                {
                    try
                    {
                        photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\" + item.nameItem + ".jpg"));

                    }
                    catch (Exception e)
                    {
                        try
                        {
                            photo = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "Image\\không có ảnh.jpg"));
                        }
                        catch
                        {

                        }
                    }

                }
                newCell.Photo = photo;

                backupAllItems.Add(newCell);
            }

            ShowItems = new ObservableCollection<CellItems>(backupAllItems);

            //Chỉnh sửa

            backupEditAllItems = new List<CellItems>(backupAllItems);
            

            EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);

            List<string> backListSuppliers = new List<string>();
            backListSuppliers.Add("Tất cả nhà cung cấp");
            backListSuppliers.AddRange(DataProvider.Ins.DB.suppliers.Select(pa => pa.nameSupplier).ToList());
            ListSuppliers = new ObservableCollection<string>(backListSuppliers);

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

            TextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {

                    if (Query == "")
                    {
                        InventoryImportGoods = new ObservableCollection<InventoryGoods>(BackupInventoryImportGoods);
                    }
                    else
                    {
                        InventoryImportGoods = new ObservableCollection<InventoryGoods>();
                        foreach (var cellItems in BackupInventoryImportGoods)
                        {
                            string code = cellItems.CodeBill.Trim().ToLower();
                            string nameEmployee = cellItems.NameEmployee.Trim().ToLower();
                            if (nameEmployee.Contains(Query.Trim().ToLower()) || code.Contains(Query.Trim().ToLower()))
                            {
                                InventoryImportGoods.Add(cellItems);
                            }
                        }
                    }
                }
            });

            comboBoxselectSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as ComboBox).SelectedItem as String;
                    if (query == "Tất cả nhà cung cấp" && textBoxSearch == "")
                    {
                        InventoryImportGoods = new ObservableCollection<InventoryGoods>(BackupInventoryImportGoods);
                    }
                    else
                    {
                        InventoryImportGoods = new ObservableCollection<InventoryGoods>();
                        foreach (InventoryGoods cellItems in BackupInventoryImportGoods)
                        {
                            string nameSupplier = cellItems.NameSupplier.Trim().ToLower();
                            string nameEmployee = cellItems.NameEmployee.Trim().ToLower();
                            if (nameEmployee.Contains(textBoxSearch.Trim().ToLower()) && nameSupplier.Contains(query.Trim().ToLower()))
                            {
                                InventoryImportGoods.Add(cellItems);
                            }
                        }
                    }

                }
            });

            clickRefreshofImportCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InventoryImportGoods = new ObservableCollection<InventoryGoods>(BackupInventoryImportGoods);
                Selected = "Tất cả sản phẩm";
                textBoxSearch = "";
            });


            comboBoxEditListSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string seleted = (p as ComboBox).SelectedItem as string;
                var cell = DataProvider.Ins.DB.suppliers.Where(pa => pa.nameSupplier == seleted);
                if (seleted == "" || seleted == null)
                {
                    EditNameSupplier = null;
                    EditEmailPhonenumberSupplier = "";
                    EditAddressSupplier = null;
                }
                else
                {
                    EditNameSupplier = seleted;
                    EditEmailPhonenumberSupplier = cell.Select(pa => pa.phoneNumberSupplier).FirstOrDefault() + " / " + cell.Select(pa => pa.emailSupplier).FirstOrDefault();
                    EditAddressSupplier = cell.Select(pa => pa.addressSupplier).FirstOrDefault();
                }

            });

            comboBoxListSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string seleted = (p as ComboBox).SelectedItem as string;
                var cell = DataProvider.Ins.DB.suppliers.Where(pa => pa.nameSupplier == seleted);
                if (seleted == "" || seleted == null)
                {
                    NameSupplier = null;
                    EmailPhonenumberSupplier = "";
                    AddressSupplier = null;
                }
                else
                {
                    NameSupplier = seleted;
                    EmailPhonenumberSupplier = cell.Select(pa => pa.phoneNumberSupplier).FirstOrDefault() + " / " + cell.Select(pa => pa.emailSupplier).FirstOrDefault();
                    AddressSupplier = cell.Select(pa => pa.addressSupplier).FirstOrDefault();
                }

            });

            CheckedGridSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var selected = (p as CellItems);
                    selected.IsSelected = true;

                    for (int i = 0; i < backupAllItems.Count; i++)
                    {
                        if (backupAllItems[i].Items == selected.Items)
                            backupAllItems[i].IsSelected = true;
                    }
                }
            });

            CheckedEditGridSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var selected = (p as CellItems);
                    selected.IsSelected = true;

                    for (int i = 0; i < backupEditAllItems.Count; i++)
                    {
                        if (backupEditAllItems[i].Items == selected.Items)
                            backupEditAllItems[i].IsSelected = true;
                    }
                }
            });

            ClickAllSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < backupAllItems.Count; i++)
                {
                    backupAllItems[i].IsSelected = true;
                }
                ShowItems = new ObservableCollection<CellItems>(backupAllItems);

                (p as DataGrid).Items.Refresh();
            });

            ClickEdit_AllSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < backupEditAllItems.Count; i++)
                {
                    backupEditAllItems[i].IsSelected = true;
                }
                EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);

                (p as DataGrid).Items.Refresh();
            });

            ClickAllUnSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < backupAllItems.Count; i++)
                {
                    backupAllItems[i].IsSelected = false;
                }
                ShowItems = new ObservableCollection<CellItems>(backupAllItems);

                (p as DataGrid).Items.Refresh();
            });

            ClickEdit_AllUnSelectedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < backupEditAllItems.Count; i++)
                {
                    backupEditAllItems[i].IsSelected = false;
                }
                EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);

                (p as DataGrid).Items.Refresh();
            });

            txtBoxTextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text.Trim().ToLower();
                    if (query == "")
                    {
                        ShowItems = new ObservableCollection<CellItems>(backupAllItems);
                    }
                    else
                    {
                        ShowItems = new ObservableCollection<CellItems>();
                        foreach (var cellItems in backupAllItems)
                        {
                            string nameItem = cellItems.Items.nameItem.Trim().ToLower();

                            if (nameItem.Contains(query))
                            {
                                ShowItems.Add(cellItems);
                            }
                        }
                    }
                }
            });

            EdittxtBoxTextChangedSearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text.Trim().ToLower();
                    if (query == "")
                    {
                        EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);
                    }
                    else
                    {
                        EditShowItems = new ObservableCollection<CellItems>();
                        foreach (var cellItems in backupEditAllItems)
                        {
                            string nameItem = cellItems.Items.nameItem.Trim().ToLower();

                            if (nameItem.Contains(query))
                            {
                                EditShowItems.Add(cellItems);
                            }
                        }
                    }
                }
            });

            ClickCompletedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                foreach (var cellItems in backupAllItems)
                {
                    if (cellItems.IsSelected && !InventoryList.Select(pa => pa.Item.idItem).Contains(cellItems.Items.idItem))
                    {
                        var _item = DataProvider.Ins.DB.items.Where(i => cellItems.Items.idItem == i.idItem).FirstOrDefault();
                        Inventory _Inventory = new Inventory();
                        _Inventory.Item = (_item as item);

                        _Inventory.Count = 0;

                        _Inventory.TotalPriceItem = 0;

                        _Inventory.QuantityBefore = 0;

                        InventoryList.Add(_Inventory);
                    }
                }
            });

            ClickEdit_CompletedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                foreach (var cellItems in backupEditAllItems)
                {
                    if (cellItems.IsSelected && !InventoryListEdit.Select(pa => pa.Item.idItem).Contains(cellItems.Items.idItem))
                    {
                        var _item = DataProvider.Ins.DB.items.Where(i => cellItems.Items.idItem == i.idItem).FirstOrDefault();
                        Inventory _Inventory = new Inventory();
                        _Inventory.Item = (_item as item);

                        _Inventory.Count = 0;

                        _Inventory.TotalPriceItem = 0;

                        _Inventory.QuantityBefore = 0;

                        InventoryListEdit.Add(_Inventory);
                    }
                }
            });

            ClickEditProductAddImportGoodCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {


                var selected = (p as Inventory);

                var cell = InventoryList.Where(pa => pa.Item.idItem == selected.Item.idItem).SingleOrDefault();

                CodeProduct = selected.Item.idItem;
                UnitProduct = selected.Item.unit;
                NameProduct = selected.Item.nameItem;
                QuantityProduct = selected.Count;
                PriceProduct = selected.Item.importPriceItem;
                TotalAllProduct = selected.TotalPriceItem;
            });

            ClickEdit_ProductAddImportGoodCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {


                var selected = (p as Inventory);

                var cell = InventoryList.Where(pa => pa.Item.idItem == selected.Item.idItem).SingleOrDefault();

                EditCodeProduct = selected.Item.idItem;
                EditUnitProduct = selected.Item.unit;
                EditNameProduct = selected.Item.nameItem;
                EditQuantityProduct = selected.Count;
                EditPriceProduct = selected.Item.importPriceItem;
                EditTotalAllProduct = selected.TotalPriceItem;
            });

            textchangeddetailquantityCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    TotalAllProducts = 0;
                    string query = (p as TextBox).Text;
                    int numericValue;
                    if (int.TryParse(query, out numericValue))
                    {
                        int quantity = Int32.Parse(query);

                        QuantityProduct = quantity;

                        TotalAllProduct = quantity * PriceProduct;
                    }

                }
            });

            textchangededitquantityCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text;
                    int numericValue;
                    if (int.TryParse(query, out numericValue))
                    {
                        int quantity = Int32.Parse(query);

                        EditQuantityProduct = quantity;

                        EditTotalAllProduct = quantity * EditPriceProduct;
                    }

                }
            });

            textchangeddetailpriceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text;
                    int numericValue;
                    if (int.TryParse(query, out numericValue))
                    {
                        int quantity = Int32.Parse(query);

                        PriceProduct = quantity;

                        TotalAllProduct = quantity * QuantityProduct;
                    }

                }
            });

            textchangededitpriceCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    string query = (p as TextBox).Text;
                    int numericValue;
                    if (int.TryParse(query, out numericValue))
                    {
                        int quantity = Int32.Parse(query);

                        EditPriceProduct = quantity;

                        EditTotalAllProduct = quantity * EditQuantityProduct;
                    }

                }
            });

            ClickUpdateProductImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    TotalAllProducts = 0;
                    var cell = InventoryList.Where(pa => pa.Item.idItem == CodeProduct).SingleOrDefault();

                    cell.Count = QuantityProduct;
                    cell.Item.importPriceItem = PriceProduct;
                    cell.TotalPriceItem = TotalAllProduct;
                    (p as DataGrid).Items.Refresh();

                    foreach (var data in InventoryList)
                    {
                        TotalAllProducts = TotalAllProducts + data.TotalPriceItem;
                    }
                }

            });

            ClickEdit_UpdateProductImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    EditTotalAllImportGoods = 0;
                    var cell = InventoryListEdit.Where(pa => pa.Item.idItem == EditCodeProduct).SingleOrDefault();

                    cell.Count = EditQuantityProduct;
                    cell.Item.importPriceItem = EditPriceProduct;
                    cell.TotalPriceItem = EditTotalAllProduct;
                    (p as DataGrid).Items.Refresh();

                    foreach (var data in InventoryListEdit)
                    {
                        EditTotalAllImportGoods = EditTotalAllImportGoods + data.TotalPriceItem;
                    }
                }

            });

            ClickRemoveProductImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var cellitem = p as Inventory;
                    InventoryList.Remove(cellitem);
                    TotalAllImportGoods = TotalAllImportGoods - cellitem.TotalPriceItem;
                    foreach (var cell in backupAllItems)
                    {
                        if (cell.Items.idItem == cellitem.Item.idItem)
                        {
                            cell.IsSelected = false;
                            break;
                        }
                    }

                    ShowItems = new ObservableCollection<CellItems>(backupAllItems);
                }

            });

            ClickEdit_RemoveProductImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var cellitem = p as Inventory;
                    EditTotalAllImportGoods = EditTotalAllImportGoods - cellitem.TotalPriceItem;
                    InventoryListEdit.Remove(cellitem);

                    foreach (var cell in backupEditAllItems)
                    {
                        if (cell.Items.idItem == cellitem.Item.idItem)
                        {
                            cell.IsSelected = false;
                            break;
                        }
                    }

                    EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);
                }

            });

            ClickDetailImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    TotalAllImportGoods = 0;
                    InventoryDetail = new ObservableCollection<Inventory>();
                    var select = (p as InventoryGoods);
                    DetailNameSupplier = select.NameSupplier;
                    var cell = DataProvider.Ins.DB.suppliers.Where(pa => pa.nameSupplier == select.NameSupplier).FirstOrDefault();
                    DetailEmailPhonenumberSupplier = cell.phoneNumberSupplier + " / " + cell.emailSupplier;
                    DetailAddressSupplier = cell.addressSupplier;

                    var cellimportbill = DataProvider.Ins.DB.importBills.Where(pa => pa.billCodeImport == select.CodeBill).ToList();

                    foreach (var data in cellimportbill)
                    {
                        item cellitem = DataProvider.Ins.DB.items.Where(pa => pa.idItem == data.idItem).FirstOrDefault();

                        int total = data.number * data.unitPrice;
                        InventoryDetail.Add(new Inventory() { Item = cellitem, Count = data.number, TotalPriceItem = data.number * data.unitPrice, QuantityBefore = 0 });

                        TotalAllImportGoods = TotalAllImportGoods + total;
                    }
                }

            });

            ClickEditImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    EditTotalAllImportGoods = 0;
                    BackupInventoryListEdit = new List<Inventory>();
                    var select = (p as InventoryGoods);
                    backupbillcode = select.CodeBill;

                    SelectedSupplier = select.NameSupplier;

                    EditNameSupplier = select.NameSupplier;
                    var cell = DataProvider.Ins.DB.suppliers.Where(pa => pa.nameSupplier == select.NameSupplier).FirstOrDefault();
                    EditEmailPhonenumberSupplier = cell.phoneNumberSupplier + " / " + cell.emailSupplier;
                    EditAddressSupplier = cell.addressSupplier;

                    var cellimportbill = DataProvider.Ins.DB.importBills.Where(pa => pa.billCodeImport == select.CodeBill).ToList();

                    foreach (var data in cellimportbill)
                    {
                        item cellitem = DataProvider.Ins.DB.items.Where(pa => pa.idItem == data.idItem).FirstOrDefault();

                        int total = data.number * data.unitPrice;
                        BackupInventoryListEdit.Add(new Inventory() { Item = cellitem, Count = data.number, TotalPriceItem = data.number * data.unitPrice, QuantityBefore = data.number });

                        EditTotalAllImportGoods = EditTotalAllImportGoods + total;
                        InventoryListEdit = new ObservableCollection<Inventory>(BackupInventoryListEdit);
                    }
                }

            });

            ClickEditCheckItemCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null && InventoryListEdit != null)
                {
                    foreach (var data in InventoryListEdit)
                    {
                        foreach (var cell in backupEditAllItems)
                        {
                            if (data.Item.idItem == cell.Items.idItem)
                            {
                                cell.IsSelected = true;
                                break;
                            }
                            else
                            {
                                cell.IsSelected = false;
                            }
                        }
                    }

                    EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);
                }

            });

            ClickEditRemoveProducttImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    var cellitem = p as Inventory;
                    InventoryListEdit.Remove(cellitem);

                    foreach (var cell in backupEditAllItems)
                    {
                        if (cell.Items.idItem == cellitem.Item.idItem)
                        {
                            cell.IsSelected = false;
                            break;
                        }
                    }

                    EditShowItems = new ObservableCollection<CellItems>(backupEditAllItems);
                }

            });

            ClickRemoveImportGoodsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá khách hàng này không ?",
                                          "Xác nhận",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var select = p as InventoryGoods;

                        bill Bill = Model.DataProvider.Ins.DB.bills.Where(pa => pa.billCode == select.CodeBill).FirstOrDefault();
                        Model.DataProvider.Ins.DB.bills.Remove(Bill);
                        InventoryImportGoods.Remove(select);
                        var profit = DataProvider.Ins.DB.profitSummaries.Where(pa => pa.billCode == select.CodeBill).FirstOrDefault();
                        var employee = DataProvider.Ins.DB.employees.Where(pa => pa.idEmployee == profit.idEmployee).FirstOrDefault();

                        DataProvider.Ins.DB.employees.Remove(employee);
                        DataProvider.Ins.DB.profitSummaries.Remove(profit);
                        DataProvider.Ins.DB.employees.Add(employee);


                        var ImportBill = DataProvider.Ins.DB.importBills.Where(pa => pa.billCodeImport == select.CodeBill).Select(pa => pa.idItem).ToList();
                        foreach (var cell in ImportBill)
                        {
                            Model.importBill cellimportBill = DataProvider.Ins.DB.importBills.Where(pa => pa.billCodeImport == select.CodeBill && pa.idItem == cell).FirstOrDefault();
                            DataProvider.Ins.DB.importBills.Remove(cellimportBill);
                            item data = DataProvider.Ins.DB.items.Where(pa => pa.idItem == cell).FirstOrDefault();
                            int quantitys = data.quantity;
                            data.quantity = quantitys - cellimportBill.number;

                        }
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Xoá thành công!");
                    }
                    
                }

            });

            ClickAddFormImportGoodsCommand = new RelayCommand<object>((p) => {
                foreach (var n in InventoryList)
                {
                    if (n.Count <= 0)
                    {
                        return false;
                    }
                }
                if (NameSupplier != null && EmailPhonenumberSupplier != null && AddressSupplier != null && InventoryList.Count() > 0)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                if (p != null)
                {
                    int c = DataProvider.Ins.DB.bills.Count() + 1;
                    string code = c < 10 ? "IP00" + c : c < 100 ? "IP0" + c : "IP" + c;
                    int totalall = 0;

                    DateTime Daynow = DateTime.Now;

                    int bud = 0;
                    var ytyy = DataProvider.Ins.DB.profitSummaries.Where(pa => pa.billType == "import").Select(pa => pa.budget);
                    foreach (int data in ytyy)
                    {
                        bud = data;
                    }

                    bill Bill = new bill()
                    {
                        billCode = code,
                        billType = "import"
                    };

                    DataProvider.Ins.DB.bills.Add(Bill);

                    foreach (var data in InventoryList)
                    {
                        Model.importBill ImportBill = new Model.importBill()
                        {

                            billCodeImport = c < 10 ? "IP00" + c : c < 100 ? "IP0" + c : "IP" + c,
                            idEmployee = "EMP002",
                            nameEmployee = "Nguyen Yen Chi",
                            number = data.Count,
                            importDate = Daynow,
                            idItem = data.Item.idItem,
                            unitPrice = data.Item.importPriceItem,
                            note = "",
                            paymentMethod = "Tiền mặt",
                            idsupplier = DataProvider.Ins.DB.suppliers.Where(pa => pa.nameSupplier == NameSupplier).Select(pa => pa.idSupplier).FirstOrDefault()
                        };
                        totalall = totalall + data.TotalPriceItem;

                        DataProvider.Ins.DB.importBills.Add(ImportBill);
                        var cell = DataProvider.Ins.DB.items.Where(pa => pa.idItem == data.Item.idItem).SingleOrDefault();

                        cell.quantity = cell.quantity + data.Count;

                    }
                    profitSummary profit = new profitSummary()
                    {
                        billCode = c < 10 ? "IP00" + c : c < 100 ? "IP0" + c : "IP" + c,
                        billType = "import",
                        rootPrice = totalall,
                        payPrice = totalall,
                        exchangePrice = totalall - totalall,
                        idCustomer = "CUS001",
                        idEmployee = "EMP001",
                        day = Daynow,
                        nameCustomer = "Nguyễn Hoàng Thắng",
                        nameEmployee = "Thái Hoàng Nhân",
                        typeGroup = "nhà cung cấp",
                        payment = "Tiền mặt",
                        nameBill = "Nhập hàng",
                        note = "",
                        budget = bud - TotalAllProducts
                    };

                    DataProvider.Ins.DB.profitSummaries.Add(profit);

                    InventoryGoods cells = new InventoryGoods()
                    {
                        CodeBill = c < 10 ? "IP00" + c : c < 100 ? "IP0" + c : "IP" + c,
                        NameSupplier = NameSupplier,
                        TotalPriceGoods = totalall,
                        NameEmployee = "Thanh Thảo",
                        ImportDay = Daynow.Day + "-" + Daynow.Month + "-" + Daynow.Year
                    };

                    InventoryImportGoods.Add(cells);
                    MessageBox.Show("Cập nhật thành công!");
                    DataProvider.Ins.DB.SaveChanges();
                    InventoryList = new ObservableCollection<Inventory>();
                    TotalAllProducts = 0;
                }

            });


            // Cập nhật sau khi chỉnh sửa
            ClickUpdateFormImportGoodsCommand = new RelayCommand<object>((p) => {
                foreach (var n in InventoryListEdit)
                {
                    if (n.Count <= 0)
                    {
                        return false;
                    }
                }
                if (EditNameSupplier != null && EditEmailPhonenumberSupplier != null && EditAddressSupplier != null && InventoryListEdit.Count() > 0)
                {
                    return true;
                }

                return false;

            }, (p) =>
            {
                if (p != null)
                {
                    List<string> id = InventoryListEdit.Select(pa => pa.Item.idItem).ToList();
                    List<string> backupid = BackupInventoryListEdit.Select(pa => pa.Item.idItem).ToList();
                    string idnhacc = DataProvider.Ins.DB.suppliers.Where(pa => pa.nameSupplier == EditNameSupplier).Select(pa => pa.idSupplier).FirstOrDefault();
                    var pro = DataProvider.Ins.DB.profitSummaries.Where(pa => pa.billCode == backupbillcode).FirstOrDefault();
                    pro.idCustomer = idnhacc;
                    foreach (string data_id in id)
                    {
                        var cell_id = InventoryListEdit.Where(pa => pa.Item.idItem == data_id).FirstOrDefault();
                        var cell = DataProvider.Ins.DB.items.Where(pa => pa.idItem == data_id).FirstOrDefault();
                        if (backupid.Contains(data_id))
                        {

                            //var cell = DataProvider.Ins.DB.importBills.Where(pa => pa.billCodeImport == backupbillcode && pa.idItem == data_id).FirstOrDefault();

                            if (cell_id.Count == cell_id.QuantityBefore && cell_id.Item.importPriceItem == cell.importPriceItem)
                            {
                                var im = DataProvider.Ins.DB.importBills.Where(pa => pa.billCodeImport == backupbillcode && pa.idItem == data_id).FirstOrDefault();
                                im.idsupplier = idnhacc;
                            }
                            else
                            {
                                cell.quantity = cell.quantity - cell_id.QuantityBefore + cell_id.Count;
                                cell.importPriceItem = cell_id.Item.importPriceItem;
                            }
                        }
                        else
                        {
                            DateTime Daynow = DateTime.Now;
                            Model.importBill ImportBill = new Model.importBill()
                            {
                                billCodeImport = backupbillcode,
                                idEmployee = "EMP002",
                                nameEmployee = "Nguyễn Yến Chi",
                                number = cell_id.Count,
                                importDate = Daynow,
                                idItem = cell_id.Item.idItem,
                                unitPrice = cell_id.Item.importPriceItem,
                                paymentMethod = "Tiền mặt",
                                note = "",
                                idsupplier = idnhacc
                            };
                            //totalall = totalall + data.TotalPriceItem;

                            DataProvider.Ins.DB.importBills.Add(ImportBill);

                            cell.quantity = cell.quantity - cell_id.QuantityBefore + cell_id.Count;
                            cell.importPriceItem = cell_id.Item.importPriceItem;

                        }


                    }

                    DataProvider.Ins.DB.SaveChanges();

                    foreach (string data_backupid in backupid)
                    {
                        if (!id.Contains(data_backupid))
                        {
                            var cell_id = DataProvider.Ins.DB.importBills.Where(pa => pa.idItem == data_backupid && pa.billCodeImport == backupbillcode).FirstOrDefault();
                            DataProvider.Ins.DB.importBills.Remove(cell_id);

                            var cell = DataProvider.Ins.DB.items.Where(pa => pa.idItem == data_backupid).FirstOrDefault();
                            cell.quantity = cell.quantity - cell_id.number;

                            DataProvider.Ins.DB.SaveChanges();

                        }
                    }
                }

            });

        }

        

        //filter
        private void Filter()
        {
            List<InventoryGoods> newListExportBill = BackupInventoryImportGoods;
            if (DisplayNameSupplier != null && DisplayNameSupplier != "")
            {
                newListExportBill = newListExportBill.Where(x => x.NameSupplier == DisplayNameSupplier).ToList();
            }

            if (displayBeginDay != null)
            {
                List<InventoryGoods> temp = new List<InventoryGoods>();
                foreach (var bill in newListExportBill)
                {
                    if (DateTime.Compare(bill.ProfitSummary.day, DateTime.ParseExact(displayBeginDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) >= 0
                     && DateTime.Compare(bill.ProfitSummary.day, DateTime.ParseExact(displayEndDay.Split(' ')[0], "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture)) <= 0)
                    {
                        temp.Add(bill);
                    }
                }
                newListExportBill = temp;
            }

            InventoryImportGoods = new ObservableCollection<InventoryGoods>(newListExportBill);
        }

        public class CellItems
        {
            public item Items { get; set; }
            public bool IsSelected { get; set; }
            public ImageSource Photo { get; set; }
        }

        public class Inventory
        {
            public item Item { get; set; }
            public int Count { get; set; }
            public int TotalPriceItem { get; set; }
            public int QuantityBefore { get; set; }
        }

        public class InventoryGoods
        {
            public profitSummary ProfitSummary { get; set; }
            public string CodeBill { get; set; }
            public string NameSupplier { get; set; }
            public int TotalPriceGoods { get; set; }
            public string NameEmployee { get; set; }
            public string ImportDay { get; set; }
        }
    }
}

