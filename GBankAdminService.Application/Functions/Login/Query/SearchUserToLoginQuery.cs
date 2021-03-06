using GBankAdminService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Functions.Login.Query
{
    public class SearchUserToLoginQuery : IRequest<List<Admin>>
    {
        public String username { get; set; }
        public String password { get; set; }
}
}
