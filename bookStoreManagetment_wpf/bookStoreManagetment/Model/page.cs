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
    public class page : BaseViewModel
    {
        private int _pageVal;
        public int PageVal
        {
            get { return _pageVal; }
            set
            {
                _pageVal = value;
                OnPropertyChanged();
            }
        }
        private Visibility _visibility;
        public Visibility PageVisi
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }
        private SolidColorBrush _background;
        public SolidColorBrush BackGround
        {
            get { return _background; }
            set
            {
                _background = value;
                OnPropertyChanged();
            }
        }       
    }
}
