using GBankAdminService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<int> DeleteUserByID(int id);
        Task<List<User>> FindByAnyColumn(String phase);
    }
}
