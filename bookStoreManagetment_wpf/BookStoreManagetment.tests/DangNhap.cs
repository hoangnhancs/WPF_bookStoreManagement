using Microsoft.VisualStudio.TestTools.UnitTesting;
using bookStoreManagetment.Model;
using bookStoreManagetment.ViewModel;
using System.Windows.Forms;

namespace BookStoreManagetment.tests
{
    [TestClass]
    public class DangNhap
    {
        #region hàm đánh giá
        private void ValidateLoginCommand(account _sampleAccount)
        {
            // Arrange - tổ chức - khai báo 
            LoginViewModel LoginVM = new LoginViewModel();
            LoginVM.User = _sampleAccount.nameAccount;
            LoginVM.Password = _sampleAccount.passwordAccount;

            // Action - thực thi
            if (LoginVM.LogindCommand.CanExecute(LoginVM))
            {
                LoginVM.LogindCommand.Execute(null);

                // Assert - kiểm tra/thông báo kết quả
                if (LoginVM.IsLogin)
                {
                    Assert.AreEqual(LoggedAccount.Account.nameAccount, _sampleAccount.nameAccount);
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("vui lòng nhập đúng tài khoản và mật khẩu");
                }
            }
            else
            {
                // Assert - kiểm tra / thông báo kết quả
                MessageBox.Show("Không thể thực thi");
            }
        }
        #endregion

        #region Test case nhập mật khẩu đúng
        [TestMethod]
        public void TestCase1()
        {
            // Arrange - tổ chức - khai báo 
            account sampleAccount = new account()
            {
                nameAccount = "admin",
                passwordAccount = "admin"
            };

            // Action - Assert
            ValidateLoginCommand(sampleAccount);
        }

        [TestMethod]
        public void TestCase2()
        {
            // Arrange - tổ chức - khai báo 
            account sampleAccount = new account()
            {
                nameAccount = "chiny",
                passwordAccount = "1"
            };

            // Action - Assert
            ValidateLoginCommand(sampleAccount);
        }
        #endregion

        #region Test case nhập sai tài khoản / mật khẩu
        [TestMethod]
        public void TestCase3()
        {
            // Arrange - tổ chức - khai báo 
            account sampleAccount = new account()
            {
                nameAccount = "chiny",
                passwordAccount = "admin"
            };

            // Action - Assert
            ValidateLoginCommand(sampleAccount);
        }

        [TestMethod]
        public void TestCase4()
        {
            // Arrange - tổ chức - khai báo 
            account sampleAccount = new account()
            {
                nameAccount = "admin",
                passwordAccount = "1"
            };

            // Action - Assert
            ValidateLoginCommand(sampleAccount);
        }
        #endregion

        #region Test Case đặt biệt
        [TestMethod]
        public void TestCase5()
        {
            // Arrange - tổ chức - khai báo 
            account sampleAccount = new account()
            {
                nameAccount = "",
                passwordAccount = null
            };

            // Action - Assert
            ValidateLoginCommand(sampleAccount);
        }
        #endregion
    }
}
