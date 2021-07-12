using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Infrastructure.Repositories.EF
{
    public class AdminPrivilegeRepository : BaseRepository<AdminPrivilege>, IAdminPrivilegeRepository
    {
        public AdminPrivilegeRepository(GBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}
