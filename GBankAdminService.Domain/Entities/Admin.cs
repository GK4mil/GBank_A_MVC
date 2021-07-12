using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace GBankAdminService.Domain.Entities
{
    [Table("Admin")]
    public partial class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Privilegesid { get; set; }
        public bool Active { get; set; }

        public virtual AdminPrivilege Privileges { get; set; }
    }
}
