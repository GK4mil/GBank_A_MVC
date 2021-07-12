using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GBankAdminService.Models
{
    public class CredentialsModel
    {
        public String username { get; set; }
        [Required(ErrorMessage="This field is required")]
        public String password { get; set; }
    }
}
