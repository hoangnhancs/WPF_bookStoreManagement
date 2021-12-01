using bookStoreManagetment.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public class TrahangInfor : BaseViewModel
    {
        private int _idtrahang;
        public int IdTrahang
        {
            get { return _idtrahang; }
            set
            {
                _idtrahang = value;
                OnPropertyChanged();
            }
        }
        private string _billcodetra;
        public string BillcodeTra
        {
            get { return _billcodetra; }
            set
            {
                _billcodetra = value;
                OnPropertyChanged();
            }
        }
        private string _billcodesell;
        public string BillcodeSell
        {
            get { return _billcodesell; }
            set
            {
                _billcodesell = value;
                OnPropertyChanged();
            }
        }
        private string _nameemployee;
        public string NameEmployee
        {
            get { return _nameemployee; }
            set
            {
                _nameemployee = value;
                OnPropertyChanged();
            }
        }
        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        private DateTime _selldate;
        public DateTime SellDate
        {
            get { return _selldate; }
            set
            {
                _selldate = value;
                OnPropertyChanged();
            }
        }
        private int _idcustomer;
        public int IdCustomer
        {
            get { return _idcustomer; }
            set
            {
                _idcustomer = value;
                OnPropertyChanged();
            }
        }
        private string _iditem;
        public string IdItem
        {
            get { return _iditem; }
            set
            {
                _iditem = value;
                OnPropertyChanged();
            }
        }
        private int _unitprice;
        public int UnitPrice
        {
            get { return _unitprice; }
            set
            {
                _unitprice = value;
                OnPropertyChanged();
            }
        }
        private int _discount;
        public int DisCount
        {
            get { return _discount; }
            set
            {
                _discount = value;
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
        private string _trangthai;
        public string TrangThai
        {
            get { return _trangthai; }
            set
            {
                _trangthai = value;
                OnPropertyChanged();
            }
        }
        private string _namecustomer;
        public string NameCustomer
        {
            get { return _namecustomer; }
            set
            {
                _namecustomer = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<itemtrave> _lstitem;
        public ObservableCollection<itemtrave> ListItem
        {
            get { return _lstitem; }
            set
            {
                _lstitem = value;
                OnPropertyChanged();
            }
        }
    }
}
