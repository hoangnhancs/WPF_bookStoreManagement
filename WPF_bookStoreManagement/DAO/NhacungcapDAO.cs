using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_bookStoreManagement.DTO;
using System.Data;

namespace WPF_bookStoreManagement.DAO
{
    public class NhacungcapDAO
    {
        private static NhacungcapDAO instance;
        
        public static NhacungcapDAO Instance
        {
            get { if (instance == null) instance = new NhacungcapDAO(); return NhacungcapDAO.instance; }
            private set { NhacungcapDAO.instance = value; }
        }

        private NhacungcapDAO() { }

        public List<NhacungcapDTO> getallNhacungcap()
        {
            List<NhacungcapDTO> lstNhacungcap = new List<NhacungcapDTO>();
            string query = "select * from supplier";
            DataTable data = dataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                NhacungcapDTO _ncc = new NhacungcapDTO(item);
                lstNhacungcap.Add(_ncc);
            }
            return lstNhacungcap;
        }
    }
}
