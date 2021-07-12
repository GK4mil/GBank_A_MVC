using System;
using System.Collections.Generic;

#nullable disable

namespace GBankAdminService.Domain.Entities
{
    public partial class AdminPrivilege
    {
        public AdminPrivilege()
        {
            Admins = new HashSet<Admin>();
        }

        public int Id { get; set; }
        public bool Addclient { get; set; }
        public bool Editclient { get; set; }
        public bool Deleteclient { get; set; }
        public bool Transfermoney { get; set; }
        public bool Withdrawmoney { get; set; }
        public bool Blockfeatures { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
    }
}
