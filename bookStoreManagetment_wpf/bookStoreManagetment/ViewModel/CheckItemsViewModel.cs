using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class CheckItemsViewModel : BaseViewModel
    {
        public ICommand LoadedCheckItemsCommand { get; set; }
        public CheckItemsViewModel()
        {

            LoadedCheckItemsCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LoadData((p as ListView));
            });
        }

        private void LoadData(ListView listViewContent)
        {

        }
    }
}
