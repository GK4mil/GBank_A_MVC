using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GBankAdminService.Domain.Entities
{
    public class User
    {
        public User()
        {
           this.Bills = new Collection<Bill>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        
       

       
        public virtual ICollection<Bill> Bills { get; set; }



    }
}
