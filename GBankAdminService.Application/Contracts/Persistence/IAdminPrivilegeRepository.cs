using GBankAdminService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Contracts.Persistence
{
    public interface IAdminPrivilegeRepository : IAsyncRepository<AdminPrivilege>
    {
    }
}
