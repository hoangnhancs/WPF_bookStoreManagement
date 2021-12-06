using Microsoft.VisualStudio.TestTools.UnitTesting;
using bookStoreManagetment.Model;
using bookStoreManagetment.ViewModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;
using KellermanSoftware.CompareNetObjects;

namespace BookStoreManagetment.tests
{
    [TestClass]
    public class ThemKhachHang
    {
        #region hàm đánh giá
        private bool checkHas(custommer cus1, custommer cus2)
        {
            if (cus1.firstName != cus2.firstName)
                return false;
            if (cus1.lastName != cus2.lastName)
                return false;
            if (cus1.idCustommer != cus2.idCustommer)
                return false;
            if (cus1.nameAccount != cus2.nameAccount)
                return false;
            if (cus1.accumulatedPoints != cus2.accumulatedPoints)
                return false;
            if (cus1.citizenIdentification != cus2.citizenIdentification)
                return false;
            if (cus1.custommerAddress != cus2.custommerAddress)
                return false;
            if (cus1.custommerEmail != cus2.custommerEmail)
                return false;
            if (cus1.custommerNote != cus2.custommerNote)
                return false;
            if (cus1.sex != cus2.sex)
                return false;
            if ((DateTime.Compare(cus1.dateOfBirth,cus2.dateOfBirth) != 0))
                return false;
            if (cus1.phoneNumber != cus2.phoneNumber)
                return false;
            return true;
        }
        private void ValidateSaveNhanVienCommand(ShowKhachHang _sampleKhachHang)
        {
            // Arrange - tổ chức - khai báo 
            DSKhachHangViewModel KhachHangVM = new DSKhachHangViewModel();
            KhachHangVM.LoadedUserControlCommand.Execute(true);
            KhachHangVM.ViewKhachHang = _sampleKhachHang;
            KhachHangVM.IsEdit = false;

            // Action - thực thi
            if (KhachHangVM.SaveNhanVienCommand.CanExecute(KhachHangVM))
            {
                // Action - thực thi
                KhachHangVM.SaveNhanVienCommand.Execute(null);

                // Assert - kiểm tra/thông báo kết quả
                _sampleKhachHang.Cus.idCustommer = KhachHangVM.ViewKhachHang.Cus.idCustommer;
                var checkSampleKH = DataProvider.Ins.DB.custommers.Where(x => x.idCustommer == _sampleKhachHang.Cus.idCustommer).FirstOrDefault();
                var nowDate = DateTime.Now;
                _sampleKhachHang.Cus.dateOfBirth = nowDate;
                checkSampleKH.dateOfBirth = nowDate;

                if (checkHas(_sampleKhachHang.Cus, checkSampleKH))
                {
                    MessageBox.Show("Đã thêm khách hàng thành công");
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng không thành công");
                }  
            }
            else
            {
                // Assert - kiểm tra / thông báo kết quả
                MessageBox.Show("vui lòng nhập đủ thông tin");
            }
        }
        #endregion

        #region Test case thêm khách hàng thành công
        [TestMethod]
        public void TestCase1()
        {
            // Arrange - tổ chức - khai báo 
            ShowKhachHang sampleCustomer = new ShowKhachHang()
            {
                counListBill = 0,
                FullName = "test people",
                isNam = true,
                isNu = false,
                ListBill = new List<string>(),
                Cus = new custommer()
                {
                    idCustommer = "",
                    accumulatedPoints = 0,
                    citizenIdentification = "",
                    custommerAddress = "TPHCM",
                    sex = "nam",
                    nameAccount = "thân thiết",
                    custommerEmail = "testCase@gmail.com",
                    custommerNote = "",
                    dateOfBirth = DateTime.ParseExact("02/12/2000", "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture),
                    phoneNumber = "02313123",
                    firstName = "people",
                    lastName ="test"
                }
            };

            // Action - Assert
            ValidateSaveNhanVienCommand(sampleCustomer);
        }

        #endregion

        #region Test case thêm khách hàng không thành công
        [TestMethod]
        public void TestCase2()
        {
            // Arrange - tổ chức - khai báo 
            ShowKhachHang sampleCustomer = new ShowKhachHang()
            {
                counListBill = 0,
                FullName = "",
                isNam = false,
                isNu = false,
                ListBill = new List<string>(),
                Cus = new custommer()
                {
                    idCustommer = "",
                    accumulatedPoints = 0,
                    citizenIdentification = "",
                    custommerAddress = "",
                    sex = "",
                    nameAccount = "thân thiết",
                    custommerEmail = "testCase@gmail.com",
                    custommerNote = "",
                    dateOfBirth = DateTime.ParseExact("02/12/2000", "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture),
                    phoneNumber = "02313123",
                    firstName = "",
                    lastName = ""
                }
            };

            // Action - Assert
            ValidateSaveNhanVienCommand(sampleCustomer);
        }
        #endregion

        #region Test Case đặt biệt
        [TestMethod]
        public void TestCase3()
        {
            // Arrange - tổ chức - khai báo 
            ShowKhachHang sampleCustomer = new ShowKhachHang()
            {
                counListBill = 0,
                FullName = "",
                isNam = false,
                isNu = false,
                ListBill = new List<string>(),
                Cus = null
            };

            // Action - Assert
            ValidateSaveNhanVienCommand(sampleCustomer);
        }

        [TestMethod]
        public void TestCase4()
        {
            // Arrange - tổ chức - khai báo 
            ShowKhachHang sampleCustomer = new ShowKhachHang()
            {
                counListBill = 0,
                FullName = "test people",
                isNam = true,
                isNu = false,
                ListBill = new List<string>(),
                Cus = new custommer()
                {
                    idCustommer = "",
                    accumulatedPoints = 2,
                    citizenIdentification = "",
                    custommerAddress = "$#!@#",
                    sex = "!@#!@#",
                    nameAccount = "thân thiết",
                    custommerEmail = "testCase@gmail.com",
                    custommerNote = "",
                    dateOfBirth = DateTime.ParseExact("02/12/2000", "M/d/yyyy", System.Globalization.CultureInfo.CurrentCulture),
                    phoneNumber = "!@#!@3",
                    firstName = "people",
                    lastName = "test"
                }
            };

            // Action - Assert
            ValidateSaveNhanVienCommand(sampleCustomer);
        }
        #endregion
    }
}
