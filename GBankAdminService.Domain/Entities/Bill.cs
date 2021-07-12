using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GBankAdminService.Domain.Entities
{
    public class Bill
    {
        public Bill()
        {
            this.Users = new Collection<User>();


        }
        public int ID { get; set; }
        public Decimal balance { get; set; }
        public String billNumber { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
