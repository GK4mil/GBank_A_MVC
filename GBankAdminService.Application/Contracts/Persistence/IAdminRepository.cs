using GBankAdminService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Contracts.Persistence
{
    public interface  IAdminRepository :IAsyncRepository<Admin>
    {
        Task<List<Admin>> GetAdminByUsernameAndPasswd(String username, String password);
    }
}
