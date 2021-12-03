using bookStoreManagetment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public class itemtrave : BaseViewModel
    {
        private item _item;
        public item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        private int _buynumber;
        public int BuyNumber
        {
            get { return _buynumber; }
            set
            {
                _buynumber = value;
                OnPropertyChanged();
            }
        }
        private int _travenumber;
        public int TraveNumber
        {
            get { return _travenumber; }
            set
            {
                _travenumber = value;
                OnPropertyChanged();
            }
        }
        private int _thanhtien;
        public int Thanhtien
        {
            get { return Item.sellPriceItem * TraveNumber; }
            set
            {
                _thanhtien = value;
                OnPropertyChanged();
            }
        }

    }
}
