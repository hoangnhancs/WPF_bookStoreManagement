using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Mail;
using System.Net;

namespace bookStoreManagetment.ViewModel
{
    public class mailViewModel : BaseViewModel
    {
        #region "mail"
        private ObservableCollection<checkmail> _InventoryList;
        public ObservableCollection<checkmail> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }
        private ObservableCollection<checkmail> _DivInventoryList;
        public ObservableCollection<checkmail> DivInventoryList { get => _DivInventoryList; set { _DivInventoryList = value; OnPropertyChanged(); } }
        private ObservableCollection<Inventory> _InventoryCustomerList;
        public ObservableCollection<Inventory> InventoryCustomerList { get => _InventoryCustomerList; set { _InventoryCustomerList = value; OnPropertyChanged(); } }
        private ObservableCollection<Inventory> _InventoryEmployeeList;
        public ObservableCollection<Inventory> InventoryEmployeeList { get => _InventoryEmployeeList; set { _InventoryEmployeeList = value; OnPropertyChanged(); } }
        public checkmail SelectedItem { get; set; }
        public ICommand LoadMailCommand { get; set; }
        #endregion

        #region "sent mail"
        private ObservableCollection<Inventory> _InventoryListSentMail;
        public ObservableCollection<Inventory> InventoryListSentMail { get => _InventoryListSentMail; set { _InventoryListSentMail = value; OnPropertyChanged(); } }
        private ObservableCollection<Inventory> _DivInventoryListSentMail;
        public ObservableCollection<Inventory> DivInventoryListSentMail { get => _DivInventoryListSentMail; set { _DivInventoryListSentMail = value; OnPropertyChanged(); } }
        public Inventory SelectedItemSentMail { get; set; }
        public ICommand ShowSentMailCommand { get; set; }
        public ICommand btnChitietSentMailCommand { get; set; }

        #endregion

        #region "add, edit, delete mail"
        private ObservableCollection<string> _cbbmailtype;
        public ObservableCollection<String> cbbMailType { get { return _cbbmailtype; } set { _cbbmailtype = value; OnPropertyChanged(); } }
        private ObservableCollection<string> _cbbsenttype;
        public ObservableCollection<String> cbbSentType { get { return _cbbsenttype; } set { _cbbsenttype = value; OnPropertyChanged(); } }
        private DateTime _datesent;
        public DateTime DateSent { get { return _datesent; } set { _datesent = value; OnPropertyChanged(); } }
        public ICommand ShowMailCommand { get; set; }
        public ICommand DetailMailCommand { get; set; }
        public ICommand btnCapnhatClickCommand { get; set; }
        public ICommand btnAddClickCommand { get; set; }
        public ICommand btnAddMailClick { get; set; }
        public ICommand btnDeleteCommand { get; set; }
        public ICommand cbbMailTypeSelectionChanged { get; set; }
        public string textBoxSearchValue { get; set; }
        public bool seen { get; set; }
        private bool _readOnly;
        public bool ReadOnly { get { return _readOnly; } set { _readOnly = value; OnPropertyChanged(); } }
        private bool _enable;
        public bool Enable { get { return _enable; } set { _enable = value; OnPropertyChanged(); } }
        private bool _cbbsenttypeenable;
        public bool ComboBoxSenttypeEnable { get { return _cbbsenttypeenable; } set { _cbbsenttypeenable = value; OnPropertyChanged(); } }
        private string subject;
        public string Subject { get { return subject; } set { subject = value; OnPropertyChanged(); } }
        private string content;
        public string Content { get { return content; } set { content = value; OnPropertyChanged(); } }
        private string sender;
        public string Sender { get { return sender; } set { sender = value; OnPropertyChanged(); Console.WriteLine(sender); } }
        private string mailtype;
        public string Mailtype { get { return mailtype; } set { mailtype = value; OnPropertyChanged(); } }
        private string senttype;
        public string Senttype { get { return senttype; } set { senttype = value; OnPropertyChanged(); } }
        #endregion

        #region "manipulation"
        private Visibility _gridDetailMailVisible;
        public Visibility GridDetailMailVisible { get { return _gridDetailMailVisible; } set { _gridDetailMailVisible = value; OnPropertyChanged(); } }
        private Visibility _gridDataGridVisible;
        public Visibility GridDataGridVisible { get { return _gridDataGridVisible; } set { _gridDataGridVisible = value; OnPropertyChanged(); } }
        private Visibility _gridSentMailVisible;
        public Visibility GridSentMailVisible { get { return _gridSentMailVisible; } set { _gridSentMailVisible = value; OnPropertyChanged(); } }
        private Visibility _gridEditMailVisible;
        public Visibility GridEditMailVisible { get { return _gridEditMailVisible; } set { _gridEditMailVisible = value; OnPropertyChanged(); } }
        public ICommand btnHuyClickCommand { get; set; }


        public ICommand OpenButton { get; set; }
        public ICommand CloseButton { get; set; }
        #endregion

        #region "filter"
        public ICommand toggleClickCommand { get; set; }
        public ICommand searchEngineer { get; set; }
        #endregion

        #region "page"
        //Page Property
        /*
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
        */

        //Page Property
        #endregion
        public mailViewModel() {
            #region "mail"
            LoadMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridDataGridVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Visible;
                GridSentMailVisible = Visibility.Collapsed;
                GridEditMailVisible = Visibility.Collapsed;
                textBoxSearchValue = "";

                //NumRowEachPageTextBox = "5";
                //NumRowEachPage = Convert.ToInt32(NumRowEachPageTextBox);
                LoadData();
                //currentpage = 1;
                //pack_page = 1;
                //settingButtonNextPrev();
                //autoSendMail("abc");
                SearchEngineer(textBoxSearchValue);
                seen = false;
                EnableChange(seen);
                //autosentmail();
            });
            ShowMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridDataGridVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Visible;
                GridSentMailVisible = Visibility.Collapsed;
                GridEditMailVisible = Visibility.Collapsed;
            });
            DetailMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                seen = false;
                EnableChange(seen);
                SelectedItem = (p as checkmail);
                Subject = SelectedItem.Mail.subjectMail;
                Content = SelectedItem.Mail.content;
                Sender = SelectedItem.Mail.sender;
                Mailtype = SelectedItem.Mail.typeMail;
                Senttype = SelectedItem.Mail.typesent;
                DateSent = (DateTime)SelectedItem.Mail.autosentDate;
                GridDataGridVisible = Visibility.Collapsed;
                GridDetailMailVisible = Visibility.Collapsed;
                GridSentMailVisible = Visibility.Collapsed;
                GridEditMailVisible = Visibility.Visible;
            });
            #endregion

            #region "sent mail"
            ShowSentMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridDataGridVisible = Visibility.Collapsed;
                GridDetailMailVisible = Visibility.Visible;
                GridSentMailVisible = Visibility.Visible;
                GridEditMailVisible = Visibility.Collapsed;
            });
            btnChitietSentMailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItemSentMail = new Inventory();
                SelectedItemSentMail = (p as Inventory);

                Subject = SelectedItemSentMail.Sentmail.subjectMail;
                Mailtype = SelectedItemSentMail.Sentmail.typeMail;
                Sender = SelectedItemSentMail.Sentmail.sender;
                int id = SelectedItemSentMail.Sentmail.idMail;
                DateSent = DataProvider.Ins.DB.Database.SqlQuery<DateTime>("select autosentDate from mail where idMail=" + id.ToString()).FirstOrDefault();
                Senttype = DataProvider.Ins.DB.Database.SqlQuery<string>("select typesent from mail where idMail=" + id.ToString()).FirstOrDefault();
                var _res = DataProvider.Ins.DB.mails.Where(i => i.idMail == id);
                Content = DataProvider.Ins.DB.Database.SqlQuery<String>("select content from mail where idMail=" + id.ToString()).FirstOrDefault();
                Console.WriteLine("select content from mail where idMail=" + id.ToString());
                /*
                foreach(var item in _res)
                {
                    Inventory _in = new Inventory();
                    Content = item.content;
                }
                */
                seen = true;
                EnableChange(seen);
                GridEditMailVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Collapsed;
                GridDataGridVisible = Visibility.Collapsed;
                GridSentMailVisible = Visibility.Collapsed;
            });
            #endregion

            #region "manipulation"
            btnHuyClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn có muốn thoát?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GridDetailMailVisible = Visibility.Visible;
                    GridDataGridVisible = Visibility.Visible;
                    GridSentMailVisible = Visibility.Collapsed;
                    GridEditMailVisible = Visibility.Collapsed;
                    LoadData();
                    //settingButtonNextPrev();
                }
            });
            OpenButton = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (p as Button).Visibility = System.Windows.Visibility.Visible;
            });
            CloseButton = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (p as Button).Visibility = System.Windows.Visibility.Collapsed;
            });
            #endregion

            #region "filter"
            toggleClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn có muốn thay đổi trạng thái? Điều này có thể sẽ lặp lại việc gửi mail?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SelectedItem = (p as checkmail);
                    var result = DataProvider.Ins.DB.mails.SingleOrDefault(i => i.idMail == SelectedItem.Mail.idMail);
                    if (result != null)
                    {
                        if (result.statusMail == "OFF")
                        {
                            result.statusMail = "ON";
                            //MessageBox.Show(SelectedItem.Mail.subjectMail + SelectedItem.Mail.typeMail + SelectedItem.Mail.typesent);
                            autosentmail(SelectedItem, SelectedItem.Mail.typeMail, SelectedItem.Mail.typesent);
                        }
                        else
                            result.statusMail = "OFF";
                        DataProvider.Ins.DB.SaveChanges();
                    }

                }

                LoadData();
            });
            searchEngineer = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SearchEngineer(textBoxSearchValue);
            });
            void SearchEngineer(string query)
            {
                InventoryList = new ObservableCollection<checkmail>();
                var lstMail = DataProvider.Ins.DB.mails.Where(i => (i.subjectMail.Contains(query) || i.typeMail.Contains(query)));
                foreach (var mail in lstMail)
                {
                    checkmail _Inventory = new checkmail();
                    _Inventory.Mail = mail;
                    if (mail.statusMail == "ON")
                        _Inventory.Check = true;
                    else
                        _Inventory.Check = false;
                    InventoryList.Add(_Inventory);
                }



            }
            #endregion

            #region "add,update,delete"
            cbbMailTypeSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    if ((p as ComboBox).SelectedItem != null)
                    {
                        string value = (p as ComboBox).SelectedItem.ToString();
                        if (value == "Sinh nhật")
                        {
                            ComboBoxSenttypeEnable = false;
                            Senttype = "EVERYYEAR";
                        }
                        else
                        {
                            ComboBoxSenttypeEnable = true;
                        }
                    }
                }
                catch
                {

                }
            });
            btnCapnhatClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (MessageBox.Show("Bạn có muốn cập nhật?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DateTime now = DateTime.Now.Date;
                    //string query = "update mail set content=N'" + Content +"',typeMail=N'"+Mailtype +"',subjectMail=N'" + Subject + "',sender=N'" + Sender + "',updateDate=N'" + now.ToString() + "' where idMail=" + SelectedItem.Mail.idMail;
                    //DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                    var res = DataProvider.Ins.DB.mails.SingleOrDefault(i => i.idMail == SelectedItem.Mail.idMail);
                    if (res != null)
                    {
                        res.content = Content;
                        res.typeMail = Mailtype;
                        res.subjectMail = Subject;
                        res.sender = Sender;
                        res.updateDate = now;
                        res.autosentDate = DateSent;
                        res.typesent = Senttype;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Cập nhật thành công");
                }
            });
            btnAddMailClick = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                clearData();
                seen = false;
                EnableChange(seen);
                GridEditMailVisible = Visibility.Visible;
                GridDetailMailVisible = Visibility.Collapsed;
                GridDataGridVisible = Visibility.Collapsed;
                GridSentMailVisible = Visibility.Collapsed;
            });
            btnAddClickCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string query;
                DateTime now = DateTime.Now;
                if (Mailtype.ToLower() != "sinh nhật")
                {
                    query = String.Format("insert into mail values (N'{0}',N'{1}', N'{2}', N'{3}', N'Sử dụng để gửi thông báo', N'ON', N'{4}',N'{5}',N'{6}')", Mailtype, now, subject, Content, Sender, DateSent, Senttype);
                }
                else
                    query = String.Format("insert into mail values (N'{0}',N'{1}', N'{2}', N'{3}', N'Sử dụng để gửi thông báo', N'ON', N'{4}', null,N'{5}')", Mailtype, now, subject, Content, Sender, Senttype);
                MessageBox.Show(query);
                try
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                    MessageBox.Show("Thêm thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clearData();
                LoadData();

            });
            btnDeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItem = (p as checkmail);
                int id = SelectedItem.Mail.idMail;

                string sbj = SelectedItem.Mail.subjectMail;

                if (MessageBox.Show(String.Format("Bạn có muốn xóa mail {0}?", sbj), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string query = "delete from mail where idMail=" + id;
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);

                    LoadData();
                    MessageBox.Show(String.Format("Bạn đã xóa thành công mail {0}", sbj));

                }
            });
            void EnableChange(bool seen)
            {

                ReadOnly = seen;
                Enable = !seen;

            }
            #endregion

            #region "select num row each page"
            /*
            tbNumRowEachPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                currentpage = 1;
                LoadData();
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
            */
            #endregion

            void LoadData()
            { 
                DivInventoryList = new ObservableCollection<checkmail>();
                DivInventoryListSentMail = new ObservableCollection<Inventory>();
                InventoryCustomerList = new ObservableCollection<Inventory>();
                InventoryEmployeeList = new ObservableCollection<Inventory>();
                InventoryListSentMail = new ObservableCollection<Inventory>();
                var lstCus = DataProvider.Ins.DB.custommers;
                foreach (var cus in lstCus)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Custommer = cus;
                    InventoryCustomerList.Add(_Inventory);
                }
                var lstEmp = DataProvider.Ins.DB.employees;
                foreach (var emp in lstEmp)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Employee = emp;
                    InventoryEmployeeList.Add(_Inventory);
                }
                var lstsentMail = DataProvider.Ins.DB.sentmails;
                foreach (var item in lstsentMail)
                {
                    Inventory _Inventory = new Inventory();
                    _Inventory.Sentmail = item;
                    InventoryListSentMail.Add(_Inventory);
                }
                InventoryList = new ObservableCollection<checkmail>();


                var lstMail = DataProvider.Ins.DB.mails;
                foreach (var mail in lstMail)
                {
                    checkmail _Inventory = new checkmail();
                    _Inventory.Mail = mail;
                    if (mail.statusMail == "ON")
                        _Inventory.Check = true;
                    else
                        _Inventory.Check = false;
                    InventoryList.Add(_Inventory);
                }
                cbbMailType = new ObservableCollection<string>();
                cbbMailType.Add("Hội họp");
                cbbMailType.Add("Sự kiện");
                cbbMailType.Add("Sinh nhật");
                cbbSentType = new ObservableCollection<string>();
                cbbSentType.Add("ONEDAY");
                cbbSentType.Add("EVERYWEEK");
                cbbSentType.Add("EVERYMONTH");
                cbbSentType.Add("EVERYYEAR");
                DateSent = DateTime.Now.Date;

            }
            #region "setting button"
            /*
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
                    int flag2 = 0;
                    foreach (var item in InventoryListSentMail)
                    {
                        if (flag2 >= startPos && flag2 <= endPos)
                            DivInventoryListSentMail.Add(item);
                        flag2++;
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


                    if (currentpage==1 && maxpage==1)
                    {
                        LeftVisi = false;
                        RightVisi = true; 
                    }
                    else
                    {
                        if(currentpage==maxpage)
                        {
                            LeftVisi = true;
                            RightVisi = false;
                        }
                        else
                        {
                            if(currentpage==1)
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


                    if(maxpage>=3)
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
                        if(maxpage==2)
                        {
                            BtnPage1.PageVisi = Visibility.Visible;
                            BtnPage2.PageVisi = Visibility.Visible;
                            BtnPage3.PageVisi = Visibility.Collapsed;
                            switch(currentpage)
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
                            BtnPage1.PageVal = (currentpage - 1) * NumRowEachPage + 1;;
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
            }*/
            #endregion
            void clearData()
            {
                Subject = "";
                Content = "";
                Sender = "";
                Mailtype = "";
            }

            void sentmail(string from, string pass, string to, string subject, string body)
            {
                try
                {
                    SmtpClient clientDetails = new SmtpClient();
                    clientDetails.Port = 587;
                    clientDetails.Host = "smtp.gmail.com";
                    clientDetails.EnableSsl = true;
                    clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;

                    clientDetails.Credentials = new NetworkCredential(from, pass);


                    MailMessage mailDetails = new MailMessage(from, to, subject, body);
                    clientDetails.Send(mailDetails);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            void autosentmail(checkmail selected, string typemail, string sentype)
            {
                DateTime now = DateTime.Now.Date;
                int nowday = now.Day;
                int nowmonth = now.Month;
                int nowyear = now.Year;
                string from = "thaihoangnhan12@gmail.com";
                string pass = "khmt2018";
                
                DayOfWeek nowdayofweek = now.DayOfWeek;
                MessageBox.Show(nowdayofweek.ToString());
                List<custommer> sentcus = new List<custommer>();
                List<employee> sentemp = new List<employee>();
                sentcus = DataProvider.Ins.DB.custommers.ToList();
                sentemp = DataProvider.Ins.DB.employees.ToList();

                if(typemail=="Sinh nhật")
                {
                    foreach(var item in sentcus)
                    {
                        if(nowday==item.dateOfBirth.Day && nowmonth==item.dateOfBirth.Month)
                        {
                            sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content);
                        }
                    }
                }
                else
                {
                    if(typemail=="Khuyến mãi")
                    {
                       
                        switch (senttype)
                        {
                            case "ONEDAY":
                                foreach (var item in sentcus)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month && nowyear == selected.Mail.autosentDate.Value.Year)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                            case "EVERYWEEK":
                                foreach (var item in sentcus)
                                {
                                    if (nowdayofweek == selected.Mail.autosentDate.Value.DayOfWeek)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                            case "EVERTYMONTH":
                                foreach (var item in sentcus)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                            case "EVERYYEAR":
                                foreach (var item in sentcus)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month)
                                    {
                                        sentmail(from, pass, item.custommerEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (senttype)
                        {
                            case "ONEDAY":
                                foreach (var item in sentemp)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month && nowyear == selected.Mail.autosentDate.Value.Year)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                            case "EVERYWEEK":
                                foreach (var item in sentemp)
                                {
                                    if (nowdayofweek == selected.Mail.autosentDate.Value.DayOfWeek)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                            case "EVERTYMONTH":
                                foreach (var item in sentemp)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                            case "EVERYYEAR":
                                foreach (var item in sentemp)
                                {
                                    if (nowday == selected.Mail.autosentDate.Value.Day && nowmonth == selected.Mail.autosentDate.Value.Month)
                                    {
                                        sentmail(from, pass, item.employeeEmail, selected.Mail.subjectMail, selected.Mail.content);
                                    }
                                }
                                break;
                        }
                    }    
                }
            }
        }
    }
}
