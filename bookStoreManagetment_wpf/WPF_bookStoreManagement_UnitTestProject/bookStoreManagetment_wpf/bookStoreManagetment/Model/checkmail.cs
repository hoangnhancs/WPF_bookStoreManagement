using bookStoreManagetment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace bookStoreManagetment.Model
{
    public class checkmail : BaseViewModel
    {
        private mail _mail;
        public mail Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                OnPropertyChanged();
            }
        }
        private bool _check;
        public bool Check
        {
            get { return _check; }
            set
            {
                _check = value;
                OnPropertyChanged();
            }
        }
    }
}
