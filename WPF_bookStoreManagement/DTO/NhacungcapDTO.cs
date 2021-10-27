using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WPF_bookStoreManagement.DAO;

namespace WPF_bookStoreManagement.DTO
{
    public class NhacungcapDTO
    {
        public NhacungcapDTO(string id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        public NhacungcapDTO(DataRow row)
        {
            this.ID = row["idSupplier"].ToString();
            this.Name = row["nameSupplier"].ToString();
            this.Status = row["statusSupplier"].ToString();
        }

        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
