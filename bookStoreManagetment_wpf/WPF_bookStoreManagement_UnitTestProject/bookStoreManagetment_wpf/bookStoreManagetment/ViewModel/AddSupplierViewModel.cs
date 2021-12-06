using bookStoreManagetment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bookStoreManagetment.ViewModel
{
    public class AddSupplierViewModel:BaseViewModel
    {
        string idSup { get; set; }
        string nameSup { get; set; }
        string tinh { get; set; }
        string huyen { get; set; }
        string xa { get; set; }
        string sonha { get; set; }
        string addressSup { get; set; }
        string emailSup { get; set; }
        string phoneSup { get; set; }
        string statusSup { get; set; }
        private ICommand _loadinsertinforcommand;
        public ICommand LoadInsertInforCommand {
            get
            {
                if (_loadinsertinforcommand == null)
                {
                    _loadinsertinforcommand = new RelayCommand<object>((p) => { return true; }, (p) => changeInfor(p)) ; 
                }
                return _loadinsertinforcommand;
            }
        }
        public ICommand insertSupplierCommand { get; set; }
        private ICommand _cleartextboxcommand;
        public ICommand clearTextboxCommand {
            get
            {
                if(_cleartextboxcommand==null)
                {
                    _cleartextboxcommand = new RelayCommand<object>((p) => { return true; }, (p) => clearTextbox(p));
                }
                return _cleartextboxcommand;
            }
        }

        public AddSupplierViewModel()
        {
            insertSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                addSupExcuteQuery();
            });
        }



        public void changeInfor(object obj)
        {
            switch ((obj as TextBox).Name)
            {
                case "nameSuptb":
                    nameSup = (obj as TextBox).Text;
                    
                    break;
                case "tinhAddressSuptb":
                    tinh = (obj as TextBox).Text;
                    
                    break;
                case "huyenAddressSuptb":
                    huyen = (obj as TextBox).Text;
                    break;
                case "xaAddressSuptb":
                    xa = (obj as TextBox).Text;
                    break;
                case "sonhaAddressSuptb":
                    sonha = (obj as TextBox).Text;
                    break;
                case "mailSuptb":
                    emailSup = (obj as TextBox).Text;
                    break;
                case "phoneSuptb":
                    phoneSup = (obj as TextBox).Text;
                    //Console.WriteLine(phoneSup);
                    break;
            }
                
        }

        public void clearTextbox(object obj)
        {
            (obj as TextBox).Text = "";
        }

        public void addSupExcuteQuery()
        {
            int countSup = DataProvider.Ins.DB.suppliers.Count()+1;
            string newIdSup;
            if (countSup < 10)
            {
                newIdSup = "NCC00" + countSup.ToString();
            }
            else
            {
                if (countSup < 100)
                {
                    newIdSup = "NCC0" + countSup.ToString();
                }
                else
                {
                    newIdSup = "NCC" + countSup.ToString();
                }
            }
            addressSup = sonha + " " + xa + " " + huyen + " " + tinh;
            string query = "insert into supplier values (N'" + newIdSup + "', N'" + nameSup + "', N'" + addressSup + "', N'" + emailSup + "', N'" + phoneSup + "', N'Đang hợp tác')";
            if (nameSup != "" && addressSup != "   " && emailSup != "" && phoneSup != "")
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand(query);
                refreshall();
                MessageBox.Show("Thêm nhà cung cấp thành công");
            }
            else
                MessageBox.Show("Vui lòng điền đủ thông tin");
        }
         

        public void refreshall()
        {
            idSup = "";
            addressSup = "";
            tinh = "";
            huyen = "";
            xa = "";
            sonha = "";
            phoneSup = "";
            emailSup = "";
            nameSup = "";
        }
    }
}
