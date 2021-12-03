using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStoreManagetment.Model
{
    public static class Permission
    {
        //1
        public static bool ChinhSuaQuyDinh { get; set; }

        //2
        public static bool ChinhSuaNhanVien { get; set; }

        public static void createPermission()
        {
            var permissons = DataProvider.Ins.DB.employees.Where(x => x.nameAccount == LoggedAccount.Account.nameAccount).FirstOrDefault().employeeNote;
            if (permissons == null)
                permissons = "";
            var listPermission = permissons.Split(',');

            //quy định
            if (listPermission.Contains("1"))
                ChinhSuaQuyDinh = true;
            else
                ChinhSuaQuyDinh = false;

            // nhân viên
            if (listPermission.Contains("2"))
                ChinhSuaNhanVien = true;
            else
                ChinhSuaNhanVien = false;
        }
    }
}
